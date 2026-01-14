# `stream`

Import:

```ts
import { stream } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, stream } from "@tsonic/nodejs/index.js";

export function main(): void {
  // In many cases you will work with Readable/Writable/Transform instances.
  // The stream module also provides helpers like finished(...) and pipeline(...).
  console.log(typeof stream.pipeline);
}
```

## API Reference

<!-- API:START -->
### `Duplex`

```ts
export interface Duplex extends Readable {
    readonly writable: boolean;
    readonly writableCorked: boolean;
    readonly writableEnded: boolean;
    readonly writableLength: int;
    cork(): void;
    destroy(error?: Exception): void;
    end(chunk?: unknown, encoding?: string, callback?: Action): void;
    uncork(): void;
    write(chunk: unknown, encoding?: string, callback?: Action): boolean;
}

export const Duplex: {
    new(): Duplex;
};
```

### `PassThrough`

```ts
export interface PassThrough extends Transform {
}

export const PassThrough: {
    new(): PassThrough;
};
```

### `Readable`

```ts
export interface Readable extends Stream {
    readonly destroyed: boolean;
    readonly readable: boolean;
    readonly readableEnded: boolean;
    readonly readableFlowing: Nullable<System_Internal.Boolean>;
    readonly readableLength: int;
    destroy(error?: Exception): void;
    isPaused(): boolean;
    pause(): Readable;
    push(chunk: unknown, encoding?: string): boolean;
    read(size?: Nullable<System_Internal.Int32>): unknown | undefined;
    resume(): Readable;
    setEncoding(encoding: string): Readable;
    unpipe(destination?: Stream): Readable;
    unshift(chunk: unknown): void;
}

export const Readable: {
    new(): Readable;
};
```

### `stream`

```ts
export declare const stream: {
  finished(stream: Stream, callback: Action<Exception>): void;
  finished(stream: Stream): Task;
  pipeline(...streams: unknown[]): void;
};
```

### `Stream`

```ts
export interface Stream extends EventEmitter {
    destroy(error?: Exception): void;
    pipe(destination: Stream, end?: boolean): Stream;
}

export const Stream: {
    new(): Stream;
};
```

### `Transform`

```ts
export interface Transform extends Duplex {
}

export const Transform: {
    new(): Transform;
};
```

### `Writable`

```ts
export interface Writable extends Stream {
    readonly destroyed: boolean;
    readonly writable: boolean;
    readonly writableCorked: boolean;
    readonly writableEnded: boolean;
    readonly writableLength: int;
    cork(): void;
    destroy(error?: Exception): void;
    end(chunk?: unknown, encoding?: string, callback?: Action): void;
    uncork(): void;
    write(chunk: unknown, encoding?: string, callback?: Action): boolean;
}

export const Writable: {
    new(): Writable;
};
```
<!-- API:END -->
