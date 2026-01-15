# `net`

Import:

```ts
import { net } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, net } from "@tsonic/nodejs/index.js";

export function main(): void {
  const server = net.createServer(() => {});
  console.log(typeof server.listen);
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

### `BlockList`

```ts
export interface BlockList {
    addAddress(address: string, type?: string): void;
    addRange(start: string, end: string, type?: string): void;
    addSubnet(network: string, prefix: int, type?: string): void;
    check(address: string, type?: string): boolean;
    getRules(): string[];
}

export const BlockList: {
    new(): BlockList;
};
```

### `IpcSocketConnectOpts`

```ts
export interface IpcSocketConnectOpts {
    path: string;
}

export const IpcSocketConnectOpts: {
    new(): IpcSocketConnectOpts;
};
```

### `ListenOptions`

```ts
export interface ListenOptions {
    backlog: Nullable<System_Internal.Int32>;
    host: string;
    ipv6Only: Nullable<System_Internal.Boolean>;
    path: string;
    port: Nullable<System_Internal.Int32>;
}

export const ListenOptions: {
    new(): ListenOptions;
};
```

### `net`

```ts
export declare const net: {
  connect(options: TcpSocketConnectOpts, connectionListener?: Action): Socket;
  connect(port: int, host?: string, connectionListener?: Action): Socket;
  connect(path: string, connectionListener?: Action): Socket;
  createConnection(options: TcpSocketConnectOpts, connectionListener?: Action): Socket;
  createConnection(port: int, host?: string, connectionListener?: Action): Socket;
  createConnection(path: string, connectionListener?: Action): Socket;
  createServer(options: ServerOpts, connectionListener?: Action<Socket>): Server;
  createServer(connectionListener?: Action<Socket>): Server;
  getDefaultAutoSelectFamily(): boolean;
  getDefaultAutoSelectFamilyAttemptTimeout(): int;
  isIP(input: string): int;
  isIPv4(input: string): boolean;
  isIPv6(input: string): boolean;
  setDefaultAutoSelectFamily(value: boolean): void;
  setDefaultAutoSelectFamilyAttemptTimeout(value: int): void;
};
```

### `Server`

```ts
export interface Server extends EventEmitter {
    readonly listening: boolean;
    maxConnections: int;
    address(): unknown;
    close(callback?: Action<Exception>): Server;
    getConnections(callback: Action<Exception, System_Internal.Int32>): void;
    listen(port: int, hostname: string, backlog: int, listeningListener?: Action): Server;
    listen(port: int, hostname: string, listeningListener?: Action): Server;
    listen(port: int, backlog: int, listeningListener?: Action): Server;
    listen(port: int, listeningListener?: Action): Server;
    listen(options: ListenOptions, listeningListener?: Action): Server;
    ref(): Server;
    unref(): Server;
}

export const Server: {
    new(): Server;
    new(connectionListener: Action<Socket>): Server;
    new(options: ServerOpts, connectionListener: Action<Socket>): Server;
};
```

### `ServerOpts`

```ts
export interface ServerOpts {
    allowHalfOpen: Nullable<System_Internal.Boolean>;
    pauseOnConnect: Nullable<System_Internal.Boolean>;
}

export const ServerOpts: {
    new(): ServerOpts;
};
```

### `Socket`

```ts
export interface Socket extends Stream {
    readonly bytesRead: long;
    readonly bytesWritten: long;
    readonly connecting: boolean;
    readonly destroyed: boolean;
    readonly localAddress: string | undefined;
    readonly localFamily: string | undefined;
    readonly localPort: Nullable<System_Internal.Int32>;
    readonly readyState: string;
    readonly remoteAddress: string | undefined;
    readonly remoteFamily: string | undefined;
    readonly remotePort: Nullable<System_Internal.Int32>;
    address(): unknown;
    connect(port: int, host?: string, connectionListener?: Action): Socket;
    connect(options: TcpSocketConnectOpts, connectionListener?: Action): Socket;
    connect(path: string, connectionListener?: Action): Socket;
    destroy(error?: Exception): Socket;
    destroy(error?: Exception): void;
    destroySoon(): void;
    end(callback?: Action): Socket;
    end(data: byte[], callback?: Action): Socket;
    end(data: string, encoding?: string, callback?: Action): Socket;
    pause(): Socket;
    ref(): Socket;
    resetAndDestroy(): Socket;
    resume(): Socket;
    setEncoding(encoding?: string): Socket;
    setKeepAlive(enable?: boolean, initialDelay?: int): Socket;
    setNoDelay(noDelay?: boolean): Socket;
    setTimeout(timeout: int, callback?: Action): Socket;
    unref(): Socket;
    write(data: byte[], callback?: Action<Exception>): boolean;
    write(data: string, encoding?: string, callback?: Action<Exception>): boolean;
}

export const Socket: {
    new(): Socket;
    new(options: SocketConstructorOpts): Socket;
};
```

### `SocketAddress`

```ts
export interface SocketAddress {
    readonly address: string;
    readonly family: string;
    readonly flowlabel: Nullable<System_Internal.Int32>;
    readonly port: int;
}

export const SocketAddress: {
    new(options: SocketAddressInitOptions): SocketAddress;
};
```

### `SocketAddressInitOptions`

```ts
export interface SocketAddressInitOptions {
    address: string;
    family: string;
    flowlabel: Nullable<System_Internal.Int32>;
    port: Nullable<System_Internal.Int32>;
}

export const SocketAddressInitOptions: {
    new(): SocketAddressInitOptions;
};
```

### `SocketConstructorOpts`

```ts
export interface SocketConstructorOpts {
    allowHalfOpen: Nullable<System_Internal.Boolean>;
    fd: Nullable<System_Internal.Int32>;
    readable: Nullable<System_Internal.Boolean>;
    writable: Nullable<System_Internal.Boolean>;
}

export const SocketConstructorOpts: {
    new(): SocketConstructorOpts;
};
```

### `TcpSocketConnectOpts`

```ts
export interface TcpSocketConnectOpts {
    family: Nullable<System_Internal.Int32>;
    hints: Nullable<System_Internal.Int32>;
    host: string;
    keepAlive: Nullable<System_Internal.Boolean>;
    keepAliveInitialDelay: Nullable<System_Internal.Int32>;
    get localAddress(): string | undefined;
    set localAddress(value: string);
    localPort: Nullable<System_Internal.Int32>;
    noDelay: Nullable<System_Internal.Boolean>;
    port: int;
}

export const TcpSocketConnectOpts: {
    new(): TcpSocketConnectOpts;
};
```
<!-- API:END -->
