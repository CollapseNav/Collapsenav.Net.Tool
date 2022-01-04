﻿namespace Collapsenav.Net.Tool;
public static partial class StreamExt
{
    /// <summary>
    /// 指针移动到开头
    /// </summary>
    public static void SeekToOrigin(this Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
    }
    /// <summary>
    /// 指针移动到末尾
    /// </summary>
    public static void SeekToEnd(this Stream stream)
    {
        stream.Seek(0, SeekOrigin.End);
    }
    /// <summary>
    /// 指针移动指定长度
    /// </summary>
    public static void SeekTo(this Stream stream, long len)
    {
        // 先归零
        stream.SeekToOrigin();
        stream.Seek(len, SeekOrigin.Current);
    }
    /// <summary>
    /// 复制流并将指针归零
    /// </summary>
    public static void SeekAndCopyTo(this Stream origin, Stream target)
    {
        // 先归零
        origin.SeekToOrigin();
        target.SeekToOrigin();
        origin.CopyTo(target);
        // 再次归零
        origin.SeekToOrigin();
        target.SeekToOrigin();
    }
    /// <summary>
    /// 复制流并将指针归零
    /// </summary>
    public static async Task SeekAndCopyToAsync(this Stream origin, Stream target)
    {
        // 先归零
        origin.SeekToOrigin();
        target.SeekToOrigin();
        await origin.CopyToAsync(target);
        // 再次归零
        origin.SeekToOrigin();
        target.SeekToOrigin();
    }

    /// <summary>
    /// 流转为byte[]
    /// </summary>
    public static byte[] ToBytes(this Stream stream)
    {
        stream.SeekToOrigin();
        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        stream.SeekToOrigin();
        return bytes;
    }
    /// <summary>
    /// 流转为byte[]
    /// </summary>
    public static async Task<byte[]> ToBytesAsync(this Stream stream)
    {
        stream.SeekToOrigin();
        byte[] bytes = new byte[stream.Length];
        await stream.ReadAsync(bytes);
        stream.SeekToOrigin();
        return bytes;
    }
    /// <summary>
    /// byte[]转为流
    /// </summary>
    public static Stream ToStream(this byte[] bytes)
    {
        MemoryStream ms = new();
        ms.Write(bytes, 0, bytes.Length);
        return ms;
    }
    /// <summary>
    /// byte[]转为流
    /// </summary>
    public static async Task<Stream> ToStreamAsync(this byte[] bytes)
    {
        MemoryStream ms = new();
        await ms.WriteAsync(bytes);
        return ms;
    }
    /// <summary>
    /// 流保存为文件
    /// </summary>
    public static void SaveTo(this Stream stream, string path)
    {
        stream.SeekToOrigin();
        using var fs = new FileStream(path, FileMode.CreateNew);
        stream.CopyTo(fs);
        stream.SeekToOrigin();
    }
    /// <summary>
    /// byte[]保存为文件
    /// </summary>
    public static void SaveTo(this byte[] bytes, string path)
    {
        using var fs = new FileStream(path, FileMode.CreateNew);
        fs.Write(bytes, 0, bytes.Length);
    }
    /// <summary>
    /// 流保存为文件
    /// </summary>
    public static async Task SaveToAsync(this Stream stream, string path)
    {
        stream.SeekToOrigin();
        using var fs = new FileStream(path, FileMode.CreateNew);
        await stream.CopyToAsync(fs);
        stream.SeekToOrigin();
    }
    /// <summary>
    /// byte[]保存为文件
    /// </summary>
    public static async Task SaveToAsync(this byte[] bytes, string path)
    {
        using var fs = new FileStream(path, FileMode.CreateNew);
        await fs.WriteAsync(bytes);
    }
}
