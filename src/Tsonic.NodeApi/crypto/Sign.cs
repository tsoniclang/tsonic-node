using System;
using System.Security.Cryptography;
using System.Text;

namespace Tsonic.NodeApi;

/// <summary>
/// The Sign class is a utility for generating signatures.
/// </summary>
public class Sign : Transform
{
    private readonly string _algorithm;
    private readonly MemoryStream _dataStream;
    private bool _finalized = false;

    internal Sign(string algorithm)
    {
        _algorithm = algorithm;
        _dataStream = new MemoryStream();
    }

    /// <summary>
    /// Updates the Sign content with the given data.
    /// </summary>
    /// <param name="data">The data to sign.</param>
    /// <param name="inputEncoding">The encoding of the data string.</param>
    /// <returns>The Sign object for chaining.</returns>
    public Sign update(string data, string? inputEncoding = null)
    {
        if (_finalized)
            throw new InvalidOperationException("Sign already finalized");

        var encoding = GetEncoding(inputEncoding ?? "utf8");
        var bytes = encoding.GetBytes(data);
        return update(bytes);
    }

    /// <summary>
    /// Updates the Sign content with the given data.
    /// </summary>
    /// <param name="data">The data to sign.</param>
    /// <returns>The Sign object for chaining.</returns>
    public Sign update(byte[] data)
    {
        if (_finalized)
            throw new InvalidOperationException("Sign already finalized");

        _dataStream.Write(data, 0, data.Length);
        return this;
    }

    /// <summary>
    /// Calculates the signature on all the data passed through using update.
    /// </summary>
    /// <param name="privateKey">The private key for signing.</param>
    /// <param name="outputEncoding">The encoding of the return value.</param>
    /// <returns>The signature.</returns>
    public string sign(string privateKey, string? outputEncoding = null)
    {
        var signature = sign(privateKey);

        if (outputEncoding == null || outputEncoding == "buffer")
        {
            return Convert.ToBase64String(signature);
        }

        return outputEncoding.ToLowerInvariant() switch
        {
            "hex" => BitConverter.ToString(signature).Replace("-", "").ToLowerInvariant(),
            "base64" => Convert.ToBase64String(signature),
            "base64url" => Convert.ToBase64String(signature).Replace("+", "-").Replace("/", "_").TrimEnd('='),
            "latin1" or "binary" => Encoding.Latin1.GetString(signature),
            _ => throw new ArgumentException($"Unknown encoding: {outputEncoding}")
        };
    }

    /// <summary>
    /// Calculates the signature on all the data passed through using update.
    /// </summary>
    /// <param name="privateKey">The private key for signing.</param>
    /// <returns>The signature as a byte array.</returns>
    public byte[] sign(string privateKey)
    {
        if (_finalized)
            throw new InvalidOperationException("Sign already finalized");

        _finalized = true;
        var data = _dataStream.ToArray();

        try
        {
            using var rsa = RSA.Create();
            rsa.ImportFromPem(privateKey);

            var hashAlgorithm = GetHashAlgorithmName(_algorithm);
            return rsa.SignData(data, hashAlgorithm, RSASignaturePadding.Pkcs1);
        }
        catch (Exception)
        {
            // Try other key formats
            try
            {
                using var ecdsa = ECDsa.Create();
                ecdsa.ImportFromPem(privateKey);

                var hashAlgorithm = GetHashAlgorithmName(_algorithm);
                return ecdsa.SignData(data, hashAlgorithm);
            }
            catch (Exception)
            {
                // Try DSA (not fully supported in .NET)
                throw new NotImplementedException("DSA signing is not yet fully supported");
            }
        }
    }

    /// <summary>
    /// Calculates the signature on all the data passed through using update.
    /// </summary>
    /// <param name="privateKey">The private key object for signing.</param>
    /// <param name="outputEncoding">The encoding of the return value.</param>
    /// <returns>The signature.</returns>
    public string sign(object privateKey, string? outputEncoding = null)
    {
        throw new NotImplementedException("KeyObject-based signing is not yet implemented");
    }

    /// <summary>
    /// Calculates the signature on all the data passed through using update.
    /// </summary>
    /// <param name="privateKey">The private key object for signing.</param>
    /// <returns>The signature as a byte array.</returns>
    public byte[] sign(object privateKey)
    {
        throw new NotImplementedException("KeyObject-based signing is not yet implemented");
    }

#pragma warning disable CS1591
    ~Sign()
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
