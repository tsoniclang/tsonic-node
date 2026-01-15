# `http` (`nodejs.Http`)

The HTTP API lives in the `nodejs.Http` namespace and is imported as a submodule.

Import:

```ts
import { http, IncomingMessage, ServerResponse } from "@tsonic/nodejs/nodejs.Http.js";
```

Example:

```ts
import { console } from "@tsonic/nodejs/index.js";
import { http, IncomingMessage, ServerResponse } from "@tsonic/nodejs/nodejs.Http.js";
import { Thread, Timeout } from "@tsonic/dotnet/System.Threading.js";

export function main(): void {
  const server = http.createServer((req: IncomingMessage, res: ServerResponse) => {
    console.log(`${req.method} ${req.url}`);
    res.setHeader("Content-Type", "text/plain");
    res.writeHead(200, "OK");
    res.end("Hello from Tsonic!");
  });

  server.listen(3000, () => {
    console.log("Listening on http://localhost:3000/");
  });

  Thread.sleep(Timeout.infinite);
}
```

## API Reference

<!-- API:START -->
### `AddressInfo`

```ts
export interface AddressInfo {
    address: string;
    family: string;
    port: int;
}

export const AddressInfo: {
    new(): AddressInfo;
};
```

### `ClientRequest`

```ts
export interface ClientRequest extends EventEmitter {
    readonly aborted: boolean;
    readonly host: string;
    readonly method: string;
    readonly path: string;
    readonly protocol: string;
    abort(): void;
    end(chunk?: string, encoding?: string, callback?: Action): Task;
    getHeader(name: string): string | undefined;
    getHeaderNames(): string[];
    removeHeader(name: string): void;
    setHeader(name: string, value: string): void;
    setTimeout(msecs: int, callback?: Action): ClientRequest;
    write(chunk: string, encoding?: string, callback?: Action): boolean;
}

export const ClientRequest: {
    new(): ClientRequest;
};
```

### `http`

```ts
export declare const http: {
  globalAgent_maxSockets: Nullable<System_Internal.Int32>;
  globalAgent_maxFreeSockets: int;
  globalAgent_timeout: int;
  maxHeaderSize: int;
  createServer(requestListener?: Action<IncomingMessage, ServerResponse>): Server;
  get(options: RequestOptions, callback?: Action<IncomingMessage>): ClientRequest;
  get(url: string, callback?: Action<IncomingMessage>): ClientRequest;
  request(options: RequestOptions, callback?: Action<IncomingMessage>): ClientRequest;
  request(url: string, callback?: Action<IncomingMessage>): ClientRequest;
  validateHeaderName(name: string): void;
  validateHeaderValue(name: string, value: unknown): void;
};
```

### `IncomingMessage`

```ts
export interface IncomingMessage extends EventEmitter {
    readonly complete: boolean;
    readonly headers: Dictionary<System_Internal.String, System_Internal.String>;
    readonly httpVersion: string;
    readonly method: string;
    readonly statusCode: Nullable<System_Internal.Int32>;
    readonly statusMessage: string;
    readonly url: string | undefined;
    destroy(): void;
    onClose(callback: Action): void;
    onData(callback: Action<System_Internal.String>): void;
    onEnd(callback: Action): void;
    readAll(): Task<System_Internal.String>;
    setTimeout(msecs: int, callback?: Action): IncomingMessage;
}

export const IncomingMessage: {
    new(): IncomingMessage;
};
```

### `RequestOptions`

```ts
export interface RequestOptions {
    get agent(): unknown | undefined;
    set agent(value: unknown);
    get auth(): string | undefined;
    set auth(value: string);
    headers: Dictionary<System_Internal.String, System_Internal.String>;
    host: string;
    hostname: string;
    method: string;
    path: string;
    port: int;
    protocol: string;
    timeout: Nullable<System_Internal.Int32>;
}

export const RequestOptions: {
    new(): RequestOptions;
};
```

### `Server`

```ts
export interface Server extends EventEmitter {
    headersTimeout: int;
    keepAliveTimeout: int;
    readonly listening: boolean;
    maxHeadersCount: int;
    requestTimeout: int;
    timeout: int;
    address(): AddressInfo;
    close(callback?: Action): Server;
    listen(port: int, hostname?: string, backlog?: Nullable<System_Internal.Int32>, callback?: Action): Server;
    listen(port: int, callback: Action): Server;
    setTimeout(msecs: int, callback?: Action): Server;
}

export const Server: {
    new(requestListener: Action<IncomingMessage, ServerResponse>): Server;
};
```

### `ServerResponse`

```ts
export interface ServerResponse extends EventEmitter {
    readonly finished: boolean;
    readonly headersSent: boolean;
    statusCode: int;
    statusMessage: string;
    end(chunk?: string, encoding?: string, callback?: Action): ServerResponse;
    flushHeaders(): void;
    getHeader(name: string): string | undefined;
    getHeaderNames(): string[];
    getHeaders(): Dictionary<System_Internal.String, System_Internal.String>;
    hasHeader(name: string): boolean;
    removeHeader(name: string): void;
    setHeader(name: string, value: string): ServerResponse;
    setTimeout(msecs: int, callback?: Action): ServerResponse;
    write(chunk: string, encoding?: string, callback?: Action): boolean;
    writeHead(statusCode: int, statusMessage?: string, headers?: Dictionary<System_Internal.String, System_Internal.String>): ServerResponse;
    writeHead(statusCode: int, headers: Dictionary<System_Internal.String, System_Internal.String>): ServerResponse;
}

export const ServerResponse: {
    new(): ServerResponse;
};
```

### `TypeError`

```ts
export interface TypeError extends Exception {
}

export const TypeError: {
    new(message: string): TypeError;
};
```
<!-- API:END -->
