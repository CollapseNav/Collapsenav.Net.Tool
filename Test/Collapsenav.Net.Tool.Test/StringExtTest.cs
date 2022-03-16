using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Collapsenav.Net.Tool.Test;
public class StringExtTest
{
    [Fact]
    public void NumToChineseTest()
    {
        string num = "10510001010";
        Assert.True("一百零五亿一千万零一千零一十" == num.ToChinese());
        num = "1010";
        Assert.True("一千零一十" == num.ToChinese());
        num = "99999";
        Assert.True("九万九千九百九十九" == num.ToChinese());
        num = "203300010001";
        Assert.True("二千零三十三亿零一万零一" == num.ToChinese());
        num = "9999999999999999";
        Assert.True("九千九百九十九万九千九百九十九亿九千九百九十九万九千九百九十九" == num.ToChinese());
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
    public void JoinTest()
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

    [Fact]
    public void EmailTest()
    {
        string emailString = "collapsenav@163.com";
        Assert.True(emailString.IsEmail());
        Assert.False("".IsEmail());
    }

    [Fact]
    public void PingTest()
    {
        string url = "https://www.bilibili.com/";
        Assert.True(url.CanPing(2000));
    }

    [Fact]
    public void UrlTest()
    {
        string url = "https://www.bilibili.com/";
        Assert.True(url.IsUrl());
        Assert.True(url.IsUrl(true));
        url = "httttttps://www.bilibili.com/";
        Assert.False(url.IsUrl());
    }

    [Fact]
    public void StringStartEndWiths()
    {
        string exampleString = "23333333333333";
        Assert.True(exampleString.HasStartsWith("23"));
        Assert.True(exampleString.HasStartsWith(new[] { "23" }.AsEnumerable()));
        Assert.True(exampleString.AllStartsWith("23", "233", "233333"));
        Assert.True(exampleString.AllStartsWith(new[] { "23", "233", "233333" }.AsEnumerable()));
        Assert.False(exampleString.HasStartsWith("2233"));
        Assert.False(exampleString.HasStartsWith(new[] { "2233" }.AsEnumerable()));
        Assert.True(exampleString.AllEndsWith("333333", "33", "3"));
        Assert.True(exampleString.AllEndsWith(new[] { "333333", "33", "3" }.AsEnumerable()));
        Assert.False(exampleString.HasEndsWith("2333333"));
        Assert.False(exampleString.HasEndsWith(new[] { "2333333" }.AsEnumerable()));
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

    [Fact]
    public void NullTest()
    {
        string nullString = null;
        string notNull = "NotNull";

        Assert.True(nullString.IsNull());
        Assert.True(notNull.NotNull());

        Assert.False(notNull.IsNull());
        Assert.False(nullString.NotNull());

        Assert.True(nullString.IsNull("233") == "233");
        Assert.True(notNull.IsNull("233") == "NotNull");
    }

    [Fact]
    public void EmptyTest()
    {
        string emptyString = "";
        string notEmpty = "NotEmpty";

        Assert.True(emptyString.IsEmpty());
        Assert.True(notEmpty.NotEmpty());

        Assert.False(notEmpty.IsEmpty());
        Assert.False(emptyString.NotEmpty());

        Assert.True(emptyString.IsEmpty("233") == "233");
        Assert.True(notEmpty.IsEmpty("233") == "NotEmpty");
    }

    [Fact]
    public void WhiteTest()
    {
        string whiteString = "   ";
        string notWhite = "NotWhite";

        Assert.True(whiteString.IsWhite());
        Assert.True(notWhite.NotWhite());

        Assert.False(notWhite.IsWhite());
        Assert.False(whiteString.NotWhite());

        Assert.True(whiteString.IsWhite("233") == "233");
        Assert.True(notWhite.IsWhite("233") == "NotWhite");
    }

    [Fact]
    public void PadLeftAndRightTest()
    {
        int iValue = 233;
        double dValue = 2.33;
        long lValue = 23333;

        Assert.True(iValue.PadLeft(6) == "   233");
        Assert.True(dValue.PadLeft(6) == "  2.33");
        Assert.True(lValue.PadLeft(6) == " 23333");

        Assert.True(iValue.PadLeft(6, '-') == "---233");
        Assert.True(dValue.PadLeft(6, '-') == "--2.33");
        Assert.True(lValue.PadLeft(6, '-') == "-23333");

        Assert.True(iValue.PadRight(6) == "233   ");
        Assert.True(dValue.PadRight(6) == "2.33  ");
        Assert.True(lValue.PadRight(6) == "23333 ");

        Assert.True(iValue.PadRight(6, '-') == "233---");
        Assert.True(dValue.PadRight(6, '-') == "2.33--");
        Assert.True(lValue.PadRight(6, '-') == "23333-");

        Assert.True(iValue.PadLeft(6, item => item + 1, '-') == "---234");
        Assert.True(dValue.PadLeft(6, item => item + 1, '-') == "--3.33");
        Assert.True(lValue.PadLeft(6, item => item + 1, '-') == "-23334");

        Assert.True(iValue.PadRight(6, item => item + 1, '-') == "234---");
        Assert.True(dValue.PadRight(6, item => item + 1, '-') == "3.33--");
        Assert.True(lValue.PadRight(6, item => item + 1, '-') == "23334-");
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
}
