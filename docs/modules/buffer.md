# `Buffer`

`Buffer` is a byte container commonly used across Node-style APIs.

Import:

```ts
import { Buffer } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, Buffer } from "@tsonic/nodejs/index.js";

export function main(): void {
  const buf = Buffer.from_("hello", "utf8");
  console.log(buf.length);
}
```

## API Reference

<!-- API:START -->
### `Buffer`

```ts
export interface Buffer {
    item: byte;
    readonly length: int;
    compare(target: Buffer, targetStart?: Nullable<System_Internal.Int32>, targetEnd?: Nullable<System_Internal.Int32>, sourceStart?: Nullable<System_Internal.Int32>, sourceEnd?: Nullable<System_Internal.Int32>): int;
    copy(target: Buffer, targetStart?: int, sourceStart?: Nullable<System_Internal.Int32>, sourceEnd?: Nullable<System_Internal.Int32>): int;
    equals(otherBuffer: Buffer): boolean;
    fill(value: unknown, offset?: int, end?: Nullable<System_Internal.Int32>, encoding?: string): Buffer;
    includes(value: unknown, byteOffset?: int, encoding?: string): boolean;
    indexOf(value: unknown, byteOffset?: int, encoding?: string): int;
    lastIndexOf(value: unknown, byteOffset?: Nullable<System_Internal.Int32>, encoding?: string): int;
    readBigInt64BE(offset?: int): long;
    readBigInt64LE(offset?: int): long;
    readBigUint64BE(offset?: int): ulong;
    readBigUInt64BE(offset?: int): ulong;
    readBigUint64LE(offset?: int): ulong;
    readBigUInt64LE(offset?: int): ulong;
    readDoubleBE(offset?: int): double;
    readDoubleLE(offset?: int): double;
    readFloatBE(offset?: int): float;
    readFloatLE(offset?: int): float;
    readInt16BE(offset?: int): short;
    readInt16LE(offset?: int): short;
    readInt32BE(offset?: int): int;
    readInt32LE(offset?: int): int;
    readInt8(offset?: int): sbyte;
    readIntBE(offset: int, byteLength: int): long;
    readIntLE(offset: int, byteLength: int): long;
    readUint16BE(offset?: int): ushort;
    readUInt16BE(offset?: int): ushort;
    readUint16LE(offset?: int): ushort;
    readUInt16LE(offset?: int): ushort;
    readUint32BE(offset?: int): uint;
    readUInt32BE(offset?: int): uint;
    readUint32LE(offset?: int): uint;
    readUInt32LE(offset?: int): uint;
    readUint8(offset?: int): byte;
    readUInt8(offset?: int): byte;
    readUintBE(offset: int, byteLength: int): ulong;
    readUIntBE(offset: int, byteLength: int): ulong;
    readUintLE(offset: int, byteLength: int): ulong;
    readUIntLE(offset: int, byteLength: int): ulong;
    reverse(): Buffer;
    slice(start?: Nullable<System_Internal.Int32>, end?: Nullable<System_Internal.Int32>): Buffer;
    subarray(start?: Nullable<System_Internal.Int32>, end?: Nullable<System_Internal.Int32>): Buffer;
    swap16(): Buffer;
    swap32(): Buffer;
    swap64(): Buffer;
    toJSON(): unknown;
    toString(encoding?: string, start?: int, end?: Nullable<System_Internal.Int32>): string;
    write(str: string, offset?: int, length?: Nullable<System_Internal.Int32>, encoding?: string): int;
    writeBigInt64BE(value: long, offset?: int): int;
    writeBigInt64LE(value: long, offset?: int): int;
    writeBigUint64BE(value: ulong, offset?: int): int;
    writeBigUInt64BE(value: ulong, offset?: int): int;
    writeBigUint64LE(value: ulong, offset?: int): int;
    writeBigUInt64LE(value: ulong, offset?: int): int;
    writeDoubleBE(value: double, offset?: int): int;
    writeDoubleLE(value: double, offset?: int): int;
    writeFloatBE(value: float, offset?: int): int;
    writeFloatLE(value: float, offset?: int): int;
    writeInt16BE(value: short, offset?: int): int;
    writeInt16LE(value: short, offset?: int): int;
    writeInt32BE(value: int, offset?: int): int;
    writeInt32LE(value: int, offset?: int): int;
    writeInt8(value: sbyte, offset?: int): int;
    writeIntBE(value: long, offset: int, byteLength: int): int;
    writeIntLE(value: long, offset: int, byteLength: int): int;
    writeUint16BE(value: ushort, offset?: int): int;
    writeUInt16BE(value: ushort, offset?: int): int;
    writeUint16LE(value: ushort, offset?: int): int;
    writeUInt16LE(value: ushort, offset?: int): int;
    writeUint32BE(value: uint, offset?: int): int;
    writeUInt32BE(value: uint, offset?: int): int;
    writeUint32LE(value: uint, offset?: int): int;
    writeUInt32LE(value: uint, offset?: int): int;
    writeUint8(value: byte, offset?: int): int;
    writeUInt8(value: byte, offset?: int): int;
    writeUintBE(value: ulong, offset: int, byteLength: int): int;
    writeUIntBE(value: ulong, offset: int, byteLength: int): int;
    writeUintLE(value: ulong, offset: int, byteLength: int): int;
    writeUIntLE(value: ulong, offset: int, byteLength: int): int;
}

export const Buffer: {
    new(): Buffer;
    poolSize: int;
    alloc(size: int, fill?: unknown, encoding?: string): Buffer;
    allocUnsafe(size: int): Buffer;
    allocUnsafeSlow(size: int): Buffer;
    byteLength(str: string, encoding?: string): int;
    compare(buf1: Buffer, buf2: Buffer): int;
    concat(list: Buffer[], totalLength?: Nullable<System_Internal.Int32>): Buffer;
    from_(buffer: Buffer): Buffer;
    from_(array: byte[]): Buffer;
    from_(array: int[]): Buffer;
    from_(str: string, encoding?: string): Buffer;
    isBuffer(obj: unknown): boolean;
    isEncoding(encoding: string): boolean;
    of_(...items: int[]): Buffer;
};
```
<!-- API:END -->
