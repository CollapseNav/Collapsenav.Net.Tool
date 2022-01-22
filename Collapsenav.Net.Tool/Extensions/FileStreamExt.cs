namespace Collapsenav.Net.Tool;

public static class FileStreamExt
{
    public static FileStream OpenStream(this string path)
    {
        return new FileStream(path, FileMode.Open);
    }
    public static FileStream OpenReadStream(this string path)
    {
        return new FileStream(path, FileMode.Open, FileAccess.Read);
    }
    public static FileStream OpenReadShareStream(this string path)
    {
        return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
    }
    public static FileStream OpenWriteStream(this string path)
    {
        return new FileStream(path, FileMode.Open, FileAccess.Write);
    }
    public static FileStream OpenWriteShareStream(this string path)
    {
        return new FileStream(path, FileMode.Open, FileAccess.Write, FileShare.Write);
    }
    public static FileStream OpenReadWirteStream(this string path)
    {
        return new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
    }
    public static FileStream OpenReadWirteShareStream(this string path)
    {
        return new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
    }


    public static FileStream CreateStream(this string path)
    {
        return new FileStream(path, FileMode.Create);
    }
    public static FileStream CreateReadStream(this string path)
    {
        return new FileStream(path, FileMode.Create, FileAccess.Read);
    }
    public static FileStream CreateReadShareStream(this string path)
    {
        return new FileStream(path, FileMode.Create, FileAccess.Read, FileShare.Read);
    }
    public static FileStream CreateWriteStream(this string path)
    {
        return new FileStream(path, FileMode.Create, FileAccess.Write);
    }
    public static FileStream CreateWriteShareStream(this string path)
    {
        return new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
    }
    public static FileStream CreateReadWirteStream(this string path)
    {
        return new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
    }
    public static FileStream CreateReadWirteShareStream(this string path)
    {
        return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
    }


    public static FileStream OpenCreateStream(this string path)
    {
        return new FileStream(path, FileMode.OpenOrCreate);
    }
    public static FileStream OpenCreateReadStream(this string path)
    {
        return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
    }
    public static FileStream OpenCreateReadShareStream(this string path)
    {
        return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
    }
    public static FileStream OpenCreateWriteStream(this string path)
    {
        return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
    }
    public static FileStream OpenCreateWriteShareStream(this string path)
    {
        return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
    }
    public static FileStream OpenCreateReadWirteStream(this string path)
    {
        return new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }
    public static FileStream OpenCreateReadWirteShareStream(this string path)
    {
        return new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
    }
}