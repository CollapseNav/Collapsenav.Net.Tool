using System.Security.Cryptography;

namespace Collapsenav.Net.Tool;
public static partial class SecurityExt
{
    /// <summary>
    /// Md5加密
    /// </summary>
    public static string Md5En(this string input)
    {
        return MD5Tool.Encrypt(input);
    }
    /// <summary>
    /// Md5加密文件
    /// </summary>
    public static string Md5En(this Stream input)
    {
        return MD5Tool.Encrypt(input);
    }
    /// <summary>
    /// Md5解密
    /// </summary>
    public static string Md5De(this string input)
    {
        return MD5Tool.Decrypt(input);
    }
    /// <summary>
    /// Sha1计算
    /// </summary>
    public static string Sha1En(this string input)
    {
        return Sha1Tool.Encrypt(input);
    }
    /// <summary>
    /// Sha1计算文件hash
    /// </summary>
    public static string Sha1En(this Stream stream)
    {
        return Sha1Tool.Encrypt(stream);
    }
    /// <summary>
    /// Sha1解密
    /// </summary>
    public static string Sha1De(this string input)
    {
        return Sha1Tool.Decrypt(input);
    }
    /// <summary>
    /// Sha256计算
    /// </summary>
    public static string Sha256En(this string input)
    {
        return Sha256Tool.Encrypt(input);
    }
    /// <summary>
    /// Sha256计算文件Hash
    /// </summary>
    public static string Sha256En(this Stream stream)
    {
        return Sha256Tool.Encrypt(stream);
    }
    /// <summary>
    /// Sha256解密
    /// </summary>
    public static string Sha256De(this string input)
    {
        return Sha256Tool.Decrypt(input);
    }
    /// <summary>
    /// Aes加密
    /// </summary>
    public static string AesEn(this string input, string key = AESTool.DefaultKey, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, int level = 32)
    {
        return AESTool.Encrypt(input, key, mode, padding, level: level);
    }
    /// <summary>
    /// Aes解密
    /// </summary>
    public static string AesDe(this string input, string key = AESTool.DefaultKey, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, int level = 32)
    {
        return AESTool.Decrypt(input, key, mode, padding, level: level);
    }
    /// <summary>
    /// Des加密
    /// </summary>
    public static string DesEn(this string input, string key = DesTool.DefaultKey, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, int level = 8)
    {
        return DesTool.Encrypt(input, key, mode, padding, level: level);
    }
    /// <summary>
    /// Des解密
    /// </summary>
    public static string DesDe(this string input, string key = DesTool.DefaultKey, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, int level = 8)
    {
        return DesTool.Decrypt(input, key, mode, padding, level: level);
    }
    /// <summary>
    /// 3Des加密
    /// </summary>
    public static string TripleDesEn(this string input, string key = TripleDesTool.DefaultKey, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, int level = 24)
    {
        return TripleDesTool.Encrypt(input, key, mode, padding, level: level);
    }
    /// <summary>
    /// 3Des解密
    /// </summary>
    public static string TripleDesDe(this string input, string key = TripleDesTool.DefaultKey, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, int level = 24)
    {
        return TripleDesTool.Decrypt(input, key, mode, padding, level: level);
    }
    /// <summary>
    /// HMacMd5加密
    /// </summary>
    public static string HMacMd5En(this string input, string key = HMacMd5Tool.DefaultKey)
    {
        return HMacMd5Tool.Encrypt(input, key);
    }
    /// <summary>
    /// HMacSha1加密
    /// </summary>
    public static string HMacSha1En(this string input, string key = HMacSha1Tool.DefaultKey)
    {
        return HMacSha1Tool.Encrypt(input, key);
    }

    /// <summary>
    /// HMacSha256加密
    /// </summary>
    public static string HMacSha256En(this string input, string key = HMacSha256Tool.DefaultKey)
    {
        return HMacSha256Tool.Encrypt(input, key);
    }
    /// <summary>
    /// HMacMd5加密
    /// </summary>
    public static string HMacMd5En(this Stream input, string key = HMacMd5Tool.DefaultKey)
    {
        return HMacMd5Tool.Encrypt(input, key);
    }
    /// <summary>
    /// HMacSha1加密
    /// </summary>
    public static string HMacSha1En(this Stream input, string key = HMacSha1Tool.DefaultKey)
    {
        return HMacSha1Tool.Encrypt(input, key);
    }

    /// <summary>
    /// HMacSha256加密
    /// </summary>
    public static string HMacSha256En(this Stream input, string key = HMacSha256Tool.DefaultKey)
    {
        return HMacSha256Tool.Encrypt(input, key);
    }
    /// <summary>
    /// HMacMd5解密
    /// </summary>
    public static string HMacMd5De(this string input, string key = HMacMd5Tool.DefaultKey)
    {
        return HMacMd5Tool.Decrypt(input, key);
    }
    /// <summary>
    /// HMacSha1解密
    /// </summary>
    public static string HMacSha1De(this string input, string key = HMacSha1Tool.DefaultKey)
    {
        return HMacSha1Tool.Decrypt(input, key);
    }
    /// <summary>
    /// HMacSha256解密
    /// </summary>
    public static string HMacSha256De(this string input, string key = HMacSha256Tool.DefaultKey)
    {
        return HMacSha256Tool.Decrypt(input, key);
    }
}
