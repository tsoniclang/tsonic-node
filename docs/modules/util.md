# `util`

Import:

```ts
import { util } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, util } from "@tsonic/nodejs/index.js";

export function main(): void {
  console.log(util.format("x=%s y=%s", "1", "2"));
  console.log(util.inspect({ a: 1, b: [1, 2, 3] }));
}
```

## API Reference

<!-- API:START -->
### `util`

```ts
export declare const util: {
  debuglog(section: string): DebugLogFunction;
  deprecate<TResult>(fn: Func<TResult>, msg: string, code?: string): Func<TResult>;
  deprecate(action: Action, msg: string, code?: string): Action;
  format(format: unknown, ...args: unknown[]): string;
  inherits(constructor_: unknown, superConstructor: unknown): void;
  inspect(obj: unknown): string;
  isArray(obj: unknown): boolean;
  isDeepStrictEqual(val1: unknown, val2: unknown): boolean;
};
```
<!-- API:END -->
