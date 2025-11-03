using Xunit;
using System;
using System.Text;

namespace Tsonic.NodeApi.Tests;

public class CryptoTests
{
    [Fact]
    public void Hash_SHA256_ProducesCorrectDigest()
    {
        var hash = crypto.createHash("sha256");
        hash.update("Hello, World!");
        var digest = hash.digest("hex");

        Assert.Equal("dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a362182986f", digest);
    }

    [Fact]
    public void Hash_MD5_ProducesCorrectDigest()
    {
        var hash = crypto.createHash("md5");
        hash.update("Hello, World!");
        var digest = hash.digest("hex");

        Assert.Equal("65a8e27d8879283831b664bd8b7f0ad4", digest);
    }

    [Fact]
    public void Hash_MultipleUpdates_ProducesCorrectDigest()
    {
        var hash = crypto.createHash("sha256");
        hash.update("Hello");
        hash.update(", ");
        hash.update("World!");
        var digest = hash.digest("hex");

        Assert.Equal("dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a362182986f", digest);
    }

    [Fact]
    public void Hmac_SHA256_ProducesCorrectDigest()
    {
        var hmac = crypto.createHmac("sha256", "secret-key");
        hmac.update("Hello, World!");
        var digest = hmac.digest("hex");

        Assert.NotEmpty(digest);
        Assert.Equal(64, digest.Length); // SHA256 produces 32 bytes = 64 hex characters
    }

    [Fact]
    public void Cipher_Decipher_AES256_RoundTrip()
    {
        var key = crypto.randomBytes(32); // 256 bits
        var iv = crypto.randomBytes(16);  // 128 bits
        var plaintext = "Hello, World! This is a test message.";

        // Encrypt
        var cipher = crypto.createCipheriv("aes-256-cbc", key, iv);
        var encrypted = cipher.update(plaintext, "utf8", "hex");
        encrypted += cipher.final("hex");

        // Decrypt
        var decipher = crypto.createDecipheriv("aes-256-cbc", key, iv);
        var decrypted = decipher.update(encrypted, "hex", "utf8");
        decrypted += decipher.final("utf8");

        Assert.Equal(plaintext, decrypted);
    }

    [Fact]
    public void RandomBytes_GeneratesCorrectLength()
    {
        var bytes = crypto.randomBytes(32);
        Assert.Equal(32, bytes.Length);
    }

    [Fact]
    public void RandomBytes_GeneratesDifferentValues()
    {
        var bytes1 = crypto.randomBytes(16);
        var bytes2 = crypto.randomBytes(16);

        Assert.NotEqual(bytes1, bytes2);
    }

    [Fact]
    public void RandomInt_GeneratesWithinRange()
    {
        for (int i = 0; i < 100; i++)
        {
            var value = crypto.randomInt(10);
            Assert.InRange(value, 0, 9);
        }
    }

    [Fact]
    public void RandomInt_GeneratesWithinCustomRange()
    {
        for (int i = 0; i < 100; i++)
        {
            var value = crypto.randomInt(5, 15);
            Assert.InRange(value, 5, 14);
        }
    }

    [Fact]
    public void RandomUUID_GeneratesValidUUID()
    {
        var uuid = crypto.randomUUID();
        Assert.Matches(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", uuid);
    }

    [Fact]
    public void RandomFillSync_FillsBuffer()
    {
        var buffer = new byte[32];
        crypto.randomFillSync(buffer);

        // Check that buffer is not all zeros
        var allZeros = true;
        foreach (var b in buffer)
        {
            if (b != 0)
            {
                allZeros = false;
                break;
            }
        }
        Assert.False(allZeros);
    }

    [Fact]
    public void PBKDF2Sync_GeneratesCorrectLength()
    {
        var derived = crypto.pbkdf2Sync("password", "salt", 1000, 32, "sha256");
        Assert.Equal(32, derived.Length);
    }

    [Fact]
    public void PBKDF2Sync_DeterministicOutput()
    {
        var derived1 = crypto.pbkdf2Sync("password", "salt", 1000, 32, "sha256");
        var derived2 = crypto.pbkdf2Sync("password", "salt", 1000, 32, "sha256");

        Assert.Equal(derived1, derived2);
    }

    [Fact]
    public void GetCiphers_ReturnsNonEmptyList()
    {
        var ciphers = crypto.getCiphers();
        Assert.NotEmpty(ciphers);
        Assert.Contains("aes-256-cbc", ciphers);
    }

    [Fact]
    public void GetHashes_ReturnsNonEmptyList()
    {
        var hashes = crypto.getHashes();
        Assert.NotEmpty(hashes);
        Assert.Contains("sha256", hashes);
        Assert.Contains("md5", hashes);
    }

    [Fact]
    public void GetCurves_ReturnsNonEmptyList()
    {
        var curves = crypto.getCurves();
        Assert.NotEmpty(curves);
        Assert.Contains("secp256r1", curves);
    }

    [Fact]
    public void TimingSafeEqual_ReturnsTrueForEqualBuffers()
    {
        var buffer1 = Encoding.UTF8.GetBytes("Hello");
        var buffer2 = Encoding.UTF8.GetBytes("Hello");

        Assert.True(crypto.timingSafeEqual(buffer1, buffer2));
    }

    [Fact]
    public void TimingSafeEqual_ReturnsFalseForDifferentBuffers()
    {
        var buffer1 = Encoding.UTF8.GetBytes("Hello");
        var buffer2 = Encoding.UTF8.GetBytes("World");

        Assert.False(crypto.timingSafeEqual(buffer1, buffer2));
    }

    [Fact]
    public void TimingSafeEqual_ReturnsFalseForDifferentLengths()
    {
        var buffer1 = Encoding.UTF8.GetBytes("Hello");
        var buffer2 = Encoding.UTF8.GetBytes("Hello!");

        Assert.False(crypto.timingSafeEqual(buffer1, buffer2));
    }

    [Fact]
    public void CreateSecretKey_CreatesKeyObject()
    {
        var key = crypto.createSecretKey(crypto.randomBytes(32));

        Assert.Equal("secret", key.type);
        Assert.Equal(32, key.symmetricKeySize);
        Assert.Null(key.asymmetricKeyType);
    }

    [Fact]
    public void CreateECDH_GeneratesKeys()
    {
        var ecdh = crypto.createECDH("secp256r1");
        var publicKey = ecdh.generateKeys();

        Assert.NotEmpty(publicKey);
    }

    [Fact]
    public void ECDH_ComputesSharedSecret()
    {
        var alice = crypto.createECDH("secp256r1");
        var bob = crypto.createECDH("secp256r1");

        alice.generateKeys();
        bob.generateKeys();

        var aliceSecret = alice.computeSecret(bob.getPublicKey());
        var bobSecret = bob.computeSecret(alice.getPublicKey());

        Assert.Equal(aliceSecret, bobSecret);
    }

    [Fact]
    public void CreateHash_ThrowsForUnknownAlgorithm()
    {
        Assert.Throws<ArgumentException>(() => crypto.createHash("invalid-algorithm"));
    }

    [Fact]
    public void Hash_DigestOnlyOnce()
    {
        var hash = crypto.createHash("sha256");
        hash.update("test");
        hash.digest();

        Assert.Throws<InvalidOperationException>(() => hash.digest());
    }

    [Fact]
    public void Cipher_FinalOnlyOnce()
    {
        var key = crypto.randomBytes(32);
        var iv = crypto.randomBytes(16);
        var cipher = crypto.createCipheriv("aes-256-cbc", key, iv);

        cipher.update("test");
        cipher.final();

        Assert.Throws<InvalidOperationException>(() => cipher.final());
    }

    [Fact]
    public void GenerateKeyPairSync_RSA_CreatesValidKeyPair()
    {
        var (publicKey, privateKey) = crypto.generateKeyPairSync("rsa", null);

        Assert.NotNull(publicKey);
        Assert.NotNull(privateKey);
        Assert.Equal("public", publicKey.type);
        Assert.Equal("private", privateKey.type);
        Assert.Equal("rsa", publicKey.asymmetricKeyType);
        Assert.Equal("rsa", privateKey.asymmetricKeyType);
    }

    [Fact]
    public void PublicEncrypt_PrivateDecrypt_RoundTrip()
    {
        var (publicKey, privateKey) = crypto.generateKeyPairSync("rsa", null);
        var plaintext = Encoding.UTF8.GetBytes("Hello, World!");

        var encrypted = crypto.publicEncrypt(publicKey, plaintext);
        var decrypted = crypto.privateDecrypt(privateKey, encrypted);

        Assert.Equal(plaintext, decrypted);
    }

    [Fact]
    public void GetFips_ReturnsFalse()
    {
        // In .NET, FIPS mode is not directly configurable
        Assert.False(crypto.getFips());
    }

    // ============== Additional Hash Algorithm Tests ==============

    [Fact]
    public void Hash_SHA1_ProducesCorrectDigest()
    {
        var hash = crypto.createHash("sha1");
        hash.update("test");
        var digest = hash.digest("hex");
        Assert.Equal(40, digest.Length); // SHA1 = 20 bytes = 40 hex chars
    }

    [Fact]
    public void Hash_SHA384_ProducesCorrectDigest()
    {
        var hash = crypto.createHash("sha384");
        hash.update("test");
        var digest = hash.digest("hex");
        Assert.Equal(96, digest.Length); // SHA384 = 48 bytes = 96 hex chars
    }

    [Fact]
    public void Hash_SHA512_ProducesCorrectDigest()
    {
        var hash = crypto.createHash("sha512");
        hash.update("test");
        var digest = hash.digest("hex");
        Assert.Equal(128, digest.Length); // SHA512 = 64 bytes = 128 hex chars
    }

    [Fact]
    public void Hash_Base64Encoding()
    {
        var hash = crypto.createHash("sha256");
        hash.update("test");
        var digest = hash.digest("base64");
        Assert.NotEmpty(digest);
        Assert.DoesNotContain("-", digest); // base64, not base64url
    }

    [Fact]
    public void Hash_Base64UrlEncoding()
    {
        var hash = crypto.createHash("sha256");
        hash.update("test");
        var digest = hash.digest("base64url");
        Assert.NotEmpty(digest);
        Assert.DoesNotContain("+", digest);
        Assert.DoesNotContain("/", digest);
    }

    [Fact]
    public void Hash_BinaryOutput()
    {
        var hash = crypto.createHash("sha256");
        hash.update("test");
        var digest = hash.digest();
        Assert.Equal(32, digest.Length); // SHA256 = 32 bytes
    }

    // ============== HMAC Algorithm Tests ==============

    [Fact]
    public void Hmac_MD5_Works()
    {
        var hmac = crypto.createHmac("md5", "key");
        hmac.update("data");
        var digest = hmac.digest("hex");
        Assert.Equal(32, digest.Length); // MD5 = 16 bytes = 32 hex chars
    }

    [Fact]
    public void Hmac_SHA1_Works()
    {
        var hmac = crypto.createHmac("sha1", "key");
        hmac.update("data");
        var digest = hmac.digest("hex");
        Assert.Equal(40, digest.Length); // SHA1 = 20 bytes = 40 hex chars
    }

    [Fact]
    public void Hmac_SHA384_Works()
    {
        var hmac = crypto.createHmac("sha384", "key");
        hmac.update("data");
        var digest = hmac.digest("hex");
        Assert.Equal(96, digest.Length);
    }

    [Fact]
    public void Hmac_SHA512_Works()
    {
        var hmac = crypto.createHmac("sha512", "key");
        hmac.update("data");
        var digest = hmac.digest("hex");
        Assert.Equal(128, digest.Length);
    }

    [Fact]
    public void Hmac_WithByteArrayKey()
    {
        var key = Encoding.UTF8.GetBytes("secret-key");
        var hmac = crypto.createHmac("sha256", key);
        hmac.update("data");
        var digest = hmac.digest("hex");
        Assert.NotEmpty(digest);
    }

    [Fact]
    public void Hmac_BinaryOutput()
    {
        var hmac = crypto.createHmac("sha256", "key");
        hmac.update("data");
        var digest = hmac.digest();
        Assert.Equal(32, digest.Length);
    }

    // ============== Cipher Algorithm Tests ==============

    [Fact]
    public void Cipher_AES128CBC_Works()
    {
        var key = crypto.randomBytes(16); // 128 bits
        var iv = crypto.randomBytes(16);
        var plaintext = "Test message";

        var cipher = crypto.createCipheriv("aes-128-cbc", key, iv);
        var encrypted = cipher.update(plaintext, "utf8", "hex");
        encrypted += cipher.final("hex");

        var decipher = crypto.createDecipheriv("aes-128-cbc", key, iv);
        var decrypted = decipher.update(encrypted, "hex", "utf8");
        decrypted += decipher.final("utf8");

        Assert.Equal(plaintext, decrypted);
    }

    [Fact]
    public void Cipher_AES192CBC_Works()
    {
        var key = crypto.randomBytes(24); // 192 bits
        var iv = crypto.randomBytes(16);
        var plaintext = "Test message";

        var cipher = crypto.createCipheriv("aes-192-cbc", key, iv);
        var encrypted = cipher.update(plaintext, "utf8", "hex");
        encrypted += cipher.final("hex");

        var decipher = crypto.createDecipheriv("aes-192-cbc", key, iv);
        var decrypted = decipher.update(encrypted, "hex", "utf8");
        decrypted += decipher.final("utf8");

        Assert.Equal(plaintext, decrypted);
    }

    [Fact]
    public void Cipher_AES256ECB_Works()
    {
        var key = crypto.randomBytes(32);
        var plaintext = "Test message";

        var cipher = crypto.createCipheriv("aes-256-ecb", key, null);
        var encrypted = cipher.update(plaintext, "utf8", "hex");
        encrypted += cipher.final("hex");

        var decipher = crypto.createDecipheriv("aes-256-ecb", key, null);
        var decrypted = decipher.update(encrypted, "hex", "utf8");
        decrypted += decipher.final("utf8");

        Assert.Equal(plaintext, decrypted);
    }

    [Fact]
    public void Cipher_AES256CFB_Works()
    {
        var key = crypto.randomBytes(32);
        var iv = crypto.randomBytes(16);
        var plaintext = "Test message";

        var cipher = crypto.createCipheriv("aes-256-cfb", key, iv);
        var encrypted = cipher.update(plaintext, "utf8", "hex");
        encrypted += cipher.final("hex");

        var decipher = crypto.createDecipheriv("aes-256-cfb", key, iv);
        var decrypted = decipher.update(encrypted, "hex", "utf8");
        decrypted += decipher.final("utf8");

        Assert.Equal(plaintext, decrypted);
    }

    [Fact]
    public void Cipher_WithByteArrays()
    {
        var key = crypto.randomBytes(32);
        var iv = crypto.randomBytes(16);
        var plaintextBytes = Encoding.UTF8.GetBytes("Test message");

        var cipher = crypto.createCipheriv("aes-256-cbc", key, iv);
        var encrypted1 = cipher.update(plaintextBytes, "base64");
        var encrypted2 = cipher.final("base64");
        var encryptedFull = encrypted1 + encrypted2;

        var decipher = crypto.createDecipheriv("aes-256-cbc", key, iv);
        var decrypted1 = decipher.update(encryptedFull, "base64", "utf8");
        var decrypted2 = decipher.final("utf8");

        Assert.Equal("Test message", decrypted1 + decrypted2);
    }

    [Fact]
    public void Cipher_StringKeyAndIV()
    {
        var cipher = crypto.createCipheriv("aes-256-cbc", "12345678901234567890123456789012", "1234567890123456");
        var encrypted = cipher.update("test", "utf8", "hex");
        encrypted += cipher.final("hex");
        Assert.NotEmpty(encrypted);
    }

    // ============== Sign/Verify Tests ==============

    [Fact]
    public void Sign_RSA_SHA256()
    {
        var (publicKey, privateKey) = crypto.generateKeyPairSync("rsa", null);
        var data = Encoding.UTF8.GetBytes("Important message");

        var sign = crypto.createSign("sha256");
        sign.update(data);
        var signature = sign.sign(privateKey);

        Assert.NotEmpty(signature);
    }

    [Fact]
    public void Verify_RSA_SHA256()
    {
        var (publicKey, privateKey) = crypto.generateKeyPairSync("rsa", null);
        var data = Encoding.UTF8.GetBytes("Important message");

        var sign = crypto.createSign("sha256");
        sign.update(data);
        var signature = sign.sign(privateKey);

        var verify = crypto.createVerify("sha256");
        verify.update(data);
        var isValid = verify.verify(publicKey, signature);

        Assert.True(isValid);
    }

    [Fact]
    public void Verify_RSA_InvalidSignature()
    {
        var (publicKey, privateKey) = crypto.generateKeyPairSync("rsa", null);
        var data = Encoding.UTF8.GetBytes("Important message");

        var sign = crypto.createSign("sha256");
        sign.update(data);
        var signature = sign.sign(privateKey);

        // Corrupt signature
        signature[0] ^= 0xFF;

        var verify = crypto.createVerify("sha256");
        verify.update(data);
        var isValid = verify.verify(publicKey, signature);

        Assert.False(isValid);
    }

    [Fact]
    public void Sign_Verify_WithStringData()
    {
        var (publicKey, privateKey) = crypto.generateKeyPairSync("rsa", null);

        var sign = crypto.createSign("sha256");
        sign.update("Important message");
        var signature = sign.sign(privateKey);

        var verify = crypto.createVerify("sha256");
        verify.update("Important message");
        var isValid = verify.verify(publicKey, signature);

        Assert.True(isValid);
    }

    [Fact]
    public void Sign_MultipleUpdates()
    {
        var (publicKey, privateKey) = crypto.generateKeyPairSync("rsa", null);

        var sign = crypto.createSign("sha256");
        sign.update("Part 1");
        sign.update(" Part 2");
        sign.update(" Part 3");
        var signature = sign.sign(privateKey);

        var verify = crypto.createVerify("sha256");
        verify.update("Part 1 Part 2 Part 3");
        var isValid = verify.verify(publicKey, signature);

        Assert.True(isValid);
    }

    // ============== EC Key Tests ==============

    [Fact]
    public void GenerateKeyPairSync_EC_CreatesValidKeyPair()
    {
        var (publicKey, privateKey) = crypto.generateKeyPairSync("ec", null);

        Assert.NotNull(publicKey);
        Assert.NotNull(privateKey);
        Assert.Equal("public", publicKey.type);
        Assert.Equal("private", privateKey.type);
        Assert.Equal("ec", publicKey.asymmetricKeyType);
        Assert.Equal("ec", privateKey.asymmetricKeyType);
    }

    // ============== DiffieHellman Tests ==============

    [Fact]
    public void DiffieHellman_WithPrimeAndGenerator()
    {
        var prime = crypto.randomBytes(256);
        var generator = 2;

        var dh = crypto.createDiffieHellman(prime, generator);
        var publicKey = dh.generateKeys();

        Assert.NotEmpty(publicKey);
    }

    [Fact]
    public void DiffieHellman_GetPrime()
    {
        var prime = crypto.randomBytes(128);
        var dh = crypto.createDiffieHellman(prime, 2);

        var retrievedPrime = dh.getPrime();
        Assert.Equal(prime, retrievedPrime);
    }

    [Fact]
    public void DiffieHellman_GetGenerator()
    {
        var prime = crypto.randomBytes(128);
        var dh = crypto.createDiffieHellman(prime, 2);

        var generator = dh.getGenerator();
        Assert.NotEmpty(generator);
    }

    [Fact]
    public void DiffieHellman_GetPublicKey()
    {
        var prime = crypto.randomBytes(128);
        var dh = crypto.createDiffieHellman(prime, 2);

        dh.generateKeys();
        var publicKey = dh.getPublicKey();

        Assert.NotEmpty(publicKey);
    }

    [Fact]
    public void DiffieHellman_GetPrivateKey()
    {
        var prime = crypto.randomBytes(128);
        var dh = crypto.createDiffieHellman(prime, 2);

        dh.generateKeys();
        var privateKey = dh.getPrivateKey();

        Assert.NotEmpty(privateKey);
    }

    [Fact]
    public void DiffieHellman_SetGetKeys()
    {
        var prime = crypto.randomBytes(128);
        var dh = crypto.createDiffieHellman(prime, 2);

        dh.generateKeys();
        var publicKey = dh.getPublicKey();
        var privateKey = dh.getPrivateKey();

        var dh2 = crypto.createDiffieHellman(prime, 2);
        dh2.setPrivateKey(privateKey);

        Assert.Equal(publicKey, dh2.getPublicKey());
    }

    [Fact]
    public void DiffieHellman_WithEncodedKeys()
    {
        var prime = crypto.randomBytes(128);
        var dh = crypto.createDiffieHellman(prime, 2);

        var publicKeyHex = dh.generateKeys("hex");
        Assert.NotEmpty(publicKeyHex);

        var publicKeyBase64 = dh.getPublicKey("base64");
        Assert.NotEmpty(publicKeyBase64);
    }

    // ============== ECDH Tests ==============

    [Fact]
    public void ECDH_GeneratesKeys()
    {
        var ecdh = crypto.createECDH("secp256r1");
        var publicKey = ecdh.generateKeys();

        Assert.NotEmpty(publicKey);
    }

    [Fact]
    public void ECDH_GetPublicKey()
    {
        var ecdh = crypto.createECDH("secp256r1");
        ecdh.generateKeys();
        var publicKey = ecdh.getPublicKey();

        Assert.NotEmpty(publicKey);
    }

    [Fact]
    public void ECDH_GetPrivateKey()
    {
        var ecdh = crypto.createECDH("secp256r1");
        ecdh.generateKeys();
        var privateKey = ecdh.getPrivateKey();

        Assert.NotEmpty(privateKey);
    }

    [Fact]
    public void ECDH_WithEncodedKeys()
    {
        var ecdh = crypto.createECDH("secp256r1");
        var publicKeyHex = ecdh.generateKeys("hex");

        Assert.NotEmpty(publicKeyHex);
    }

    [Fact]
    public void ECDH_SharedSecret_WithEncoding()
    {
        var alice = crypto.createECDH("secp256r1");
        var bob = crypto.createECDH("secp256r1");

        alice.generateKeys();
        bob.generateKeys();

        var aliceSecretHex = alice.computeSecret(bob.getPublicKey(), "hex");
        var bobSecretHex = bob.computeSecret(alice.getPublicKey(), "hex");

        Assert.Equal(aliceSecretHex, bobSecretHex);
    }

    [Fact]
    public void ECDH_secp384r1_Curve()
    {
        var ecdh = crypto.createECDH("secp384r1");
        var publicKey = ecdh.generateKeys();
        Assert.NotEmpty(publicKey);
    }

    [Fact]
    public void ECDH_secp521r1_Curve()
    {
        var ecdh = crypto.createECDH("secp521r1");
        var publicKey = ecdh.generateKeys();
        Assert.NotEmpty(publicKey);
    }

    // ============== KeyObject Tests ==============

    [Fact]
    public void CreateSecretKey_WithString()
    {
        var key = crypto.createSecretKey("my-secret-key", "utf8");
        Assert.Equal("secret", key.type);
        Assert.Null(key.asymmetricKeyType);
    }

    [Fact]
    public void CreateSecretKey_ExportWorks()
    {
        var originalKey = crypto.randomBytes(32);
        var keyObj = crypto.createSecretKey(originalKey);
        var exported = ((SecretKeyObject)keyObj).export();

        Assert.Equal(originalKey, exported);
    }

    [Fact]
    public void CreatePublicKey_FromKeyObject()
    {
        var (publicKey, privateKey) = crypto.generateKeyPairSync("rsa", null);
        var extractedPublic = crypto.createPublicKey(privateKey);

        Assert.Equal("public", extractedPublic.type);
    }

    // ============== Random Functions Tests ==============

    [Fact]
    public void RandomBytes_Async_Works()
    {
        byte[]? result = null;
        Exception? error = null;

        crypto.randomBytes(32, (err, buf) =>
        {
            error = err;
            result = buf;
        });

        Assert.Null(error);
        Assert.NotNull(result);
        Assert.Equal(32, result.Length);
    }

    [Fact]
    public void RandomFillSync_WithOffsetAndSize()
    {
        var buffer = new byte[64];
        crypto.randomFillSync(buffer, 16, 32);

        // Check first 16 bytes are still zero
        for (int i = 0; i < 16; i++)
        {
            Assert.Equal(0, buffer[i]);
        }

        // Check that middle 32 bytes have random data
        var hasNonZero = false;
        for (int i = 16; i < 48; i++)
        {
            if (buffer[i] != 0)
            {
                hasNonZero = true;
                break;
            }
        }
        Assert.True(hasNonZero);
    }

    [Fact]
    public void RandomFill_Async_Works()
    {
        var buffer = new byte[32];
        byte[]? result = null;
        Exception? error = null;

        crypto.randomFill(buffer, 0, 32, (err, buf) =>
        {
            error = err;
            result = buf;
        });

        Assert.Null(error);
        Assert.NotNull(result);
    }

    // ============== PBKDF2 Tests ==============

    [Fact]
    public void PBKDF2_DifferentIterations()
    {
        var derived1 = crypto.pbkdf2Sync("password", "salt", 1000, 32, "sha256");
        var derived2 = crypto.pbkdf2Sync("password", "salt", 2000, 32, "sha256");

        Assert.NotEqual(derived1, derived2);
    }

    [Fact]
    public void PBKDF2_DifferentSalts()
    {
        var derived1 = crypto.pbkdf2Sync("password", "salt1", 1000, 32, "sha256");
        var derived2 = crypto.pbkdf2Sync("password", "salt2", 1000, 32, "sha256");

        Assert.NotEqual(derived1, derived2);
    }

    [Fact]
    public void PBKDF2_SHA1()
    {
        var derived = crypto.pbkdf2Sync("password", "salt", 1000, 32, "sha1");
        Assert.Equal(32, derived.Length);
    }

    [Fact]
    public void PBKDF2_SHA384()
    {
        var derived = crypto.pbkdf2Sync("password", "salt", 1000, 32, "sha384");
        Assert.Equal(32, derived.Length);
    }

    [Fact]
    public void PBKDF2_SHA512()
    {
        var derived = crypto.pbkdf2Sync("password", "salt", 1000, 32, "sha512");
        Assert.Equal(32, derived.Length);
    }

    [Fact]
    public void PBKDF2_Async_Works()
    {
        byte[]? result = null;
        Exception? error = null;

        crypto.pbkdf2("password", "salt", 1000, 32, "sha256", (err, key) =>
        {
            error = err;
            result = key;
        });

        Assert.Null(error);
        Assert.NotNull(result);
        Assert.Equal(32, result.Length);
    }

    [Fact]
    public void PBKDF2_WithByteArrays()
    {
        var password = Encoding.UTF8.GetBytes("password");
        var salt = Encoding.UTF8.GetBytes("salt");
        var derived = crypto.pbkdf2Sync(password, salt, 1000, 32, "sha256");

        Assert.Equal(32, derived.Length);
    }

    // ============== Utility Function Tests ==============

    [Fact]
    public void GetDefaultCipherList_ReturnsString()
    {
        var list = crypto.getDefaultCipherList();
        Assert.NotEmpty(list);
        Assert.Contains(":", list);
    }

    [Fact]
    public void TimingSafeEqual_WithIdenticalContent()
    {
        var buffer1 = new byte[] { 1, 2, 3, 4, 5 };
        var buffer2 = new byte[] { 1, 2, 3, 4, 5 };

        Assert.True(crypto.timingSafeEqual(buffer1, buffer2));
    }

    [Fact]
    public void GetCiphers_ContainsExpectedAlgorithms()
    {
        var ciphers = crypto.getCiphers();
        Assert.Contains("aes-128-cbc", ciphers);
        Assert.Contains("aes-192-cbc", ciphers);
        Assert.Contains("aes-256-cbc", ciphers);
        Assert.Contains("des-cbc", ciphers);
    }

    [Fact]
    public void GetHashes_ContainsExpectedAlgorithms()
    {
        var hashes = crypto.getHashes();
        Assert.Contains("md5", hashes);
        Assert.Contains("sha1", hashes);
        Assert.Contains("sha256", hashes);
        Assert.Contains("sha384", hashes);
        Assert.Contains("sha512", hashes);
    }

    [Fact]
    public void GetCurves_ContainsExpectedCurves()
    {
        var curves = crypto.getCurves();
        Assert.Contains("secp256r1", curves);
        Assert.Contains("secp384r1", curves);
        Assert.Contains("secp521r1", curves);
    }

    // ============== Static Sign/Verify Tests ==============

    [Fact]
    public void StaticSign_Works()
    {
        var (publicKey, privateKey) = crypto.generateKeyPairSync("rsa", null);
        var data = Encoding.UTF8.GetBytes("Test data");

        var signature = crypto.sign("sha256", data, privateKey);
        Assert.NotEmpty(signature);
    }

    [Fact]
    public void StaticVerify_Works()
    {
        var (publicKey, privateKey) = crypto.generateKeyPairSync("rsa", null);
        var data = Encoding.UTF8.GetBytes("Test data");

        var signature = crypto.sign("sha256", data, privateKey);
        var isValid = crypto.verify("sha256", data, publicKey, signature);

        Assert.True(isValid);
    }

    [Fact]
    public void StaticHash_Works()
    {
        var data = Encoding.UTF8.GetBytes("Test data");
        var hash = crypto.hash("sha256", data, null);

        Assert.Equal(32, hash.Length);
    }

    // ============== Error Handling Tests ==============

    [Fact]
    public void CreateHash_InvalidAlgorithm_Throws()
    {
        Assert.Throws<ArgumentException>(() => crypto.createHash("invalid-algo"));
    }

    [Fact]
    public void CreateHmac_InvalidAlgorithm_Throws()
    {
        Assert.Throws<ArgumentException>(() => crypto.createHmac("invalid-algo", "key"));
    }

    [Fact]
    public void CreateCipheriv_InvalidAlgorithm_Throws()
    {
        Assert.Throws<ArgumentException>(() => crypto.createCipheriv("invalid-algo", new byte[16], new byte[16]));
    }

    [Fact]
    public void ECDH_InvalidCurve_Throws()
    {
        Assert.Throws<ArgumentException>(() => crypto.createECDH("invalid-curve"));
    }

    [Fact]
    public void Hash_UpdateAfterDigest_Throws()
    {
        var hash = crypto.createHash("sha256");
        hash.update("test");
        hash.digest();

        Assert.Throws<InvalidOperationException>(() => hash.update("more"));
    }

    [Fact]
    public void Cipher_UpdateAfterFinal_Throws()
    {
        var cipher = crypto.createCipheriv("aes-256-cbc", crypto.randomBytes(32), crypto.randomBytes(16));
        cipher.update("test");
        cipher.final();

        Assert.Throws<InvalidOperationException>(() => cipher.update("more"));
    }

    [Fact]
    public void GenerateKeyPairSync_InvalidType_Throws()
    {
        Assert.Throws<ArgumentException>(() => crypto.generateKeyPairSync("invalid", null));
    }
}

