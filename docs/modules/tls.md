# `tls`

Import:

```ts
import { tls } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, tls } from "@tsonic/nodejs/index.js";

export function main(): void {
  console.log(tls.getCiphers().slice(0, 5));
}
```

