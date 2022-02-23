using System.Security.Cryptography;

namespace Collapsenav.Net.Tool;

/// <summary>
/// 3Des
/// </summary>
public partial class TripleDesTool
{
    public const string DefaultKey = "Collapsenav.Net.Tool";
    public const string DefaultIV = "looT.teN.vanespalloC";
    /// <summary>
    /// Des解密
    /// </summary>
    /// <param name="tripleDes"></param>
    /// <param name="key"></param>
    /// <param name="mode"></param>
    /// <param name="padding"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static string Decrypt(string tripleDes, string key = DefaultKey, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, string iv = DefaultIV)
    {
        byte[] decryptMsg = tripleDes.FromBase64();
        using var DCSP = TripleDES.Create();
        DCSP.Mode = mode;
        DCSP.Padding = padding;
        using var decrypt = DCSP.CreateDecryptor(GetTripleDESBytes(key), GetTripleDESBytes(iv, 8));
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
    /// <returns></returns>
    public static string Encrypt(string msg, string key = DefaultKey, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, string iv = DefaultIV)
    {
        using var DCSP = TripleDES.Create();
        DCSP.Mode = mode;
        DCSP.Padding = padding;
        using var encrypt = DCSP.CreateEncryptor(GetTripleDESBytes(key), GetTripleDESBytes(iv, 8));
        var result = encrypt.TransformFinalBlock(msg.ToBytes(), 0, msg.Length);
        return result.ToBase64();
    }

    public static byte[] GetTripleDESBytes(string value, int size = 24)
    {
        if (value.Length < size)
            return value.PadLeft(size, '#').ToBytes();
        return value.First(size).ToBytes();
    }
}