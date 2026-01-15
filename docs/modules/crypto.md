# `crypto`

Import:

```ts
import { crypto } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, crypto } from "@tsonic/nodejs/index.js";

export function main(): void {
  const hash = crypto.createHash("sha256").update("hello").digest("hex");
  console.log(hash);
}
```

## API Reference

<!-- API:START -->
### `Certificate`

```ts
export declare const Certificate: {
  exportChallenge(spkac: byte[]): byte[];
  exportChallenge(spkac: string): byte[];
  exportPublicKey(spkac: byte[]): byte[];
  exportPublicKey(spkac: string): byte[];
  verifySpkac(spkac: byte[]): boolean;
  verifySpkac(spkac: string): boolean;
};
```

### `Cipher`

```ts
export interface Cipher extends Transform {
    dispose(): void;
    final(outputEncoding?: string): string;
    final(): byte[];
    getAuthTag(): byte[];
    setAAD(buffer: byte[]): void;
    setAuthTag(tagLength: int): void;
    update(data: string, inputEncoding?: string, outputEncoding?: string): string;
    update(data: byte[], outputEncoding?: string): string;
}

export const Cipher: {
    new(): Cipher;
};
```

### `crypto`

```ts
export declare const crypto: {
  createCipheriv(algorithm: string, key: byte[], iv: byte[]): Cipher;
  createCipheriv(algorithm: string, key: string, iv: string): Cipher;
  createDecipheriv(algorithm: string, key: byte[], iv: byte[]): Decipher;
  createDecipheriv(algorithm: string, key: string, iv: string): Decipher;
  createDiffieHellman(prime: byte[], generator: byte[]): DiffieHellman;
  createDiffieHellman(prime: byte[], generator?: int): DiffieHellman;
  createDiffieHellman(primeLength: int, generator?: int): DiffieHellman;
  createDiffieHellman(prime: string, primeEncoding: string, generator?: int): DiffieHellman;
  createDiffieHellman(prime: string, primeEncoding: string, generator: string, generatorEncoding: string): DiffieHellman;
  createECDH(curveName: string): ECDH;
  createHash(algorithm: string): Hash;
  createHmac(algorithm: string, key: byte[]): Hmac;
  createHmac(algorithm: string, key: string): Hmac;
  createPrivateKey(key: byte[]): KeyObject;
  createPrivateKey(key: string): KeyObject;
  createPublicKey(key: KeyObject): KeyObject;
  createPublicKey(key: byte[]): KeyObject;
  createPublicKey(key: string): KeyObject;
  createSecretKey(key: byte[]): KeyObject;
  createSecretKey(key: string, encoding?: string): KeyObject;
  createSign(algorithm: string): Sign;
  createVerify(algorithm: string): Verify;
  generateKey(type: string, options: unknown, callback: Action<Exception, KeyObject>): void;
  generateKey(type: string, options: unknown): KeyObject;
  generateKeyPair(type: string, options: unknown, callback: Action<Exception, unknown, unknown>): void;
  generateKeyPairSync(type: string, options?: unknown): ValueTuple<KeyObject, KeyObject>;
  getCiphers(): string[];
  getCurves(): string[];
  getDefaultCipherList(): string;
  getDiffieHellman(groupName: string): DiffieHellman;
  getFips(): boolean;
  getHashes(): string[];
  hash(algorithm: string, data: byte[], outputEncoding?: string): byte[];
  hkdf(digest: string, ikm: byte[], salt: byte[], info: byte[], keylen: int, callback: Action<Exception, byte[]>): void;
  hkdfSync(digest: string, ikm: byte[], salt: byte[], info: byte[], keylen: int): byte[];
  pbkdf2(password: string, salt: string, iterations: int, keylen: int, digest: string, callback: Action<Exception, byte[]>): void;
  pbkdf2Sync(password: byte[], salt: byte[], iterations: int, keylen: int, digest: string): byte[];
  pbkdf2Sync(password: string, salt: string, iterations: int, keylen: int, digest: string): byte[];
  privateDecrypt(key: unknown, buffer: byte[]): byte[];
  privateDecrypt(key: string, buffer: byte[]): byte[];
  privateEncrypt(key: unknown, buffer: byte[]): byte[];
  privateEncrypt(key: string, buffer: byte[]): byte[];
  publicDecrypt(key: unknown, buffer: byte[]): byte[];
  publicDecrypt(key: string, buffer: byte[]): byte[];
  publicEncrypt(key: unknown, buffer: byte[]): byte[];
  publicEncrypt(key: string, buffer: byte[]): byte[];
  randomBytes(size: int, callback: Action<Exception, byte[]>): void;
  randomBytes(size: int): byte[];
  randomFill(buffer: byte[], offset: int, size: int, callback: Action<Exception, byte[]>): void;
  randomFillSync(buffer: byte[], offset?: int, size?: Nullable<System_Internal.Int32>): byte[];
  randomInt(min: int, max: int): int;
  randomInt(max: int): int;
  randomUUID(): string;
  scrypt(password: string, salt: string, keylen: int, options: unknown, callback: Action<Exception, byte[]>): void;
  scryptSync(password: byte[], salt: byte[], keylen: int, options?: unknown): byte[];
  scryptSync(password: string, salt: string, keylen: int, options?: unknown): byte[];
  setDefaultEncoding(encoding: string): void;
  setFips(enabled: boolean): void;
  sign(algorithm: string, data: byte[], privateKey: KeyObject): byte[];
  sign(algorithm: string, data: byte[], privateKey: string): byte[];
  timingSafeEqual(a: byte[], b: byte[]): boolean;
  verify(algorithm: string, data: byte[], publicKey: KeyObject, signature: byte[]): boolean;
  verify(algorithm: string, data: byte[], publicKey: string, signature: byte[]): boolean;
};
```

### `Decipher`

```ts
export interface Decipher extends Transform {
    dispose(): void;
    final(outputEncoding?: string): string;
    final(): byte[];
    setAAD(buffer: byte[]): void;
    setAuthTag(buffer: byte[]): void;
    update(data: string, inputEncoding?: string, outputEncoding?: string): string;
    update(data: byte[], outputEncoding?: string): string;
}

export const Decipher: {
    new(): Decipher;
};
```

### `DiffieHellman`

```ts
export interface DiffieHellman {
    computeSecret(otherPublicKey: string, inputEncoding?: string, outputEncoding?: string): string;
    computeSecret(otherPublicKey: byte[], outputEncoding?: string): string;
    computeSecret(otherPublicKey: byte[]): byte[];
    dispose(): void;
    generateKeys(encoding?: string): string;
    generateKeys(): byte[];
    getGenerator(encoding?: string): string;
    getGenerator(): byte[];
    getPrime(encoding?: string): string;
    getPrime(): byte[];
    getPrivateKey(encoding?: string): string;
    getPrivateKey(): byte[];
    getPublicKey(encoding?: string): string;
    getPublicKey(): byte[];
    getVerifyError(): int;
    setPrivateKey(privateKey: string, encoding?: string): void;
    setPrivateKey(privateKey: byte[]): void;
    setPublicKey(publicKey: string, encoding?: string): void;
    setPublicKey(publicKey: byte[]): void;
}

export const DiffieHellman: {
    new(): DiffieHellman;
};
```

### `DSAPrivateKeyObject`

```ts
export interface DSAPrivateKeyObject extends KeyObject {
    readonly asymmetricKeyType: string | undefined;
    readonly symmetricKeySize: Nullable<System_Internal.Int32>;
    readonly type: string;
    dispose(): void;
    export(options?: unknown): unknown;
}

export const DSAPrivateKeyObject: {
    new(): DSAPrivateKeyObject;
};
```

### `DSAPublicKeyObject`

```ts
export interface DSAPublicKeyObject extends KeyObject {
    readonly asymmetricKeyType: string | undefined;
    readonly symmetricKeySize: Nullable<System_Internal.Int32>;
    readonly type: string;
    dispose(): void;
    export(options?: unknown): unknown;
}

export const DSAPublicKeyObject: {
    new(): DSAPublicKeyObject;
};
```

### `ECDH`

```ts
export interface ECDH {
    computeSecret(otherPublicKey: string, inputEncoding?: string, outputEncoding?: string): string;
    computeSecret(otherPublicKey: byte[], outputEncoding?: string): string;
    computeSecret(otherPublicKey: byte[]): byte[];
    dispose(): void;
    generateKeys(encoding?: string, format?: string): string;
    generateKeys(): byte[];
    getPrivateKey(encoding?: string): string;
    getPrivateKey(): byte[];
    getPublicKey(encoding?: string, format?: string): string;
    getPublicKey(): byte[];
    setPrivateKey(privateKey: string, encoding?: string): void;
    setPrivateKey(privateKey: byte[]): void;
    setPublicKey(publicKey: string, encoding?: string): void;
    setPublicKey(publicKey: byte[]): void;
}

export const ECDH: {
    new(): ECDH;
};
```

### `EdDSAPrivateKeyObject`

```ts
export interface EdDSAPrivateKeyObject extends KeyObject {
    readonly asymmetricKeyType: string | undefined;
    readonly symmetricKeySize: Nullable<System_Internal.Int32>;
    readonly type: string;
    dispose(): void;
    export(options?: unknown): unknown;
}

export const EdDSAPrivateKeyObject: {
    new(): EdDSAPrivateKeyObject;
};
```

### `EdDSAPublicKeyObject`

```ts
export interface EdDSAPublicKeyObject extends KeyObject {
    readonly asymmetricKeyType: string | undefined;
    readonly symmetricKeySize: Nullable<System_Internal.Int32>;
    readonly type: string;
    dispose(): void;
    export(options?: unknown): unknown;
}

export const EdDSAPublicKeyObject: {
    new(): EdDSAPublicKeyObject;
};
```

### `Hash`

```ts
export interface Hash extends Transform {
    copy(): Hash;
    digest(encoding: string): string;
    digest(): byte[];
    digest(outputLength: int): byte[];
    dispose(): void;
    update(data: string, inputEncoding?: string): Hash;
    update(data: byte[]): Hash;
}

export const Hash: {
    new(): Hash;
};
```

### `Hmac`

```ts
export interface Hmac extends Transform {
    digest(encoding?: string): string;
    digest(): byte[];
    dispose(): void;
    update(data: string, inputEncoding?: string): Hmac;
    update(data: byte[]): Hmac;
}

export const Hmac: {
    new(): Hmac;
};
```

### `KeyObject`

```ts
export interface KeyObject {
    readonly asymmetricKeyType: string | undefined;
    readonly symmetricKeySize: Nullable<System_Internal.Int32>;
    readonly type: string;
    dispose(): void;
    export(options?: unknown): unknown;
}

export const KeyObject: {
};
```

### `PrivateKeyObject`

```ts
export interface PrivateKeyObject extends KeyObject {
    readonly asymmetricKeyType: string | undefined;
    readonly symmetricKeySize: Nullable<System_Internal.Int32>;
    readonly type: string;
    dispose(): void;
    export(options?: unknown): unknown;
    export(format: string, type?: string, cipher?: string, passphrase?: string): string;
}

export const PrivateKeyObject: {
    new(): PrivateKeyObject;
};
```

### `PublicKeyObject`

```ts
export interface PublicKeyObject extends KeyObject {
    readonly asymmetricKeyType: string | undefined;
    readonly symmetricKeySize: Nullable<System_Internal.Int32>;
    readonly type: string;
    dispose(): void;
    export(options?: unknown): unknown;
    export(format: string, type?: string): string;
}

export const PublicKeyObject: {
    new(): PublicKeyObject;
};
```

### `SecretKeyObject`

```ts
export interface SecretKeyObject extends KeyObject {
    readonly asymmetricKeyType: string | undefined;
    readonly symmetricKeySize: Nullable<System_Internal.Int32>;
    readonly type: string;
    dispose(): void;
    export(options?: unknown): unknown;
    export(): byte[];
}

export const SecretKeyObject: {
    new(): SecretKeyObject;
};
```

### `Sign`

```ts
export interface Sign extends Transform {
    dispose(): void;
    sign(privateKey: string, outputEncoding?: string): string;
    sign(privateKey: string): byte[];
    sign(privateKey: unknown, outputEncoding?: string): string;
    sign(privateKey: unknown): byte[];
    update(data: string, inputEncoding?: string): Sign;
    update(data: byte[]): Sign;
}

export const Sign: {
    new(): Sign;
};
```

### `Verify`

```ts
export interface Verify extends Transform {
    dispose(): void;
    update(data: string, inputEncoding?: string): Verify;
    update(data: byte[]): Verify;
    verify(publicKey: string, signature: string, signatureEncoding?: string): boolean;
    verify(publicKey: string, signature: byte[]): boolean;
    verify(publicKey: unknown, signature: string, signatureEncoding?: string): boolean;
    verify(publicKey: unknown, signature: byte[]): boolean;
}

export const Verify: {
    new(): Verify;
};
```

### `X509CertificateExtensions`

```ts
export declare const X509CertificateExtensions: {
  parseCertificate(certificate: byte[]): X509CertificateInfo;
  parseCertificate(certificate: string): X509CertificateInfo;
};
```

### `X509CertificateInfo`

```ts
export interface X509CertificateInfo {
    readonly fingerprint: string;
    readonly fingerprint256: string;
    readonly fingerprint512: string;
    readonly issuer: string;
    readonly publicKey: byte[];
    readonly raw: byte[];
    readonly serialNumber: string;
    readonly subject: string;
    readonly validFrom: DateTime;
    readonly validTo: DateTime;
    checkEmail(email: string): string | undefined;
    checkHost(hostname: string): string | undefined;
    checkIP(ip: string): string | undefined;
    checkIssued(otherCert: X509CertificateInfo): string | undefined;
    toPEM(): string;
    toString(): string;
    verify(issuerCert: X509CertificateInfo): boolean;
}

export const X509CertificateInfo: {
    new(): X509CertificateInfo;
};
```
<!-- API:END -->
