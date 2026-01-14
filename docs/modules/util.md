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

