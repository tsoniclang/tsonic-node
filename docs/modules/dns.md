# `dns`

Import:

```ts
import { dns } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, dns } from "@tsonic/nodejs/index.js";

export function main(): void {
  dns.resolve("example.com", (err, records) => {
    console.log(err ?? records);
  });
}
```

