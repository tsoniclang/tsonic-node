# `querystring`

Import:

```ts
import { querystring } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, querystring } from "@tsonic/nodejs/index.js";

export function main(): void {
  const parsed = querystring.parse("a=1&b=2");
  console.log(parsed);
  console.log(querystring.stringify(parsed));
}
```

