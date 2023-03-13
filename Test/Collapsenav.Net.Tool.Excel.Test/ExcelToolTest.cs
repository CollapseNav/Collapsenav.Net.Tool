using System;
using System.Data;
using System.IO;
using System.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;
public class ExcelToolTest
{
    private readonly string path = "./TestExcel.xlsx";
    private void TestExcelTest(IExcelReader reader1, IExcelReader reader2)
    {
        Assert.True(reader1[0, 0] == reader2[0, 0]);
        Assert.True(reader1[0, 1] == reader2[0, 1]);
        Assert.True(reader1[2, 2] == reader2[2, 2]);
        Assert.True(reader1[4, 2] == reader2[4, 2]);
    }
    [Fact]
    public void IsXlsTest()
    {
        Assert.False(path.IsXls());

        try
        {
            _ = "".IsXls();
        }
        catch (Exception ex)
        {
            Assert.True(ex.GetType() == typeof(NoNullAllowedException));
        }
        try
        {
            _ = "dafks.xxx".IsXls();
        }
        catch (Exception ex)
        {
            Assert.True(ex.Message == "文件必须为excel格式");
        }

        try
        {
            _ = "sfaeds.xlsx".IsXls();
        }
        catch (Exception ex)
        {
            Assert.True(ex.GetType() == typeof(FileNotFoundException));
        }
    }

    [Fact]
    public void EPPlusPackageTest()
    {
        using var pack1 = EPPlusTool.EPPlusPackage(path);
        using var stream = path.OpenReadShareStream();
        using var pack2 = EPPlusTool.EPPlusPackage(stream);
        using var reader1 = new EPPlusExcelReader(pack1.Workbook.Worksheets[ExcelTool.EPPlusZero]);
        using var reader2 = new EPPlusExcelReader(pack2.Workbook.Worksheets[ExcelTool.EPPlusZero]);
    }

    [Fact]
    public void EPPlusWorkbookTest()
    {
        using var book1 = EPPlusTool.EPPlusWorkbook(path);
        using var stream = path.OpenReadShareStream();
        using var book2 = EPPlusTool.EPPlusWorkbook(stream);
        using var reader1 = new EPPlusExcelReader(book1.Worksheets[ExcelTool.EPPlusZero]);
        using var reader2 = new EPPlusExcelReader(book2.Worksheets[ExcelTool.EPPlusZero]);
        TestExcelTest(reader1, reader2);
    }

    [Fact]
    public void EPPlusWorkSheetsTest()
    {
        var sheets1 = EPPlusTool.EPPlusSheets(path);
        using var stream = path.OpenReadShareStream();
        var sheets2 = EPPlusTool.EPPlusSheets(stream);
        using var reader1 = new EPPlusExcelReader(sheets1[ExcelTool.EPPlusZero]);
        using var reader2 = new EPPlusExcelReader(sheets2[ExcelTool.EPPlusZero]);
        TestExcelTest(reader1, reader2);
    }

    [Fact]
    public void EPPlusWorkSheetTest()
    {
        var sheet1 = EPPlusTool.EPPlusSheet(path);
        using var stream = path.OpenReadShareStream();
        var sheet2 = EPPlusTool.EPPlusSheet(stream);
        using var reader1 = new EPPlusExcelReader(sheet1);
        using var reader2 = new EPPlusExcelReader(sheet2);
        TestExcelTest(reader1, reader2);
    }

    [Fact]
    public void EPPlusWorkSheetWithIndexTest()
    {
        using var sheet1 = EPPlusTool.EPPlusSheet(path, ExcelTool.EPPlusZero);
        using var stream = path.OpenReadShareStream();
        using var sheet2 = EPPlusTool.EPPlusSheet(stream, ExcelTool.EPPlusZero);
        using var reader1 = new EPPlusExcelReader(sheet1);
        using var reader2 = new EPPlusExcelReader(sheet2);
        TestExcelTest(reader1, reader2);
    }


    [Fact]
    public void NPOIWorkbookTest()
    {
        var book1 = NPOITool.NPOIWorkbook(path);
        using var stream = path.OpenReadShareStream();
        var book2 = NPOITool.NPOIWorkbook(stream);
        using var reader1 = new NPOIExcelReader(book1.GetSheetAt(ExcelTool.NPOIZero));
        using var reader2 = new NPOIExcelReader(book2.GetSheetAt(ExcelTool.NPOIZero));
        TestExcelTest(reader1, reader2);
    }

    [Fact]
    public void NPOISheetTest()
    {
        var sheet1 = NPOITool.NPOISheet(path);
        using var stream = path.OpenReadShareStream();
        var sheet2 = NPOITool.NPOISheet(stream);
        using var reader1 = new NPOIExcelReader(sheet1);
        using var reader2 = new NPOIExcelReader(sheet2);
        TestExcelTest(reader1, reader2);
    }

    [Fact]
    public void NPOISheetWithIndexTest()
    {
        var sheet1 = NPOITool.NPOISheet(path, ExcelTool.NPOIZero);
        using var stream = path.OpenReadShareStream();
        var sheet2 = NPOITool.NPOISheet(stream, ExcelTool.NPOIZero);
        using var reader1 = new NPOIExcelReader(sheet1);
        using var reader2 = new NPOIExcelReader(sheet2);
        TestExcelTest(reader1, reader2);
    }


    [Fact]
    public void ExcelHeaderTest()
    {
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var sheet1 = NPOITool.NPOISheet(path);
        using var stream = path.OpenReadShareStream();
        var sheet2 = EPPlusTool.EPPlusSheet(stream);
        var headers1 = NPOITool.ExcelHeader(sheet1);
        var headers2 = EPPlusTool.ExcelHeader(sheet2);
        Assert.True(headers1.SequenceEqual(headers2));
        Assert.True(headers1.SequenceEqual(realHeader));
    }
}
