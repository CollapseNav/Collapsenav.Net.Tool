using System.Security.Cryptography;

namespace Collapsenav.Net.Tool;
public partial class HMacMd5Tool
{
    public const string DefaultKey = "Collapsenav.Net.Tool";
    private static HMACMD5 md5;
    /// <summary>
    /// 解密
    /// </summary>
    public static string Decrypt(string input, string key = DefaultKey)
    {
        throw new Exception("Are you kidding ?");
    }
    /// <summary>
    /// 加密
    /// </summary>
    public static string Encrypt(string msg, string key = DefaultKey)
    {
        md5 ??= new HMACMD5(key.ToBytes());
        var result = md5.ComputeHash(msg.ToBytes());
        return BitConverter.ToString(result).Replace("-", "");
    }
    /// <summary>
    /// 加密
    /// </summary>
    public static string Encrypt(Stream stream, string key = DefaultKey)
    {
        stream.SeekToOrigin();
        md5 ??= new HMACMD5(key.ToBytes());
        var result = md5.ComputeHash(stream);
        stream.SeekToOrigin();
        return BitConverter.ToString(result).Replace("-", "");
    }
}