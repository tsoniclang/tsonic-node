# `dgram`

Import:

```ts
import { dgram } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { dgram } from "@tsonic/nodejs/index.js";

export function main(): void {
  const socket = dgram.createSocket("udp4");
  socket.bind(0);
  socket.close();
}
```

