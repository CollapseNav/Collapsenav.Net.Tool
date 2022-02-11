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
    public static string AesEn(this string input, string key = AESTool.DefaultKey)
    {
        return AESTool.Encrypt(input, key);
    }
    /// <summary>
    /// Aes解密
    /// </summary>
    public static string AesDe(this string input, string key = AESTool.DefaultKey)
    {
        return AESTool.Decrypt(input, key);
    }

    /// <summary>
    /// Des加密
    /// </summary>
    public static string DesEn(this string input, string key = DesTool.DefaultKey)
    {
        return DesTool.Encrypt(input, key);
    }
    /// <summary>
    /// Des解密
    /// </summary>
    public static string DesDe(this string input, string key = DesTool.DefaultKey)
    {
        return DesTool.Decrypt(input, key);
    }
}
