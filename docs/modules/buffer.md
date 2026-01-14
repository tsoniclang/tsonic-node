# `Buffer`

`Buffer` is a byte container commonly used across Node-style APIs.

Import:

```ts
import { Buffer } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, Buffer } from "@tsonic/nodejs/index.js";

export function main(): void {
  const buf = Buffer.from_("hello", "utf8");
  console.log(buf.length);
}
```

