using System;
using System.Net.Http.Headers;
using Xunit;

namespace Collapsenav.Net.Tool.Test
{
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
        }

        [Fact]
        public void EmailTest()
        {
            string emailString = "collapsenav@163.com";
            Assert.True(emailString.IsEmail());
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
        public void StartEndWiths()
        {
            string exampleString = "23333333333333";
            Assert.True(StringExt.StartsWith(exampleString, "23"));
            Assert.True(exampleString.StartsWith("23", "233", "233333"));
            Assert.False(StringExt.StartsWith(exampleString, "2233"));
            Assert.True(exampleString.EndsWith("333333", "33", "3"));
            Assert.False(StringExt.EndsWith(exampleString, "2333333"));
        }
    }
}
