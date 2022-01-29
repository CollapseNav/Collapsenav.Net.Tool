using System;
using System.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;

public class CellReadGetValueTest
{
    #region 测试 直接使用下标 精确获取单元格数据
    [Fact]
    public void EPPlusCellReadIndexTest()
    {
        var path = "./CellRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };

        IExcelCellRead reader = new EPPlusCellRead(path);
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        Assert.True(reader[1, 0].StringValue == "233");
        Assert.True(reader[1, 3].StringValue == "233.33");
        Assert.True(reader[10, 0].StringValue == "1122");
        Assert.True(reader[10, 3].StringValue == "123.23");
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new EPPlusCellRead(fs);

        Assert.True(reader["Field0", 1].StringValue == "233");
        Assert.True(reader["Field3", 1].StringValue == "233.33");
        Assert.True(reader["Field0", 10].StringValue == "1122");
        Assert.True(reader["Field3", 10].StringValue == "123.23");
        reader.Dispose();
    }

    [Fact]
    public void NPOIExcelCellReadIndexTest()
    {
        var path = "./CellRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };

        IExcelCellRead reader = new NPOICellRead(path);
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        Assert.True(reader[1, 0].StringValue == "233");
        Assert.True(reader[1, 3].StringValue == "233.33");
        Assert.True(reader[10, 0].StringValue == "1122");
        Assert.True(reader[10, 3].StringValue == "123.23");
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new NPOICellRead(fs);

        Assert.True(reader["Field0", 1].StringValue == "233");
        Assert.True(reader["Field3", 1].StringValue == "233.33");
        Assert.True(reader["Field0", 10].StringValue == "1122");
        Assert.True(reader["Field3", 10].StringValue == "123.23");
        reader.Dispose();
    }

    [Fact]
    public void MinIExcelCellReadIndexTest()
    {
        var path = "./CellRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };

        IExcelCellRead reader = new MiniCellRead(path);
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        Assert.True(reader[1, 0].StringValue == "233");
        Assert.True(reader[1, 3].StringValue == "233.33");
        Assert.True(reader[10, 0].StringValue == "1122");
        Assert.True(reader[10, 3].StringValue == "123.23");
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new MiniCellRead(fs);

        Assert.True(reader["Field0", 1].StringValue == "233");
        Assert.True(reader["Field3", 1].StringValue == "233.33");
        Assert.True(reader["Field0", 10].StringValue == "1122");
        Assert.True(reader["Field3", 10].StringValue == "123.23");
        reader.Dispose();
    }
    #endregion


    #region 测试获取 行 列
    [Fact]
    public void EPPlusExcelRowAndFieldTest()
    {
        var path = "./CellRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelCellRead reader = new EPPlusCellRead(path);
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));

        Assert.True(reader[0].Select(item => item.StringValue).SequenceEqual(realHeader));
        Assert.True(reader[1].Select(item => item.StringValue).SequenceEqual(row1Data));
        Assert.True(reader[10].Select(item => item.StringValue).SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new EPPlusCellRead(fs);

        var field0Data = new[] { "Field0", "233", "1122", "233", "1122", "233", "1122", "233", "1122", "233", "1122" };
        var field3Data = new[] { "Field3", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23" };

        Assert.True(reader["Field0"].Select(item => item.StringValue).SequenceEqual(field0Data));
        Assert.True(reader["Field3"].Select(item => item.StringValue).SequenceEqual(field3Data));
        reader.Dispose();
    }

    [Fact]
    public void NPOIExcelRowAndFieldTest()
    {
        var path = "./CellRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelCellRead reader = new NPOICellRead(path);
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));

        Assert.True(reader[0].Select(item => item.StringValue).SequenceEqual(realHeader));
        Assert.True(reader[1].Select(item => item.StringValue).SequenceEqual(row1Data));
        Assert.True(reader[10].Select(item => item.StringValue).SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new NPOICellRead(fs);

        var field0Data = new[] { "Field0", "233", "1122", "233", "1122", "233", "1122", "233", "1122", "233", "1122" };
        var field3Data = new[] { "Field3", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23" };

        Assert.True(reader["Field0"].Select(item => item.StringValue).SequenceEqual(field0Data));
        Assert.True(reader["Field3"].Select(item => item.StringValue).SequenceEqual(field3Data));
        reader.Dispose();
    }

    [Fact]
    public void MiniExcelRowAndFieldTest()
    {
        var path = "./CellRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelCellRead reader = new MiniCellRead(path);
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));

        Assert.True(reader[0].Select(item => item.StringValue).SequenceEqual(realHeader));
        Assert.True(reader[1].Select(item => item.StringValue).SequenceEqual(row1Data));
        Assert.True(reader[10].Select(item => item.StringValue).SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new MiniCellRead(fs);

        var field0Data = new[] { "Field0", "233", "1122", "233", "1122", "233", "1122", "233", "1122", "233", "1122" };
        var field3Data = new[] { "Field3", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23" };

        Assert.True(reader["Field0"].Select(item => item.StringValue).SequenceEqual(field0Data));
        Assert.True(reader["Field3"].Select(item => item.StringValue).SequenceEqual(field3Data));
        reader.Dispose();
    }
    #endregion


    #region  简单测试 实现的IEnumerable
    [Fact]
    public void EPPlusExcelEnumerableTest()
    {
        var path = "./CellRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelCellRead reader = new EPPlusCellRead(path);
        Assert.True(reader.Count() == 11);
        Assert.True(reader.First().Select(item => item.StringValue).SequenceEqual(realHeader));

        Assert.True(reader.Skip(1).First().Select(item => item.StringValue).SequenceEqual(row1Data));
        Assert.True(reader.Last().Select(item => item.StringValue).SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new EPPlusCellRead(fs);

        Assert.True(reader.Reverse().First().Select(item => item.StringValue).SequenceEqual(row10Data));
        Assert.True(reader.SkipLast(1).Last().Select(item => item.StringValue).SequenceEqual(row1Data));

        reader.Dispose();
    }

    [Fact]
    public void NPOIExcelEnumerableTest()
    {
        var path = "./CellRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelCellRead reader = new NPOICellRead(path);
        Assert.True(reader.Count() == 11);
        Assert.True(reader.First().Select(item => item.StringValue).SequenceEqual(realHeader));

        Assert.True(reader.Skip(1).First().Select(item => item.StringValue).SequenceEqual(row1Data));
        Assert.True(reader.Last().Select(item => item.StringValue).SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new NPOICellRead(fs);

        Assert.True(reader.Reverse().First().Select(item => item.StringValue).SequenceEqual(row10Data));
        Assert.True(reader.SkipLast(1).Last().Select(item => item.StringValue).SequenceEqual(row1Data));

        reader.Dispose();
    }

    [Fact]
    public void MiniExcelEnumerableTest()
    {
        var path = "./CellRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };

        IExcelCellRead reader = new MiniCellRead(path);
        Assert.True(reader.Count() == 11);
        Assert.True(reader.First().Select(item => item.StringValue).SequenceEqual(realHeader));

        Assert.True(reader.Skip(1).First().Select(item => item.StringValue).SequenceEqual(row1Data));
        Assert.True(reader.Last().Select(item => item.StringValue).SequenceEqual(row10Data));
        reader.Dispose();

        using var fs = path.OpenReadShareStream();
        reader = new MiniCellRead(fs);

        Assert.True(reader.Reverse().First().Select(item => item.StringValue).SequenceEqual(row10Data));
        Assert.True(reader.SkipLast(1).Last().Select(item => item.StringValue).SequenceEqual(row1Data));

        reader.Dispose();
    }
    #endregion
}