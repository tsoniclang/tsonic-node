# `os`

Import:

```ts
import { os } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, os } from "@tsonic/nodejs/index.js";

export function main(): void {
  console.log(os.platform());
  console.log(os.arch());
  console.log(os.homedir());
}
```

