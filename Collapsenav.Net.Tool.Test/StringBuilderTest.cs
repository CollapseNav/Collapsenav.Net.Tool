using Xunit;
using System.Text;

namespace Collapsenav.Net.Tool.Test
{
    public class StringBuilderTest
    {
        [Fact]
        public void AddIfTest()
        {
            StringBuilder sb = new();
            sb
            .AddIf(true, "1")
            .AddIf(true, "2")
            .AddIf("3")
            .AddIf(false, "4")
            .AddIf("5", "6")
            ;
            Assert.True("1236" == sb.ToString());
        }
    }
}
