using System.Security.Cryptography;

namespace Collapsenav.Net.Tool;
/// <summary>
/// Sha1工具
/// </summary>
public partial class HMacSha1Tool
{
    public const string DefaultKey = "Collapsenav.Net.Tool";
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
        using var sha1 = new HMACSHA1(key.ToBytes());
        var result = sha1.ComputeHash(msg.ToBytes());
        return BitConverter.ToString(result).Replace("-", "");
    }
    /// <summary>
    /// 加密
    /// </summary>
    public static string Encrypt(Stream stream, string key = DefaultKey)
    {
        stream.SeekToOrigin();
        using var sha1 = new HMACSHA1(key.ToBytes());
        var result = sha1.ComputeHash(stream);
        stream.SeekToOrigin();
        return BitConverter.ToString(result).Replace("-", "");
    }
}
