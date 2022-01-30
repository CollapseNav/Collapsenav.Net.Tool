using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;

namespace Collapsenav.Net.Tool.Test;
public class StreamTest
{
    [Fact]
    public async Task SeekTest()
    {
        var filePath = "./Collapsenav.Net.Tool.Test.dll";
        using var fs = filePath.OpenReadShareStream();
        Assert.True(fs.Position == 0);

        using var ms = new MemoryStream();
        fs.CopyTo(ms);
        Assert.True(ms.Position == ms.Length);
        Assert.True(fs.Position == fs.Length);

        fs.SeekTo(fs.Length / 2);
        Assert.True(fs.Position == fs.Length / 2);
        fs.SeekToEnd();
        Assert.True(fs.Position == fs.Length);
        fs.SeekToOrigin();
        Assert.True(fs.Position == 0);

        ms.SeekToOrigin();
        Assert.True(ms.Position == 0);

        fs.SeekAndCopyTo(ms);
        Assert.True(ms.Position == 0);
        Assert.True(fs.Position == 0);
        Assert.True(ms.Length == fs.Length);

        await fs.SeekAndCopyToAsync(ms);
        Assert.True(ms.Position == 0);
        Assert.True(fs.Position == 0);
        Assert.True(ms.Length == fs.Length);


    }

    [Fact]
    public async Task ByteAndStreamTest()
    {
        var filePath = "./Collapsenav.Net.Tool.Test.dll";
        using var fs = filePath.OpenReadShareStream();
        var bytes = fs.ToBytes();
        var stream = bytes.ToStream();
        Assert.True(fs.Length == stream.Length);
        Assert.True(fs.Md5En() == stream.Md5En());
        fs.SeekToOrigin();
        bytes = await fs.ToBytesAsync();
        stream = await bytes.ToStreamAsync();
        Assert.True(fs.Length == stream.Length);
        Assert.True(fs.Md5En() == stream.Md5En());
    }

    [Fact]
    public async Task SaveTest()
    {
        var filePath = "./Collapsenav.Net.Tool.Test.dll";
        using var fs = filePath.OpenReadShareStream();
        string toFile = "./SomeFile";
        File.Delete(toFile);
        fs.SaveTo(toFile);
        Assert.True(File.Exists(toFile));
        using var toFs1 = toFile.OpenReadShareStream();
        Assert.True(fs.Length == toFs1.Length);
        Assert.True(toFs1.Sha256En() == fs.Sha256En());
        toFs1.Dispose();
        File.Delete(toFile);

        await fs.SaveToAsync(toFile);
        using var toFs2 = toFile.OpenReadShareStream();
        Assert.True(fs.Length == toFs2.Length);
        Assert.True(toFs2.Sha1En() == fs.Sha1En());
        toFs2.Dispose();
        File.Delete(toFile);

        string originStr = "Collapsenav.Net.Tool";
        var strBytes = originStr.ToBytes();
        strBytes.SaveTo(toFile);
        Assert.True(File.Exists(toFile));
        using var toFs3 = toFile.OpenReadShareStream();
        Assert.True(strBytes.Length == toFs3.Length);
        toFs3.Dispose();
        File.Delete(toFile);

        strBytes = originStr.ToBytes();
        await strBytes.SaveToAsync(toFile);
        Assert.True(File.Exists(toFile));
        using var toFs4 = toFile.OpenReadShareStream();
        Assert.True(strBytes.Length == toFs4.Length);
        toFs4.Dispose();
        File.Delete(toFile);
    }

    [Fact]
    public void OpenFileStreamTest()
    {
        var path = "./vscode2.png";
        var fs = path.OpenReadStream();
        Assert.True(fs.CanRead);
        Assert.False(fs.CanWrite);
        fs.Dispose();

        fs = path.OpenWriteStream();
        Assert.False(fs.CanRead);
        Assert.True(fs.CanWrite);
        fs.Dispose();

        fs = path.OpenReadWriteStream();
        Assert.True(fs.CanWrite);
        Assert.True(fs.CanRead);
        fs.Dispose();

        fs = path.OpenReadWriteShareStream();
        Assert.True(fs.CanWrite);
        Assert.True(fs.CanRead);
        var fs1 = path.OpenReadWriteShareStream();
        Assert.True(fs1.CanRead);
        Assert.True(fs1.CanWrite);
        fs.Dispose();
        fs1.Dispose();

        fs = path.OpenReadShareStream();
        Assert.True(fs.CanRead);
        Assert.False(fs.CanWrite);
        fs1 = path.OpenReadShareStream();
        Assert.True(fs1.CanRead);
        Assert.False(fs1.CanWrite);
        fs.Dispose();
        fs1.Dispose();

        fs = path.OpenWriteShareStream();
        Assert.False(fs.CanRead);
        Assert.True(fs.CanWrite);
        fs1 = path.OpenWriteShareStream();
        Assert.False(fs1.CanRead);
        Assert.True(fs1.CanWrite);
        fs.Dispose();
        fs1.Dispose();
    }
    [Fact]
    public void CreateFileStreamTest()
    {
        var path = "./vscode3.png";
        Assert.False(File.Exists(path));
        var fs = path.CreateWriteStream();
        Assert.True(File.Exists(path));
        Assert.True(fs.CanWrite);
        Assert.False(fs.CanRead);
        fs.Dispose();
        File.Delete(path);

        Assert.False(File.Exists(path));
        fs = path.CreateWriteShareStream();
        Assert.True(File.Exists(path));
        Assert.True(fs.CanWrite);
        Assert.False(fs.CanRead);
        var fs1 = path.CreateWriteShareStream();
        fs.Dispose();
        fs1.Dispose();
        File.Delete(path);

        Assert.False(File.Exists(path));
        fs = path.CreateWriteStream();
        Assert.True(File.Exists(path));
        Assert.True(fs.CanWrite);
        Assert.False(fs.CanRead);
        fs.Dispose();
        File.Delete(path);

        Assert.False(File.Exists(path));
        fs = path.CreateReadWriteStream();
        Assert.True(File.Exists(path));
        Assert.True(fs.CanWrite);
        Assert.True(fs.CanRead);
        fs.Dispose();
        File.Delete(path);

        Assert.False(File.Exists(path));
        fs = path.CreateReadWriteShareStream();
        Assert.True(File.Exists(path));
        Assert.True(fs.CanWrite);
        Assert.True(fs.CanRead);
        fs1 = path.CreateReadWriteShareStream();
        fs.Dispose();
        fs1.Dispose();
        File.Delete(path);
    }

    [Fact]
    public void OpenCreateFileStreamTest()
    {
        var path = "./vscode4.png";
        Assert.False(File.Exists(path));
        var fs = path.OpenCreateStream();
        Assert.True(File.Exists(path));
        fs.Dispose();
        File.Delete(path);

        Assert.False(File.Exists(path));
        fs = path.OpenCreateReadStream();
        Assert.True(File.Exists(path));
        Assert.True(fs.CanRead);
        Assert.False(fs.CanWrite);
        fs.Dispose();
        File.Delete(path);

        Assert.False(File.Exists(path));
        fs = path.OpenCreateWriteStream();
        Assert.True(File.Exists(path));
        Assert.True(fs.CanWrite);
        Assert.False(fs.CanRead);
        fs.Dispose();
        File.Delete(path);

        Assert.False(File.Exists(path));
        fs = path.OpenCreateReadShareStream();
        Assert.True(File.Exists(path));
        Assert.True(fs.CanRead);
        Assert.False(fs.CanWrite);
        var fs1 = path.OpenCreateReadShareStream();
        fs.Dispose();
        fs1.Dispose();
        File.Delete(path);

        Assert.False(File.Exists(path));
        fs = path.OpenCreateWriteShareStream();
        Assert.True(File.Exists(path));
        Assert.False(fs.CanRead);
        Assert.True(fs.CanWrite);
        fs1 = path.OpenCreateWriteShareStream();
        fs.Dispose();
        fs1.Dispose();
        File.Delete(path);

        Assert.False(File.Exists(path));
        fs = path.OpenCreateReadWriteStream();
        Assert.True(File.Exists(path));
        Assert.True(fs.CanRead);
        Assert.True(fs.CanWrite);
        fs.Dispose();
        File.Delete(path);

        Assert.False(File.Exists(path));
        fs = path.OpenCreateReadWriteShareStream();
        Assert.True(File.Exists(path));
        Assert.True(fs.CanRead);
        Assert.True(fs.CanWrite);
        fs1 = path.OpenCreateReadWriteShareStream();
        fs.Dispose();
        fs1.Dispose();
        File.Delete(path);
    }
}

