using System.Collections.Generic;
using Xunit;

namespace Collapsenav.Net.Tool.Test;
public class DictionaryTest
{
    [Fact]
    public void AddOrUpdateTest()
    {
        Dictionary<int, string> dict = new();
        dict.AddOrUpdate(new KeyValuePair<int, string>(1, "1"))
        .AddOrUpdate(new KeyValuePair<int, string>(1, "1"))
        .AddOrUpdate(1, "1");
        Assert.True(dict.Count == 1);
    }


    [Fact]
    public void AddRangeTest()
    {
        Dictionary<int, string> dict = new();
        Dictionary<int, string> dict2 = new()
        {
            { 1, "1" },
            { 2, "2" },
            { 3, "3" },
        };
        dict.AddRange(dict2);
        Assert.True(dict.Count == 3);
        List<KeyValuePair<int, string>> value = new()
        {
            new(1, "1"),
            new(2, "2"),
            new(3, "3"),
            new(4, "4"),
            new(5, "5"),
        };
        dict.AddRange(value);
        Assert.True(dict.Count == 5);
    }

    [Fact]
    public void ToDictionaryTest()
    {
        List<KeyValuePair<int, string>> value = new()
        {
            new(1, "1"),
            new(2, "2"),
            new(3, "3"),
        };
        IDictionary<int, string> dict = value.ToDictionary();
        Assert.True(dict[1] == "1");
        Assert.True(dict[3] == "3");

        string[] nums = new[] { "1", "2", "3", "4", "5" };
        dict = nums.ToDictionary(item => int.Parse(item));
        Assert.True(dict[5] == "5");
    }

    [Fact]
    public void GetAndRemoveTest()
    {
        var dict = new Dictionary<int, string>()
        .AddOrUpdate(1, "1")
        .AddOrUpdate(2, "2")
        .AddOrUpdate(3, "4")
        ;

        var value = dict.GetAndRemove(1);
        Assert.True(value == "1");
        Assert.True(dict.Count == 2);
    }
    [Fact]
    public void PopTest()
    {
        var dict = new Dictionary<int, string>()
        .AddOrUpdate(1, "1")
        .AddOrUpdate(2, "2")
        .AddOrUpdate(3, "4")
        ;

        var value = dict.Pop(1);
        Assert.True(value == "1");
        Assert.True(dict.Count == 2);
    }

    [Fact]
    public void DeconstructTest()
    {
        Dictionary<int, string> dict = new()
        {
            { 1, "1" },
            { 2, "2" },
            { 3, "3" },
        };
        foreach (var (value, index) in dict.Deconstruct())
            Assert.True(value.ToInt() == index);

        foreach (var (value, index) in dict.Deconstruct(value => value.ToInt(), key => key))
        {
            Assert.True(value is int);
            Assert.True(value == index);
        }
    }
}

