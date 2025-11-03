/**
 * `Buffer` objects are used to represent a fixed-length sequence of bytes.
 * This is a simplified implementation for Tsonic (TypeScript-to-C#).
 *
 * @see [Node.js Buffer Documentation](https://nodejs.org/api/buffer.html)
 */

export type BufferEncoding =
    | "ascii"
    | "utf8"
    | "utf-8"
    | "utf16le"
    | "utf-16le"
    | "ucs2"
    | "ucs-2"
    | "base64"
    | "base64url"
    | "latin1"
    | "binary"
    | "hex";

export class Buffer {
    /**
     * Gets the length of the buffer in bytes.
     */
    readonly length: number;

    /**
     * Allocates a new Buffer of size bytes. If fill is undefined, the Buffer will be zero-filled.
     */
    static alloc(size: number, fill?: string | number | Buffer, encoding?: BufferEncoding): Buffer;

    /**
     * Allocates a new Buffer of size bytes without initializing the memory.
     */
    static allocUnsafe(size: number): Buffer;

    /**
     * Allocates a new non-pooled Buffer of size bytes.
     */
    static allocUnsafeSlow(size: number): Buffer;

    /**
     * Creates a new Buffer containing the UTF-8 bytes of a string.
     */
    static from(str: string, encoding?: BufferEncoding): Buffer;

    /**
     * Creates a new Buffer from an array of bytes.
     */
    static from(array: number[]): Buffer;

    /**
     * Creates a new Buffer from an array of bytes.
     */
    static from(arrayBuffer: Uint8Array): Buffer;

    /**
     * Creates a new Buffer from a Buffer (creates a copy).
     */
    static from(buffer: Buffer): Buffer;

    /**
     * Creates a Buffer of the given elements.
     */
    static of(...items: number[]): Buffer;

    /**
     * Returns true if obj is a Buffer, false otherwise.
     */
    static isBuffer(obj: any): obj is Buffer;

    /**
     * Returns true if encoding is the name of a supported character encoding.
     */
    static isEncoding(encoding: string): encoding is BufferEncoding;

    /**
     * Returns the byte length of a string when encoded using encoding.
     */
    static byteLength(string: string, encoding?: BufferEncoding): number;

    /**
     * Compares buf1 to buf2, typically for sorting arrays of Buffer instances.
     */
    static compare(buf1: Buffer, buf2: Buffer): -1 | 0 | 1;

    /**
     * Returns a new Buffer which is the result of concatenating all the Buffer instances together.
     */
    static concat(list: Buffer[], totalLength?: number): Buffer;

    /**
     * The size (in bytes) of pre-allocated internal Buffer instances used for pooling.
     */
    static poolSize: number;

    /**
     * Allows indexer access to buffer bytes.
     */
    [index: number]: number;

    /**
     * Decodes buf to a string according to the specified character encoding.
     */
    toString(encoding?: BufferEncoding, start?: number, end?: number): string;

    /**
     * Returns a JSON representation of buf.
     */
    toJSON(): { type: "Buffer"; data: number[] };

    /**
     * Writes string to buf at offset according to the character encoding.
     */
    write(string: string, offset?: number, length?: number, encoding?: BufferEncoding): number;

    /**
     * Returns a new Buffer that references the same memory as the original.
     */
    slice(start?: number, end?: number): Buffer;

    /**
     * Returns a view of the buffer memory.
     */
    subarray(start?: number, end?: number): Buffer;

    /**
     * Copies data from a region of buf to a region in target.
     */
    copy(target: Buffer, targetStart?: number, sourceStart?: number, sourceEnd?: number): number;

    /**
     * Fills buf with the specified value.
     */
    fill(value: string | number | Buffer, offset?: number, end?: number, encoding?: BufferEncoding): this;

    /**
     * Returns true if both buf and otherBuffer have exactly the same bytes.
     */
    equals(otherBuffer: Buffer): boolean;

    /**
     * Compares buf with target and returns a number indicating sort order.
     */
    compare(
        target: Buffer,
        targetStart?: number,
        targetEnd?: number,
        sourceStart?: number,
        sourceEnd?: number
    ): -1 | 0 | 1;

    /**
     * Returns true if value is found in buf.
     */
    includes(value: string | number | Buffer, byteOffset?: number, encoding?: BufferEncoding): boolean;

    /**
     * Returns the index of the first occurrence of value in buf, or -1 if not found.
     */
    indexOf(value: string | number | Buffer, byteOffset?: number, encoding?: BufferEncoding): number;

    /**
     * Returns the index of the last occurrence of value in buf, or -1 if not found.
     */
    lastIndexOf(value: string | number | Buffer, byteOffset?: number, encoding?: BufferEncoding): number;

    /**
     * Reverses the buffer in-place.
     */
    reverse(): this;

    /**
     * Interprets buf as an array of unsigned 16-bit integers and swaps the byte order in-place.
     */
    swap16(): this;

    /**
     * Interprets buf as an array of unsigned 32-bit integers and swaps the byte order in-place.
     */
    swap32(): this;

    /**
     * Interprets buf as an array of 64-bit numbers and swaps byte order in-place.
     */
    swap64(): this;

    // Read methods - 8-bit
    readUInt8(offset?: number): number;
    readUint8(offset?: number): number;
    readInt8(offset?: number): number;

    // Read methods - 16-bit
    readUInt16LE(offset?: number): number;
    readUint16LE(offset?: number): number;
    readInt16LE(offset?: number): number;
    readUInt16BE(offset?: number): number;
    readUint16BE(offset?: number): number;
    readInt16BE(offset?: number): number;

    // Read methods - 32-bit
    readUInt32LE(offset?: number): number;
    readUint32LE(offset?: number): number;
    readInt32LE(offset?: number): number;
    readUInt32BE(offset?: number): number;
    readUint32BE(offset?: number): number;
    readInt32BE(offset?: number): number;

    // Read methods - 64-bit
    readBigUInt64LE(offset?: number): bigint;
    readBigUint64LE(offset?: number): bigint;
    readBigInt64LE(offset?: number): bigint;
    readBigUInt64BE(offset?: number): bigint;
    readBigUint64BE(offset?: number): bigint;
    readBigInt64BE(offset?: number): bigint;

    // Read methods - floating point
    readFloatLE(offset?: number): number;
    readFloatBE(offset?: number): number;
    readDoubleLE(offset?: number): number;
    readDoubleBE(offset?: number): number;

    // Read methods - variable length
    readUIntLE(offset: number, byteLength: number): number;
    readUintLE(offset: number, byteLength: number): number;
    readIntLE(offset: number, byteLength: number): number;
    readUIntBE(offset: number, byteLength: number): number;
    readUintBE(offset: number, byteLength: number): number;
    readIntBE(offset: number, byteLength: number): number;

    // Write methods - 8-bit
    writeUInt8(value: number, offset?: number): number;
    writeUint8(value: number, offset?: number): number;
    writeInt8(value: number, offset?: number): number;

    // Write methods - 16-bit
    writeUInt16LE(value: number, offset?: number): number;
    writeUint16LE(value: number, offset?: number): number;
    writeInt16LE(value: number, offset?: number): number;
    writeUInt16BE(value: number, offset?: number): number;
    writeUint16BE(value: number, offset?: number): number;
    writeInt16BE(value: number, offset?: number): number;

    // Write methods - 32-bit
    writeUInt32LE(value: number, offset?: number): number;
    writeUint32LE(value: number, offset?: number): number;
    writeInt32LE(value: number, offset?: number): number;
    writeUInt32BE(value: number, offset?: number): number;
    writeUint32BE(value: number, offset?: number): number;
    writeInt32BE(value: number, offset?: number): number;

    // Write methods - 64-bit
    writeBigUInt64LE(value: bigint, offset?: number): number;
    writeBigUint64LE(value: bigint, offset?: number): number;
    writeBigInt64LE(value: bigint, offset?: number): number;
    writeBigUInt64BE(value: bigint, offset?: number): number;
    writeBigUint64BE(value: bigint, offset?: number): number;
    writeBigInt64BE(value: bigint, offset?: number): number;

    // Write methods - floating point
    writeFloatLE(value: number, offset?: number): number;
    writeFloatBE(value: number, offset?: number): number;
    writeDoubleLE(value: number, offset?: number): number;
    writeDoubleBE(value: number, offset?: number): number;

    // Write methods - variable length
    writeUIntLE(value: number, offset: number, byteLength: number): number;
    writeUintLE(value: number, offset: number, byteLength: number): number;
    writeIntLE(value: number, offset: number, byteLength: number): number;
    writeUIntBE(value: number, offset: number, byteLength: number): number;
    writeUintBE(value: number, offset: number, byteLength: number): number;
    writeIntBE(value: number, offset: number, byteLength: number): number;
}
