# `readline`

Import:

```ts
import { readline } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, readline } from "@tsonic/nodejs/index.js";

export function main(): void {
  // Cursor helpers (requires a Writable stream to target).
  // See bindings for the full API surface.
  console.log(typeof readline.createInterface);
}
```

