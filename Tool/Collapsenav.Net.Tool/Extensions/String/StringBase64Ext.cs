using System.Diagnostics;

namespace Collapsenav.Net.Tool;
public static partial class StringExt
{
    /// <summary>
    /// bytes 转为 base64字符串
    /// </summary>
    public static string ToBase64(this byte[] bytes)
    {
        return Convert.ToBase64String(bytes);
    }
    public static string ToBase64(this string origin)
    {
        return Convert.ToBase64String(origin.ToBytes());
    }

    /// <summary>
    /// base64字符串 转为 bytes
    /// </summary>
    public static byte[] FromBase64(this string base64String)
    {
        return Convert.FromBase64String(base64String);
    }
    /// <summary>
    /// base64字符串 转为 bytes
    /// </summary>
    public static string FromBase64ToString(this string base64String)
    {
        return Convert.FromBase64String(base64String).BytesToString();
    }

    /// <summary>
    /// 图片文件转为base64字符串
    /// </summary>
    public static string ImageToBase64(this Stream stream)
    {
        return "data:image;base64," + stream.StreamToBase64();
    }

    /// <summary>
    /// 图片文件转为base64字符串
    /// </summary>
    public static string ImageToBase64(this string filepath)
    {
        using var fs = filepath.OpenReadShareStream();
        return "data:image;base64," + fs.StreamToBase64();
    }

    /// <summary>
    /// 流转为base64字符串
    /// </summary>
    public static string StreamToBase64(this Stream stream)
    {
        return stream.ToBytes().ToBase64();
    }

    /// <summary>
    /// base64图片文件中取出string
    /// </summary>
    public static string Base64ImageToString(this string base64String)
    {
        return base64String[(base64String.IndexOf("base64,") + "base64,".Length)..].Trim();
    }

    /// <summary>
    /// base64图片字符串转为流
    /// </summary>
    public static Stream Base64ImageToStream(this string base64String)
    {
        return base64String.Base64ImageToString().FromBase64().ToStream();
    }
    /// <summary>
    /// base64图片字符串转为文件
    /// </summary>
    public static void Base64ImageSaveTo(this string base64String, string filepath)
    {
        base64String.Base64ImageToString().FromBase64().SaveTo(filepath);
    }

    /// <summary>
    /// base64图片字符串转为文件
    /// </summary>
    public static async Task Base64ImageSaveToAsync(this string base64String, string filepath)
    {
        await base64String.Base64ImageToString().FromBase64().SaveToAsync(filepath);
    }
}