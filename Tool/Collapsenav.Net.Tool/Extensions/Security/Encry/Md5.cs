using System.Security.Cryptography;
namespace Collapsenav.Net.Tool;
/// <summary>
/// Md5工具
/// </summary>
public partial class MD5Tool
{
    private static MD5 md5;
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
        md5 ??= MD5.Create();
        var result = md5.ComputeHash(msg.ToBytes());
        return BitConverter.ToString(result).Replace("-", "");
    }

    /// <summary>
    /// 加密
    /// </summary>
    public static string Encrypt(Stream stream)
    {
        stream.SeekToOrigin();
        md5 ??= MD5.Create();
        var result = md5.ComputeHash(stream);
        stream.SeekToOrigin();
        return BitConverter.ToString(result).Replace("-", "");
    }
}
