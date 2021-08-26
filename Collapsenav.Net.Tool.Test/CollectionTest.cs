using System;
using Xunit;
using Collapsenav.Net.Tool;

namespace Collapsenav.Net.Tool.Test
{
    public class CollectionTest
    {
        [Fact]
        public void ContainerAndTest()
        {
            string[] strList = { "1", "2", "3", "4", "5", "6" };
            Assert.True(strList.ContainAnd(new[] { "2", "6" }));
            Assert.False(strList.ContainAnd(new[] { "2", "8" }));
        }

        [Fact]
        public void ContainerOrTest()
        {
            string[] strList = { "1", "2", "3", "4", "5", "6" };
            Assert.True(strList.ContainOr(new[] { "2", "6" }));
            Assert.True(strList.ContainOr(new[] { "2", "8" }));
            Assert.False(strList.ContainOr(new[] { "7", "8" }));
        }
    }
}
