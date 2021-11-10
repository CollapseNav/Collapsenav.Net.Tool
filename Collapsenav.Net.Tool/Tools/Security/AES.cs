using System;
using System.Buffers;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Collapsenav.Net.Tool
{
    /// <summary>
    /// Aes工具
    /// </summary>
    public partial class AESTool
    {
        public const string DefaultKey = "Collapsenav.Net.Tool";
        public const string DefaultIV = "looT.teN.vanespalloC";
        /// <summary>
        /// 解密
        /// </summary>
        public static string Decrypt(string secmsg, string key = DefaultKey)
        {
            var decryptKey = GetAesBytes(key);
            var decryptMsg = Convert.FromBase64String(secmsg);

            using var aes = Aes.Create();
            aes.Key = decryptKey;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            using var decrypt = aes.CreateDecryptor();
            var result = decrypt.TransformFinalBlock(decryptMsg, 0, decryptMsg.Length);
            return Encoding.UTF8.GetString(result);
        }
        /// <summary>
        /// 加密
        /// </summary>
        public static string Encrypt(string msg, string key = DefaultKey, string iv = DefaultIV, int level = 32)
        {
            var encryptMsg = Encoding.UTF8.GetBytes(msg);

            using var aes = Aes.Create();
            aes.Key = GetAesBytes(key, level);
            aes.IV = iv.IsNull() ? aes.IV : GetAesBytes(iv, 16);
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            using var encrypt = aes.CreateEncryptor();
            var result = encrypt.TransformFinalBlock(encryptMsg, 0, msg.Length);
            return Convert.ToBase64String(result);
        }

        public static byte[] GetAesBytes(string key, int level = 32)
        {
            if (key.Length < level)
                key = key.PadLeft(level, '#');
            return Encoding.UTF8.GetBytes(key.Substring(0, level));
        }
        public static byte[] GetAesBytes(byte[] key, int level = 32)
        {
            if (key.Length < level)
                key = key.Concat(Encoding.UTF8.GetBytes('#'.ToString(level - key.Length))).ToArray();
            return key.Take(level).ToArray();
        }
    }
}
