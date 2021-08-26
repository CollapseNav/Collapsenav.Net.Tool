namespace Collapsenav.Net.Tool
{
    public static class SecurityExt
    {
        public static string Md5En(this string msg)
        {
            return MD5Tool.Encrypt(msg);
        }
        public static string AesEn(this string msg, string key = AESTool.DefaultKey)
        {
            return AESTool.Encrypt(msg, key);
        }
        public static string AesDe(this string msg, string key = AESTool.DefaultKey)
        {
            return AESTool.Decrypt(msg, key);
        }

    }
}
