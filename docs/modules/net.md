# `net`

Import:

```ts
import { net } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, net } from "@tsonic/nodejs/index.js";

export function main(): void {
  const server = net.createServer(() => {});
  console.log(typeof server.listen);
}
```

