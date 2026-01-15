# `assert`

Import:

```ts
import { assert } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { assert } from "@tsonic/nodejs/index.js";

export function main(): void {
  assert.strictEqual(1, 1);
  assert.deepStrictEqual({ x: 1 }, { x: 1 });
}
```

## API Reference

<!-- API:START -->
### `assert`

```ts
export declare const assert: {
  deepEqual(actual: unknown, expected: unknown, message?: string): void;
  deepStrictEqual(actual: unknown, expected: unknown, message?: string): void;
  doesNotMatch(string: string, regexp: Regex, message?: string): void;
  doesNotThrow(fn: Action, message?: string): void;
  equal(actual: unknown, expected: unknown, message?: string): void;
  fail(message?: string): void;
  ifError(value: unknown): void;
  match(string: string, regexp: Regex, message?: string): void;
  notDeepEqual(actual: unknown, expected: unknown, message?: string): void;
  notDeepStrictEqual(actual: unknown, expected: unknown, message?: string): void;
  notEqual(actual: unknown, expected: unknown, message?: string): void;
  notStrictEqual(actual: unknown, expected: unknown, message?: string): void;
  ok(value: boolean, message?: string): void;
  strictEqual(actual: unknown, expected: unknown, message?: string): void;
  throws(fn: Action, message?: string): void;
};
```

### `AssertionError`

```ts
export interface AssertionError extends Exception {
    get actual(): unknown | undefined;
    set actual(value: unknown);
    readonly code: string;
    get expected(): unknown | undefined;
    set expected(value: unknown);
    generatedMessage: boolean;
    operator: string;
}

export const AssertionError: {
    new(message: string, actual: unknown, expected: unknown, operator: string): AssertionError;
};
```
<!-- API:END -->
