using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Test;
public class UniqueTestModel
{
    public int Index { get; set; } = 1;
}
public class CollectionTest
{
    [Fact]
    public void UniqueTest()
    {
        int[] intList = { 1, 1, 2, 2, 3, 3, 4 };
        var uniqueIntList = intList.Unique(item => item);
        Assert.True(uniqueIntList.SequenceEqual(new[] { 1, 2, 3, 4 }));

        uniqueIntList = intList.Unique((x, y) => x == y);
        Assert.True(uniqueIntList.SequenceEqual(new[] { 1, 2, 3, 4 }));

        uniqueIntList = intList.Unique();
        Assert.True(uniqueIntList.SequenceEqual(new[] { 1, 2, 3, 4 }));

        var data = new List<UniqueTestModel>{
                new UniqueTestModel(),
                new UniqueTestModel(),
                new UniqueTestModel(),
                new UniqueTestModel(),
                new UniqueTestModel(),
                new UniqueTestModel(),
            };

        var uniqueData = data.Unique();
        Assert.True(uniqueData.Count() == 6);
        uniqueData = data.Unique((x, y) => x.Index == y.Index);
        Assert.True(uniqueData.Count() == 1);
        uniqueData = data.Unique(item => item.Index.GetHashCode());
        Assert.True(uniqueData.Count() == 1);
        uniqueData = data.Unique(item => item.Index);
        Assert.True(uniqueData.Count() == 1);
    }

    [Fact]
    public void EqualTest()
    {
        int[] intList = new[] { 1, 2, 3, 4 };
        int[] intListCopy = new[] { 1, 2, 3, 4 };
        Assert.True(intList.SequenceEqual(intListCopy));

        IEnumerable<UserInfo> userInfos = new List<UserInfo> {
                new UserInfo{UserName="23331",Age=231 },
                new UserInfo{UserName="23332",Age=232 },
                new UserInfo{UserName="23333",Age=233 },
                new UserInfo{UserName="23334",Age=234 },
            };

        IEnumerable<UserInfo> userInfosCopy = new List<UserInfo> {
                new UserInfo{UserName="23331",Age=231 },
                new UserInfo{UserName="23332",Age=232 },
                new UserInfo{UserName="23333",Age=233 },
                new UserInfo{UserName="23334",Age=234 },
            };
        Assert.False(userInfos.SequenceEqual(userInfosCopy));
        Assert.True(userInfos.SequenceEqual(userInfosCopy, (x, y) => x.UserName == y.UserName));
        Assert.True(userInfos.SequenceEqual(userInfosCopy, item => item.Age));
        Assert.True(userInfos.SequenceEqual(userInfosCopy, item => item.Age.GetHashCode()));

        IEnumerable<UserInfo> userInfosCopy2 = new List<UserInfo> {
                new UserInfo{UserName="23331",Age=23 },
                new UserInfo{UserName="23332",Age=23 },
                new UserInfo{UserName="23333",Age=23 },
                new UserInfo{UserName="23334",Age=23 },
            };
        Assert.True(userInfosCopy.SequenceEqual(userInfosCopy2, (x, y) => x.UserName == y.UserName));
        Assert.True(userInfos.SequenceEqual(userInfosCopy2, (x, y) => x.UserName == y.UserName));
        Assert.False(userInfos.SequenceEqual(userInfosCopy2, item => item.Age.GetHashCode()));
        Assert.False(userInfos.SequenceEqual(userInfosCopy2, item => item.Age));
    }

    [Fact]
    public void ContainAndTest()
    {
        string[] strList = { "1", "2", "3", "4", "5", "6" };
        Assert.True(strList.ContainAnd(new[] { "2", "6" }));
        Assert.True(strList.ContainAnd("2", "6"));
        Assert.False(strList.ContainAnd(new[] { "2", "8" }));
        Assert.True(strList.ContainAnd((x, y) => x == y, "2", "6"));
        Assert.False(strList.ContainAnd((x, y) => x == y, "2", "8"));
        Assert.False(strList.AsEnumerable().ContainAnd((x, y) => x == y, new[] { "2", "8" }.AsEnumerable()));
    }

    [Fact]
    public void ContainOrTest()
    {
        string[] strList = { "1", "2", "3", "4", "5", "6" };
        Assert.True(strList.ContainOr(new[] { "2", "6" }));
        Assert.True(strList.ContainOr(new[] { "2", "8" }));
        Assert.False(strList.ContainOr(new[] { "7", "8" }));
        Assert.True(strList.ContainOr((x, y) => x == y, "2", "6"));
        Assert.True(strList.ContainOr((x, y) => x == y, "2", "8"));
        Assert.False(strList.ContainOr((x, y) => x == y, "7", "8"));
        Assert.False(strList.AsEnumerable().ContainOr((x, y) => x == y, new[] { "7", "8" }.AsEnumerable()));
    }


    [Fact]
    public void IEnumerableWhereIfTest()
    {
        int[] intList = { 1, 1, 2, 2, 3, 3, 4 };
        intList = intList.WhereIf(true, item => item > 1)
        .WhereIf(false, item => item < 3)
        .WhereIf("", item => item != 2)
        .ToArray();
        Assert.True(intList.SequenceEqual(new[] { 2, 2, 3, 3, 4 }));
        Assert.False(new[] { 2, 2, 3, 3, 4 }.Except(intList).Any());
        Assert.True(intList.Length == 5);
    }

    [Fact]
    public void IQueryableWhereIfTest()
    {
        int[] intList = { 1, 1, 2, 2, 3, 3, 4 };
        intList = intList.AsQueryable()
        .WhereIf(true, item => item > 1)
        .WhereIf(false, item => item < 3)
        .WhereIf("", item => item != 2)
        .ToArray();
        Assert.True(intList.SequenceEqual(new[] { 2, 2, 3, 3, 4 }));
        Assert.False(new[] { 2, 2, 3, 3, 4 }.Except(intList).Any());
        Assert.True(intList.Length == 5);
    }

    [Fact]
    public void MergeCollectionTest()
    {
        string[] strs = new[] { "CollapseNav", "Net", "Tool" };
        string uniqueString = "ColapseNvtT";
        var strMergeList = strs.Merge();
        Assert.True(strMergeList.Join("") == "CollapseNavNetTool");
        strMergeList = strs.Merge(true);
        Assert.True(strMergeList.Join("") == uniqueString);

        strMergeList = strs.Merge(strs);
        Assert.True(strMergeList.Join("") == "CollapseNavNetToolCollapseNavNetTool");

        strMergeList = strs.Merge(strs, true);
        Assert.True(strMergeList.Join("") == uniqueString);

        string str = "CollapseNavNetTool";
        strMergeList = strs.Merge(str);
        Assert.True(strMergeList.Join("") == "CollapseNavNetToolCollapseNavNetTool");

        strMergeList = strs.Merge(str, true);
        Assert.True(strMergeList.Join("") == uniqueString);

        IEnumerable<int[]> nums = new List<int[]>()
            {
                new[] {1,2,3},
                new[] {4,5,6},
                new[] {7,8,9},
                new[] {10},
            };
        int[] mergeInts = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var numMergeList = nums.Merge();
        Assert.True(numMergeList.SequenceEqual(mergeInts));
        numMergeList = nums.Merge((x, y) => x == y);
        Assert.True(numMergeList.SequenceEqual(mergeInts));

        numMergeList = nums.Take(2).Merge(nums.TakeLast(2));
        Assert.True(numMergeList.SequenceEqual(mergeInts));
        numMergeList = nums.Merge((x, y) => x == y, item => item.GetHashCode());
        Assert.True(numMergeList.SequenceEqual(mergeInts));

        numMergeList = nums.Take(2).Merge(new[] { 7, 8, 9, 10 });
        Assert.True(numMergeList.SequenceEqual(mergeInts));

        nums = null;
        Assert.Null(nums.Merge());
    }

    [Fact]
    public void SpliteCollectionTest()
    {
        var nums = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var numSplit = nums.SpliteCollection(2);
        Assert.True(numSplit.Count() == 5);
        Assert.True(numSplit.First().SequenceEqual(new[] { 1, 2 }));

        numSplit = nums.SpliteCollection(3);
        Assert.True(numSplit.Count() == 4);
        Assert.True(numSplit.First().SequenceEqual(new[] { 1, 2, 3 }));
        Assert.True(numSplit.Last().SequenceEqual(new[] { 10 }));
    }

    [Fact]
    public void EmptyTest()
    {
        string[] nullArray = null;
        Assert.True(nullArray.IsEmpty());
        List<string> emptyList = new();
        Assert.False(emptyList.NotEmpty());
        Assert.True(emptyList.IsEmpty());
        int[] notEmptyArray = new[] { 1, 2, 3, 4 };
        Assert.True(notEmptyArray.NotEmpty());
        Assert.False(notEmptyArray.IsEmpty());
    }

    [Fact]
    public void ShuffleTest()
    {
        var nums = new[] { 1, 2, 3, 4, 5 };
        var oNums = new[] { 1, 2, 3, 4, 5 };
        Assert.True(nums.SequenceEqual(oNums));
        Assert.False(nums.Shuffle().SequenceEqual(oNums));
    }

    [Fact]
    public void IEnumerableAddRangeTest()
    {
        IEnumerable<int> nums = new[] { 1, 2, 3 };
        var enums = nums.AddRange(4, 5);
        Assert.True(enums.Count() == 5);
        enums = nums.AddRange(new[] { 4, 5, 6 });
        Assert.True(enums.Count() == 6);
        enums = nums.AddRange((x, y) => x == y, 3, 4, 5);
        Assert.True(enums.Count() == 5);
        enums = nums.AddRange(x => x.GetHashCode(), 2, 3, 4);
        Assert.True(enums.Count() == 4);

        enums = nums.AddRange(new[] { 4, 5 }.AsEnumerable());
        Assert.True(enums.Count() == 5);
        enums = nums.AddRange(new[] { 4, 5, 6 }.AsEnumerable());
        Assert.True(enums.Count() == 6);
        enums = nums.AddRange((x, y) => x == y, new[] { 3, 4, 5 }.AsEnumerable());
        Assert.True(enums.Count() == 5);
        enums = nums.AddRange(x => x.GetHashCode(), new[] { 2, 3, 4 }.AsEnumerable());
        Assert.True(enums.Count() == 4);
    }

    [Fact]
    public void ICollectionAddRangeTest()
    {
        ICollection<int> nums = new List<int> { 1, 2, 3 };
        nums.AddRange(4, 5, 6);
        Assert.True(nums.Count == 6);
        nums.AddRange(new[] { 4, 5, 6 });
        Assert.True(nums.Count == 9);
        nums.AddRange((x, y) => x == y, 6, 7, 8);
        Assert.True(nums.Count == 11);
        nums.AddRange(x => x.GetHashCode(), 8, 9, 10);
        Assert.True(nums.Count == 13);

        nums.AddRange(new[] { 4, 5, 6 }.AsEnumerable());
        Assert.True(nums.Count == 16);
        nums.AddRange((x, y) => x == y, new[] { 6, 7, 8 }.AsEnumerable());
        Assert.True(nums.Count == 16);
        nums.AddRange(x => x.GetHashCode(), new[] { 8, 9, 10 }.AsEnumerable());
        Assert.True(nums.Count == 16);
        nums.AddRange((x, y) => x == y, x => x.GetHashCode(), new[] { 8, 9, 10 }.AsEnumerable());
        Assert.True(nums.Count == 16);
    }

    [Fact]
    public void ForEachTest()
    {
        var sum = 0;
        int[] nums = new[] { 1, 2, 3 };
        nums.ForEach(item => sum += item);
        Assert.True(sum == 6);
        sum = 0;
        nums.ForEach(item => sum += item * item);
        Assert.True(sum == 14);
    }

    [Fact]
    public void SelectWithIndexTest()
    {
        int[] nums = new[] { 1, 2, 3 };
        foreach (var (item, index) in nums.SelectWithIndex())
            Assert.True(index + 1 == item);

        foreach (var (item, index) in nums.SelectWithIndex(num => num * num))
            Assert.True(index == item * item);
        foreach (var (item, index) in nums.SelectWithIndex(num => num.ToString(), num => num))
        {
            Assert.True(item is string);
            Assert.True(item.ToInt() == index);
        }
    }

    [Fact]
    public void BuildArrayTest()
    {
        int num = 2;
        var array = num.BuildArray();
        Assert.True(array.Length == 1);
        Assert.True(array.All(item => item == 2));
        array = num.BuildArray(10);
        Assert.True(array.Length == 10);
        Assert.True(array.All(item => item == 2));
        array = 233.BuildArray(233);
        Assert.True(array.Length == 233);
        Assert.True(array.All(item => item == 233));
    }
    [Fact]
    public void BuildListTest()
    {
        int num = 2;
        var array = num.BuildList();
        Assert.True(array.Count == 1);
        Assert.True(array.All(item => item == 2));
        array = num.BuildList(10);
        Assert.True(array.Count == 10);
        Assert.True(array.All(item => item == 2));
        array = 233.BuildList(233);
        Assert.True(array.Count == 233);
        Assert.True(array.All(item => item == 233));
    }
}
