# `child_process`

Import:

```ts
import { child_process } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, child_process } from "@tsonic/nodejs/index.js";

export function main(): void {
  const result = child_process.execSyncString("echo hello");
  console.log(result);
}
```

## API Reference

<!-- API:START -->
### `child_process`

```ts
export declare const child_process: {
  exec(command: string, options: ExecOptions, callback: Action<Exception, System_Internal.String, System_Internal.String>): void;
  exec(command: string, callback: Action<Exception, System_Internal.String, System_Internal.String>): void;
  execFile(file: string, args: string[], options: ExecOptions, callback: Action<Exception, System_Internal.String, System_Internal.String>): void;
  execFileSync(file: string, args?: string[], options?: ExecOptions): unknown;
  execSync(command: string, options: ExecOptions): unknown;
  execSync(command: string): byte[];
  fork(modulePath: string, args?: string[], options?: ExecOptions): ChildProcess;
  spawn(command: string, args?: string[], options?: ExecOptions): ChildProcess;
  spawnSync(command: string, args?: string[], options?: ExecOptions): SpawnSyncReturns<byte[]>;
  spawnSyncString(command: string, args?: string[], options?: ExecOptions): SpawnSyncReturns<System_Internal.String>;
};
```

### `ChildProcess`

```ts
export interface ChildProcess extends EventEmitter {
    readonly connected: boolean;
    readonly exitCode: Nullable<System_Internal.Int32>;
    readonly killed: boolean;
    readonly pid: int;
    readonly referenced: boolean;
    readonly signalCode: string | undefined;
    readonly spawnargs: string[];
    readonly spawnfile: string;
    readonly stderr: Readable;
    readonly stdin: Writable | undefined;
    readonly stdout: Readable;
    disconnect(): void;
    kill(signal?: string): boolean;
    ref(): void;
    send(message: unknown, sendHandle?: unknown, options?: unknown, callback?: Action<Exception>): boolean;
    unref(): void;
}

export const ChildProcess: {
    new(): ChildProcess;
};
```

### `ExecOptions`

```ts
export interface ExecOptions {
    get argv0(): string | undefined;
    set argv0(value: string);
    get cwd(): string | undefined;
    set cwd(value: string);
    detached: boolean;
    get encoding(): string | undefined;
    set encoding(value: string);
    get env(): unknown | undefined;
    set env(value: unknown);
    gid: Nullable<System_Internal.Int32>;
    get input(): string | undefined;
    set input(value: string);
    get killSignal(): string | undefined;
    set killSignal(value: string);
    maxBuffer: int;
    get shell(): string | undefined;
    set shell(value: string);
    get stdio(): string | undefined;
    set stdio(value: string);
    timeout: int;
    uid: Nullable<System_Internal.Int32>;
    windowsHide: boolean;
    windowsVerbatimArguments: boolean;
}

export const ExecOptions: {
    new(): ExecOptions;
};
```

### `SpawnSyncReturns`

```ts
export interface SpawnSyncReturns<T> {
    get error(): Exception | undefined;
    set error(value: Exception);
    output: (T | undefined)[];
    pid: int;
    get signal(): string | undefined;
    set signal(value: string);
    status: Nullable<System_Internal.Int32>;
    stderr: T;
    stdout: T;
}

export const SpawnSyncReturns: {
    new<T>(): SpawnSyncReturns<T>;
};
```
<!-- API:END -->
