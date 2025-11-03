using System;
using System.Security.Cryptography;
using System.Text;

namespace Tsonic.NodeApi;

/// <summary>
/// The Hash class is a utility for creating hash digests of data.
/// </summary>
public class Hash : Transform
{
    private readonly HashAlgorithm _algorithm;
    private bool _finalized = false;

    internal Hash(string algorithm)
    {
        _algorithm = CreateHashAlgorithm(algorithm);
    }

    /// <summary>
    /// Updates the hash content with the given data.
    /// </summary>
    /// <param name="data">The data to hash.</param>
    /// <param name="inputEncoding">The encoding of the data string.</param>
    /// <returns>The Hash object for chaining.</returns>
    public Hash update(string data, string? inputEncoding = null)
    {
        if (_finalized)
            throw new InvalidOperationException("Digest already called");

        var encoding = GetEncoding(inputEncoding ?? "utf8");
        var bytes = encoding.GetBytes(data);
        _algorithm.TransformBlock(bytes, 0, bytes.Length, null, 0);
        return this;
    }

    /// <summary>
    /// Updates the hash content with the given data.
    /// </summary>
    /// <param name="data">The data to hash.</param>
    /// <returns>The Hash object for chaining.</returns>
    public Hash update(byte[] data)
    {
        if (_finalized)
            throw new InvalidOperationException("Digest already called");

        _algorithm.TransformBlock(data, 0, data.Length, null, 0);
        return this;
    }

    /// <summary>
    /// Calculates the digest of all the data passed to be hashed.
    /// </summary>
    /// <param name="encoding">The encoding of the return value.</param>
    /// <returns>The calculated hash.</returns>
    public string digest(string? encoding = null)
    {
        if (_finalized)
            throw new InvalidOperationException("Digest already called");

        _finalized = true;
        _algorithm.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
        var hash = _algorithm.Hash!;

        if (encoding == null || encoding == "buffer")
        {
            // Return hex by default
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        return encoding.ToLowerInvariant() switch
        {
            "hex" => BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant(),
            "base64" => Convert.ToBase64String(hash),
            "base64url" => Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_").TrimEnd('='),
            "latin1" or "binary" => Encoding.Latin1.GetString(hash),
            _ => throw new ArgumentException($"Unknown encoding: {encoding}")
        };
    }

    /// <summary>
    /// Calculates the digest of all the data passed to be hashed and returns a Buffer.
    /// </summary>
    /// <returns>The calculated hash as a byte array.</returns>
    public byte[] digest()
    {
        if (_finalized)
            throw new InvalidOperationException("Digest already called");

        _finalized = true;
        _algorithm.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
        return _algorithm.Hash!;
    }

    /// <summary>
    /// Creates a copy of the Hash object in its current state.
    /// </summary>
    /// <returns>A new Hash object.</returns>
    public Hash copy()
    {
        throw new NotImplementedException("Hash.copy() is not yet implemented");
    }

#pragma warning disable CS1591
    ~Hash()
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
            _algorithm?.Dispose();
        }
    }
#pragma warning restore CS1591

    private static HashAlgorithm CreateHashAlgorithm(string algorithm)
    {
        return algorithm.ToLowerInvariant() switch
        {
            "md5" => MD5.Create(),
            "sha1" or "sha-1" => SHA1.Create(),
            "sha256" or "sha-256" => SHA256.Create(),
            "sha384" or "sha-384" => SHA384.Create(),
            "sha512" or "sha-512" => SHA512.Create(),
            "sha512-224" => throw new NotImplementedException($"Algorithm {algorithm} is not implemented"),
            "sha512-256" => throw new NotImplementedException($"Algorithm {algorithm} is not implemented"),
            "sha3-224" => throw new NotImplementedException($"Algorithm {algorithm} is not implemented"),
            "sha3-256" => throw new NotImplementedException($"Algorithm {algorithm} is not implemented"),
            "sha3-384" => throw new NotImplementedException($"Algorithm {algorithm} is not implemented"),
            "sha3-512" => throw new NotImplementedException($"Algorithm {algorithm} is not implemented"),
            "shake128" => throw new NotImplementedException($"Algorithm {algorithm} is not implemented"),
            "shake256" => throw new NotImplementedException($"Algorithm {algorithm} is not implemented"),
            "ripemd160" or "rmd160" => throw new NotImplementedException($"Algorithm {algorithm} is not implemented"),
            "blake2b512" => throw new NotImplementedException($"Algorithm {algorithm} is not implemented"),
            "blake2s256" => throw new NotImplementedException($"Algorithm {algorithm} is not implemented"),
            _ => throw new ArgumentException($"Unknown hash algorithm: {algorithm}")
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
            "base64" => Encoding.ASCII, // Base64 is ASCII-based
            "hex" => Encoding.ASCII, // Hex is ASCII-based
            _ => Encoding.UTF8
        };
    }
}
