namespace Collapsenav.Net.Tool;

public static class FileStreamExt
{
    /// <summary>
    /// 打开一个流
    /// </summary>
    public static FileStream OpenStream(this string path)
    {
        return new FileStream(path, FileMode.Open);
    }
    /// <summary>
    /// 文件读取流
    /// </summary>
    public static FileStream ReadStream(this string path)
    {
        return new FileStream(path, FileMode.Open, FileAccess.Read);
    }
    /// <summary>
    /// share 方式打开一个流
    /// </summary>
    public static FileStream ReadShareStream(this string path)
    {
        return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
    }
    /// <summary>
    /// 文件创建流
    /// </summary>
    public static FileStream CreateStream(this string path)
    {
        return new FileStream(path, FileMode.Create);
    }
    /// <summary>
    /// share 方式 创建流
    /// </summary>
    public static FileStream CreateShareStream(this string path)
    {
        return new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
    }
    /// <summary>
    /// 打开一个写入流
    /// </summary>
    public static FileStream WriteStream(this string path)
    {
        return new FileStream(path, FileMode.Open, FileAccess.Write);
    }
    /// <summary>
    /// share 方法打开一个写入流
    /// </summary>
    public static FileStream WriteShareStream(this string path)
    {
        return new FileStream(path, FileMode.Open, FileAccess.Write, FileShare.Write);
    }
    /// <summary>
    /// 读写流
    /// </summary>
    public static FileStream ReadAndWirteStream(this string path)
    {
        return new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
    }
    /// <summary>
    /// share方式打开一个读写流
    /// </summary>
    public static FileStream ReadAndWriteShareStream(this string path)
    {
        return new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
    }
    public static FileStream OpenOrCreateStream(this string path)
    {
        return new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }
    public static FileStream OpenOrCreateShareStream(this string path)
    {
        return new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
    }
}