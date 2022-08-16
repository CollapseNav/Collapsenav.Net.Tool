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
    }

    [Theory]
    [InlineData("123123123123", "123123123123", "1231231231233")]
    [InlineData("123", "123", "1234")]
    public void Sha1Test(string origin, string same, string different)
    {
        Assert.Equal(origin.Sha1En(), same.Sha1En());
        Assert.NotEqual(same.Sha1En(), different.Sha1En());
    }

    [Theory]
    [InlineData("123123123123", "123123123123", "1231231231233")]
    [InlineData("123", "123", "1234")]
    public void Sha256Test(string origin, string same, string different)
    {
        Assert.Equal(origin.Sha256En(), same.Sha256En());
        Assert.NotEqual(same.Sha256En(), different.Sha256En());
    }
}
