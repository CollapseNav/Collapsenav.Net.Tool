using System;
using Xunit;

namespace Collapsenav.Net.Tool.Test;
public class SecurityTest
{
    [Fact]
    public void AesTest()
    {
        var msg = "123123123";
        var msg2 = "123123123";
        var msg3 = "123123123123";
        var key = "23333";
        Assert.True(msg.AesEn() == msg2.AesEn());
        Assert.False(msg.AesEn() == msg3.AesEn());

        Assert.True(msg.AesEn(key) == msg2.AesEn(key));
        Assert.False(msg.AesEn(key) == msg3.AesEn(key));

        Assert.True(msg.AesEn(key).AesDe(key) == msg);
        Assert.True(msg2.AesEn(key).AesDe(key) == msg2);
        Assert.True(msg3.AesEn(key).AesDe(key) == msg3);

        Assert.True(msg.AesEn(key).AesDe(key) == msg);
        Assert.True(msg2.AesEn(key).AesDe(key) == msg2);
        Assert.True(msg3.AesEn(key).AesDe(key) == msg3);

        key = Guid.NewGuid().ToString();

        Assert.True(msg.AesEn(key) == msg2.AesEn(key));
        Assert.False(msg.AesEn(key) == msg3.AesEn(key));

        Assert.True(msg.AesEn(key).AesDe(key) == msg);
        Assert.True(msg2.AesEn(key).AesDe(key) == msg2);
        Assert.True(msg3.AesEn(key).AesDe(key) == msg3);

        Assert.True(msg.AesEn(key).AesDe(key) == msg);
        Assert.True(msg2.AesEn(key).AesDe(key) == msg2);
        Assert.True(msg3.AesEn(key).AesDe(key) == msg3);
    }

    [Fact]
    public void DesTest()
    {
        var msg = "123123123";
        var msg2 = "123123123";
        var msg3 = "123123123123";
        var key = "23333";
        Assert.True(msg.DesEn() == msg2.DesEn());
        Assert.False(msg.DesEn() == msg3.DesEn());

        Assert.True(msg.DesEn(key) == msg2.DesEn(key));
        Assert.False(msg.DesEn(key) == msg3.DesEn(key));

        Assert.True(msg.DesEn(key).DesDe(key) == msg);
        Assert.True(msg2.DesEn(key).DesDe(key) == msg2);
        Assert.True(msg3.DesEn(key).DesDe(key) == msg3);

        Assert.True(msg.DesEn(key).DesDe(key) == msg);
        Assert.True(msg2.DesEn(key).DesDe(key) == msg2);
        Assert.True(msg3.DesEn(key).DesDe(key) == msg3);

        key = Guid.NewGuid().ToString();

        Assert.True(msg.DesEn(key) == msg2.DesEn(key));
        Assert.False(msg.DesEn(key) == msg3.DesEn(key));

        Assert.True(msg.DesEn(key).DesDe(key) == msg);
        Assert.True(msg2.DesEn(key).DesDe(key) == msg2);
        Assert.True(msg3.DesEn(key).DesDe(key) == msg3);

        Assert.True(msg.DesEn(key).DesDe(key) == msg);
        Assert.True(msg2.DesEn(key).DesDe(key) == msg2);
        Assert.True(msg3.DesEn(key).DesDe(key) == msg3);
    }


    [Fact]
    public void TripleDesTest()
    {
        var msg = "123123123";
        var msg2 = "123123123";
        var msg3 = "123123123123";
        var key = "23333";
        Assert.True(msg.TripleDesEn() == msg2.TripleDesEn());
        Assert.False(msg.TripleDesEn() == msg3.TripleDesEn());

        Assert.True(msg.TripleDesEn(key) == msg2.TripleDesEn(key));
        Assert.False(msg.TripleDesEn(key) == msg3.TripleDesEn(key));

        Assert.True(msg.TripleDesEn(key).TripleDesDe(key) == msg);
        Assert.True(msg2.TripleDesEn(key).TripleDesDe(key) == msg2);
        Assert.True(msg3.TripleDesEn(key).TripleDesDe(key) == msg3);

        Assert.True(msg.TripleDesEn(key).TripleDesDe(key) == msg);
        Assert.True(msg2.TripleDesEn(key).TripleDesDe(key) == msg2);
        Assert.True(msg3.TripleDesEn(key).TripleDesDe(key) == msg3);

        key = Guid.NewGuid().ToString();

        Assert.True(msg.TripleDesEn(key) == msg2.TripleDesEn(key));
        Assert.False(msg.TripleDesEn(key) == msg3.TripleDesEn(key));

        Assert.True(msg.TripleDesEn(key).TripleDesDe(key) == msg);
        Assert.True(msg2.TripleDesEn(key).TripleDesDe(key) == msg2);
        Assert.True(msg3.TripleDesEn(key).TripleDesDe(key) == msg3);

        Assert.True(msg.TripleDesEn(key).TripleDesDe(key) == msg);
        Assert.True(msg2.TripleDesEn(key).TripleDesDe(key) == msg2);
        Assert.True(msg3.TripleDesEn(key).TripleDesDe(key) == msg3);
    }

    [Fact]
    public void Md5Test()
    {
        var msg = "123123123";
        var msg2 = "123123123";
        var msg3 = "233333";
        var msg21 = "233333";
        Assert.True(msg.Md5En() == msg2.Md5En());
        Assert.True(msg3.Md5En() == msg21.Md5En());
        Assert.False(msg.Md5En() == msg3.Md5En());
    }

    [Fact]
    public void Sha1Test()
    {
        var msg = "123123123";
        var msg2 = "123123123";
        var msg3 = "233333";
        var msg21 = "233333";
        Assert.True(msg.Sha1En() == msg2.Sha1En());
        Assert.True(msg3.Sha1En() == msg21.Sha1En());
        Assert.False(msg.Sha1En() == msg3.Sha1En());
    }

    [Fact]
    public void Sha256Test()
    {
        var msg = "123123123";
        var msg2 = "123123123";
        var msg3 = "233333";
        var msg21 = "233333";
        Assert.True(msg.Sha256En() == msg2.Sha256En());
        Assert.True(msg3.Sha256En() == msg21.Sha256En());
        Assert.False(msg.Sha256En() == msg3.Sha256En());
    }
}
