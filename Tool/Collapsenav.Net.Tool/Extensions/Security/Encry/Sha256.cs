using System.Security.Cryptography;

namespace Collapsenav.Net.Tool;
/// <summary>
/// Sha256工具
/// </summary>
public partial class Sha256Tool
{
    private static SHA256 Algorithm;
    public static void Clear() => Algorithm = null;
    /// <summary>
    /// 解密
    /// </summary>
    public static string Decrypt(string md5)
    {
        throw new Exception("Are you kidding ?");
    }
    /// <summary>
    /// 加密
    /// </summary>
    public static string Encrypt(string msg)
    {
        Algorithm ??= SHA256.Create();
        var result = Algorithm.ComputeHash(msg.ToBytes());
        return BitConverter.ToString(result).Replace("-", "");
    }
    /// <summary>
    /// 加密
    /// </summary>
    public static string Encrypt(Stream stream)
    {
        stream.SeekToOrigin();
        Algorithm ??= SHA256.Create();
        var result = Algorithm.ComputeHash(stream);
        stream.SeekToOrigin();
        return BitConverter.ToString(result).Replace("-", "");
    }
}
