using System.Security.Cryptography;

namespace Collapsenav.Net.Tool;
/// <summary>
/// Sha1工具
/// </summary>
public partial class Sha1Tool
{
    private static SHA1 Algorithm;
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
        Algorithm ??= SHA1.Create();
        var result = Algorithm.ComputeHash(msg.ToBytes());
        return BitConverter.ToString(result).Replace("-", "");
    }
    /// <summary>
    /// 加密
    /// </summary>
    public static string Encrypt(Stream stream)
    {
        stream.SeekToOrigin();
        Algorithm ??= SHA1.Create();
        var result = Algorithm.ComputeHash(stream);
        stream.SeekToOrigin();
        return BitConverter.ToString(result).Replace("-", "");
    }
}
