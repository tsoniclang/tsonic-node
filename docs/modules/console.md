# `console`

Import:

```ts
import { console } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console } from "@tsonic/nodejs/index.js";

export function main(): void {
  console.log("hello");
  console.warn("warn");
  console.error("error");
}
```

## API Reference

<!-- API:START -->
### `console`

```ts
export declare const console: {
  assert(value: boolean, message?: string, ...optionalParams: unknown[]): void;
  clear(): void;
  count(label?: string): void;
  countReset(label?: string): void;
  debug(message?: unknown, ...optionalParams: unknown[]): void;
  dir(obj: unknown, ...options: unknown[]): void;
  dirxml(...data: unknown[]): void;
  error(message?: unknown, ...optionalParams: unknown[]): void;
  group(...label: unknown[]): void;
  groupCollapsed(...label: unknown[]): void;
  groupEnd(): void;
  info(message?: unknown, ...optionalParams: unknown[]): void;
  log(message?: unknown, ...optionalParams: unknown[]): void;
  profile(label?: string): void;
  profileEnd(label?: string): void;
  table(tabularData: unknown, properties?: string[]): void;
  time(label?: string): void;
  timeEnd(label?: string): void;
  timeLog(label?: string, ...data: unknown[]): void;
  timeStamp(label?: string): void;
  trace(message?: unknown, ...optionalParams: unknown[]): void;
  warn(message?: unknown, ...optionalParams: unknown[]): void;
};
```
<!-- API:END -->
