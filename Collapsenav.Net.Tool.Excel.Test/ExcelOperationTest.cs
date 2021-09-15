using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public void HeaderTest()
        {
            var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);
            var headers = ExcelReadOperation.GetExcelHeader(fs);
            Assert.NotEmpty(headers);
            Assert.True(realHeader.All(header => headers.Contains(header)));
        }

        [Fact]
        public async Task DataTest()
        {
            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);
            var datas = await ExcelReadOperation.GetExcelDataAsync(fs);
            Assert.NotEmpty(datas);
            Assert.True(datas.Count() == 3000);
        }

        [Fact]
        public async Task ConvertTest()
        {
            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);
            var config = new ReadConfig<ExcelTestDto>()
            .Add("Field0", item => item.Field0)
            .Add("Field1", item => item.Field1)
            .Add("Field2", item => item.Field2, item => item == "Male")
            .Add("Field3", item => item.Field3)
            ;
            var datas = await ExcelReadOperation.ExcelToEntity(fs, config);

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
            var datas = await ExcelReadOperation.GenExcelDataByOptionsAsync(fs, config);
            Assert.NotEmpty(datas);
            Assert.True(datas.Count() == 3001);
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
            var datas = await ExcelReadOperation.ExcelToEntity(fs, config);

            var exportConfig = new ExportConfig<ExcelTestDto>(datas)
            .Add("Field0", item => item.Field0)
            .Add("Field1", item => item.Field1)
            .Add("Field2", item => item.Field2 ? "Male" : "Female")
            .Add("Field3", item => item.Field3)
            ;

            var stream = await ExcelExportOperation.ExportHeaderAsync("./fs_header.xlsx", exportConfig);
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
            var datas = await ExcelReadOperation.ExcelToEntity(fs, config);

            var exportConfig = new ExportConfig<ExcelTestDto>(datas)
            .Add("Field0", item => item.Field0)
            .Add("Field1", item => item.Field1)
            .Add("Field2", item => item.Field2 ? "Male" : "Female")
            .Add("Field3", item => item.Field3)
            ;

            var stream = await ExcelExportOperation.ExportDataAsync("./fs_data.xlsx", exportConfig);
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
            var datas = await ExcelReadOperation.ExcelToEntity(fs, config);

            var exportConfig = new ExportConfig<ExcelTestDto>(datas)
            .Add("Field0", item => item.Field0)
            .Add("Field1", item => item.Field1)
            .Add("Field2", item => item.Field2 ? "Male" : "Female")
            .Add("Field3", item => item.Field3)
            ;

            var stream = await ExcelExportOperation.ExportAsync("./fs.xlsx", exportConfig);
            Assert.True(File.Exists("./fs.xlsx"));
        }
    }
}
