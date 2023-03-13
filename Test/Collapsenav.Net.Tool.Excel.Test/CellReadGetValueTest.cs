using System;
using System.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;

public class CellReadGetValueTest
{
    private readonly string path = "./CellRead.xlsx";
    #region 测试 直接使用下标 精确获取单元格数据
    [Fact]
    public void EPPlusCellReadIndexTest()
    {
        IExcelCellReader reader = new EPPlusCellReader(path);
        IndexTest1(reader);
        reader.Dispose();

        using var fs = path.OpenReadWriteShareStream();
        reader = new EPPlusCellReader(fs);
        IndexTest2(reader);
        reader.Dispose();
    }

    [Fact]
    public void NPOIExcelCellReadIndexTest()
    {
        IExcelCellReader reader = new NPOICellReader(path);
        IndexTest1(reader);
        reader.Dispose();

        using var fs = path.OpenReadWriteShareStream();
        reader = new NPOICellReader(fs);
        IndexTest2(reader);
        reader.Dispose();
    }

    [Fact]
    public void MinIExcelCellReadIndexTest()
    {
        IExcelCellReader reader = new MiniCellReader(path);
        IndexTest1(reader);
        reader.Dispose();

        using var fs = path.OpenReadWriteShareStream();
        reader = new MiniCellReader(fs);
        IndexTest2(reader);
        reader.Dispose();
    }
    private void IndexTest1(IExcelCellReader reader)
    {
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        Assert.True(reader[1, 0].StringValue == "233");
        Assert.True(reader[1, 0].Row == 1);
        Assert.True(reader[1, 0].Col == 0);
        Assert.True(reader[1, 3].StringValue == "233.33");
        Assert.True(reader[10, 0].StringValue == "1122");
        Assert.True(reader[10, 3].StringValue == "123.23");
        Assert.True(reader[10, 3].Row == 10);
        Assert.True(reader[10, 3].Col == 3);
        Assert.True(reader[10, 3].ValueType == typeof(double));
    }
    private void IndexTest2(IExcelCellReader reader)
    {
        Assert.True(reader["Field0", 1].StringValue == "233");
        Assert.True(reader["Field3", 1].StringValue == "233.33");
        Assert.True(reader["Field0", 10].StringValue == "1122");
        Assert.True(reader["Field3", 10].StringValue == "123.23");
    }
    #endregion


    #region 测试获取 行 列
    [Fact]
    public void EPPlusExcelRowAndFieldTest()
    {
        IExcelCellReader reader = new EPPlusCellReader(path);
        RowAndFieldTest1(reader);
        reader.Dispose();

        using var fs = path.OpenReadWriteShareStream();
        reader = new EPPlusCellReader(fs);
        RowAndFieldTest2(reader);
        reader.Dispose();
    }

    [Fact]
    public void NPOIExcelRowAndFieldTest()
    {
        IExcelCellReader reader = new NPOICellReader(path);
        RowAndFieldTest1(reader);
        reader.Dispose();

        using var fs = path.OpenReadWriteShareStream();
        reader = new NPOICellReader(fs);
        RowAndFieldTest2(reader);
        reader.Dispose();
    }

    [Fact]
    public void MiniExcelRowAndFieldTest()
    {
        IExcelCellReader reader = new MiniCellReader(path);
        RowAndFieldTest1(reader);
        reader.Dispose();

        using var fs = path.OpenReadWriteShareStream();
        reader = new MiniCellReader(fs);
        RowAndFieldTest2(reader);
        reader.Dispose();
    }
    private void RowAndFieldTest1(IExcelCellReader reader)
    {
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));

        Assert.True(reader[0].Select(item => item.StringValue).SequenceEqual(realHeader));
        Assert.True(reader[1].Select(item => item.StringValue).SequenceEqual(row1Data));
        Assert.True(reader[10].Select(item => item.StringValue).SequenceEqual(row10Data));
    }

    private void RowAndFieldTest2(IExcelCellReader reader)
    {
        var field0Data = new[] { "Field0", "233", "1122", "233", "1122", "233", "1122", "233", "1122", "233", "1122" };
        var field3Data = new[] { "Field3", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23", "233.33", "123.23" };
        Assert.True(reader["Field0"].Select(item => item.StringValue).SequenceEqual(field0Data));
        Assert.True(reader["Field3"].Select(item => item.StringValue).SequenceEqual(field3Data));
    }
    #endregion


    #region  简单测试 实现的IEnumerable
    [Fact]
    public void EPPlusExcelEnumerableTest()
    {
        IExcelCellReader reader = new EPPlusCellReader(path);
        EnumerableTest1(reader);
        reader.Dispose();

        using var fs = path.OpenReadWriteShareStream();
        reader = new EPPlusCellReader(fs);
        EnumerableTest2(reader);

        reader.Dispose();
    }

    [Fact]
    public void NPOIExcelEnumerableTest()
    {
        IExcelCellReader reader = new NPOICellReader(path);
        EnumerableTest1(reader);
        reader.Dispose();

        using var fs = path.OpenReadWriteShareStream();
        reader = new NPOICellReader(fs);
        EnumerableTest2(reader);

        reader.Dispose();
    }

    [Fact]
    public void MiniExcelEnumerableTest()
    {
        IExcelCellReader reader = new MiniCellReader(path);
        EnumerableTest1(reader);
        reader.Dispose();

        using var fs = path.OpenReadWriteShareStream();
        reader = new MiniCellReader(fs);
        EnumerableTest2(reader);

        reader.Dispose();
    }

    private void EnumerableTest1(IExcelCellReader reader)
    {
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };
        Assert.True(reader.Count() == 11);
        Assert.True(reader.First().Select(item => item.StringValue).SequenceEqual(realHeader));

        Assert.True(reader.Skip(1).First().Select(item => item.StringValue).SequenceEqual(row1Data));
        Assert.True(reader.Last().Select(item => item.StringValue).SequenceEqual(row10Data));
    }

    private void EnumerableTest2(IExcelCellReader reader)
    {
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };
        Assert.True(reader.Reverse().First().Select(item => item.StringValue).SequenceEqual(row10Data));
        Assert.True(reader.SkipLast(1).Last().Select(item => item.StringValue).SequenceEqual(row1Data));
    }
    #endregion

    [Fact]
    public void CollapseNavNetToolCollectionExtTest()
    {
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var row1Data = new[] { "233", "23", "Male", "233.33" };
        var row10Data = new[] { "1122", "12", "Female", "123.23" };
        IExcelCellReader reader = new MiniCellReader(path);
        var mergeData = reader.Merge();
        Assert.True(mergeData.Count() == 44);
        Assert.True(mergeData.Take(4).Select(item => item.StringValue).SequenceEqual(realHeader));
        Assert.True(mergeData.Skip(4).Take(4).Select(item => item.StringValue).SequenceEqual(row1Data));
        Assert.True(mergeData.TakeLast(4).Select(item => item.StringValue).SequenceEqual(row10Data));
    }
}