# `timers`

Import:

```ts
import { timers } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, timers } from "@tsonic/nodejs/index.js";
import { Thread } from "@tsonic/dotnet/System.Threading.js";

export function main(): void {
  timers.setTimeout(() => console.log("tick"), 50);
  Thread.sleep(100);
}
```

