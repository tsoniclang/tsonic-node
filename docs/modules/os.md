# `os`

Import:

```ts
import { os } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, os } from "@tsonic/nodejs/index.js";

export function main(): void {
  console.log(os.platform());
  console.log(os.arch());
  console.log(os.homedir());
}
```

## API Reference

<!-- API:START -->
### `CpuInfo`

```ts
export interface CpuInfo {
    model: string;
    speed: int;
    times: CpuTimes;
}

export const CpuInfo: {
    new(): CpuInfo;
};
```

### `CpuTimes`

```ts
export interface CpuTimes {
    idle: long;
    irq: long;
    nice: long;
    sys: long;
    user: long;
}

export const CpuTimes: {
    new(): CpuTimes;
};
```

### `os`

```ts
export declare const os: {
  readonly devNull: string;
  readonly EOL: string;
  arch(): string;
  availableParallelism(): int;
  cpus(): CpuInfo[];
  endianness(): string;
  freemem(): long;
  homedir(): string;
  hostname(): string;
  loadavg(): double[];
  platform(): string;
  release(): string;
  tmpdir(): string;
  totalmem(): long;
  type(): string;
  uptime(): long;
  userInfo(): UserInfo;
};
```

### `UserInfo`

```ts
export interface UserInfo {
    gid: int;
    homedir: string;
    get shell(): string | undefined;
    set shell(value: string);
    uid: int;
    username: string;
}

export const UserInfo: {
    new(): UserInfo;
};
```
<!-- API:END -->
