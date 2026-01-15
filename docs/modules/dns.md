# `dns`

Import:

```ts
import { dns } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, dns } from "@tsonic/nodejs/index.js";

export function main(): void {
  dns.resolve("example.com", (err, records) => {
    console.log(err ?? records);
  });
}
```

## API Reference

<!-- API:START -->
### `AnyAaaaRecord`

```ts
export interface AnyAaaaRecord extends RecordWithTtl {
    readonly type: string;
}

export const AnyAaaaRecord: {
    new(): AnyAaaaRecord;
};
```

### `AnyARecord`

```ts
export interface AnyARecord extends RecordWithTtl {
    readonly type: string;
}

export const AnyARecord: {
    new(): AnyARecord;
};
```

### `AnyCaaRecord`

```ts
export interface AnyCaaRecord extends CaaRecord {
    readonly type: string;
}

export const AnyCaaRecord: {
    new(): AnyCaaRecord;
};
```

### `AnyCnameRecord`

```ts
export interface AnyCnameRecord {
    readonly type: string;
    value: string;
}

export const AnyCnameRecord: {
    new(): AnyCnameRecord;
};
```

### `AnyMxRecord`

```ts
export interface AnyMxRecord extends MxRecord {
    readonly type: string;
}

export const AnyMxRecord: {
    new(): AnyMxRecord;
};
```

### `AnyNaptrRecord`

```ts
export interface AnyNaptrRecord extends NaptrRecord {
    readonly type: string;
}

export const AnyNaptrRecord: {
    new(): AnyNaptrRecord;
};
```

### `AnyNsRecord`

```ts
export interface AnyNsRecord {
    readonly type: string;
    value: string;
}

export const AnyNsRecord: {
    new(): AnyNsRecord;
};
```

### `AnyPtrRecord`

```ts
export interface AnyPtrRecord {
    readonly type: string;
    value: string;
}

export const AnyPtrRecord: {
    new(): AnyPtrRecord;
};
```

### `AnyRecord`

```ts
export interface AnyRecord {
    readonly type: string;
}

export const AnyRecord: {
};
```

### `AnySoaRecord`

```ts
export interface AnySoaRecord extends SoaRecord {
    readonly type: string;
}

export const AnySoaRecord: {
    new(): AnySoaRecord;
};
```

### `AnySrvRecord`

```ts
export interface AnySrvRecord extends SrvRecord {
    readonly type: string;
}

export const AnySrvRecord: {
    new(): AnySrvRecord;
};
```

### `AnyTlsaRecord`

```ts
export interface AnyTlsaRecord extends TlsaRecord {
    readonly type: string;
}

export const AnyTlsaRecord: {
    new(): AnyTlsaRecord;
};
```

### `AnyTxtRecord`

```ts
export interface AnyTxtRecord {
    entries: string[];
    readonly type: string;
}

export const AnyTxtRecord: {
    new(): AnyTxtRecord;
};
```

### `CaaRecord`

```ts
export interface CaaRecord {
    get contactemail(): string | undefined;
    set contactemail(value: string);
    get contactphone(): string | undefined;
    set contactphone(value: string);
    critical: int;
    get iodef(): string | undefined;
    set iodef(value: string);
    get issue(): string | undefined;
    set issue(value: string);
    get issuewild(): string | undefined;
    set issuewild(value: string);
}

export const CaaRecord: {
    new(): CaaRecord;
};
```

### `dns`

```ts
export declare const dns: {
  readonly ADDRCONFIG: int;
  readonly V4MAPPED: int;
  readonly ALL: int;
  readonly NODATA: string;
  readonly FORMERR: string;
  readonly SERVFAIL: string;
  readonly NOTFOUND: string;
  readonly NOTIMP: string;
  readonly REFUSED: string;
  readonly BADQUERY: string;
  readonly BADNAME: string;
  readonly BADFAMILY: string;
  readonly BADRESP: string;
  readonly CONNREFUSED: string;
  readonly TIMEOUT: string;
  readonly EOF: string;
  readonly FILE: string;
  readonly NOMEM: string;
  readonly DESTRUCTION: string;
  readonly BADSTR: string;
  readonly BADFLAGS: string;
  readonly NONAME: string;
  readonly BADHINTS: string;
  readonly NOTINITIALIZED: string;
  readonly LOADIPHLPAPI: string;
  readonly ADDRGETNETWORKPARAMS: string;
  readonly CANCELLED: string;
  getDefaultResultOrder(): string;
  getServers(): string[];
  lookup(hostname: string, options: LookupOptions, callback: Action<Exception, LookupAddress[]>): void;
  lookup(hostname: string, options: LookupOptions, callback: Action<Exception, System_Internal.String, System_Internal.Int32>): void;
  lookup(hostname: string, callback: Action<Exception, System_Internal.String, System_Internal.Int32>): void;
  lookup(hostname: string, family: int, callback: Action<Exception, System_Internal.String, System_Internal.Int32>): void;
  lookupService(address: string, port: int, callback: Action<Exception, System_Internal.String, System_Internal.String>): void;
  resolve(hostname: string, callback: Action<Exception, string[]>): void;
  resolve(hostname: string, rrtype: string, callback: Action<Exception, unknown>): void;
  resolve4(hostname: string, options: ResolveOptions, callback: Action<Exception, unknown>): void;
  resolve4(hostname: string, callback: Action<Exception, string[]>): void;
  resolve6(hostname: string, options: ResolveOptions, callback: Action<Exception, unknown>): void;
  resolve6(hostname: string, callback: Action<Exception, string[]>): void;
  resolveAny(hostname: string, callback: Action<Exception, unknown[]>): void;
  resolveCaa(hostname: string, callback: Action<Exception, CaaRecord[]>): void;
  resolveCname(hostname: string, callback: Action<Exception, string[]>): void;
  resolveMx(hostname: string, callback: Action<Exception, MxRecord[]>): void;
  resolveNaptr(hostname: string, callback: Action<Exception, NaptrRecord[]>): void;
  resolveNs(hostname: string, callback: Action<Exception, string[]>): void;
  resolvePtr(hostname: string, callback: Action<Exception, string[]>): void;
  resolveSoa(hostname: string, callback: Action<Exception, SoaRecord>): void;
  resolveSrv(hostname: string, callback: Action<Exception, SrvRecord[]>): void;
  resolveTlsa(hostname: string, callback: Action<Exception, TlsaRecord[]>): void;
  resolveTxt(hostname: string, callback: Action<Exception, string[][]>): void;
  reverse(ip: string, callback: Action<Exception, string[]>): void;
  setDefaultResultOrder(order: string): void;
  setServers(servers: string[]): void;
};
```

### `LookupAddress`

```ts
export interface LookupAddress {
    address: string;
    family: int;
}

export const LookupAddress: {
    new(): LookupAddress;
};
```

### `LookupOptions`

```ts
export interface LookupOptions {
    all: Nullable<System_Internal.Boolean>;
    family: unknown;
    hints: Nullable<System_Internal.Int32>;
    get order(): string | undefined;
    set order(value: string);
    verbatim: Nullable<System_Internal.Boolean>;
}

export const LookupOptions: {
    new(): LookupOptions;
};
```

### `MxRecord`

```ts
export interface MxRecord {
    exchange: string;
    priority: int;
}

export const MxRecord: {
    new(): MxRecord;
};
```

### `NaptrRecord`

```ts
export interface NaptrRecord {
    flags: string;
    order: int;
    preference: int;
    regexp: string;
    replacement: string;
    service: string;
}

export const NaptrRecord: {
    new(): NaptrRecord;
};
```

### `RecordWithTtl`

```ts
export interface RecordWithTtl {
    address: string;
    ttl: int;
}

export const RecordWithTtl: {
    new(): RecordWithTtl;
};
```

### `ResolveOptions`

```ts
export interface ResolveOptions {
    ttl: boolean;
}

export const ResolveOptions: {
    new(): ResolveOptions;
};
```

### `Resolver`

```ts
export interface Resolver {
    cancel(): void;
    getServers(): string[];
    resolve(hostname: string, callback: Action<Exception, string[]>): void;
    resolve(hostname: string, rrtype: string, callback: Action<Exception, unknown>): void;
    resolve4(hostname: string, callback: Action<Exception, string[]>): void;
    resolve4(hostname: string, options: ResolveOptions, callback: Action<Exception, unknown>): void;
    resolve6(hostname: string, callback: Action<Exception, string[]>): void;
    resolve6(hostname: string, options: ResolveOptions, callback: Action<Exception, unknown>): void;
    resolveAny(hostname: string, callback: Action<Exception, unknown[]>): void;
    resolveCaa(hostname: string, callback: Action<Exception, CaaRecord[]>): void;
    resolveCname(hostname: string, callback: Action<Exception, string[]>): void;
    resolveMx(hostname: string, callback: Action<Exception, MxRecord[]>): void;
    resolveNaptr(hostname: string, callback: Action<Exception, NaptrRecord[]>): void;
    resolveNs(hostname: string, callback: Action<Exception, string[]>): void;
    resolvePtr(hostname: string, callback: Action<Exception, string[]>): void;
    resolveSoa(hostname: string, callback: Action<Exception, SoaRecord>): void;
    resolveSrv(hostname: string, callback: Action<Exception, SrvRecord[]>): void;
    resolveTlsa(hostname: string, callback: Action<Exception, TlsaRecord[]>): void;
    resolveTxt(hostname: string, callback: Action<Exception, string[][]>): void;
    reverse(ip: string, callback: Action<Exception, string[]>): void;
    setLocalAddress(ipv4?: string, ipv6?: string): void;
    setServers(servers: string[]): void;
}

export const Resolver: {
    new(): Resolver;
    new(options: ResolverOptions): Resolver;
};
```

### `ResolverOptions`

```ts
export interface ResolverOptions {
    maxTimeout: Nullable<System_Internal.Int32>;
    timeout: Nullable<System_Internal.Int32>;
    tries: Nullable<System_Internal.Int32>;
}

export const ResolverOptions: {
    new(): ResolverOptions;
};
```

### `SoaRecord`

```ts
export interface SoaRecord {
    expire: int;
    hostmaster: string;
    minttl: int;
    nsname: string;
    refresh: int;
    retry: int;
    serial: int;
}

export const SoaRecord: {
    new(): SoaRecord;
};
```

### `SrvRecord`

```ts
export interface SrvRecord {
    name: string;
    port: int;
    priority: int;
    weight: int;
}

export const SrvRecord: {
    new(): SrvRecord;
};
```

### `TlsaRecord`

```ts
export interface TlsaRecord {
    certUsage: int;
    data: byte[];
    match: int;
    selector: int;
}

export const TlsaRecord: {
    new(): TlsaRecord;
};
```
<!-- API:END -->
