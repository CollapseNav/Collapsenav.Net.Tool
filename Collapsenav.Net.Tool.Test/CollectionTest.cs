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
            Assert.False(new[] { 1, 2, 3, 4 }.Except(uniqueIntList).Any());
            Assert.True(uniqueIntList.Count() == 4);
        }

        [Fact]
        public void WhereIfTest()
        {
            int[] intList = { 1, 1, 2, 2, 3, 3, 4 };
            intList = intList.WhereIf(true, item => item > 1)
            .WhereIf(false, item => item < 3)
            .ToArray();
            Assert.False(new[] { 2, 2, 3, 3, 4 }.Except(intList).Any());
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

        [Fact]
        public void MergeCollectionTest()
        {
            string[] strs = new[] { "CollapseNav", "Net", "Tool" };
            var mergeList = strs.Select(str => str.Select(s => s)).Merge();
            Assert.True(mergeList.Count() == 18);
            Assert.True(mergeList.Join("") == "CollapseNavNetTool");

            mergeList = strs.Select(str => str.Select(s => s)).Merge(strs.Select(str => str.Select(s => s)));
            Assert.True(mergeList.Count() == 36);
            Assert.True(mergeList.Join("") == "CollapseNavNetToolCollapseNavNetTool");

            string str = "CollapseNavNetTool";
            mergeList = strs.Select(str => str.Select(s => s)).Merge(str.Select(item => item));
            Assert.True(mergeList.Count() == 36);
            Assert.True(mergeList.Join("") == "CollapseNavNetToolCollapseNavNetTool");
        }
    }
}
