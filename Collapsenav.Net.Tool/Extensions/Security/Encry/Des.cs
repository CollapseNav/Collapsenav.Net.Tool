using System.Security.Cryptography;
using System.Text;

namespace Collapsenav.Net.Tool;

public partial class DesTool
{
    public const string DefaultKey = "Collapsenav.Net.Tool";
    public const string DefaultIV = "looT.teN.vanespalloC";
    public static string Decrypt(string des, string key = DefaultKey)
    {
        byte[] rgbKey = Encoding.UTF8.GetBytes(key.First(16));
        byte[] rgbIV = DefaultIV.First(16).ToBytes();
        byte[] inputByteArray = des.FromBase64();
        var DCSP = Aes.Create();
        MemoryStream mStream = new();
        CryptoStream cStream = new(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
        cStream.Write(inputByteArray, 0, inputByteArray.Length);
        cStream.FlushFinalBlock();
        return Encoding.UTF8.GetString(mStream.ToArray());
    }
    public static string Encrypt(string msg, string key = DefaultKey)
    {
        if (key.Length < 16)
            key = key.PadLeft(16);
        byte[] rgbKey = Encoding.UTF8.GetBytes(key.First(16));
        byte[] rgbIV = DefaultIV.First(16).ToBytes();
        byte[] inputByteArray = msg.ToBytes();
        var DCSP = Aes.Create();
        MemoryStream mStream = new();
        CryptoStream cStream = new(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
        cStream.Write(inputByteArray, 0, inputByteArray.Length);
        cStream.FlushFinalBlock();
        return mStream.ToArray().ToBase64();
    }
}