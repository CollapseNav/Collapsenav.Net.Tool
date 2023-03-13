using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;
public class ExportConfigTest
{
    /// <summary>
    /// 测试默认配置
    /// </summary>
    [Fact]
    public async Task DefaultConfigTest()
    {
        var path = "./Export-DefaultExcel.xlsx";
        var export = ExportConfig<ExcelDefaultDto>.GenDefaultConfig();
        var read = ReadConfig<ExcelDefaultDto>.GenDefaultConfig();

        Assert.True(export.FieldOption.Count() == 3);
        var stream = await export.NPOIExportAsync(path, ExcelDefaultDto.ExportData());
        var data = await read.NPOIToEntityAsync(stream);
        TestData(data);

        data = await read.MiniToEntityAsync(stream);
        TestData(data);
        await stream.DisposeAsync();

        File.Delete(path);
        stream = await export.EPPlusExportAsync(path, ExcelDefaultDto.ExportData());
        data = await read.EPPlusToEntityAsync(stream);
        TestData(data);

        data = await read.NPOIToEntityAsync(stream);
        TestData(data);

        data = await read.MiniToEntityAsync(stream);
        TestData(data);
        await stream.DisposeAsync();

        static void TestData(System.Collections.Generic.IEnumerable<ExcelDefaultDto> data)
        {
            Assert.True(data.Count() == 10);
            Assert.True(data.OrderBy(item => item.Field1).ToList().SequenceEqual(ExcelDefaultDto.ExportData().OrderBy(item => item.Field1).ToList(), item => item.Field0.GetHashCode() ^ item.Field1.GetHashCode() ^ item.Field3.GetHashCode()));
        }
    }

    /// <summary>
    /// 测试自定义Add添加配置
    /// </summary>
    [Fact]
    public async Task AddCellOptionTest()
    {
        var path = "./Export-TestExcel.xlsx";
        var read = ReadConfig<ExcelTestDto>.GenDefaultConfig();
        var export = new ExportConfig<ExcelTestDto>(ExcelTestDto.ExportData())
        .Add("Field0", item => item.Field0 + "233")
        .AddIf(true, "Field1", item => item.Field1)
        .Add("Field2", item => item.Field2)
        .AddIf(true, "Field3", item => item.Field3)
        ;
        var stream = await export.NPOIExportAsync(path);
        var data = await read.NPOIToEntityAsync(stream);
        TestData(data);

        data = await read.MiniToEntityAsync(stream);
        TestData(data);
        await stream.DisposeAsync();

        File.Delete(path);
        stream = await export.EPPlusExportAsync(path, ExcelTestDto.ExportData());
        data = await read.EPPlusToEntityAsync(stream);
        TestData(data);

        data = await read.NPOIToEntityAsync(stream);
        TestData(data);

        data = await read.MiniToEntityAsync(stream);
        TestData(data);
        await stream.DisposeAsync();

        static void TestData(System.Collections.Generic.IEnumerable<ExcelTestDto> data)
        {
            Assert.True(data.Count() == 10);
            Assert.True(data.All(item => item.Field0.AllEndsWith("233")));
            Assert.True(data.OrderBy(item => item.Field1).ToList().SequenceEqual(ExcelTestDto.ExportData().OrderBy(item => item.Field1).ToList(), item => item.Field1.GetHashCode() ^ item.Field2.GetHashCode() ^ item.Field3.GetHashCode()));
        }
    }

    /// <summary>
    /// 测试完全使用字符串添加的配置
    /// </summary>
    [Fact]
    public async Task StringCellOptionTest()
    {
        var path = "./Export-StringExcel.xlsx";
        var read = ReadConfig<ExcelDefaultDto>.GenDefaultConfig();
        var config = new ExportConfig<ExcelDefaultDto>(ExcelDefaultDto.ExportData())
        .Add("Field0", "Field0")
        .Add("Field1", "Field1")
        .Add("Field3", "Field3")
        .AddIf(false, "Field3", "Field3")
        ;
        Assert.True(config.FieldOption.Count() == 3);
        var stream = await config.NPOIExportAsync(path);
        var data = await read.NPOIToEntityAsync(stream);
        TestData(data);

        data = await read.MiniToEntityAsync(stream);
        TestData(data);
        await stream.DisposeAsync();

        File.Delete(path);
        stream = await config.EPPlusExportAsync(path);
        data = await read.NPOIToEntityAsync(stream);
        TestData(data);

        data = await read.EPPlusToEntityAsync(stream);
        TestData(data);

        data = await read.MiniToEntityAsync(stream);
        TestData(data);
        await stream.DisposeAsync();

        static void TestData(System.Collections.Generic.IEnumerable<ExcelDefaultDto> data)
        {
            Assert.True(data.Count() == 10);
            Assert.True(data.OrderBy(item => item.Field1).ToList().SequenceEqual(ExcelDefaultDto.ExportData().OrderBy(item => item.Field1).ToList(), item => item.Field1.GetHashCode() ^ item.Field0.GetHashCode() ^ item.Field3.GetHashCode()));
        }
    }

    /// <summary>
    /// 测试根据注解生成的配置
    /// </summary>
    [Fact]
    public async Task AttributeConfigTest()
    {
        var path = "./Export-AttributeExcel.xlsx";
        var read = ReadConfig<ExcelAttrDto>.GenDefaultConfig();
        var export = ExportConfig<ExcelAttrDto>.GenDefaultConfig(ExcelAttrDto.ExportData());
        Assert.True(export.FieldOption.Count() == 3);
        var stream = await export.NPOIExportAsync(path);
        var data = await read.NPOIToEntityAsync(stream);
        TestData(data);

        data = await read.MiniToEntityAsync(stream);
        TestData(data);
        await stream.DisposeAsync();

        File.Delete(path);
        stream = await export.EPPlusExportAsync(path);
        data = await read.NPOIToEntityAsync(stream);
        TestData(data);

        data = await read.NPOIToEntityAsync(stream);
        TestData(data);

        data = await read.MiniToEntityAsync(stream);
        TestData(data);
        await stream.DisposeAsync();

        static void TestData(System.Collections.Generic.IEnumerable<ExcelAttrDto> data)
        {
            Assert.True(data.Count() == 10);
            Assert.True(data.OrderBy(item => item.Field1).ToList().SequenceEqual(ExcelAttrDto.ExportData().OrderBy(item => item.Field1).ToList(), item => item.Field1.GetHashCode() ^ item.Field0.GetHashCode() ^ item.Field3.GetHashCode()));
        }
    }

    [Fact]
    public async Task ExportToStreamTest()
    {
        var path = "./ExportTest-To-Stream.xlsx";
        var read = ReadConfig<ExcelTestDto>.GenDefaultConfig();
        var config = new ExportConfig<ExcelTestDto>(ExcelTestDto.ExportData())
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2)
        .Add("Field3", item => item.Field3)
        ;
        var fs = path.OpenCreateReadWriteShareStream();
        _ = await config.EPPlusExportAsync(fs);
        fs.Dispose();
        var data = await read.MiniToEntityAsync(path);
        Assert.True(data.Count() == 10);
        Assert.True(data.OrderBy(item => item.Field1).ToList().SequenceEqual(ExcelTestDto.ExportData().OrderBy(item => item.Field1).ToList(), item => item.Field1.GetHashCode() ^ item.Field2.GetHashCode() ^ item.Field3.GetHashCode()));

        fs = path.OpenCreateReadWriteShareStream();
        _ = await config.NPOIExportAsync(fs);
        fs.Dispose();
        data = await read.MiniToEntityAsync(path);
        Assert.True(data.Count() == 10);
        Assert.True(data.OrderBy(item => item.Field1).ToList().SequenceEqual(ExcelTestDto.ExportData().OrderBy(item => item.Field1).ToList(), item => item.Field1.GetHashCode() ^ item.Field2.GetHashCode() ^ item.Field3.GetHashCode()));
    }


    [Fact]
    public async Task ExportAsStreamTest()
    {
        var path = "./ExportTest-As-Stream.xlsx";
        var read = ReadConfig<ExcelTestDto>.GenDefaultConfig();
        var config = new ExportConfig<ExcelTestDto>(ExcelTestDto.ExportData())
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2)
        .Add("Field3", item => item.Field3)
        ;
        var fs = await config.EPPlusExportAsync();
        await fs.SaveToAsync(path);
        fs.Dispose();
        var data = await read.MiniToEntityAsync(path);
        Assert.True(data.Count() == 10);
        Assert.True(data.OrderBy(item => item.Field1).ToList().SequenceEqual(ExcelTestDto.ExportData().OrderBy(item => item.Field1).ToList(), item => item.Field1.GetHashCode() ^ item.Field2.GetHashCode() ^ item.Field3.GetHashCode()));

        fs = await config.NPOIExportAsync();
        await fs.SaveToAsync(path);
        fs.Dispose();
        data = await read.MiniToEntityAsync(path);
        Assert.True(data.Count() == 10);
        Assert.True(data.OrderBy(item => item.Field1).ToList().SequenceEqual(ExcelTestDto.ExportData().OrderBy(item => item.Field1).ToList(), item => item.Field1.GetHashCode() ^ item.Field2.GetHashCode() ^ item.Field3.GetHashCode()));
    }

    [Fact]
    public void ExportHeaderTest()
    {
        var path = "./HeaderTest.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var config = new ExportConfig<ExcelTestDto>()
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2)
        .Add("Field3", item => item.Field3)
        ;
        _ = config.EPPlusExportHeader(path);
        IExcelCellReader reader = IExcelCellReader.GetCellReader(path, ExcelType.MiniExcel);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        reader.Dispose();

        _ = config.NPOIExportHeader(path);
        reader = IExcelCellReader.GetCellReader(path, ExcelType.MiniExcel);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        reader.Dispose();
    }

    [Fact]
    public void ExportHeaderToStreamTest()
    {
        var path = "./HeaderTest-To-Stream.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var config = new ExportConfig<ExcelTestDto>()
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2)
        .Add("Field3", item => item.Field3)
        ;
        var fs = path.OpenCreateReadWriteShareStream();
        _ = config.EPPlusExportHeader(fs);
        IExcelCellReader reader = IExcelCellReader.GetCellReader(path, ExcelType.MiniExcel);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        reader.Dispose();
        fs.Dispose();

        fs = path.OpenCreateReadWriteShareStream();
        _ = config.NPOIExportHeader(fs);
        reader = IExcelCellReader.GetCellReader(path, ExcelType.MiniExcel);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        reader.Dispose();
        fs.Dispose();
    }

    [Fact]
    public void ExportHeaderAsStreamTest()
    {
        var path = "./HeaderTest-As-Stream.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var config = new ExportConfig<ExcelTestDto>()
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2)
        .Add("Field3", item => item.Field3)
        ;
        var fs = config.EPPlusExportHeader();
        fs.SaveTo(path);
        fs.Dispose();
        IExcelCellReader reader = IExcelCellReader.GetCellReader(path, ExcelType.MiniExcel);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        reader.Dispose();

        fs = config.NPOIExportHeader();
        fs.SaveTo(path);
        fs.Dispose();
        reader = IExcelCellReader.GetCellReader(path, ExcelType.MiniExcel);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        reader.Dispose();
    }


}
