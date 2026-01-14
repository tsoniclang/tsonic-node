# `tls`

Import:

```ts
import { tls } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, tls } from "@tsonic/nodejs/index.js";

export function main(): void {
  console.log(tls.getCiphers().slice(0, 5));
}
```

## API Reference

<!-- API:START -->
### `CipherNameAndProtocol`

```ts
export interface CipherNameAndProtocol {
    name: string;
    standardName: string;
    version: string;
}

export const CipherNameAndProtocol: {
    new(): CipherNameAndProtocol;
};
```

### `CommonConnectionOptions`

```ts
export interface CommonConnectionOptions {
    get alpnProtocols(): string[] | undefined;
    set alpnProtocols(value: string[]);
    enableTrace: Nullable<System_Internal.Boolean>;
    rejectUnauthorized: Nullable<System_Internal.Boolean>;
    requestCert: Nullable<System_Internal.Boolean>;
    get secureContext(): SecureContext | undefined;
    set secureContext(value: SecureContext);
}

export const CommonConnectionOptions: {
    new(): CommonConnectionOptions;
};
```

### `ConnectionOptions`

```ts
export interface ConnectionOptions extends CommonConnectionOptions {
    get ca(): unknown | undefined;
    set ca(value: unknown);
    get cert(): unknown | undefined;
    set cert(value: unknown);
    host: string;
    get key(): unknown | undefined;
    set key(value: unknown);
    get passphrase(): string | undefined;
    set passphrase(value: string);
    port: Nullable<System_Internal.Int32>;
    get servername(): string | undefined;
    set servername(value: string);
    timeout: Nullable<System_Internal.Int32>;
}

export const ConnectionOptions: {
    new(): ConnectionOptions;
};
```

### `DetailedPeerCertificate`

```ts
export interface DetailedPeerCertificate extends PeerCertificate {
    get issuerCertificate(): DetailedPeerCertificate | undefined;
    set issuerCertificate(value: DetailedPeerCertificate);
}

export const DetailedPeerCertificate: {
    new(): DetailedPeerCertificate;
};
```

### `EphemeralKeyInfo`

```ts
export interface EphemeralKeyInfo {
    name: string;
    size: int;
    type_: string;
}

export const EphemeralKeyInfo: {
    new(): EphemeralKeyInfo;
};
```

### `PeerCertificate`

```ts
export interface PeerCertificate {
    ca: boolean;
    get ext_key_usage(): string[] | undefined;
    set ext_key_usage(value: string[]);
    fingerprint: string;
    fingerprint256: string;
    fingerprint512: string;
    issuer: TLSCertificateInfo;
    raw: byte[];
    serialNumber: string;
    subject: TLSCertificateInfo;
    get subjectaltname(): string | undefined;
    set subjectaltname(value: string);
    valid_from: string;
    valid_to: string;
}

export const PeerCertificate: {
    new(): PeerCertificate;
};
```

### `SecureContext`

```ts
export interface SecureContext {
    readonly caCertificates: X509Certificate2Collection | undefined;
    readonly certificate: X509Certificate2 | undefined;
    get context(): unknown | undefined;
    set context(value: unknown);
    readonly protocols: SslProtocols;
    loadCACertificates(ca: unknown): void;
    loadCertificate(cert: unknown, key: unknown, passphrase: string): void;
    setProtocols(minVersion: string, maxVersion: string): void;
}

export const SecureContext: {
    new(): SecureContext;
};
```

### `SecureContextOptions`

```ts
export interface SecureContextOptions {
    get ca(): unknown | undefined;
    set ca(value: unknown);
    get cert(): unknown | undefined;
    set cert(value: unknown);
    get ciphers(): string | undefined;
    set ciphers(value: string);
    get key(): unknown | undefined;
    set key(value: unknown);
    get maxVersion(): string | undefined;
    set maxVersion(value: string);
    get minVersion(): string | undefined;
    set minVersion(value: string);
    get passphrase(): string | undefined;
    set passphrase(value: string);
    get pfx(): unknown | undefined;
    set pfx(value: unknown);
}

export const SecureContextOptions: {
    new(): SecureContextOptions;
};
```

### `tls`

```ts
export declare const tls: {
  readonly CLIENT_RENEG_LIMIT: int;
  readonly CLIENT_RENEG_WINDOW: int;
  DEFAULT_ECDH_CURVE: string;
  DEFAULT_MAX_VERSION: string;
  DEFAULT_MIN_VERSION: string;
  DEFAULT_CIPHERS: string;
  readonly rootCertificates: string[];
  checkServerIdentity(hostname: string, cert: PeerCertificate): Exception | undefined;
  connect(options: ConnectionOptions, secureConnectListener?: Action): TLSSocket;
  connect(port: int, options?: ConnectionOptions, secureConnectListener?: Action): TLSSocket;
  connect(port: int, host?: string, options?: ConnectionOptions, secureConnectListener?: Action): TLSSocket;
  createSecureContext(options?: SecureContextOptions): SecureContext;
  createServer(options: TlsOptions, secureConnectionListener?: Action<TLSSocket>): TLSServer;
  createServer(secureConnectionListener?: Action<TLSSocket>): TLSServer;
  getCACertificates(type_?: string): string[];
  getCiphers(): string[];
  setDefaultCACertificates(certs: string[]): void;
};
```

### `TLSCertificateInfo`

```ts
export interface TLSCertificateInfo {
    C: string;
    CN: string;
    L: string;
    O: string;
    OU: string;
    ST: string;
}

export const TLSCertificateInfo: {
    new(): TLSCertificateInfo;
};
```

### `TlsOptions`

```ts
export interface TlsOptions extends CommonConnectionOptions {
    allowHalfOpen: Nullable<System_Internal.Boolean>;
    get ca(): unknown | undefined;
    set ca(value: unknown);
    get cert(): unknown | undefined;
    set cert(value: unknown);
    handshakeTimeout: Nullable<System_Internal.Int32>;
    get key(): unknown | undefined;
    set key(value: unknown);
    get passphrase(): string | undefined;
    set passphrase(value: string);
    pauseOnConnect: Nullable<System_Internal.Boolean>;
    sessionTimeout: Nullable<System_Internal.Int32>;
}

export const TlsOptions: {
    new(): TlsOptions;
};
```

### `TLSServer`

```ts
export interface TLSServer extends Server {
    addContext(hostname: string, context: unknown): void;
    getTicketKeys(): byte[];
    setSecureContext(options: SecureContextOptions): void;
    setTicketKeys(keys: byte[]): void;
}

export const TLSServer: {
    new(): TLSServer;
    new(secureConnectionListener: Action<TLSSocket>): TLSServer;
    new(options: TlsOptions, secureConnectionListener: Action<TLSSocket>): TLSServer;
};
```

### `TLSSocket`

```ts
export interface TLSSocket extends Socket {
    readonly alpnProtocol: string | undefined;
    readonly authorizationError: Exception | undefined;
    readonly authorized: boolean;
    readonly encrypted: boolean;
    disableRenegotiation(): void;
    enableTrace(): void;
    exportKeyingMaterial(length: int, label: string, context: byte[]): byte[];
    getCertificate(): PeerCertificate | undefined;
    getCipher(): CipherNameAndProtocol;
    getEphemeralKeyInfo(): EphemeralKeyInfo | undefined;
    getFinished(): byte[] | undefined;
    getPeerCertificate(detailed?: boolean): PeerCertificate | undefined;
    getPeerFinished(): byte[] | undefined;
    getPeerX509Certificate(): unknown | undefined;
    getProtocol(): string | undefined;
    getSession(): byte[] | undefined;
    getSharedSigalgs(): string[];
    getTLSTicket(): byte[] | undefined;
    getX509Certificate(): unknown | undefined;
    isSessionReused(): boolean;
    renegotiate(options: unknown, callback: Action<Exception>): boolean;
    setKeyCert(context: unknown): void;
    setMaxSendFragment(size: int): boolean;
    write(data: byte[], callback?: Action<Exception>): boolean;
    write(data: string, encoding?: string, callback?: Action<Exception>): boolean;
    write(data: byte[], callback?: Action<Exception>): boolean;
    write(data: string, encoding?: string, callback?: Action<Exception>): boolean;
}

export const TLSSocket: {
    new(socket: Socket, options: TLSSocketOptions): TLSSocket;
};
```

### `TLSSocketOptions`

```ts
export interface TLSSocketOptions extends CommonConnectionOptions {
    get ca(): unknown | undefined;
    set ca(value: unknown);
    get cert(): unknown | undefined;
    set cert(value: unknown);
    isServer: Nullable<System_Internal.Boolean>;
    get key(): unknown | undefined;
    set key(value: unknown);
    get passphrase(): string | undefined;
    set passphrase(value: string);
    get server(): Server | undefined;
    set server(value: Server);
    get servername(): string | undefined;
    set servername(value: string);
}

export const TLSSocketOptions: {
    new(): TLSSocketOptions;
};
```
<!-- API:END -->
