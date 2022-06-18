using System.Security.Cryptography;

namespace Collapsenav.Net.Tool;
/// <summary>
/// Sha256工具
/// </summary>
public partial class Sha256Tool
{
    private static SHA256 sha256;
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
        sha256 ??= SHA256.Create();
        var result = sha256.ComputeHash(msg.ToBytes());
        return BitConverter.ToString(result).Replace("-", "");
    }
    /// <summary>
    /// 加密
    /// </summary>
    public static string Encrypt(Stream stream)
    {
        stream.SeekToOrigin();
        sha256 ??= SHA256.Create();
        var result = sha256.ComputeHash(stream);
        stream.SeekToOrigin();
        return BitConverter.ToString(result).Replace("-", "");
    }
}
