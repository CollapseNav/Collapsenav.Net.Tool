using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;
public class EPPlusExcelOperationTest
{
    [Fact]
    public void HeaderTest()
    {
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };

        var headers = EPPlusExcelReadTool.ExcelHeader($@"./TestExcel.xlsx");
        Assert.True(headers.SequenceEqual(realHeader));

        using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open, FileAccess.Read, FileShare.Read);
        headers = EPPlusExcelReadTool.ExcelHeader(fs);
        Assert.True(headers.SequenceEqual(realHeader));
    }

    [Fact]
    public async Task ExportTest()
    {
        using FileStream fs = $@"./TestExcel.xlsx".OpenReadShareStream();
        var config = new ReadConfig<ExcelTestDto>()
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2, item => item == "Male")
        .Add("Field3", item => item.Field3)
        ;
        var datas = await config.EPPlusToEntityAsync(fs);

        var exportConfig = new ExportConfig<ExcelTestDto>(datas)
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2 ? "Male" : "Female")
        .Add("Field3", item => item.Field3)
        ;
        using var headerMs = new MemoryStream();
        var headerStream = await exportConfig.EPPlusExportHeaderAsync(headerMs);
        using var exportHeaderFs = "./EPPlus-Export-Header.xlsx".OpenWriteShareStream();
        headerStream.CopyTo(exportHeaderFs);
        exportHeaderFs.Dispose();
        Assert.True(File.Exists("./EPPlus-Export-Header.xlsx"));
        Assert.True(new FileInfo("./EPPlus-Export-Header.xlsx").Length > 0);

        using var entityMs = new MemoryStream();
        var entityStream = await exportConfig.EPPlusExportAsync(entityMs);
        using var exportEntityFs = "./EPPlus-Export-Entity.xlsx".OpenWriteShareStream();
        entityMs.CopyTo(exportEntityFs);
        exportEntityFs.Dispose();
        Assert.True(File.Exists("./EPPlus-Export-Entity.xlsx"));
        Assert.True(new FileInfo("./EPPlus-Export-Entity.xlsx").Length > 0);
    }
    [Fact]
    public async Task DefaultConfigTest()
    {
        var read = ReadConfig<ExcelDefaultDto>.GenDefaultConfig();
        Assert.True(read.FieldOption.Count() == 3);
        var data = await read.EPPlusToEntityAsync("./DefaultExcel.xlsx");
        Assert.True(data.Count() == 20);

        var export = ExportConfig<ExcelDefaultDto>.GenDefaultConfig(data);
        await export.EPPlusExportAsync("./EPPlus-DefaultExportExcel.xlsx");
        Assert.True(File.Exists("./EPPlus-DefaultExportExcel.xlsx"));
        Assert.True(new FileInfo("./EPPlus-DefaultExportExcel.xlsx").Length > 0);
        File.Delete("./EPPlus-DefaultExportExcel.xlsx");
    }

    [Fact]
    public async Task AttributeConfigTest()
    {
        var read = ReadConfig<ExcelAttrDto>.GenDefaultConfig();
        Assert.True(read.FieldOption.Count() == 3);
        var data = await read.EPPlusToEntityAsync("./AttributeExcel.xlsx");
        Assert.True(data.Count() == 20);

        var export = ExportConfig<ExcelAttrDto>.GenDefaultConfig(data);
        await export.EPPlusExportAsync("./EPPlus-AttributeExportExcel.xlsx");
        Assert.True(File.Exists("./EPPlus-AttributeExportExcel.xlsx"));
        Assert.True(new FileInfo("./EPPlus-AttributeExportExcel.xlsx").Length > 0);
        File.Delete("./EPPlus-AttributeExportExcel.xlsx");
    }

    [Fact]
    public async Task ExportByConfig()
    {
        IEnumerable<ExcelConfigDto> data = new[] {
                new ExcelConfigDto{Num=1,Name="Name-1",Flag=true,Time = DateTime.Now.AddDays(1)},
                new ExcelConfigDto{Num=2,Name="Name-2",Flag=true,Time = DateTime.Now.AddDays(2)},
                new ExcelConfigDto{Num=3,Name="Name-3",Flag=true,Time = DateTime.Now.AddDays(3)},
                new ExcelConfigDto{Num=4,Name="Name-4",Flag=true,Time = DateTime.Now.AddDays(4)},
                new ExcelConfigDto{Num=5,Name="Name-5",Flag=true,Time = DateTime.Now.AddDays(5)},
                new ExcelConfigDto{Num=6,Name="Name-6",Flag=true,Time = DateTime.Now.AddDays(6)},
                new ExcelConfigDto{Num=7,Name="Name-7",Flag=true,Time = DateTime.Now.AddDays(7)},
                new ExcelConfigDto{Num=8,Name="Name-8",Flag=true,Time = DateTime.Now.AddDays(8)},
                new ExcelConfigDto{Num=9,Name="Name-9",Flag=true,Time = DateTime.Now.AddDays(9)},
                new ExcelConfigDto{Num=10,Name="Name-10",Flag=true,Time = DateTime.Now.AddDays(10)},
            };
        var exportConfig = new ExportConfig<ExcelConfigDto>(data)
        .Add("Num", "Num")
        .Add("Name", "Name")
        .Add("Flag", "Flag")
        .Add("Time", "Time")
        ;
        await exportConfig.EPPlusExportAsync("./EPPlus-Config-Export.xlsx");

        var readConfig = new ReadConfig<ExcelConfigDto>()
        .Add("Num", "Num")
        .Add("Name", "Name")
        .Add("Flag", "Flag")
        .Add("Time", "Time")
        ;
        var readData = await readConfig.EPPlusToEntityAsync("./EPPlus-Config-Export.xlsx");
        Assert.True(readData.Count() == 10);
        Assert.True(readData.OrderBy(item => item.Num).SequenceEqual(data, (x, y) => x.Name == y.Name && x.Num == y.Num && x.Flag == y.Flag && x.Time.ToString() == y.Time.ToString()));
    }
}
