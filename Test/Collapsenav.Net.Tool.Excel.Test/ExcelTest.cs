using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;
public class ExcelTest
{
    [Fact]
    public async Task OneLineReadTest()
    {
        var data = await ReadConfig<ExcelDefaultDto>.ExcelToEntityAsync("./TestExcel.xlsx");
        Assert.True(data.Count() == 3000);
        data = await ExcelReadTool.ExcelToEntityAsync<ExcelDefaultDto>("./TestExcel.xlsx");
        Assert.True(data.Count() == 3000);
    }

    [Fact]
    public async Task OneLineExportTest()
    {
        IEnumerable<ExcelDefaultDto> exportData = new[] {
            new ExcelDefaultDto{Field0="Name-1",Field1=1,Field3= 1},
            new ExcelDefaultDto{Field0="Name-2",Field1=2,Field3= 2},
            new ExcelDefaultDto{Field0="Name-3",Field1=3,Field3= 3},
            new ExcelDefaultDto{Field0="Name-4",Field1=4,Field3= 4},
            new ExcelDefaultDto{Field0="Name-5",Field1=5,Field3= 5},
            new ExcelDefaultDto{Field0="Name-6",Field1=6,Field3= 6},
            new ExcelDefaultDto{Field0="Name-7",Field1=7,Field3= 7},
            new ExcelDefaultDto{Field0="Name-8",Field1=8,Field3= 8},
            new ExcelDefaultDto{Field0="Name-9",Field1=9,Field3= 9},
            new ExcelDefaultDto{Field0="Name-10",Field1=10,Field3= 10}
        };
        var filePath = "./TestExport.xlsx";
        var headerPath = "./TestHeaderExport.xlsx";
        await ExportConfig<ExcelDefaultDto>.ConfigExportHeaderAsync(headerPath);
        await ExportConfig<ExcelDefaultDto>.DataExportAsync(filePath, exportData);
        var data = await ReadConfig<ExcelDefaultDto>.ExcelToEntityAsync(filePath);
        Assert.True(data.Count() == 10);


        await ExcelExportTool.DataExportAsync(filePath, exportData);
        data = await ReadConfig<ExcelDefaultDto>.ExcelToEntityAsync(filePath);
        Assert.True(data.Count() == 10);

        if (File.Exists(filePath))
            File.Delete(filePath);
        if (File.Exists(headerPath))
            File.Delete(headerPath);
    }

    [Fact]
    public async Task ConfigDefaultOperationTest()
    {
        IEnumerable<ExcelDefaultDto> exportData = new[] {
            new ExcelDefaultDto{Field0="Name-1",Field1=1,Field3= 1},
            new ExcelDefaultDto{Field0="Name-2",Field1=2,Field3= 2},
            new ExcelDefaultDto{Field0="Name-3",Field1=3,Field3= 3},
            new ExcelDefaultDto{Field0="Name-4",Field1=4,Field3= 4},
            new ExcelDefaultDto{Field0="Name-5",Field1=5,Field3= 5},
            new ExcelDefaultDto{Field0="Name-6",Field1=6,Field3= 6},
            new ExcelDefaultDto{Field0="Name-7",Field1=7,Field3= 7},
            new ExcelDefaultDto{Field0="Name-8",Field1=8,Field3= 8},
            new ExcelDefaultDto{Field0="Name-9",Field1=9,Field3= 9},
            new ExcelDefaultDto{Field0="Name-10",Field1=10,Field3= 10}
        };
        var exportConfig = new ExportConfig<ExcelDefaultDto>(exportData)
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field3", item => item.Field3)
        ;
        var filePath = "./TestExport.xlsx";
        await exportConfig.ExportAsync(filePath);
        var readConfig = new ReadConfig<ExcelDefaultDto>()
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field3", item => item.Field3)
        ;
        var data = await readConfig.ToEntityAsync(filePath);
        Assert.True(data.OrderBy(item => item.Field1).SequenceEqual(exportData, (x, y) => x.Field0 == y.Field0 && x.Field1 == y.Field1 && x.Field3 == y.Field3));
    }
}
