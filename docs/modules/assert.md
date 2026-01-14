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

