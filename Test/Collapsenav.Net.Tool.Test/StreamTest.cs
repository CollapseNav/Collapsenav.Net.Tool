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
        using var fs = filePath.ReadShareStream();
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
        using var fs = filePath.ReadShareStream();
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
        using var fs = filePath.ReadShareStream();
        string toFile = "./SomeFile";
        File.Delete(toFile);
        fs.SaveTo(toFile);
        Assert.True(File.Exists(toFile));
        using var toFs1 = toFile.ReadShareStream();
        Assert.True(fs.Length == toFs1.Length);
        Assert.True(toFs1.Sha256En() == fs.Sha256En());
        toFs1.Dispose();
        File.Delete(toFile);

        await fs.SaveToAsync(toFile);
        using var toFs2 = toFile.ReadShareStream();
        Assert.True(fs.Length == toFs2.Length);
        Assert.True(toFs2.Sha1En() == fs.Sha1En());
        toFs2.Dispose();
        File.Delete(toFile);

        string originStr = "Collapsenav.Net.Tool";
        var strBytes = originStr.ToBytes();
        strBytes.SaveTo(toFile);
        Assert.True(File.Exists(toFile));
        using var toFs3 = toFile.ReadShareStream();
        Assert.True(strBytes.Length == toFs3.Length);
        toFs3.Dispose();
        File.Delete(toFile);

        strBytes = originStr.ToBytes();
        await strBytes.SaveToAsync(toFile);
        Assert.True(File.Exists(toFile));
        using var toFs4 = toFile.ReadShareStream();
        Assert.True(strBytes.Length == toFs4.Length);
        toFs4.Dispose();
        File.Delete(toFile);
    }
}

