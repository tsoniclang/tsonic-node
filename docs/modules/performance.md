# `performance`

Import:

```ts
import { performance } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, performance } from "@tsonic/nodejs/index.js";

export function main(): void {
  const start = performance.now();
  // ...work...
  const end = performance.now();
  console.log(`elapsed: ${end - start}ms`);
}
```

