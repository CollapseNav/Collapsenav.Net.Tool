using System.Security.Cryptography;

namespace Collapsenav.Net.Tool;
/// <summary>
/// Sha256工具
/// </summary>
public partial class HMacSha256Tool
{
    public const string DefaultKey = "Collapsenav.Net.Tool";
    private static HMACSHA256 Algorithm;
    public static void Clear() => Algorithm = null;
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
        Algorithm ??= new HMACSHA256(key.ToBytes());
        var result = Algorithm.ComputeHash(msg.ToBytes());
        return BitConverter.ToString(result).Replace("-", "");
    }
    /// <summary>
    /// 加密
    /// </summary>
    public static string Encrypt(Stream stream, string key = DefaultKey)
    {
        stream.SeekToOrigin();
        Algorithm ??= new HMACSHA256(key.ToBytes());
        var result = Algorithm.ComputeHash(stream);
        stream.SeekToOrigin();
        return BitConverter.ToString(result).Replace("-", "");
    }
}
