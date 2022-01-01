using System.Text;
using Xunit;

namespace Collapsenav.Net.Tool.Test;
    public class StringBuilderTest
    {
        [Fact]
        public void AddIfTest()
        {
            StringBuilder sb = new StringBuilder()
            .AddIf(true, "1")
            .AddIf(true, "2")
            .AddIf("3")
            .AddIf(false, "4")
            .AddIf("5", "6")
            ;
            Assert.True("1236" == sb.ToString());
        }

        [Fact]
        public void AndIfTest()
        {
            StringBuilder sb = new StringBuilder()
            .AndIf(true, "1")
            .AndIf(false, "2")
            .AndIf("3")
            .AndIf(false, "4")
            .AndIf("5", "6")
            ;
            Assert.True(" AND 1 AND 3 AND 6" == sb.ToString());
        }

        [Fact]
        public void AndOrTest()
        {
            StringBuilder sb = new StringBuilder()
            .OrIf(true, "1")
            .OrIf(false, "2")
            .OrIf("3")
            .OrIf(false, "4")
            .OrIf("5", "6")
            ;
            Assert.True(" OR 1 OR 3 OR 6" == sb.ToString());
        }
}
