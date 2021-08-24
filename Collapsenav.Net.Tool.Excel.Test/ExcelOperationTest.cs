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
        public void HeaderTest()
        {
            var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);
            var headers = ExcelOperation.GetExcelHeader(fs);
            Assert.NotEmpty(headers);
            Assert.True(realHeader.All(header => headers.Contains(header)));
        }

        [Fact]
        public async Task DataTest()
        {
            using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open);

            var datas = await ExcelOperation.GetExcelDataAsync(fs);
            Assert.NotEmpty(datas);
            Assert.True(datas.Count() == 3000);
        }

        [Fact]
        public async Task ConvertTest()
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
            var datas = await ExcelOperation.ExcelToEntity(fs, config);
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
            var datas = await ExcelOperation.GenExcelDataByOptionsAsync(fs, config);
            Assert.NotEmpty(datas);
            Assert.True(datas.Count() == 3001);
        }
    }
}
