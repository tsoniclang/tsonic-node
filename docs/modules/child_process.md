# `child_process`

Import:

```ts
import { child_process } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, child_process } from "@tsonic/nodejs/index.js";

export function main(): void {
  const result = child_process.execSyncString("echo hello");
  console.log(result);
}
```

