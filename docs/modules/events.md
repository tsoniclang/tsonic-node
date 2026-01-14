# `events`

Import:

```ts
import { EventEmitter } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, EventEmitter } from "@tsonic/nodejs/index.js";

class MyEmitter extends EventEmitter {}

export function main(): void {
  const emitter = new MyEmitter();
  emitter.on("data", (chunk) => console.log(chunk));
  emitter.emit("data", "hello");
}
```

## API Reference

<!-- API:START -->
### `EventEmitter`

```ts
export interface EventEmitter {
    addListener(eventName: string, listener: Function): EventEmitter;
    emit(eventName: string, ...args: unknown[]): boolean;
    eventNames(): string[];
    getMaxListeners(): int;
    listenerCount(eventName: string): int;
    listeners(eventName: string): Function[];
    off(eventName: string, listener: Function): EventEmitter;
    on(eventName: string, listener: Function): EventEmitter;
    once(eventName: string, listener: Function): EventEmitter;
    prependListener(eventName: string, listener: Function): EventEmitter;
    prependOnceListener(eventName: string, listener: Function): EventEmitter;
    rawListeners(eventName: string): Function[];
    removeAllListeners(eventName?: string): EventEmitter;
    removeListener(eventName: string, listener: Function): EventEmitter;
    setMaxListeners(n: int): EventEmitter;
}

export const EventEmitter: {
    new(): EventEmitter;
    defaultMaxListeners: int;
    once(emitter: EventEmitter, eventName: string): Task<(unknown | undefined)[]>;
};
```
<!-- API:END -->
