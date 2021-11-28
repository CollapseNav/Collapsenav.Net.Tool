using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        [Fact]
        public void PingTest()
        {
            string url = "https://www.bilibili.com/";
            Assert.True(url.CanPing());
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
            Assert.True(StringExt.HasStartsWith(exampleString, "23"));
            Assert.True(exampleString.AllStartsWith("23", "233", "233333"));
            Assert.False(StringExt.HasStartsWith(exampleString, "2233"));
            Assert.True(exampleString.AllEndsWith("333333", "33", "3"));
            Assert.False(StringExt.HasEndsWith(exampleString, "2333333"));
        }

        [Fact]
        public void NullOrEmptyTest()
        {
            string empty = "";
            string notEmpty = "NotEmpty";
            string whiteSpace = "   ";
            Assert.True(empty.IsNull() && empty.IsEmpty());
            Assert.True(notEmpty.NotEmpty() && notEmpty.NotNull());
            Assert.True(whiteSpace.IsEmpty());
            Assert.False(empty.NotEmpty());
            Assert.False(whiteSpace.NotNull());
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
    }
}
