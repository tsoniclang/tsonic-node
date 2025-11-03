using System;
using System.Security.Cryptography;
using System.Text;

namespace Tsonic.NodeApi;

/// <summary>
/// The Verify class is a utility for verifying signatures.
/// </summary>
public class Verify : Transform
{
    private readonly string _algorithm;
    private readonly MemoryStream _dataStream;
    private bool _finalized = false;

    internal Verify(string algorithm)
    {
        _algorithm = algorithm;
        _dataStream = new MemoryStream();
    }

    /// <summary>
    /// Updates the Verify content with the given data.
    /// </summary>
    /// <param name="data">The data to verify.</param>
    /// <param name="inputEncoding">The encoding of the data string.</param>
    /// <returns>The Verify object for chaining.</returns>
    public Verify update(string data, string? inputEncoding = null)
    {
        if (_finalized)
            throw new InvalidOperationException("Verify already finalized");

        var encoding = GetEncoding(inputEncoding ?? "utf8");
        var bytes = encoding.GetBytes(data);
        return update(bytes);
    }

    /// <summary>
    /// Updates the Verify content with the given data.
    /// </summary>
    /// <param name="data">The data to verify.</param>
    /// <returns>The Verify object for chaining.</returns>
    public Verify update(byte[] data)
    {
        if (_finalized)
            throw new InvalidOperationException("Verify already finalized");

        _dataStream.Write(data, 0, data.Length);
        return this;
    }

    /// <summary>
    /// Verifies the provided data using the given public key and signature.
    /// </summary>
    /// <param name="publicKey">The public key for verification.</param>
    /// <param name="signature">The signature to verify.</param>
    /// <param name="signatureEncoding">The encoding of the signature.</param>
    /// <returns>True if verification succeeds, false otherwise.</returns>
    public bool verify(string publicKey, string signature, string? signatureEncoding = null)
    {
        byte[] signatureBytes;
        var encoding = (signatureEncoding ?? "base64").ToLowerInvariant();

        signatureBytes = encoding switch
        {
            "hex" => Convert.FromHexString(signature.Replace("-", "")),
            "base64" => Convert.FromBase64String(signature),
            "base64url" => Convert.FromBase64String(signature.Replace("-", "+").Replace("_", "/")),
            "latin1" or "binary" => Encoding.Latin1.GetBytes(signature),
            _ => Encoding.UTF8.GetBytes(signature)
        };

        return verify(publicKey, signatureBytes);
    }

    /// <summary>
    /// Verifies the provided data using the given public key and signature.
    /// </summary>
    /// <param name="publicKey">The public key for verification.</param>
    /// <param name="signature">The signature to verify.</param>
    /// <returns>True if verification succeeds, false otherwise.</returns>
    public bool verify(string publicKey, byte[] signature)
    {
        if (_finalized)
            throw new InvalidOperationException("Verify already finalized");

        _finalized = true;
        var data = _dataStream.ToArray();

        try
        {
            using var rsa = RSA.Create();
            rsa.ImportFromPem(publicKey);

            var hashAlgorithm = GetHashAlgorithmName(_algorithm);
            return rsa.VerifyData(data, signature, hashAlgorithm, RSASignaturePadding.Pkcs1);
        }
        catch (Exception)
        {
            // Try other key formats
            try
            {
                using var ecdsa = ECDsa.Create();
                ecdsa.ImportFromPem(publicKey);

                var hashAlgorithm = GetHashAlgorithmName(_algorithm);
                return ecdsa.VerifyData(data, signature, hashAlgorithm);
            }
            catch (Exception)
            {
                // Try DSA (not fully supported in .NET)
                throw new NotImplementedException("DSA verification is not yet fully supported");
            }
        }
    }

    /// <summary>
    /// Verifies the provided data using the given public key object and signature.
    /// </summary>
    /// <param name="publicKey">The public key object for verification.</param>
    /// <param name="signature">The signature to verify.</param>
    /// <param name="signatureEncoding">The encoding of the signature.</param>
    /// <returns>True if verification succeeds, false otherwise.</returns>
    public bool verify(object publicKey, string signature, string? signatureEncoding = null)
    {
        byte[] signatureBytes;
        var encoding = (signatureEncoding ?? "base64").ToLowerInvariant();

        signatureBytes = encoding switch
        {
            "hex" => Convert.FromHexString(signature.Replace("-", "")),
            "base64" => Convert.FromBase64String(signature),
            "base64url" => Convert.FromBase64String(signature.Replace("-", "+").Replace("_", "/")),
            "latin1" or "binary" => Encoding.Latin1.GetBytes(signature),
            _ => Encoding.UTF8.GetBytes(signature)
        };

        return verify(publicKey, signatureBytes);
    }

    /// <summary>
    /// Verifies the provided data using the given public key object and signature.
    /// </summary>
    /// <param name="publicKey">The public key object for verification.</param>
    /// <param name="signature">The signature to verify.</param>
    /// <returns>True if verification succeeds, false otherwise.</returns>
    public bool verify(object publicKey, byte[] signature)
    {
        if (_finalized)
            throw new InvalidOperationException("Verify already finalized");

        if (publicKey is not PublicKeyObject keyObject)
            throw new ArgumentException("publicKey must be a PublicKeyObject", nameof(publicKey));

        _finalized = true;
        var data = _dataStream.ToArray();

        var key = keyObject.GetKey();
        var hashAlgorithm = GetHashAlgorithmName(_algorithm);

        if (key is RSA rsa)
        {
            return rsa.VerifyData(data, signature, hashAlgorithm, RSASignaturePadding.Pkcs1);
        }
        else if (key is ECDsa ecdsa)
        {
            return ecdsa.VerifyData(data, signature, hashAlgorithm);
        }
        else
        {
            throw new NotSupportedException($"Verification with key type {keyObject.asymmetricKeyType} is not supported");
        }
    }

#pragma warning disable CS1591
    ~Verify()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dataStream?.Dispose();
        }
    }
#pragma warning restore CS1591

    private static HashAlgorithmName GetHashAlgorithmName(string algorithm)
    {
        var alg = algorithm.ToLowerInvariant();

        // Handle algorithm names with and without dashes
        alg = alg.Replace("sha-", "sha").Replace("rsa-", "");

        return alg switch
        {
            "sha1" => HashAlgorithmName.SHA1,
            "sha256" => HashAlgorithmName.SHA256,
            "sha384" => HashAlgorithmName.SHA384,
            "sha512" => HashAlgorithmName.SHA512,
            "md5" => HashAlgorithmName.MD5,
            _ when alg.Contains("sha1") => HashAlgorithmName.SHA1,
            _ when alg.Contains("sha256") => HashAlgorithmName.SHA256,
            _ when alg.Contains("sha384") => HashAlgorithmName.SHA384,
            _ when alg.Contains("sha512") => HashAlgorithmName.SHA512,
            _ => throw new ArgumentException($"Unsupported signature algorithm: {algorithm}")
        };
    }

    private static Encoding GetEncoding(string encoding)
    {
        return encoding.ToLowerInvariant() switch
        {
            "utf8" or "utf-8" => Encoding.UTF8,
            "ascii" => Encoding.ASCII,
            "latin1" or "binary" => Encoding.Latin1,
            "utf16le" or "utf-16le" => Encoding.Unicode,
            "base64" => Encoding.ASCII,
            "hex" => Encoding.ASCII,
            _ => Encoding.UTF8
        };
    }
}
