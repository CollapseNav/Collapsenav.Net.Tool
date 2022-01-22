namespace Collapsenav.Net.Tool;
public partial class StringTool
{

    /// <summary>
    /// bytes 转为 base64字符串
    /// </summary>
    public static string ToBase64(byte[] bytes)
    {
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// bytes 转为 base64字符串
    /// </summary>
    public static byte[] FromBase64(string base64String)
    {
        return Convert.FromBase64String(base64String);
    }

    /// <summary>
    /// 图片文件转为base64字符串
    /// </summary>
    public static string ImageToBase64(Stream stream)
    {
        return "data:image;base64," + StreamToBase64(stream);
    }

    /// <summary>
    /// 图片文件转为base64字符串
    /// </summary>
    public static string ImageToBase64(string filepath)
    {
        using var fs = filepath.OpenReadShareStream();
        return "data:image;base64," + StreamToBase64(fs);
    }

    /// <summary>
    /// 流转为base64字符串
    /// </summary>
    public static string StreamToBase64(Stream stream)
    {
        return ToBase64(stream.ToBytes());
    }

    /// <summary>
    /// base64图片文件中取出string
    /// </summary>
    public static string Base64ImageToString(string base64String)
    {
        return base64String[(base64String.IndexOf("base64,") + "base64,".Length)..].Trim();
    }

    /// <summary>
    /// base64图片字符串转为流
    /// </summary>
    public static Stream Base64ImageToStream(string base64String)
    {
        return FromBase64(Base64ImageToString(base64String)).ToStream();
    }

    /// <summary>
    /// base64图片字符串转为文件
    /// </summary>
    public static void Base64ImageToStream(string base64String, string filepath)
    {
        FromBase64(Base64ImageToString(base64String)).SaveTo(filepath);
    }

    /// <summary>
    /// base64图片字符串转为文件
    /// </summary>
    public static async Task Base64ImageToStreamAsync(string base64String, string filepath)
    {
        await FromBase64(Base64ImageToString(base64String)).SaveToAsync(filepath);
    }
}