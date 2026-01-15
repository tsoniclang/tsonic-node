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

## API Reference

<!-- API:START -->
### `BindOptions`

```ts
export interface BindOptions {
    address: string;
    exclusive: boolean;
    fd: Nullable<System_Internal.Int32>;
    port: Nullable<System_Internal.Int32>;
}

export const BindOptions: {
    new(): BindOptions;
};
```

### `dgram`

```ts
export declare const dgram: {
  createSocket(options: SocketOptions, callback?: Action<byte[], RemoteInfo>): DgramSocket;
  createSocket(type: string, callback?: Action<byte[], RemoteInfo>): DgramSocket;
};
```

### `DgramSocket`

```ts
export interface DgramSocket extends EventEmitter {
    addMembership(multicastAddress: string, multicastInterface?: string): void;
    address(): AddressInfo;
    addSourceSpecificMembership(sourceAddress: string, groupAddress: string, multicastInterface?: string): void;
    bind(port?: int, address?: string, callback?: Action): DgramSocket;
    bind(port: int, callback: Action): DgramSocket;
    bind(callback: Action): DgramSocket;
    bind(options: BindOptions, callback?: Action): DgramSocket;
    close(callback?: Action): DgramSocket;
    connect(port: int, address?: string, callback?: Action): void;
    connect(port: int, callback: Action): void;
    disconnect(): void;
    dropMembership(multicastAddress: string): void;
    dropSourceSpecificMembership(sourceAddress: string, groupAddress: string, multicastInterface?: string): void;
    getRecvBufferSize(): int;
    getSendBufferSize(): int;
    getSendQueueCount(): int;
    getSendQueueSize(): int;
    ref(): DgramSocket;
    remoteAddress(): AddressInfo;
    send(msg: byte[], port?: Nullable<System_Internal.Int32>, address?: string, callback?: Action<Exception, System_Internal.Int32>): void;
    send(msg: string, port?: Nullable<System_Internal.Int32>, address?: string, callback?: Action<Exception, System_Internal.Int32>): void;
    send(msg: byte[], port: int, callback: Action<Exception, System_Internal.Int32>): void;
    send(msg: string, port: int, callback: Action<Exception, System_Internal.Int32>): void;
    send(msg: byte[], callback: Action<Exception, System_Internal.Int32>): void;
    send(msg: string, callback: Action<Exception, System_Internal.Int32>): void;
    send(msg: byte[], offset: int, length: int, port?: Nullable<System_Internal.Int32>, address?: string, callback?: Action<Exception, System_Internal.Int32>): void;
    send(msg: byte[], offset: int, length: int, port: int, callback: Action<Exception, System_Internal.Int32>): void;
    send(msg: byte[], offset: int, length: int, callback: Action<Exception, System_Internal.Int32>): void;
    setBroadcast(flag: boolean): void;
    setMulticastInterface(multicastInterface: string): void;
    setMulticastLoopback(flag: boolean): boolean;
    setMulticastTTL(ttl: int): int;
    setRecvBufferSize(size: int): void;
    setSendBufferSize(size: int): void;
    setTTL(ttl: int): int;
    unref(): DgramSocket;
}

export const DgramSocket: {
    new(): DgramSocket;
};
```

### `RemoteInfo`

```ts
export interface RemoteInfo {
    address: string;
    family: string;
    port: int;
    size: int;
}

export const RemoteInfo: {
    new(): RemoteInfo;
};
```

### `SocketOptions`

```ts
export interface SocketOptions {
    ipv6Only: boolean;
    recvBufferSize: Nullable<System_Internal.Int32>;
    reuseAddr: boolean;
    reusePort: boolean;
    sendBufferSize: Nullable<System_Internal.Int32>;
    type: string;
}

export const SocketOptions: {
    new(): SocketOptions;
};
```
<!-- API:END -->
