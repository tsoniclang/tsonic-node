# `events`

Import:

```ts
import { EventEmitter } from "@tsonic/nodejs";
```

Example:

```ts
import { console, EventEmitter } from "@tsonic/nodejs";

class MyEmitter extends EventEmitter {}

export function main(): void {
  const emitter = new MyEmitter();
  emitter.on("data", (chunk) => console.log(chunk));
  emitter.emit("data", "hello");
}
```

