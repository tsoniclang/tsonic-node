# `fs`

Import:

```ts
import { fs } from "@tsonic/nodejs/index.js";
```

Examples:

```ts
import { console, fs } from "@tsonic/nodejs/index.js";

export function main(): void {
  const file = "./README.md";

  if (!fs.existsSync(file)) {
    console.log("Missing README.md");
    return;
  }

  const text = fs.readFileSync(file, "utf-8");
  console.log(text);

  fs.writeFileSync("./out.txt", "Hello from Tsonic!");
}
```

## API Reference

<!-- API:START -->
### `fs`

```ts
export declare const fs: {
  access(path: string, mode?: int): Task;
  accessSync(path: string, mode?: int): void;
  appendFile(path: string, data: string, encoding?: string): Task;
  appendFileSync(path: string, data: string, encoding?: string): void;
  chmod(path: string, mode: int): Task;
  chmodSync(path: string, mode: int): void;
  close(fd: int): Task;
  closeSync(fd: int): void;
  copyFile(src: string, dest: string, mode?: int): Task;
  copyFileSync(src: string, dest: string, mode?: int): void;
  cp(src: string, dest: string, recursive?: boolean): Task;
  cpSync(src: string, dest: string, recursive?: boolean): void;
  existsSync(path: string): boolean;
  fstat(fd: int): Task<Stats>;
  fstatSync(fd: int): Stats;
  mkdir(path: string, recursive?: boolean): Task;
  mkdirSync(path: string, recursive?: boolean): void;
  open(path: string, flags: string, mode?: Nullable<System_Internal.Int32>): Task<System_Internal.Int32>;
  openSync(path: string, flags: string, mode?: Nullable<System_Internal.Int32>): int;
  read(fd: int, buffer: byte[], offset: int, length: int, position: Nullable<System_Internal.Int32>): Task<System_Internal.Int32>;
  readdir(path: string, withFileTypes?: boolean): Task<string[]>;
  readdirSync(path: string, withFileTypes?: boolean): string[];
  readFile(path: string, encoding?: string): Task<System_Internal.String>;
  readFileBytes(path: string): Task<byte[]>;
  readFileSync(path: string, encoding?: string): string;
  readFileSyncBytes(path: string): byte[];
  readlink(path: string): Task<System_Internal.String>;
  readlinkSync(path: string): string;
  readSync(fd: int, buffer: byte[], offset: int, length: int, position: Nullable<System_Internal.Int32>): int;
  realpath(path: string): Task<System_Internal.String>;
  realpathSync(path: string): string;
  rename(oldPath: string, newPath: string): Task;
  renameSync(oldPath: string, newPath: string): void;
  rm(path: string, recursive?: boolean): Task;
  rmdir(path: string, recursive?: boolean): Task;
  rmdirSync(path: string, recursive?: boolean): void;
  rmSync(path: string, recursive?: boolean): void;
  stat(path: string): Task<Stats>;
  statSync(path: string): Stats;
  symlink(target: string, path: string, type?: string): Task;
  symlinkSync(target: string, path: string, type?: string): void;
  truncate(path: string, len?: long): Task;
  truncateSync(path: string, len?: long): void;
  unlink(path: string): Task;
  unlinkSync(path: string): void;
  write(fd: int, buffer: byte[], offset: int, length: int, position: Nullable<System_Internal.Int32>): Task<System_Internal.Int32>;
  write(fd: int, data: string, position?: Nullable<System_Internal.Int32>, encoding?: string): Task<System_Internal.Int32>;
  writeFile(path: string, data: string, encoding?: string): Task;
  writeFileBytes(path: string, data: byte[]): Task;
  writeFileSync(path: string, data: string, encoding?: string): void;
  writeFileSyncBytes(path: string, data: byte[]): void;
  writeSync(fd: int, buffer: byte[], offset: int, length: int, position: Nullable<System_Internal.Int32>): int;
  writeSync(fd: int, data: string, position?: Nullable<System_Internal.Int32>, encoding?: string): int;
};
```

### `Stats`

```ts
export interface Stats {
    atime: DateTime;
    birthtime: DateTime;
    ctime: DateTime;
    isDirectory2: boolean;
    isFile2: boolean;
    mode: int;
    mtime: DateTime;
    size: long;
    isBlockDevice(): boolean;
    isCharacterDevice(): boolean;
    isDirectory(): boolean;
    isFIFO(): boolean;
    isFile(): boolean;
    isSocket(): boolean;
    isSymbolicLink(): boolean;
}

export const Stats: {
    new(): Stats;
};
```
<!-- API:END -->
