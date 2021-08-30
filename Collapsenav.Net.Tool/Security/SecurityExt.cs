using System.Data;

namespace Collapsenav.Net.Tool
{
    public static class SecurityExt
    {
        /// <summary>
        /// Md5加密
        /// </summary>
        public static string Md5En(this string input)
        {
            return MD5Tool.Encrypt(input);
        }
        /// <summary>
        /// Md5解密
        /// </summary>
        public static string Md5De(this string input)
        {
            return MD5Tool.Decrypt(input);
        }
        /// <summary>
        /// Aes加密
        /// </summary>
        public static string AesEn(this string input, string key = AESTool.DefaultKey)
        {
            return AESTool.Encrypt(input, key);
        }
        /// <summary>
        /// Aes解密
        /// </summary>
        public static string AesDe(this string input, string key = AESTool.DefaultKey)
        {
            return AESTool.Decrypt(input, key);
        }

    }
}
