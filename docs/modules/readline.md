# `readline`

Import:

```ts
import { readline } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, readline } from "@tsonic/nodejs/index.js";

export function main(): void {
  // Cursor helpers (requires a Writable stream to target).
  // See bindings for the full API surface.
  console.log(typeof readline.createInterface);
}
```

## API Reference

<!-- API:START -->
### `CursorPosition`

```ts
export interface CursorPosition {
    cols: int;
    rows: int;
}

export const CursorPosition: {
    new(): CursorPosition;
};
```

### `Interface`

```ts
export interface Interface extends EventEmitter {
    readonly cursor: int;
    readonly line: string;
    close(): void;
    getCursorPos(): CursorPosition;
    getPrompt(): string;
    pause(): Interface;
    prompt(preserveCursor?: boolean): void;
    question(query: string, callback: Action<System_Internal.String>): void;
    questionAsync(query: string): Task<System_Internal.String>;
    resume(): Interface;
    setPrompt(prompt: string): void;
    write(data: unknown, key?: unknown): void;
}

export const Interface: {
    new(): Interface;
};
```

### `InterfaceOptions`

```ts
export interface InterfaceOptions {
    escapeCodeTimeout: Nullable<System_Internal.Int32>;
    get history(): string[] | undefined;
    set history(value: string[]);
    historySize: Nullable<System_Internal.Int32>;
    get input(): Readable | undefined;
    set input(value: Readable);
    output: Writable;
    get prompt(): string | undefined;
    set prompt(value: string);
    removeHistoryDuplicates: Nullable<System_Internal.Boolean>;
    tabSize: Nullable<System_Internal.Int32>;
    terminal: Nullable<System_Internal.Boolean>;
}

export const InterfaceOptions: {
    new(): InterfaceOptions;
};
```

### `readline`

```ts
export declare const readline: {
  clearLine(stream: Writable, dir: int, callback?: Action): boolean;
  clearScreenDown(stream: Writable, callback?: Action): boolean;
  createAsyncIterator(input: Readable, options?: InterfaceOptions): IAsyncEnumerable<System_Internal.String>;
  createInterface(options: InterfaceOptions): Interface;
  createInterface(input: Readable, output?: Writable): Interface;
  cursorTo(stream: Writable, x: int, y?: Nullable<System_Internal.Int32>, callback?: Action): boolean;
  moveCursor(stream: Writable, dx: int, dy: int, callback?: Action): boolean;
};
```
<!-- API:END -->
