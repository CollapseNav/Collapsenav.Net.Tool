using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test
{
    public class EPPlusExcelOperationTest
    {
        [Fact]
        public void HeaderTest()
        {
            var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };

            var headers = EPPlusExcelReadTool.ExcelHeader($@"./TestExcel.xlsx");
            Assert.True(headers.SequenceEqual(realHeader));

            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);
            headers = EPPlusExcelReadTool.ExcelHeader(fs);
            Assert.True(headers.SequenceEqual(realHeader));
        }

        [Fact]
        public async Task DataTest()
        {
            var datas = await EPPlusExcelReadTool.ExcelDataAsync($@"./TestExcel.xlsx");
            Assert.True(datas?.Count() == 3000);

            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);
            datas = await EPPlusExcelReadTool.ExcelDataAsync(fs);
            Assert.True(datas?.Count() == 3000);
        }

        [Fact]
        public void OptionHeaderTest()
        {
            var realHeader = new[] { "Field0", "Field1" };

            var config = new ReadConfig<ExcelTestDto>()
            .Require("Field0", item => item.Field0)
            .Add("Field1", item => item.Field1)
            ;
            var headers = config.EPPlusExcelHeaderByOptions($@"./TestExcel.xlsx");
            Assert.True(headers.Select(item => item.Key).SequenceEqual(realHeader));

            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);
            headers = config.EPPlusExcelHeaderByOptions(fs);
            Assert.True(headers.Select(item => item.Key).SequenceEqual(realHeader));
        }

        [Fact]
        public async Task OptionDataTest()
        {
            var config = new ReadConfig<ExcelTestDto>()
            .Require("Field0", item => item.Field0)
            .Add("Field1", item => item.Field1)
            .Default(item => item.Field3, 233)
            .AddInit(item =>
            {
                item.Field0 += "23333";
                item.Field2 = false;
                return item;
            })
            ;

            var datas = await config.EPPlusExcelDataByOptionsAsync($@"./TestExcel.xlsx");
            Assert.True(datas?.Length == 3000);


            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);
            datas = await config.EPPlusExcelDataByOptionsAsync(fs);
            Assert.True(datas?.Length == 3000);
        }

        [Fact]
        public async Task OptionEntityTest()
        {
            var config = new ReadConfig<ExcelTestDto>()
            .Require("Field0", item => item.Field0)
            .Add("Field1", item => item.Field1)
            .Default(item => item.Field3, 233)
            .AddInit(item =>
            {
                item.Field0 += "23333";
                item.Field2 = false;
                return item;
            })
            ;
            var entitys = await config.EPPlusExcelToEntityAsync($@"./TestExcel.xlsx");
            Assert.True(entitys.Count() == 3000);
            Assert.True(entitys.All(item => item.Field2 == false));

            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);
            Assert.True(entitys.Count() == 3000);
            Assert.True(entitys.All(item => item.Field2 == false));
        }

        [Fact]
        public async Task ExportTest()
        {
            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);
            var config = new ReadConfig<ExcelTestDto>()
            .Add("Field0", item => item.Field0)
            .Add("Field1", item => item.Field1)
            .Add("Field2", item => item.Field2, item => item == "Male")
            .Add("Field3", item => item.Field3)
            ;
            var datas = await config.EPPlusExcelToEntityAsync(fs);

            var exportConfig = new ExportConfig<ExcelTestDto>(datas)
            .Add("Field0", item => item.Field0)
            .Add("Field1", item => item.Field1)
            .Add("Field2", item => item.Field2 ? "Male" : "Female")
            .Add("Field3", item => item.Field3)
            ;
            using var headerMs = new MemoryStream();
            var headerStream = await exportConfig.EPPlusExportHeaderAsync(headerMs);
            using var exportHeaderFs = new FileStream("./Export-Header.xlsx", FileMode.OpenOrCreate);
            headerStream.CopyTo(exportHeaderFs);
            exportHeaderFs.Dispose();
            Assert.True(File.Exists("./Export-Header.xlsx"));
            Assert.True(new FileInfo("./Export-Header.xlsx").Length > 0);

            using var dataMs = new MemoryStream();
            var dataStream = await exportConfig.EPPlusExportDataAsync(dataMs);
            using var exportDataFs = new FileStream($@"./Export-Data.xlsx", FileMode.OpenOrCreate);
            dataStream.CopyTo(exportDataFs);
            exportDataFs.Dispose();
            Assert.True(File.Exists("./Export-Data.xlsx"));
            Assert.True(new FileInfo("./Export-Data.xlsx").Length > 0);

            using var entityMs = new MemoryStream();
            var entityStream = await exportConfig.EPPlusExportAsync(entityMs);
            using var exportEntityFs = new FileStream("./Export-Entity.xlsx", FileMode.OpenOrCreate);
            entityMs.CopyTo(exportEntityFs);
            exportEntityFs.Dispose();
            Assert.True(File.Exists("./Export-Entity.xlsx"));
            Assert.True(new FileInfo("./Export-Entity.xlsx").Length > 0);
        }
    }
}
