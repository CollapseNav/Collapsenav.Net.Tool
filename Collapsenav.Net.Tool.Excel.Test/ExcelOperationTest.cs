using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test
{

    public class ExcelTestDto
    {
        public string Field0 { get; set; }
        public int Field1 { get; set; }
        public bool Field2 { get; set; }
        public double Field3 { get; set; }
    }
    public class ExcelOperationTest
    {
        public ExcelOperationTest() { }

        [Fact]
        public async Task HeaderTest()
        {
            var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);
            var headers = EPPlusExcelReadTool.GetExcelHeader(fs);
            Assert.NotEmpty(headers);
            Assert.True(realHeader.All(header => headers.Contains(header)));
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
            var headerDict = EPPlusExcelReadTool.GetExcelHeaderByOptions<ExcelTestDto>(fs, config.ReadOption);
            var configHeader = new[] { "Field0", "Field1" };
            Assert.NotEmpty(headers);
            Assert.True(configHeader.All(header => headerDict.ContainsKey(header)));
            fs.Close();
        }

        [Fact]
        public async Task DataTest()
        {
            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);
            var datas = await EPPlusExcelReadTool.GetExcelDataAsync(fs);
            Assert.NotEmpty(datas);
            Assert.True(datas.Count() == 3000);
        }

        [Fact]
        public async Task ConvertExcelTest()
        {
            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);
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
            var datas = await EPPlusExcelReadTool.ExcelToEntityAsync(fs, config);
            Assert.NotEmpty(datas);
            Assert.True(datas.Count() == 3000);
            datas = await EPPlusExcelReadTool.ExcelToEntityAsync(fs, config.ReadOption, config.Init);
            Assert.NotEmpty(datas);
            Assert.True(datas.Count() == 3000);
        }

        [Fact]
        public async Task OperationDataTest()
        {
            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);
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
            var datas = await EPPlusExcelReadTool.GetExcelDataByOptionsAsync(fs, config);
            Assert.NotEmpty(datas);
            Assert.True(datas.Length == 3000);
            datas = await EPPlusExcelReadTool.GetExcelDataByOptionsAsync<ExcelTestDto>(fs, config.ReadOption);
            Assert.NotEmpty(datas);
            Assert.True(datas.Length == 3000);
        }

        [Fact]
        public async Task ExportHeaderTest()
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

            var stream = await exportConfig.EPPlusExportHeaderAsync("./fs_header.xlsx");
            Assert.True(File.Exists("./fs_header.xlsx"));
        }

        [Fact]
        public async Task ExportDataTest()
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
            var stream = await exportConfig.EPPlusExportDataAsync("./fs_data.xlsx");
            Assert.True(File.Exists("./fs_data.xlsx"));
        }

        [Fact]
        public async Task ExportAllTest()
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

            var stream = await exportConfig.EPPlusExportAsync("./fs.xlsx");
            Assert.True(File.Exists("./fs.xlsx"));
        }
    }
}
