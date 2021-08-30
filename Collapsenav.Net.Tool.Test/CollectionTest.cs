using Xunit;
using System.Linq;

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

        [Fact]
        public void UniqueTest()
        {
            int[] intList = { 1, 1, 2, 2, 3, 3, 4 };
            var uniqueIntList = intList.Unique(item => item);
            Assert.True(new[] { 1, 2, 3, 4 }.ContainAnd(uniqueIntList.ToArray()));
            Assert.True(uniqueIntList.ContainAnd(new[] { 1, 2, 3, 4 }));
            Assert.True(uniqueIntList.Count() == 4);
        }

        [Fact]
        public void WhereIfTest()
        {
            int[] intList = { 1, 1, 2, 2, 3, 3, 4 };
            intList = intList.WhereIf(intList.Length == 7, item => item > 1).ToArray();
            Assert.True(intList.Length == 5);
        }

        [Fact]
        public void RemoveRepeatTest()
        {
            int[] intList = { 1, 1, 2, 2, 3, 3, 4 };
            intList = intList.RemoveRepeat(new[] { 2, 3, 4 }).ToArray();
            Assert.True(intList.Length == 1);
            Assert.True(intList.First() == 1);
        }
    }
}
