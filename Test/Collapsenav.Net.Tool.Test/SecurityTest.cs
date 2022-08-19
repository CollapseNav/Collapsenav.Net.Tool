using System;
using Xunit;

namespace Collapsenav.Net.Tool.Test;
public class SecurityTest
{
    [Theory]
    [InlineData("123", "123123123123", "123123123123", "1231231231233")]
    [InlineData("WTFWTFWTF", "123", "123", "1234")]
    public void AesTest(string key, string origin, string same, string different)
    {
        Assert.Equal(origin.AesEn(), same.AesEn());
        Assert.NotEqual(origin.AesEn(), different.AesEn());
        Assert.NotEqual(same.AesEn(), different.AesEn());

        Assert.Equal(origin.AesEn(key), same.AesEn(key));
        Assert.NotEqual(origin.AesEn(key), different.AesEn(key));
        Assert.NotEqual(same.AesEn(key), different.AesEn(key));

        Assert.NotEqual(origin.AesEn(key), same.AesEn(key + Guid.NewGuid().ToString()));


        Assert.Equal(origin.AesEn().AesDe(), same);
        Assert.NotEqual(origin.AesEn().AesDe(), different);

        Assert.Equal(origin.AesEn(key).AesDe(key), same);
        Assert.NotEqual(origin.AesEn(key).AesDe(key), different);
        Assert.ThrowsAny<Exception>(() => origin.AesEn(key).AesDe());
    }

    [Theory]
    [InlineData("123", "123123123123", "123123123123", "1231231231233")]
    [InlineData("WTF", "123", "123", "1234")]
    public void DesTest(string key, string origin, string same, string different)
    {
        Assert.Equal(origin.DesEn(), same.DesEn());
        Assert.NotEqual(origin.DesEn(), different.DesEn());
        Assert.NotEqual(same.DesEn(), different.DesEn());

        Assert.Equal(origin.DesEn(key), same.DesEn(key));
        Assert.NotEqual(origin.DesEn(key), different.DesEn(key));
        Assert.NotEqual(same.DesEn(key), different.DesEn(key));

        Assert.NotEqual(origin.DesEn(key), same.DesEn(key + Guid.NewGuid().ToString()));


        Assert.Equal(origin.DesEn().DesDe(), same);
        Assert.NotEqual(origin.DesEn().DesDe(), different);

        Assert.Equal(origin.DesEn(key).DesDe(key), same);
        Assert.NotEqual(origin.DesEn(key).DesDe(key), different);
        Assert.ThrowsAny<Exception>(() => origin.DesEn(key).DesDe());
    }


    [Theory]
    [InlineData("123", "123123123123", "123123123123", "1231231231233")]
    [InlineData("WTF", "123", "123", "1234")]
    public void TripleDesTest(string key, string origin, string same, string different)
    {
        Assert.Equal(origin.TripleDesEn(), same.TripleDesEn());
        Assert.NotEqual(origin.TripleDesEn(), different.TripleDesEn());
        Assert.NotEqual(same.TripleDesEn(), different.TripleDesEn());

        Assert.Equal(origin.TripleDesEn(key), same.TripleDesEn(key));
        Assert.NotEqual(origin.TripleDesEn(key), different.TripleDesEn(key));
        Assert.NotEqual(same.TripleDesEn(key), different.TripleDesEn(key));

        Assert.NotEqual(origin.TripleDesEn(key), same.TripleDesEn(key + Guid.NewGuid().ToString()));


        Assert.Equal(origin.TripleDesEn().TripleDesDe(), same);
        Assert.NotEqual(origin.TripleDesEn().TripleDesDe(), different);

        Assert.Equal(origin.TripleDesEn(key).TripleDesDe(key), same);
        Assert.NotEqual(origin.TripleDesEn(key).TripleDesDe(key), different);
        Assert.ThrowsAny<Exception>(() => origin.TripleDesEn(key).TripleDesDe());
    }

    [Theory]
    [InlineData("123123123123", "123123123123", "1231231231233")]
    [InlineData("123", "123", "1234")]
    public void Md5Test(string origin, string same, string different)
    {
        Assert.Equal(origin.Md5En(), same.Md5En());
        Assert.NotEqual(same.Md5En(), different.Md5En());
        Assert.ThrowsAny<Exception>(() => origin.Md5En().Md5De());
    }

    [Theory]
    [InlineData("123123123123", "123123123123", "1231231231233")]
    [InlineData("123", "123", "1234")]
    public void Sha1Test(string origin, string same, string different)
    {
        Assert.Equal(origin.Sha1En(), same.Sha1En());
        Assert.NotEqual(same.Sha1En(), different.Sha1En());
        Assert.ThrowsAny<Exception>(() => origin.Sha1En().Sha1De());
    }

    [Theory]
    [InlineData("123123123123", "123123123123", "1231231231233")]
    [InlineData("123", "123", "1234")]
    public void Sha256Test(string origin, string same, string different)
    {
        Assert.Equal(origin.Sha256En(), same.Sha256En());
        Assert.NotEqual(same.Sha256En(), different.Sha256En());
        Assert.ThrowsAny<Exception>(() => origin.Sha256En().Sha256De());
    }
    [Theory]
    [InlineData("123", "123123123123", "123123123123", "1231231231233")]
    [InlineData("123123", "123123123123", "123123123123", "1231231231233")]
    public void HMacMd5Test(string key, string origin, string same, string different)
    {
        Assert.Equal(origin.HMacMd5En(key), same.HMacMd5En(key));
        Assert.NotEqual(same.HMacMd5En(key), different.HMacMd5En(key));
        Assert.ThrowsAny<Exception>(() => origin.HMacMd5En().HMacMd5De());
    }

    [Theory]
    [InlineData("123", "123123123123", "123123123123", "1231231231233")]
    [InlineData("123123", "123123123123", "123123123123", "1231231231233")]
    public void HMacSha1Test(string key, string origin, string same, string different)
    {
        Assert.Equal(origin.HMacSha1En(key), same.HMacSha1En(key));
        Assert.NotEqual(same.HMacSha1En(key), different.HMacSha1En(key));
        Assert.ThrowsAny<Exception>(() => origin.HMacSha1En().HMacSha1De());
    }

    [Theory]
    [InlineData("123", "123123123123", "123123123123", "1231231231233")]
    [InlineData("123123", "123123123123", "123123123123", "1231231231233")]
    public void HMacSha256Test(string key, string origin, string same, string different)
    {
        Assert.Equal(origin.HMacSha256En(key), same.HMacSha256En(key));
        Assert.NotEqual(same.HMacSha256En(key), different.HMacSha256En(key));
        Assert.ThrowsAny<Exception>(() => origin.HMacSha256En().HMacSha256De());
    }

    [Theory]
    [InlineData("./vscode.png", "./vscode2.png", "123123")]
    public void StreamHashTest(string path1, string path2, string key)
    {
        using var fs1 = path1.OpenReadWriteShareStream();
        using var fs2 = path2.OpenReadWriteShareStream();
        Assert.Equal(fs1.Md5En(), fs2.Md5En());
        Assert.Equal(fs1.Sha1En(), fs2.Sha1En());
        Assert.Equal(fs1.Sha256En(), fs2.Sha256En());
        Assert.Equal(fs1.HMacMd5En(key), fs2.HMacMd5En(key));
        Assert.Equal(fs1.HMacSha1En(key), fs2.HMacSha1En(key));
        Assert.Equal(fs1.HMacSha256En(key), fs2.HMacSha256En(key));
    }
}
