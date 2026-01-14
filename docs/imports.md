# Importing Modules

## Importing from `@tsonic/nodejs`

Most Node-style modules are exported as named values from the package root:

```ts
import { console, fs, path, process, crypto } from "@tsonic/nodejs";
```

Types that are part of those modules are also exported from the root:

```ts
import { EventEmitter } from "@tsonic/nodejs";
```

## Importing submodules (namespaces)

Some namespaces are emitted as separate entry points. For example, HTTP lives in the `nodejs.Http` namespace:

```ts
import { http, IncomingMessage, ServerResponse } from "@tsonic/nodejs/nodejs.Http.js";
```

## Do not import Node builtins (`"fs"`, `"path"`, ...)

Tsonic projects compile to .NET and do **not** run on Node.js. You should not write:

```ts
// ❌ Not supported
import * as fs from "fs";
import * as path from "path";
```

Instead import from `@tsonic/nodejs`:

```ts
// ✅ Supported
import { fs, path } from "@tsonic/nodejs";
```

