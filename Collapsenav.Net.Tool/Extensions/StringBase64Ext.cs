namespace Collapsenav.Net.Tool;
public static partial class StringExt
{
    /// <summary>
    /// bytes 转为 base64字符串
    /// </summary>
    public static string ToBase64(this byte[] bytes)
    {
        return StringTool.ToBase64(bytes);
    }

    /// <summary>
    /// base64字符串 转为 bytes
    /// </summary>
    public static byte[] FromBase64(this string base64String)
    {
        return StringTool.FromBase64(base64String);
    }

    /// <summary>
    /// 图片文件转为base64字符串
    /// </summary>
    public static string ImageToBase64(this Stream stream)
    {
        return StringTool.ImageToBase64(stream);
    }

    /// <summary>
    /// 图片文件转为base64字符串
    /// </summary>
    public static string ImageToBase64(this string filepath)
    {
        return StringTool.ImageToBase64(filepath);
    }

    /// <summary>
    /// 流转为base64字符串
    /// </summary>
    public static string StreamToBase64(this Stream stream)
    {
        return StringTool.StreamToBase64(stream);
    }

    /// <summary>
    /// base64图片文件中取出string
    /// </summary>
    public static string Base64ImageToString(this string base64String)
    {
        return StringTool.Base64ImageToString(base64String);
    }

    /// <summary>
    /// base64图片字符串转为流
    /// </summary>
    public static Stream Base64ImageToStream(this string base64String)
    {
        return StringTool.Base64ImageToStream(base64String);
    }
    /// <summary>
    /// base64图片字符串转为文件
    /// </summary>
    public static void Base64ImageToStream(this string base64String, string filepath)
    {
        StringTool.Base64ImageToStream(base64String, filepath);
    }

    /// <summary>
    /// base64图片字符串转为文件
    /// </summary>
    public static async Task Base64ImageToStreamAsync(this string base64String, string filepath)
    {
        await StringTool.Base64ImageToStreamAsync(base64String, filepath);
    }

}