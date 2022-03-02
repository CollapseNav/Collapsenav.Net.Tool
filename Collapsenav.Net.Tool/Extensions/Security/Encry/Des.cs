using System.Security.Cryptography;

namespace Collapsenav.Net.Tool;

public partial class DesTool
{
    public const string DefaultKey = "Collapsenav.Net.Tool";
    public const string DefaultIV = "looT.teN.vanespalloC";
    /// <summary>
    /// Des解密
    /// </summary>
    /// <param name="sec"></param>
    /// <param name="key"></param>
    /// <param name="mode"></param>
    /// <param name="padding"></param>
    /// <param name="iv"></param>
    /// <param name="level"></param>
    public static string Decrypt(string sec, string key = DefaultKey, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, string iv = DefaultIV, int level = 8)
    {
        byte[] decryptMsg = sec.FromBase64();
        using var des = DES.Create();
        des.Mode = mode;
        des.Padding = padding;
        using var decrypt = des.CreateDecryptor(GetDESBytes(key, level), GetDESBytes(iv, 8));
        var result = decrypt.TransformFinalBlock(decryptMsg, 0, decryptMsg.Length);
        return result.BytesToString();
    }
    /// <summary>
    /// Des加密
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="key"></param>
    /// <param name="mode"></param>
    /// <param name="padding"></param>
    /// <param name="iv"></param>
    /// <param name="level"></param>
    public static string Encrypt(string msg, string key = DefaultKey, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, string iv = DefaultIV, int level = 8)
    {
        using var des = DES.Create();
        des.Mode = mode;
        des.Padding = padding;
        using var encrypt = des.CreateEncryptor(GetDESBytes(key, level), GetDESBytes(iv, 8));
        var result = encrypt.TransformFinalBlock(msg.ToBytes(), 0, msg.Length);
        return result.ToBase64();
    }
    public static byte[] GetDESBytes(string value, int level = 8)
    {
        if (value.Length < level)
            return value.PadLeft(level, '#').ToBytes();
        return value.First(level).ToBytes();
    }
}