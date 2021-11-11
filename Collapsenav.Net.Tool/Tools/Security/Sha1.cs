using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Collapsenav.Net.Tool
{
    /// <summary>
    /// Sha1工具
    /// </summary>
    public partial class Sha1Tool
    {
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
            using var sha1 = SHA1.Create();
            var result = sha1.ComputeHash(Encoding.UTF8.GetBytes(msg));
            return BitConverter.ToString(result).Replace("-", "");
        }
        /// <summary>
        /// 加密
        /// </summary>
        public static string Encrypt(Stream stream)
        {
            using var sha1 = SHA1.Create();
            var result = sha1.ComputeHash(stream);
            return BitConverter.ToString(result).Replace("-", "");
        }
    }
}
