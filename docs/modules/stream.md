# `stream`

Import:

```ts
import { stream } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, stream } from "@tsonic/nodejs/index.js";

export function main(): void {
  // In many cases you will work with Readable/Writable/Transform instances.
  // The stream module also provides helpers like finished(...) and pipeline(...).
  console.log(typeof stream.pipeline);
}
```
