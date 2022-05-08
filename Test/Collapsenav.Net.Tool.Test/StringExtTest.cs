using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Collapsenav.Net.Tool.Test;
public class StringExtTest
{
    [Theory]
    [InlineData("10510001010", "一百零五亿一千万零一千零一十")]
    [InlineData("1010", "一千零一十")]
    [InlineData("99999", "九万九千九百九十九")]
    [InlineData("203300010001", "二千零三十三亿零一万零一")]
    [InlineData("9999999999999999", "九千九百九十九万九千九百九十九亿九千九百九十九万九千九百九十九")]
    public void NumToChineseTest(string num, string result)
    {
        Assert.True(num.ToChinese() == result);
    }

    [Fact]
    public void StringConvertTest()
    {
        string timeString = "2021-08-28 23:23:23";
        Assert.True(timeString.ToDateTime() == new DateTime(2021, 8, 28, 23, 23, 23));
        string intString = "2333";
        Assert.True(intString.ToInt() == 2333);
        string doubleString = "233.333";
        Assert.True(doubleString.ToDouble() == 233.333);
        string guidString = "00000000-0000-0000-0000-000000000000";
        Assert.True(guidString.ToGuid() == Guid.Empty);
        string longString = "233333333333333";
        Assert.True(longString.ToLong() == 233333333333333);
    }

    [Fact]
    public void StringListJoinTest()
    {
        string strJoin = "233.233.233.233";
        Assert.True("2@3@3@.@2@3@3@.@2@3@3@.@2@3@3" == strJoin.Join("@"));
        int[] intJoin = { 1, 2, 3 };
        Assert.True("1@2@3" == intJoin.Join("@"));
        Assert.True("-1@-2@-3" == intJoin.Join("@", item => -item));

        int[] ints = new[] { 1, 3, 5, 6, 8, 13, 457, };
        Assert.True(ints.Join("@") == "1@3@5@6@8@13@457");
        Dictionary<int, string> dicts = new()
        {
            { 1, "11" },
            { 2, "22" },
            { 4, "44" },
            { 6, "66" },
        };
        Assert.True(dicts.Where(item => item.Key % 2 == 0).Select(item => item.Value).Join("@") == "22@44@66");
    }

    [Theory]
    [InlineData("collapsenav@163.com", true)]
    [InlineData("", false)]
    public void EmailTest(string email, bool result)
    {
        Assert.True(email.IsEmail() == result);
    }

    [Theory]
    [InlineData("https://www.bilibili.com/")]
    [InlineData("https://www.baidu.com/")]
    public void PingTest(string url)
    {
        Assert.True(url.CanPing(2000));
    }

    [Theory]
    [InlineData("https://www.bilibili.com/")]
    [InlineData("https://www.baidu.com/")]
    public void IsUrlTest(string url)
    {
        Assert.True(url.IsUrl());
    }

    [Theory]
    [InlineData("httpsssss://www.bilibili.com/")]
    [InlineData("https//www.baidu.com/")]
    public void NotUrlTest(string url)
    {
        Assert.False(url.IsUrl());
    }

    [Theory]
    [InlineData("https://www.bilibili.com/")]
    [InlineData("https://www.baidu.com/")]
    public void IsUrlAndCanPingTest(string url)
    {
        Assert.True(url.IsUrl(true));
    }

    [Theory]
    [InlineData("https://www.bilibiliffffffffffffffffffffffffff.com/")]
    [InlineData("https://www.baiduuuuuuuuuuuuuuuuuuuuuuuuuu.com/")]
    public void IsUrlAndCanNotPingTest(string url)
    {
        Assert.ThrowsAny<Exception>(() => url.IsUrl(true));
    }

    [Theory]
    [InlineData(true, "23333333333333", "2333", "23")]
    [InlineData(true, "23333333333333", "2333", "23", "3")]
    [InlineData(false, "23333333333333", "333", "333333", "3")]
    public void StringHasStartWiths(bool result, string origin, params string[] strs)
    {
        Assert.Equal(result, origin.HasStartsWith(strs));
        Assert.Equal(result, origin.HasStartsWith(strs.AsEnumerable()));
        Assert.Equal(result, origin.HasStartsWith(strs.ToList()));
    }
    [Theory]
    [InlineData(true, "23333333333333", "2333")]
    [InlineData(true, "23333333333333", "2333", "23")]
    [InlineData(false, "23333333333333", "2333", "23333333", "3")]
    public void StringAllStartWiths(bool result, string origin, params string[] strs)
    {
        Assert.Equal(result, origin.AllStartsWith(strs));
        Assert.Equal(result, origin.AllStartsWith(strs.AsEnumerable()));
        Assert.Equal(result, origin.AllStartsWith(strs.ToList()));
    }
    [Theory]
    [InlineData(true, "23333333333333", "3", "333333")]
    [InlineData(true, "23333333333333", "3", "3333333", "2")]
    [InlineData(false, "23333333333333", "2333", "23333333", "2")]
    public void StringHasEndWiths(bool result, string origin, params string[] strs)
    {
        Assert.Equal(result, origin.HasEndsWith(strs));
        Assert.Equal(result, origin.HasEndsWith(strs.AsEnumerable()));
        Assert.Equal(result, origin.HasEndsWith(strs.ToList()));
    }
    [Theory]
    [InlineData(true, "23333333333333", "3", "333333")]
    [InlineData(true, "23333333333333", "3", "3333333333", "23333333333333")]
    [InlineData(false, "23333333333333", "3", "3333333333", "233333333333")]
    public void StringAllEndWiths(bool result, string origin, params string[] strs)
    {
        Assert.Equal(result, origin.AllEndsWith(strs));
        Assert.Equal(result, origin.AllEndsWith(strs.AsEnumerable()));
        Assert.Equal(result, origin.AllEndsWith(strs.ToList()));
    }

    [Fact]
    public void StringCollectionStartEndWithTest()
    {
        string[] strs = new[] { "2333", "233333333333333", "2332" };
        Assert.True(strs.HasStartsWith("233"));
        Assert.True(strs.HasEndsWith("33"));
        Assert.False(strs.AllStartsWith("2333"));
        Assert.False(strs.AllEndsWith("33"));

        strs = new[] { "ABCD", "AbcD", "abcd" };
        Assert.True(strs.HasStartsWith("aBcd", true));
        Assert.False(strs.HasStartsWith("aBcd"));
        Assert.True(strs.AllStartsWith("aBcd", true));
        Assert.True(strs.HasEndsWith("aBcd", true));
        Assert.False(strs.HasEndsWith("aBcd"));
        Assert.True(strs.AllEndsWith("aBcd", true));
    }

    [Theory]
    [InlineData("")]
    [InlineData("", "default")]
    [InlineData(null)]
    [InlineData(null, "default")]
    public void NullTest(string value, string defaultValue = "")
    {
        Assert.True(value.IsNull());
        Assert.True(value.IsEmpty());
        Assert.False(value.NotNull());
        Assert.False(value.NotEmpty());
        if (!string.IsNullOrEmpty(defaultValue))
        {
            Assert.True(value.IsNull(defaultValue) == defaultValue);
            Assert.True(value.IsEmpty(defaultValue) == defaultValue);
        }
    }
    [Theory]
    [InlineData("123")]
    [InlineData(" ")]
    public void NotNullTest(string value)
    {
        Assert.False(value.IsNull());
        Assert.False(value.IsEmpty());
        Assert.True(value.NotNull());
        Assert.True(value.NotEmpty());
    }

    [Theory]
    [InlineData("")]
    [InlineData("", "default")]
    [InlineData(" ")]
    [InlineData(" ", "default")]
    [InlineData("               ")]
    [InlineData("               ", "default")]
    public void WhiteTest(string value, string defaultValue = "")
    {
        Assert.True(value.IsWhite());
        Assert.False(value.NotWhite());
        if (!string.IsNullOrEmpty(defaultValue))
            Assert.True(value.IsWhite(defaultValue) == defaultValue);
    }
    [Theory]
    [InlineData("123")]
    public void NotWhiteTest(string value)
    {
        Assert.True(value.NotWhite());
        Assert.False(value.IsWhite());
    }
    [Theory]
    [InlineData("   233", 233, 6)]
    [InlineData("---233", 233, 6, '-')]
    [InlineData("@@@233", 233, 6, '@')]
    public void PadLeftTest(string result, int iValue, int len, char fill = ' ')
    {
        Assert.True(iValue.PadLeft(len, fill) == result);
    }
    [Theory]
    [InlineData("233   ", 233, 6)]
    [InlineData("233---", 233, 6, '-')]
    [InlineData("233@@@", 233, 6, '@')]
    public void PadRightTest(string result, int iValue, int len, char fill = ' ')
    {
        Assert.True(iValue.PadRight(len, fill) == result);
    }

    [Theory]
    [InlineData("   466", 233, 6)]
    [InlineData("---466", 233, 6, '-')]
    [InlineData("@@@466", 233, 6, '@')]
    public void PadLeftWithFuncTest(string result, int iValue, int len, char fill = ' ')
    {
        var func = (int item) => (item + item).ToString();
        Assert.True(iValue.PadLeft(len, func, fill) == result);
    }
    [Theory]
    [InlineData("466   ", 233, 6)]
    [InlineData("466---", 233, 6, '-')]
    [InlineData("466@@@", 233, 6, '@')]
    public void PadRightWithFuncTest(string result, int iValue, int len, char fill = ' ')
    {
        var func = (int item) => (item + item).ToString();
        Assert.True(iValue.PadRight(len, func, fill) == result);
    }



    [Fact]
    public void GetDomainTest()
    {
        string domain = "https://www.github.com";
        Assert.True(domain.GetDomain() == "www.github.com");
        string notDomain = "htasdafsgzzcvnxfxttps://www.github.com";
        Assert.False(notDomain.GetDomain() == "www.github.com");
    }

    [Fact]
    public void AutoMaskTest()
    {
        string origin = "1";
        var data = origin.AutoMask();
        Assert.True(data == "#*1");
        origin = "CollapseNav.Net.Tool";
        data = origin.AutoMask();
        Assert.True(data == "Col*ool");
        data = "".AutoMask();
        Assert.True(data == "***");
    }

    [Fact]
    public async Task Base64ImageTest()
    {
        var imagePath = "./vscode.png";
        var imagePath2 = "./vscode-64.png";
        var imagePath3 = "./vscode-642.png";
        var fs = imagePath.OpenReadShareStream();
        var base64String = fs.ImageToBase64();
        var stream = base64String.Base64ImageToStream();
        Assert.True(stream.Sha1En() == fs.Sha1En());
        Assert.True(imagePath.ImageToBase64() == base64String);
        fs.Dispose();
        base64String.Base64ImageSaveTo(imagePath2);
        await base64String.Base64ImageSaveToAsync(imagePath3);
        fs = imagePath.OpenReadShareStream();
        var fs2 = imagePath2.OpenReadShareStream();
        var fs3 = imagePath3.OpenReadShareStream();

        Assert.True(fs.Md5En() == fs2.Md5En());
        Assert.True(fs3.Md5En() == fs2.Md5En());

        fs.Dispose();
        fs2.Dispose();
        fs3.Dispose();
    }

    [Fact]
    public void TakeFirstLastTest()
    {
        string str = "12345678987654321";
        Assert.True(str.First(5) == "12345");
        Assert.True(str.Last(5) == "54321");
    }

    [Fact]
    public void ContainTest()
    {
        string temp = "123456789";
        Assert.True(temp.AllContain(new[] { "123", "345" }));
        Assert.True(temp.AllContain("789", "567"));
        Assert.True(temp.HasContain(new[] { "234", "444" }));
        Assert.True(temp.HasContain("456", "898"));

        Assert.False(temp.AllContain("789", "5679"));
        Assert.False(temp.HasContain("7899", "5679"));
        temp = "ABCD";
        Assert.True(temp.AllContain(new[] { "ABc", "cD" }, ignoreCase: true));
        Assert.False(temp.AllContain(new[] { "ABc", "cD" }, ignoreCase: false));
        Assert.True(temp.HasContain(new[] { "ABc", "CD" }, ignoreCase: true));
        Assert.True(temp.HasContain(new[] { "ABc", "CD" }, ignoreCase: false));
        Assert.False(temp.HasContain(new[] { "ABcE", "cD" }, ignoreCase: false));
    }

    [Fact]
    public void HexStringTest()
    {
        string str = "123456789";
        var hexStr = str.ToBytes().ToHexString();
        Assert.True(str == hexStr.HexToBytes().BytesToString());
    }

    [Fact]
    public void AddBeginTest()
    {
        string[] strs = new[] { "2", "3" };
        strs.AddBegin("999");
        Assert.True(strs.AllStartsWith("999"));
        List<string> strList = new() { "4", "5" };
        strList.AddBegin("233");
        Assert.True(strList.AllStartsWith("233"));
        IEnumerable<string> strEnums = new[] { "6", "7" };
        var newStrEnums = strEnums.AddBegin("777");
        Assert.True(newStrEnums.AllStartsWith("777"));
        Assert.False(strEnums.AllStartsWith("777"));
    }

    [Fact]
    public void AddEndTest()
    {
        string[] strs = new[] { "2", "3" };
        strs.AddEnd("999");
        Assert.True(strs.AllEndsWith("999"));
        List<string> strList = new() { "4", "5" };
        strList.AddEnd("233");
        Assert.True(strList.AllEndsWith("233"));
        IEnumerable<string> strEnums = new[] { "6", "7" };
        var newStrEnums = strEnums.AddEnd("777");
        Assert.True(newStrEnums.AllEndsWith("777"));
        Assert.False(strEnums.AllEndsWith("777"));
    }
    [Fact]
    public void FirstToTest()
    {
        string value = "123456789.2333";
        Assert.True(value.FirstTo(".") == "123456789");
        Assert.True(value.FirstTo('.') == "123456789");
    }

    [Fact]
    public void EndToTest()
    {
        string value = "123456789.2333";
        Assert.True(value.EndTo(".") == "2333");
        Assert.True(value.EndTo('.') == "2333");
    }

    [Fact]
    public void ToUpperFirstTest()
    {
        string data = "collapsenav.net.tool";
        Assert.True(data.ToUpperFirst() == "Collapsenav.net.tool");
        Assert.True(data.ToUpperFirst(3) == "COLlapsenav.net.tool");
        Assert.True(data.ToUpperFirst(data.Length) == "COLLAPSENAV.NET.TOOL");
    }

    [Fact]
    public void ToLowerFirstTest()
    {
        string data = "COLLAPSENAV.NET.TOOL";
        Assert.True(data.ToLowerFirst() == "cOLLAPSENAV.NET.TOOL");
        Assert.True(data.ToLowerFirst(3) == "colLAPSENAV.NET.TOOL");
        Assert.True(data.ToLowerFirst(data.Length) == "collapsenav.net.tool");
    }

    [Fact]
    public void ToUpperEndTest()
    {
        string data = "collapsenav.net.tool";
        Assert.True(data.ToUpperEnd() == "collapsenav.net.tooL");
        Assert.True(data.ToUpperEnd(3) == "collapsenav.net.tOOL");
        Assert.True(data.ToUpperEnd(data.Length) == "COLLAPSENAV.NET.TOOL");
    }

    [Fact]
    public void ToLowerEndTest()
    {
        string data = "COLLAPSENAV.NET.TOOL";
        Assert.True(data.ToLowerEnd() == "COLLAPSENAV.NET.TOOl");
        Assert.True(data.ToLowerEnd(3) == "COLLAPSENAV.NET.Tool");
        Assert.True(data.ToLowerEnd(data.Length) == "collapsenav.net.tool");
    }

    [Theory]
    [InlineData("collapsenav.net.tool", "Collapsenav.Net.Tool")]
    [InlineData("collapsenav net tool", "Collapsenav Net Tool")]
    public void UpFirstLetterTest(string origin, string result)
    {
        Assert.True(origin.UpFirstLetter() == result);
    }

    [Theory]
    [InlineData("123", "789", 9, "123   789")]
    [InlineData("123", "789", 3, "123789")]
    public void PadWithTest(string origin, string value, int len, string result)
    {
        Assert.True(origin.PadWith(value, len) == result);
        if (origin.Length + value.Length <= len)
            Assert.True(origin.PadWith(value, len).Length == len);
        else
            Assert.True(origin.PadWith(value, len).Length == origin.Length + value.Length);
    }
}
