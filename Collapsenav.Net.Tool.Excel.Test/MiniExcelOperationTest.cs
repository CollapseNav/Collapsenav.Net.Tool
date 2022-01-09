using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;
public class MiniExcelOperationTest
{
    [Fact]
    public void HeaderTest()
    {
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };

        var headers = MiniExcelReadTool.ExcelHeader($@"./TestExcel.xlsx");
        Assert.True(headers.SequenceEqual(realHeader));

        using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open, FileAccess.Read, FileShare.Read);
        headers = MiniExcelReadTool.ExcelHeader(fs);
        Assert.True(headers.SequenceEqual(realHeader));
    }

    [Fact]
    public void OptionHeaderTest()
    {
        var realHeader = new[] { "Field0", "Field1" };

        var config = new ReadConfig<ExcelTestDto>()
        .Require("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        ;
        var headers = config.MiniExcelHeaderByOptions($@"./TestExcel.xlsx");
        Assert.True(headers.Select(item => item.Key).SequenceEqual(realHeader));

        using FileStream fs = new($@"./TestExcel.xlsx", FileMode.Open, FileAccess.Read, FileShare.Read);
        headers = config.MiniExcelHeaderByOptions(fs);
        Assert.True(headers.Select(item => item.Key).SequenceEqual(realHeader));
    }
}
