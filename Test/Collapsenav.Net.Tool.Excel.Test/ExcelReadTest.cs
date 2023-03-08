using System;
using System.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;

public class ExcelReadTest
{
    private readonly string path = "./ExcelRead.xlsx";
    #region 测试 直接使用下标 精确获取单元格数据
    [Fact]
    public void EPPlusExcelReadIndexTest()
    {
        IExcelReader reader = new EPPlusExcelReader(path);
        IndexTest1(reader);
        reader.Dispose();
        using var fs = path.OpenReadShareStream();
        reader = new EPPlusExcelReader(fs);
        IndexTest2(reader);
        reader.Dispose();
    }
    [Fact]
    public void NPOIExcelReadIndexTest()
    {
        IExcelReader reader = new NPOIExcelReader(path);
        IndexTest1(reader);
        reader.Dispose();
        using var fs = path.OpenReadShareStream();
        reader = new NPOIExcelReader(fs);
        IndexTest2(reader);
        reader.Dispose();
    }
    [Fact]
    public void MiniExcelReadIndexTest()
    {
        IExcelReader reader = new MiniExcelReader(path);
        IndexTest1(reader);
        reader.Dispose();
        using var fs = path.OpenReadShareStream();
        reader = new MiniExcelReader(fs);
        IndexTest2(reader);
        reader.Dispose();
    }
    private void IndexTest1(IExcelReader reader)
    {
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        Assert.True(reader[1, 0] == "233");
        Assert.True(reader[1, 3] == "233.33");
        Assert.True(reader[10, 0] == "1122");
        Assert.True(reader[10, 3] == "123.23");
    }
    private void IndexTest2(IExcelReader reader)
    {
        Assert.True(reader["Field0", 1] == "233");
        Assert.True(reader["Field3", 1] == "233.33");
        Assert.True(reader["Field0", 10] == "1122");
        Assert.True(reader["Field3", 10] == "123.23");
    }
    #endregion


    #region 测试获取 行 列
    [Fact]
    public void EPPlusExcelRowAndFieldTest()
    {
        IExcelReader reader = new EPPlusExcelReader(path);
        RowAndFieldTest1(reader);
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new EPPlusExcelReader(fs);
        RowAndFieldTest2(reader);
        reader.Dispose();
    }
    [Fact]
    public void NPOIExcelRowAndFieldTest()
    {
        IExcelReader reader = new NPOIExcelReader(path);
        RowAndFieldTest1(reader);
        reader.Dispose();
        using var fs = path.OpenReadShareStream();
        reader = new NPOIExcelReader(fs);
        RowAndFieldTest2(reader);
        reader.Dispose();
    }
    [Fact]
    public void MiniExcelRowAndFieldTest()
    {
        IExcelReader reader = new MiniExcelReader(path);
        RowAndFieldTest1(reader);
        reader.Dispose();
        using var fs = path.OpenReadShareStream();
        reader = new MiniExcelReader(fs);
        RowAndFieldTest2(reader);
        reader.Dispose();
    }
    private void RowAndFieldTest1(IExcelReader reader)
    {
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));

        Assert.True(reader[0].SequenceEqual(realHeader));
        Assert.True(reader[1].SequenceEqual(row1Data));
        Assert.True(reader[10].SequenceEqual(row10Data));
    }
    private void RowAndFieldTest2(IExcelReader reader)
    {
        var field0Data = new[] { "Field0", "233", "1122", "233", "1122", "233", "1122", "233", "1122", "233", "1122" };
        var field3Data = new[] { "Field3", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23" };
        Assert.True(reader["Field0"].SequenceEqual(field0Data));
        Assert.True(reader["Field3"].SequenceEqual(field3Data));
    }
    #endregion


    #region  简单测试 实现的IEnumerable
    [Fact]
    public void EPPlusExcelEnumerableTest()
    {
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelReader reader = new EPPlusExcelReader(path);
        EnumerableTest1(reader);
        Assert.True(reader.Count() == 11);
        Assert.True(reader.First().SequenceEqual(realHeader));

        Assert.True(reader.Skip(1).First().SequenceEqual(row1Data));
        Assert.True(reader.Last().SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new EPPlusExcelReader(fs);

        Assert.True(reader.Reverse().First().SequenceEqual(row10Data));
        Assert.True(reader.SkipLast(1).Last().SequenceEqual(row1Data));

        reader.Dispose();
    }
    [Fact]
    public void NPOIExcelEnumerableTest()
    {
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelReader reader = new NPOIExcelReader(path);
        EnumerableTest1(reader);
        Assert.True(reader.Count() == 11);
        Assert.True(reader.First().SequenceEqual(realHeader));

        Assert.True(reader.Skip(1).First().SequenceEqual(row1Data));
        Assert.True(reader.Last().SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new NPOIExcelReader(fs);

        Assert.True(reader.Reverse().First().SequenceEqual(row10Data));
        Assert.True(reader.SkipLast(1).Last().SequenceEqual(row1Data));

        reader.Dispose();
    }
    [Fact]
    public void MiniExcelEnumerableTest()
    {
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelReader reader = new MiniExcelReader(path);
        EnumerableTest1(reader);
        Assert.True(reader.Count() == 11);
        Assert.True(reader.First().SequenceEqual(realHeader));

        Assert.True(reader.Skip(1).First().SequenceEqual(row1Data));
        Assert.True(reader.Last().SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new MiniExcelReader(fs);

        Assert.True(reader.Reverse().First().SequenceEqual(row10Data));
        Assert.True(reader.SkipLast(1).Last().SequenceEqual(row1Data));

        reader.Dispose();
    }
    private void EnumerableTest1(IExcelReader reader)
    {
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };
        Assert.True(reader.Count() == 11);
        Assert.True(reader.First().SequenceEqual(realHeader));

        Assert.True(reader.Skip(1).First().SequenceEqual(row1Data));
        Assert.True(reader.Last().SequenceEqual(row10Data));
    }
    #endregion

    [Fact]
    public void CollapseNavNetToolCollectionExtTest()
    {
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };
        IExcelReader reader = new MiniExcelReader(path);
        var mergeData = reader.Merge();
        Assert.True(mergeData.Count() == 44);
        Assert.True(mergeData.Take(4).SequenceEqual(realHeader));
        Assert.True(mergeData.Skip(4).Take(4).SequenceEqual(row1Data));
        Assert.True(mergeData.TakeLast(4).SequenceEqual(row10Data));
    }
}