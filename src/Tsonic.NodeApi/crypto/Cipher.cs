using System;
using System.Security.Cryptography;
using System.Text;

namespace Tsonic.NodeApi;

/// <summary>
/// Instances of the Cipher class are used to encrypt data.
/// </summary>
public class Cipher : Transform
{
    private readonly ICryptoTransform _encryptor;
    private readonly MemoryStream _memoryStream;
    private readonly CryptoStream _cryptoStream;
    private bool _finalized = false;

    internal Cipher(string algorithm, byte[] key, byte[]? iv)
    {
        var (cipher, transform) = CreateCipher(algorithm, key, iv);
        _encryptor = transform;
        _memoryStream = new MemoryStream();
        _cryptoStream = new CryptoStream(_memoryStream, _encryptor, CryptoStreamMode.Write);
    }

    /// <summary>
    /// Updates the cipher with data.
    /// </summary>
    /// <param name="data">The data to encrypt.</param>
    /// <param name="inputEncoding">The encoding of the data.</param>
    /// <param name="outputEncoding">The encoding of the return value.</param>
    /// <returns>The encrypted data.</returns>
    public string update(string data, string? inputEncoding = null, string? outputEncoding = null)
    {
        if (_finalized)
            throw new InvalidOperationException("Cipher already finalized");

        var encoding = GetEncoding(inputEncoding ?? "utf8");
        var bytes = encoding.GetBytes(data);
        return update(bytes, outputEncoding);
    }

    /// <summary>
    /// Updates the cipher with data.
    /// </summary>
    /// <param name="data">The data to encrypt.</param>
    /// <param name="outputEncoding">The encoding of the return value.</param>
    /// <returns>The encrypted data.</returns>
    public string update(byte[] data, string? outputEncoding = null)
    {
        if (_finalized)
            throw new InvalidOperationException("Cipher already finalized");

        _cryptoStream.Write(data, 0, data.Length);
        var encrypted = _memoryStream.ToArray();
        _memoryStream.SetLength(0);

        if (outputEncoding == null || outputEncoding == "buffer")
        {
            return Convert.ToBase64String(encrypted);
        }

        return outputEncoding.ToLowerInvariant() switch
        {
            "hex" => BitConverter.ToString(encrypted).Replace("-", "").ToLowerInvariant(),
            "base64" => Convert.ToBase64String(encrypted),
            "base64url" => Convert.ToBase64String(encrypted).Replace("+", "-").Replace("/", "_").TrimEnd('='),
            "latin1" or "binary" => Encoding.Latin1.GetString(encrypted),
            _ => throw new ArgumentException($"Unknown encoding: {outputEncoding}")
        };
    }

    /// <summary>
    /// Returns any remaining enciphered contents.
    /// </summary>
    /// <param name="outputEncoding">The encoding of the return value.</param>
    /// <returns>The remaining encrypted data.</returns>
    public string final(string? outputEncoding = null)
    {
        if (_finalized)
            throw new InvalidOperationException("Cipher already finalized");

        _finalized = true;
        _cryptoStream.FlushFinalBlock();
        var encrypted = _memoryStream.ToArray();

        if (outputEncoding == null || outputEncoding == "buffer")
        {
            return Convert.ToBase64String(encrypted);
        }

        return outputEncoding.ToLowerInvariant() switch
        {
            "hex" => BitConverter.ToString(encrypted).Replace("-", "").ToLowerInvariant(),
            "base64" => Convert.ToBase64String(encrypted),
            "base64url" => Convert.ToBase64String(encrypted).Replace("+", "-").Replace("/", "_").TrimEnd('='),
            "latin1" or "binary" => Encoding.Latin1.GetString(encrypted),
            _ => throw new ArgumentException($"Unknown encoding: {outputEncoding}")
        };
    }

    /// <summary>
    /// Returns any remaining enciphered contents as a Buffer.
    /// </summary>
    /// <returns>The remaining encrypted data.</returns>
    public byte[] final()
    {
        if (_finalized)
            throw new InvalidOperationException("Cipher already finalized");

        _finalized = true;
        _cryptoStream.FlushFinalBlock();
        return _memoryStream.ToArray();
    }

    /// <summary>
    /// When using an authenticated encryption mode, sets the length of the authentication tag.
    /// </summary>
    /// <param name="tagLength">The tag length in bytes.</param>
    public void setAuthTag(int tagLength)
    {
        throw new NotImplementedException("setAuthTag is not yet implemented for non-GCM modes");
    }

    /// <summary>
    /// When using an authenticated encryption mode, returns the authentication tag.
    /// </summary>
    /// <returns>The authentication tag.</returns>
    public byte[] getAuthTag()
    {
        throw new NotImplementedException("getAuthTag is not yet implemented for non-GCM modes");
    }

    /// <summary>
    /// When using an authenticated encryption mode, sets AAD (Additional Authenticated Data).
    /// </summary>
    /// <param name="buffer">The AAD data.</param>
    public void setAAD(byte[] buffer)
    {
        throw new NotImplementedException("setAAD is not yet implemented for non-GCM modes");
    }

#pragma warning disable CS1591
    ~Cipher()
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
            _cryptoStream?.Dispose();
            _memoryStream?.Dispose();
            _encryptor?.Dispose();
        }
    }
#pragma warning restore CS1591

    private static (SymmetricAlgorithm, ICryptoTransform) CreateCipher(string algorithm, byte[] key, byte[]? iv)
    {
        var alg = algorithm.ToLowerInvariant();

        // Parse algorithm name (e.g., "aes-256-cbc", "des-ede3-cbc")
        if (alg.StartsWith("aes-"))
        {
            var aes = Aes.Create();
            aes.Key = key;
            if (iv != null) aes.IV = iv;

            if (alg.Contains("-ecb")) aes.Mode = CipherMode.ECB;
            else if (alg.Contains("-cbc")) aes.Mode = CipherMode.CBC;
            else if (alg.Contains("-cfb")) aes.Mode = CipherMode.CFB;
            else if (alg.Contains("-ctr")) aes.Mode = CipherMode.ECB; // CTR not directly supported
            else if (alg.Contains("-gcm")) throw new NotImplementedException("AES-GCM mode requires special handling");
            else aes.Mode = CipherMode.CBC; // default

            aes.Padding = PaddingMode.PKCS7;
            return (aes, aes.CreateEncryptor());
        }
        else if (alg.StartsWith("des-ede3") || alg == "des3")
        {
            var des3 = TripleDES.Create();
            des3.Key = key;
            if (iv != null) des3.IV = iv;
            des3.Mode = alg.Contains("-ecb") ? CipherMode.ECB : CipherMode.CBC;
            des3.Padding = PaddingMode.PKCS7;
            return (des3, des3.CreateEncryptor());
        }
        else if (alg.StartsWith("des-"))
        {
            var des = DES.Create();
            des.Key = key;
            if (iv != null) des.IV = iv;
            des.Mode = alg.Contains("-ecb") ? CipherMode.ECB : CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            return (des, des.CreateEncryptor());
        }
        else if (alg.StartsWith("rc2-"))
        {
            var rc2 = RC2.Create();
            rc2.Key = key;
            if (iv != null) rc2.IV = iv;
            rc2.Mode = alg.Contains("-ecb") ? CipherMode.ECB : CipherMode.CBC;
            rc2.Padding = PaddingMode.PKCS7;
            return (rc2, rc2.CreateEncryptor());
        }

        throw new ArgumentException($"Unknown or unsupported cipher algorithm: {algorithm}");
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
