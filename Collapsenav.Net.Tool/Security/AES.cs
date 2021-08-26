using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace Collapsenav.Net.Tool
{
    public class AESTool
    {
        public const string DefaultKey = "Collapsenav.Net.Tool";
        /// <summary>
        /// 解密
        /// </summary>
        public static string Decrypt(string secmsg, string key = DefaultKey)
        {
            var decryptKey = GetAesKey(key);
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
        public static string Encrypt(string msg, string key = DefaultKey)
        {
            var encryptKey = GetAesKey(key);
            var encryptMsg = Encoding.UTF8.GetBytes(msg);

            using var aes = Aes.Create();
            aes.Key = encryptKey;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            using var encrypt = aes.CreateEncryptor();
            var result = encrypt.TransformFinalBlock(encryptMsg, 0, encryptMsg.Length);
            return Convert.ToBase64String(result);
        }

        public static byte[] GetAesKey(string key)
        {
            return GetAesKey(Encoding.UTF8.GetBytes(key));
        }
        public static byte[] GetAesKey(byte[] key)
        {
            int size = 16;
            return key.Length >= size ? key.Take(size).ToArray() : key.Concat(Encoding.UTF8.GetBytes(DefaultKey.Substring(0, size - key.Length))).ToArray();
        }
    }
}
