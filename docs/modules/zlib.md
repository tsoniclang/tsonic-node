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

## API Reference

<!-- API:START -->
### `BrotliOptions`

```ts
export interface BrotliOptions {
    chunkSize: Nullable<System_Internal.Int32>;
    maxOutputLength: Nullable<System_Internal.Int32>;
    quality: Nullable<System_Internal.Int32>;
}

export const BrotliOptions: {
    new(): BrotliOptions;
};
```

### `zlib`

```ts
export declare const zlib: {
  brotliCompressSync(buffer: byte[], options?: BrotliOptions): byte[];
  brotliDecompressSync(buffer: byte[], options?: BrotliOptions): byte[];
  crc32(data: byte[], value?: uint): uint;
  crc32(data: string, value?: uint): uint;
  deflateRawSync(buffer: byte[], options?: ZlibOptions): byte[];
  deflateSync(buffer: byte[], options?: ZlibOptions): byte[];
  gunzipSync(buffer: byte[], options?: ZlibOptions): byte[];
  gzipSync(buffer: byte[], options?: ZlibOptions): byte[];
  inflateRawSync(buffer: byte[], options?: ZlibOptions): byte[];
  inflateSync(buffer: byte[], options?: ZlibOptions): byte[];
  unzipSync(buffer: byte[], options?: ZlibOptions): byte[];
};
```

### `ZlibOptions`

```ts
export interface ZlibOptions {
    chunkSize: Nullable<System_Internal.Int32>;
    level: Nullable<System_Internal.Int32>;
    maxOutputLength: Nullable<System_Internal.Int32>;
    memLevel: Nullable<System_Internal.Int32>;
    strategy: Nullable<System_Internal.Int32>;
    windowBits: Nullable<System_Internal.Int32>;
}

export const ZlibOptions: {
    new(): ZlibOptions;
};
```
<!-- API:END -->
