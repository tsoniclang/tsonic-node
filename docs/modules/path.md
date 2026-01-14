# `path`

Import:

```ts
import { path } from "@tsonic/nodejs/index.js";
```

Examples:

```ts
import { console, path } from "@tsonic/nodejs/index.js";

export function main(): void {
  console.log(path.join("a", "b", "c"));
  console.log(path.basename("/tmp/file.txt"));
  console.log(path.extname("index.html"));
  console.log(path.dirname("/tmp/file.txt"));
}
```

## API Reference

<!-- API:START -->
### `ParsedPath`

```ts
export interface ParsedPath {
    base: string;
    dir: string;
    ext: string;
    name: string;
    root: string;
}

export const ParsedPath: {
    new(): ParsedPath;
};
```

### `path`

```ts
export declare const path: {
  readonly sep: string;
  readonly delimiter: string;
  readonly posix: PathModule;
  readonly win32: PathModule;
  basename(path: string, suffix?: string): string;
  dirname(path: string): string;
  extname(path: string): string;
  format(pathObject: ParsedPath): string;
  isAbsolute(path: string): boolean;
  join(...paths: string[]): string;
  matchesGlob(path: string, pattern: string): boolean;
  normalize(path: string): string;
  parse(path: string): ParsedPath;
  relative(from_: string, to: string): string;
  resolve(...paths: string[]): string;
  toNamespacedPath(path: string): string;
};
```

### `PathModule`

```ts
export interface PathModule {
    readonly delimiter: string;
    readonly posix: PathModule;
    readonly sep: string;
    readonly win32: PathModule;
    basename(path: string, suffix?: string): string;
    dirname(path: string): string;
    extname(path: string): string;
    format(pathObject: ParsedPath): string;
    isAbsolute(path: string): boolean;
    join(...paths: string[]): string;
    matchesGlob(path: string, pattern: string): boolean;
    normalize(path: string): string;
    parse(path: string): ParsedPath;
    relative(from_: string, to: string): string;
    resolve(...paths: string[]): string;
    toNamespacedPath(path: string): string;
}

export const PathModule: {
    new(): PathModule;
    readonly instance: PathModule;
};
```
<!-- API:END -->
