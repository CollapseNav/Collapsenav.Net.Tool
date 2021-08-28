using System;
using Xunit;
using Collapsenav.Net.Tool;
using System.Reflection.Emit;

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
    }
}
