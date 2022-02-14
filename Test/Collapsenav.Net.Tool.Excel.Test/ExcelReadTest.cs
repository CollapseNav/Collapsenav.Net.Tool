using System;
using System.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;

public class ExcelReadTest
{
    #region 测试 直接使用下标 精确获取单元格数据
    [Fact]
    public void EPPlusExcelReadIndexTest()
    {
        var path = "./ExcelRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };

        IExcelRead reader = new EPPlusExcelRead(path);
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        Assert.True(reader[1, 0] == "233");
        Assert.True(reader[1, 3] == "233.33");
        Assert.True(reader[10, 0] == "1122");
        Assert.True(reader[10, 3] == "123.23");
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new EPPlusExcelRead(fs);

        Assert.True(reader["Field0", 1] == "233");
        Assert.True(reader["Field3", 1] == "233.33");
        Assert.True(reader["Field0", 10] == "1122");
        Assert.True(reader["Field3", 10] == "123.23");
        reader.Dispose();
    }

    [Fact]
    public void NPOIExcelReadIndexTest()
    {
        var path = "./ExcelRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };

        IExcelRead reader = new NPOIExcelRead(path);
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        Assert.True(reader[1, 0] == "233");
        Assert.True(reader[1, 3] == "233.33");
        Assert.True(reader[10, 0] == "1122");
        Assert.True(reader[10, 3] == "123.23");
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new NPOIExcelRead(fs);

        Assert.True(reader["Field0", 1] == "233");
        Assert.True(reader["Field3", 1] == "233.33");
        Assert.True(reader["Field0", 10] == "1122");
        Assert.True(reader["Field3", 10] == "123.23");
        reader.Dispose();
    }

    [Fact]
    public void MiniExcelReadIndexTest()
    {
        var path = "./ExcelRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };

        IExcelRead reader = new MiniExcelRead(path);
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        Assert.True(reader[1, 0] == "233");
        Assert.True(reader[1, 3] == "233.33");
        Assert.True(reader[10, 0] == "1122");
        Assert.True(reader[10, 3] == "123.23");
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new MiniExcelRead(fs);

        Assert.True(reader["Field0", 1] == "233");
        Assert.True(reader["Field3", 1] == "233.33");
        Assert.True(reader["Field0", 10] == "1122");
        Assert.True(reader["Field3", 10] == "123.23");
        reader.Dispose();
    }
    #endregion


    #region 测试获取 行 列
    [Fact]
    public void EPPlusExcelRowAndFieldTest()
    {
        var path = "./ExcelRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelRead reader = new EPPlusExcelRead(path);
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));

        Assert.True(reader[0].SequenceEqual(realHeader));
        Assert.True(reader[1].SequenceEqual(row1Data));
        Assert.True(reader[10].SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new EPPlusExcelRead(fs);

        var field0Data = new[] { "Field0", "233", "1122", "233", "1122", "233", "1122", "233", "1122", "233", "1122" };
        var field3Data = new[] { "Field3", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23" };

        Assert.True(reader["Field0"].SequenceEqual(field0Data));
        Assert.True(reader["Field3"].SequenceEqual(field3Data));
        reader.Dispose();
    }

    [Fact]
    public void NPOIExcelRowAndFieldTest()
    {
        var path = "./ExcelRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelRead reader = new NPOIExcelRead(path);
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));

        Assert.True(reader[0].SequenceEqual(realHeader));
        Assert.True(reader[1].SequenceEqual(row1Data));
        Assert.True(reader[10].SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new NPOIExcelRead(fs);

        var field0Data = new[] { "Field0", "233", "1122", "233", "1122", "233", "1122", "233", "1122", "233", "1122" };
        var field3Data = new[] { "Field3", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23" };

        Assert.True(reader["Field0"].SequenceEqual(field0Data));
        Assert.True(reader["Field3"].SequenceEqual(field3Data));
        reader.Dispose();
    }

    [Fact]
    public void MiniExcelRowAndFieldTest()
    {
        var path = "./ExcelRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelRead reader = new MiniExcelRead(path);
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));

        Assert.True(reader[0].SequenceEqual(realHeader));
        Assert.True(reader[1].SequenceEqual(row1Data));
        Assert.True(reader[10].SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new MiniExcelRead(fs);

        var field0Data = new[] { "Field0", "233", "1122", "233", "1122", "233", "1122", "233", "1122", "233", "1122" };
        var field3Data = new[] { "Field3", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23" };

        Assert.True(reader["Field0"].SequenceEqual(field0Data));
        Assert.True(reader["Field3"].SequenceEqual(field3Data));
        reader.Dispose();
    }
    #endregion


    #region  简单测试 实现的IEnumerable
    [Fact]
    public void EPPlusExcelEnumerableTest()
    {
        var path = "./ExcelRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelRead reader = new EPPlusExcelRead(path);
        Assert.True(reader.Count() == 11);
        Assert.True(reader.First().SequenceEqual(realHeader));

        Assert.True(reader.Skip(1).First().SequenceEqual(row1Data));
        Assert.True(reader.Last().SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new EPPlusExcelRead(fs);

        Assert.True(reader.Reverse().First().SequenceEqual(row10Data));
        Assert.True(reader.SkipLast(1).Last().SequenceEqual(row1Data));

        reader.Dispose();
    }

    [Fact]
    public void NPOIExcelEnumerableTest()
    {
        var path = "./ExcelRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelRead reader = new NPOIExcelRead(path);
        Assert.True(reader.Count() == 11);
        Assert.True(reader.First().SequenceEqual(realHeader));

        Assert.True(reader.Skip(1).First().SequenceEqual(row1Data));
        Assert.True(reader.Last().SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new NPOIExcelRead(fs);

        Assert.True(reader.Reverse().First().SequenceEqual(row10Data));
        Assert.True(reader.SkipLast(1).Last().SequenceEqual(row1Data));

        reader.Dispose();
    }

    [Fact]
    public void MiniExcelEnumerableTest()
    {
        var path = "./ExcelRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelRead reader = new MiniExcelRead(path);
        Assert.True(reader.Count() == 11);
        Assert.True(reader.First().SequenceEqual(realHeader));

        Assert.True(reader.Skip(1).First().SequenceEqual(row1Data));
        Assert.True(reader.Last().SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new MiniExcelRead(fs);

        Assert.True(reader.Reverse().First().SequenceEqual(row10Data));
        Assert.True(reader.SkipLast(1).Last().SequenceEqual(row1Data));

        reader.Dispose();
    }
    #endregion

    [Fact]
    public void CollapseNavNetToolCollectionExtTest()
    {
        var path = "./ExcelRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };
        IExcelRead reader = new MiniExcelRead(path);
        var mergeData = reader.Merge();
        Assert.True(mergeData.Count() == 44);
        Assert.True(mergeData.Take(4).SequenceEqual(realHeader));
        Assert.True(mergeData.Skip(4).Take(4).SequenceEqual(row1Data));
        Assert.True(mergeData.TakeLast(4).SequenceEqual(row10Data));
    }
}