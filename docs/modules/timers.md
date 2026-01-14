# `timers`

Import:

```ts
import { timers } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, timers } from "@tsonic/nodejs/index.js";
import { Thread } from "@tsonic/dotnet/System.Threading.js";

export function main(): void {
  timers.setTimeout(() => console.log("tick"), 50);
  Thread.sleep(100);
}
```

## API Reference

<!-- API:START -->
### `Immediate`

```ts
export interface Immediate {
    dispose(): void;
    hasRef(): boolean;
    ref(): Immediate;
    unref(): Immediate;
}

export const Immediate: {
    new(): Immediate;
};
```

### `Timeout`

```ts
export interface Timeout {
    close(): void;
    dispose(): void;
    hasRef(): boolean;
    ref(): Timeout;
    refresh(): Timeout;
    unref(): Timeout;
}

export const Timeout: {
    new(): Timeout;
};
```

### `timers`

```ts
export declare const timers: {
  clearImmediate(immediate: Immediate): void;
  clearInterval(timeout: Timeout): void;
  clearTimeout(timeout: Timeout): void;
  queueMicrotask(callback: Action): void;
  setImmediate(callback: Action): Immediate;
  setInterval(callback: Action, delay?: int): Timeout;
  setTimeout(callback: Action, delay?: int): Timeout;
};
```
<!-- API:END -->
