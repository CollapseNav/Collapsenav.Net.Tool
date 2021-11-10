using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Collapsenav.Net.Tool
{
    /// <summary>
    /// Sha256工具
    /// </summary>
    public partial class Sha256Tool
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
            using var sha256 = SHA256.Create();
            var result = sha256.ComputeHash(Encoding.UTF8.GetBytes(msg));
            return BitConverter.ToString(result).Replace("-", "");
        }
        /// <summary>
        /// 加密
        /// </summary>
        public static string Encrypt(Stream stream)
        {
            using var sha256 = SHA256.Create();
            var result = sha256.ComputeHash(stream);
            return BitConverter.ToString(result).Replace("-", "");
        }
    }
}
