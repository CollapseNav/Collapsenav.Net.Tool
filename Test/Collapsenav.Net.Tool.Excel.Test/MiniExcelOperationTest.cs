using System;
using System.IO;
using System.Linq;
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

        using FileStream fs = $@"./TestExcel.xlsx".OpenReadShareStream();
        headers = MiniExcelReadTool.ExcelHeader(fs);
        Assert.True(headers.SequenceEqual(realHeader));
    }
}
