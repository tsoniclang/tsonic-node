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

## API Reference

<!-- API:START -->
### `querystring`

```ts
export declare const querystring: {
  decode(str: string, sep?: string, eq?: string, maxKeys?: int): Dictionary<System_Internal.String, unknown>;
  encode(obj: Dictionary<System_Internal.String, unknown>, sep?: string, eq?: string): string;
  escape(str: string): string;
  parse(str: string, sep?: string, eq?: string, maxKeys?: int): Dictionary<System_Internal.String, unknown>;
  stringify(obj: Dictionary<System_Internal.String, unknown>, sep?: string, eq?: string): string;
  unescape(str: string): string;
};
```
<!-- API:END -->
