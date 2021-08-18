using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Collapsenav.Net.Tool.Excel.Test
{

    public class ExcelTestDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public double Height { get; set; }
    }
    public class ExcelOperationTest
    {

        public ExcelOperationTest() { }
        [Fact]
        public void HeaderTest()
        {
            var realHeader = new[] { "Name", "Age", "Gender", "Height" };
            using FileStream fs = new($@"../../../TestExcel.xlsx", FileMode.Open);
            var headers = ExcelOperation.GetExcelHeader(fs);
            Assert.NotEmpty(headers);
            Assert.True(realHeader.All(header => headers.Contains(header)));
        }

        [Fact]
        public async Task DataTest()
        {
            using FileStream fs = new($@"../../../TestExcel.xlsx", FileMode.Open);

            var datas = await ExcelOperation.GetExcelDataAsync(fs);
            Assert.NotEmpty(datas);
            Assert.True(datas.Count() == 100);
        }

        [Fact]
        public async Task ConvertTest()
        {
            using FileStream fs = new($@"../../../TestExcel.xlsx", FileMode.Open);

            var config = new ReadConfig<ExcelTestDto>()
            .Require("姓名", item => item.Name, null)
            .Add("年龄", item => item.Age)
            .Default(item => item.Height, 233)
            .AddInit(item =>
            {
                item.Name += "hhhhhhhh";
                item.Gender = false;
                return item;
            })
            ;
            var datas = await ExcelOperation.ExcelToEntity(fs, config);
            Assert.NotEmpty(datas);
            Assert.True(datas.Count() == 100);
        }
    }
}
