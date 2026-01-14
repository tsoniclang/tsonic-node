# `zlib`

Import:

```ts
import { zlib } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, fs, zlib } from "@tsonic/nodejs/index.js";

export function main(): void {
  const input = fs.readFileSyncBytes("./README.md");
  const compressed = zlib.brotliCompressSync(input);
  const roundtrip = zlib.brotliDecompressSync(compressed);
  console.log(roundtrip.length);
}
```
