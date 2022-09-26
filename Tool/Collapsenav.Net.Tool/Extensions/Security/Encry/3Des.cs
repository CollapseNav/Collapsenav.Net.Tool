using System.Security.Cryptography;

namespace Collapsenav.Net.Tool;

/// <summary>
/// 3Des
/// </summary>
public partial class TripleDesTool
{
    public const string DefaultKey = "Collapsenav.Net.Tool";
    public const string DefaultIV = "looT.teN.vanespalloC";
    private static TripleDES Algorithm;
    public static void Clear() => Algorithm = null;
    /// <summary>
    /// Des解密
    /// </summary>
    /// <param name="tripleDes"></param>
    /// <param name="key"></param>
    /// <param name="mode"></param>
    /// <param name="padding"></param>
    /// <param name="iv"></param>
    /// <param name="level"></param>
    public static string Decrypt(string tripleDes, string key = DefaultKey, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, string iv = DefaultIV, int level = 24)
    {
        byte[] decryptMsg = tripleDes.FromBase64();
        Algorithm ??= TripleDES.Create();
        Algorithm.Mode = mode;
        Algorithm.Padding = padding;
        using var decrypt = Algorithm.CreateDecryptor(GetTripleDESBytes(key, level), GetTripleDESBytes(iv, 8));
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
    public static string Encrypt(string msg, string key = DefaultKey, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, string iv = DefaultIV, int level = 24)
    {
        Algorithm ??= TripleDES.Create();
        Algorithm.Mode = mode;
        Algorithm.Padding = padding;
        using var encrypt = Algorithm.CreateEncryptor(GetTripleDESBytes(key, level), GetTripleDESBytes(iv, 8));
        var result = encrypt.TransformFinalBlock(msg.ToBytes(), 0, msg.Length);
        return result.ToBase64();
    }

    public static byte[] GetTripleDESBytes(string value, int level = 24)
    {
        if (value.Length < level)
            return value.PadLeft(level, '#').ToBytes();
        return value.First(level).ToBytes();
    }
}