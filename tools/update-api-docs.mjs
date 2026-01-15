#!/usr/bin/env node
import fs from "node:fs";
import path from "node:path";

const repoRoot = path.resolve(path.dirname(new URL(import.meta.url).pathname), "..");

const args = new Map();
for (let i = 2; i < process.argv.length; i += 1) {
  const current = process.argv[i];
  if (!current.startsWith("--")) continue;
  const key = current.slice(2);
  const value = process.argv[i + 1];
  if (value && !value.startsWith("--")) {
    args.set(key, value);
    i += 1;
  } else {
    args.set(key, "true");
  }
}

const exists = (candidatePath) => {
  try {
    return fs.existsSync(candidatePath);
  } catch {
    return false;
  }
};

const resolveFirstExisting = (candidates) => {
  for (const candidatePath of candidates) {
    if (exists(candidatePath)) return candidatePath;
  }
  return undefined;
};

const nodejsPackageRoot = args.get("nodejs-package-root")
  ? path.resolve(repoRoot, args.get("nodejs-package-root"))
  : resolveFirstExisting([
      path.resolve(repoRoot, "../nodejs"),
      path.resolve(repoRoot, "node_modules/@tsonic/nodejs"),
    ]);

if (!nodejsPackageRoot) {
  console.error(
    "Unable to locate @tsonic/nodejs package root. Pass --nodejs-package-root <path>.",
  );
  process.exit(1);
}

const nodejsFacadePath = path.resolve(nodejsPackageRoot, "index.d.ts");
const nodejsInternalPath = path.resolve(
  nodejsPackageRoot,
  "index/internal/index.d.ts",
);

const httpFacadePath = path.resolve(nodejsPackageRoot, "nodejs.Http.d.ts");
const httpInternalPath = path.resolve(
  nodejsPackageRoot,
  "nodejs.Http/internal/index.d.ts",
);

for (const requiredPath of [nodejsFacadePath, nodejsInternalPath]) {
  if (!exists(requiredPath)) {
    console.error(`Missing required file: ${requiredPath}`);
    process.exit(1);
  }
}

const readText = (filePath) => fs.readFileSync(filePath, "utf8");

const nodejsFacade = readText(nodejsFacadePath);
const nodejsInternal = readText(nodejsInternalPath);

const httpFacade = exists(httpFacadePath) ? readText(httpFacadePath) : "";
const httpInternal = exists(httpInternalPath) ? readText(httpInternalPath) : "";

const parseFacadeExports = (source) => {
  const exportsMap = new Map();
  const exportRe =
    /^export\s+\{\s*([A-Za-z0-9_$]+)\s+as\s+([A-Za-z0-9_$]+)\s*\}\s+from\s+['"][^'"]+['"];\s*$/gm;
  let match;
  while ((match = exportRe.exec(source))) {
    const internalName = match[1];
    const publicName = match[2];
    exportsMap.set(publicName, internalName);
  }
  return exportsMap;
};

const nodejsExports = parseFacadeExports(nodejsFacade);
const httpExports = parseFacadeExports(httpFacade);

const getDeclarationRange = (source, startIndex) => {
  const length = source.length;

  const isWhitespace = (char) => char === " " || char === "\t" || char === "\n" || char === "\r";

  const startBrace = source.indexOf("{", startIndex);
  if (startBrace === -1) return undefined;

  let index = startBrace;
  let depth = 0;
  let inSingleQuote = false;
  let inDoubleQuote = false;
  let inLineComment = false;
  let inBlockComment = false;

  while (index < length) {
    const char = source[index];
    const next = index + 1 < length ? source[index + 1] : "";

    if (inLineComment) {
      if (char === "\n") inLineComment = false;
      index += 1;
      continue;
    }

    if (inBlockComment) {
      if (char === "*" && next === "/") {
        inBlockComment = false;
        index += 2;
        continue;
      }
      index += 1;
      continue;
    }

    if (!inSingleQuote && !inDoubleQuote) {
      if (char === "/" && next === "/") {
        inLineComment = true;
        index += 2;
        continue;
      }
      if (char === "/" && next === "*") {
        inBlockComment = true;
        index += 2;
        continue;
      }
    }

    if (!inDoubleQuote && char === "'" && source[index - 1] !== "\\") {
      inSingleQuote = !inSingleQuote;
      index += 1;
      continue;
    }

    if (!inSingleQuote && char === "\"" && source[index - 1] !== "\\") {
      inDoubleQuote = !inDoubleQuote;
      index += 1;
      continue;
    }

    if (inSingleQuote || inDoubleQuote) {
      index += 1;
      continue;
    }

    if (char === "{") depth += 1;
    if (char === "}") {
      depth -= 1;
      if (depth === 0) {
        let endIndex = index + 1;
        while (endIndex < length && isWhitespace(source[endIndex])) endIndex += 1;
        if (source[endIndex] === ";") endIndex += 1;
        return { start: startIndex, end: endIndex };
      }
    }
    index += 1;
  }

  return undefined;
};

const extractExportBlock = (source, startMarker) => {
  const startIndex = source.indexOf(startMarker);
  if (startIndex === -1) return undefined;
  const range = getDeclarationRange(source, startIndex);
  if (!range) return undefined;
  return source.slice(range.start, range.end).trimEnd();
};

const extractExportLine = (source, linePrefix) => {
  const startIndex = source.indexOf(linePrefix);
  if (startIndex === -1) return undefined;
  const lineEnd = source.indexOf("\n", startIndex);
  return source
    .slice(startIndex, lineEnd === -1 ? source.length : lineEnd)
    .trimEnd();
};

const escapeRegExp = (text) => text.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");

const replaceIdentifier = (source, from, to) => {
  if (from === to) return source;
  const fromInstance = `${from}$instance`;
  const toInstance = to;
  let result = source.replace(new RegExp(`\\b${escapeRegExp(fromInstance)}\\b`, "g"), toInstance);
  result = result.replace(new RegExp(`\\b${escapeRegExp(from)}\\b`, "g"), to);
  return result;
};

const applyExportRenames = (source, internalToPublic) => {
  const candidates = Array.from(internalToPublic.entries())
    .filter(([internalName, publicName]) => internalName !== publicName)
    .filter(([internalName]) => /^[A-Za-z0-9_]+$/.test(internalName))
    .sort((a, b) => b[0].length - a[0].length);

  let result = source;
  for (const [internalName, publicName] of candidates) {
    result = replaceIdentifier(result, internalName, publicName);
  }
  return result;
};

const renderStaticContainer = (block, publicName, internalName, internalToPublic) => {
  const lines = block.split("\n");
  const openBraceIndex = lines.findIndex((line) => line.includes("{"));
  const closeBraceIndex = lines.findLastIndex((line) => line.includes("}"));
  const bodyLines =
    openBraceIndex === -1 || closeBraceIndex === -1
      ? []
      : lines.slice(openBraceIndex + 1, closeBraceIndex);

  const renderedBody = bodyLines
    .map((line) =>
      line
        .replace(/^\s*static\s+readonly\s+/, "readonly ")
        .replace(/^\s*static\s+/, "")
        .trimEnd(),
    )
    .filter((line) => line.trim().length > 0)
    .map((line) => `  ${line.trimStart()}`)
    .join("\n");

  const headerComment = internalName.endsWith("$instance")
    ? ""
    : `// from ${internalName}\n`;

  const rendered = `${headerComment}export declare const ${publicName}: {\n${renderedBody}\n};`;
  return internalToPublic ? applyExportRenames(rendered, internalToPublic) : rendered;
};

const renderTypeLike = (internalSource, publicName, internalName, internalToPublic) => {
  const instanceName = `${internalName}$instance`;
  const interfaceMarker = `export interface ${instanceName}`;
  const abstractClassMarker = `export abstract class ${instanceName}`;
  const classMarker = `export class ${instanceName}`;

  const instanceBlock =
    extractExportBlock(internalSource, interfaceMarker) ??
    extractExportBlock(internalSource, abstractClassMarker) ??
    extractExportBlock(internalSource, classMarker);

  if (!instanceBlock) {
    const typeAliasLine = extractExportLine(internalSource, `export type ${internalName} =`);
    if (typeAliasLine) {
      return replaceIdentifier(typeAliasLine, internalName, publicName);
    }
    return undefined;
  }

  const constBlock = extractExportBlock(internalSource, `export const ${internalName}:`);

  const cleanedInstance = replaceIdentifier(
    instanceBlock.replace(instanceName, publicName),
    internalName,
    publicName,
  );

  const cleanedConst = constBlock
    ? replaceIdentifier(constBlock, internalName, publicName)
    : undefined;

  const rendered = [cleanedInstance, cleanedConst].filter(Boolean).join("\n\n");
  return internalToPublic ? applyExportRenames(rendered, internalToPublic) : rendered;
};

const renderExport = (internalSource, publicName, internalName, internalToPublic) => {
  if (internalName.endsWith("$instance")) {
    const block = extractExportBlock(internalSource, `export abstract class ${internalName}`);
    if (!block) return undefined;
    return renderStaticContainer(block, publicName, internalName, internalToPublic);
  }

  const instanceBlock =
    extractExportBlock(internalSource, `export abstract class ${internalName}$instance`) ??
    extractExportBlock(internalSource, `export interface ${internalName}$instance`) ??
    extractExportBlock(internalSource, `export class ${internalName}$instance`);

  if (!instanceBlock) return undefined;

  if (instanceBlock.startsWith("export abstract class")) {
    return renderStaticContainer(instanceBlock, publicName, internalName, internalToPublic);
  }

  return renderTypeLike(internalSource, publicName, internalName, internalToPublic);
};

const scanPublicTypes = (directoryPath) => {
  const results = new Map();
  const entries = fs.readdirSync(directoryPath, { withFileTypes: true });
  for (const entry of entries) {
    if (!entry.isFile() || !entry.name.endsWith(".cs")) continue;
    const filePath = path.join(directoryPath, entry.name);
    const text = readText(filePath);

    const typeRe =
      /\bpublic\s+(?:static\s+)?(?:abstract\s+)?(?:sealed\s+)?(?:partial\s+)?(?:class|interface|struct|record)\s+([A-Za-z_][A-Za-z0-9_]*)(?:\s*<([^>]+)>)?/g;

    const delegateRe =
      /\bpublic\s+delegate\s+[^\s]+\s+([A-Za-z_][A-Za-z0-9_]*)(?:\s*<([^>]+)>)?/g;

    for (const re of [typeRe, delegateRe]) {
      let match;
      while ((match = re.exec(text))) {
        const name = match[1];
        const genericParams = match[2]?.trim();
        const arity = genericParams
          ? genericParams.split(",").map((part) => part.trim()).filter(Boolean).length
          : 0;
        results.set(name, Math.max(results.get(name) ?? 0, arity));
      }
    }
  }
  return Array.from(results.entries()).map(([name, arity]) => ({ name, arity }));
};

const docsModulesDir = path.resolve(repoRoot, "docs/modules");
const sourceModulesDir = path.resolve(repoRoot, "src/nodejs");

const specialPages = {
  performance: { sourceDir: "perf_hooks", entry: "index" },
  http: { sourceDir: "http", entry: "http" },
  x509: {
    sourceDir: "crypto",
    entry: "index",
    only: ["Certificate", "X509CertificateExtensions", "X509CertificateInfo"],
  },
};

const modulePages = fs
  .readdirSync(docsModulesDir)
  .filter((name) => name.endsWith(".md"))
  .map((name) => name.slice(0, -3))
  .sort();

const buildPageSymbols = (pageName) => {
  const config = specialPages[pageName] ?? { sourceDir: pageName, entry: "index" };

  const sourceDirPath = path.resolve(sourceModulesDir, config.sourceDir);
  if (!exists(sourceDirPath)) {
    console.error(`Missing source directory for ${pageName}: ${sourceDirPath}`);
    process.exit(1);
  }

  const discovered = scanPublicTypes(sourceDirPath);
  const names = config.only
    ? discovered.filter((entry) => config.only.includes(entry.name))
    : discovered;

  return { entry: config.entry, symbols: names.map((entry) => entry.name) };
};

const groupForEntry = (entryName) => {
  if (entryName === "http") {
    const internalToPublic = new Map(Array.from(httpExports.entries()).map(([publicName, internalName]) => [internalName, publicName]));
    return { exports: httpExports, internal: httpInternal, internalToPublic };
  }
  const internalToPublic = new Map(Array.from(nodejsExports.entries()).map(([publicName, internalName]) => [internalName, publicName]));
  return { exports: nodejsExports, internal: nodejsInternal, internalToPublic };
};

const renderPageApi = (pageName) => {
  const { entry, symbols } = buildPageSymbols(pageName);
  const { exports: exportMap, internal, internalToPublic } = groupForEntry(entry);

  const uniqueExports = Array.from(
    new Set(
      symbols.filter((name) => exportMap.has(name)),
    ),
  );

  if (uniqueExports.length === 0) {
    return "";
  }

  const sections = [];
  for (const publicName of uniqueExports.sort((a, b) => a.localeCompare(b))) {
    const internalName = exportMap.get(publicName);
    const rendered = renderExport(internal, publicName, internalName, internalToPublic);
    if (!rendered) continue;
    sections.push(
      `### \`${publicName}\`\n\n\`\`\`ts\n${rendered}\n\`\`\``,
    );
  }

  return sections.join("\n\n");
};

const injectApiIntoDoc = (docPath, apiMarkdown) => {
  const source = readText(docPath);
  const startToken = "<!-- API:START -->";
  const endToken = "<!-- API:END -->";

  const hasTokens = source.includes(startToken) && source.includes(endToken);

  const apiBlock = `${startToken}\n${apiMarkdown}\n${endToken}`;

  if (hasTokens) {
    const before = source.split(startToken)[0].trimEnd();
    const after = source.split(endToken)[1].trimStart();
    const updated = `${before}\n\n${apiBlock}\n\n${after}`.trimEnd() + "\n";
    fs.writeFileSync(docPath, updated, "utf8");
    return;
  }

  const trimmed = source.trimEnd();
  const suffix = `\n\n## API Reference\n\n${apiBlock}\n`;
  fs.writeFileSync(docPath, trimmed + suffix, "utf8");
};

for (const pageName of modulePages) {
  const apiMarkdown = renderPageApi(pageName);
  const docPath = path.resolve(docsModulesDir, `${pageName}.md`);
  injectApiIntoDoc(docPath, apiMarkdown);
}

const collectAliasedGenericNames = (...exportMaps) => {
  const aliased = [];
  for (const exportMap of exportMaps) {
    for (const [publicName, internalName] of exportMap.entries()) {
      if (publicName === internalName) continue;
      if (!/_\d+$/.test(internalName)) continue;
      aliased.push({ publicName, internalName });
    }
  }
  return aliased;
};

const assertDocsHideAliasedGenerics = (docPaths, aliasedGenericNames) => {
  if (aliasedGenericNames.length === 0) return;

  const escaped = aliasedGenericNames
    .map(({ internalName }) => escapeRegExp(internalName))
    .join("|");
  const needle = new RegExp(`\\b(?:${escaped})\\b`, "g");

  const failures = [];
  for (const docPath of docPaths) {
    const contents = readText(docPath);
    const matches = contents.match(needle);
    if (matches?.length) {
      failures.push({ docPath, matches: Array.from(new Set(matches)).sort() });
    }
  }

  if (failures.length) {
    console.error("Docs leaked internal generic-arity names (expected facade names instead):");
    for (const failure of failures) {
      console.error(`  - ${path.relative(repoRoot, failure.docPath)}: ${failure.matches.join(", ")}`);
    }
    process.exit(1);
  }
};

assertDocsHideAliasedGenerics(
  modulePages.map((pageName) => path.resolve(docsModulesDir, `${pageName}.md`)),
  collectAliasedGenericNames(nodejsExports, httpExports),
);

console.log(`Updated API reference sections for ${modulePages.length} pages.`);
