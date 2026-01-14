# `process`

Import:

```ts
import { process } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, process } from "@tsonic/nodejs/index.js";

export function main(): void {
  console.log(`pid: ${process.pid}`);
  console.log(`platform: ${process.platform}`);
  console.log(`cwd: ${process.cwd()}`);
}
```

## API Reference

<!-- API:START -->
### `process`

```ts
export declare const process: {
  readonly arch: string;
  argv: string[];
  argv0: string;
  readonly env: ProcessEnv;
  readonly execPath: string;
  exitCode: Nullable<System_Internal.Int32>;
  readonly pid: int;
  readonly ppid: int;
  readonly platform: string;
  readonly version: string;
  readonly versions: ProcessVersions;
  chdir(directory: string): void;
  cwd(): string;
  exit(code?: Nullable<System_Internal.Int32>): void;
  kill(pid: int, signal?: unknown): boolean;
};
```

### `ProcessEnv`

```ts
export interface ProcessEnv {
    readonly count: int;
    readonly isReadOnly: boolean;
    get item(): string | undefined;
    set item(value: string);
    readonly keys: ICollection<System_Internal.String>;
    readonly values: ICollection<string | undefined>;
    add(key: string, value: string): void;
    add(item: KeyValuePair<System_Internal.String, System_Internal.String>): void;
    clear(): void;
    contains(item: KeyValuePair<System_Internal.String, System_Internal.String>): boolean;
    containsKey(key: string): boolean;
    copyTo(array: KeyValuePair<System_Internal.String, System_Internal.String>[], arrayIndex: int): void;
    getEnumerator(): IEnumerator<KeyValuePair<System_Internal.String, System_Internal.String>>;
    remove(key: string): boolean;
    remove(item: KeyValuePair<System_Internal.String, System_Internal.String>): boolean;
    tryGetValue(key: string, value: string): boolean;
}

export const ProcessEnv: {
    new(): ProcessEnv;
};
```

### `ProcessVersions`

```ts
export interface ProcessVersions {
    dotnet: string;
    node: string;
    tsonic: string;
    v8: string;
}

export const ProcessVersions: {
    new(): ProcessVersions;
};
```
<!-- API:END -->
