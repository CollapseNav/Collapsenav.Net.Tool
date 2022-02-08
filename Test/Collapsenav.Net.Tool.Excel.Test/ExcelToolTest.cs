using System;
using System.Data;
using System.IO;
using System.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;
public class ExcelToolTest
{
    [Fact]
    public void IsXlsTest()
    {
        string path = "./TestExcel.xlsx";
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
        string path = "./TestExcel.xlsx";
        using var pack1 = ExcelTool.EPPlusPackage(path);
        using var stream = path.OpenReadShareStream();
        using var pack2 = ExcelTool.EPPlusPackage(stream);
        using var reader1 = new EPPlusExcelRead(pack1.Workbook.Worksheets[ExcelTool.EPPlusZero]);
        using var reader2 = new EPPlusExcelRead(pack2.Workbook.Worksheets[ExcelTool.EPPlusZero]);
        Assert.True(reader1[0, 0] == reader2[0, 0]);
        Assert.True(reader1[0, 1] == reader2[0, 1]);
        Assert.True(reader1[2, 2] == reader2[2, 2]);
        Assert.True(reader1[4, 2] == reader2[4, 2]);
    }

    [Fact]
    public void EPPlusWorkbookTest()
    {
        string path = "./TestExcel.xlsx";
        using var book1 = ExcelTool.EPPlusWorkbook(path);
        using var stream = path.OpenReadShareStream();
        using var book2 = ExcelTool.EPPlusWorkbook(stream);
        using var reader1 = new EPPlusExcelRead(book1.Worksheets[ExcelTool.EPPlusZero]);
        using var reader2 = new EPPlusExcelRead(book2.Worksheets[ExcelTool.EPPlusZero]);
        Assert.True(reader1[0, 0] == reader2[0, 0]);
        Assert.True(reader1[0, 1] == reader2[0, 1]);
        Assert.True(reader1[2, 2] == reader2[2, 2]);
        Assert.True(reader1[4, 2] == reader2[4, 2]);
    }

    [Fact]
    public void EPPlusWorkSheetsTest()
    {
        string path = "./TestExcel.xlsx";
        var sheets1 = ExcelTool.EPPlusSheets(path);
        using var stream = path.OpenReadShareStream();
        var sheets2 = ExcelTool.EPPlusSheets(stream);
        using var reader1 = new EPPlusExcelRead(sheets1[ExcelTool.EPPlusZero]);
        using var reader2 = new EPPlusExcelRead(sheets2[ExcelTool.EPPlusZero]);
        Assert.True(reader1[0, 0] == reader2[0, 0]);
        Assert.True(reader1[0, 1] == reader2[0, 1]);
        Assert.True(reader1[2, 2] == reader2[2, 2]);
        Assert.True(reader1[4, 2] == reader2[4, 2]);
    }

    [Fact]
    public void EPPlusWorkSheetTest()
    {
        string path = "./TestExcel.xlsx";
        var sheet1 = ExcelTool.EPPlusSheet(path);
        using var stream = path.OpenReadShareStream();
        var sheet2 = ExcelTool.EPPlusSheet(stream);
        using var reader1 = new EPPlusExcelRead(sheet1);
        using var reader2 = new EPPlusExcelRead(sheet2);
        Assert.True(reader1[0, 0] == reader2[0, 0]);
        Assert.True(reader1[0, 1] == reader2[0, 1]);
        Assert.True(reader1[2, 2] == reader2[2, 2]);
        Assert.True(reader1[4, 2] == reader2[4, 2]);
    }

    [Fact]
    public void EPPlusWorkSheetWithIndexTest()
    {
        string path = "./TestExcel.xlsx";
        using var sheet1 = ExcelTool.EPPlusSheet(path, ExcelTool.EPPlusZero);
        using var stream = path.OpenReadShareStream();
        using var sheet2 = ExcelTool.EPPlusSheet(stream, ExcelTool.EPPlusZero);
        using var reader1 = new EPPlusExcelRead(sheet1);
        using var reader2 = new EPPlusExcelRead(sheet2);
        Assert.True(reader1[0, 0] == reader2[0, 0]);
        Assert.True(reader1[0, 1] == reader2[0, 1]);
        Assert.True(reader1[2, 2] == reader2[2, 2]);
        Assert.True(reader1[4, 2] == reader2[4, 2]);
    }





    [Fact]
    public void NPOIWorkbookTest()
    {
        string path = "./TestExcel.xlsx";
        var book1 = ExcelTool.NPOIWorkbook(path);
        using var stream = path.OpenReadShareStream();
        var book2 = ExcelTool.NPOIWorkbook(stream);
        using var reader1 = new NPOIExcelRead(book1.GetSheetAt(ExcelTool.NPOIZero));
        using var reader2 = new NPOIExcelRead(book2.GetSheetAt(ExcelTool.NPOIZero));
        Assert.True(reader1[0, 0] == reader2[0, 0]);
        Assert.True(reader1[0, 1] == reader2[0, 1]);
        Assert.True(reader1[2, 2] == reader2[2, 2]);
        Assert.True(reader1[4, 2] == reader2[4, 2]);
    }

    [Fact]
    public void NPOISheetTest()
    {
        string path = "./TestExcel.xlsx";
        var sheet1 = ExcelTool.NPOISheet(path);
        using var stream = path.OpenReadShareStream();
        var sheet2 = ExcelTool.NPOISheet(stream);
        using var reader1 = new NPOIExcelRead(sheet1);
        using var reader2 = new NPOIExcelRead(sheet2);
        Assert.True(reader1[0, 0] == reader2[0, 0]);
        Assert.True(reader1[0, 1] == reader2[0, 1]);
        Assert.True(reader1[2, 2] == reader2[2, 2]);
        Assert.True(reader1[4, 2] == reader2[4, 2]);
    }

    [Fact]
    public void NPOISheetWithIndexTest()
    {
        string path = "./TestExcel.xlsx";
        var sheet1 = ExcelTool.NPOISheet(path, ExcelTool.NPOIZero);
        using var stream = path.OpenReadShareStream();
        var sheet2 = ExcelTool.NPOISheet(stream, ExcelTool.NPOIZero);
        using var reader1 = new NPOIExcelRead(sheet1);
        using var reader2 = new NPOIExcelRead(sheet2);
        Assert.True(reader1[0, 0] == reader2[0, 0]);
        Assert.True(reader1[0, 1] == reader2[0, 1]);
        Assert.True(reader1[2, 2] == reader2[2, 2]);
        Assert.True(reader1[4, 2] == reader2[4, 2]);
    }


    [Fact]
    public void ExcelHeaderTest()
    {
        string path = "./TestExcel.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };
        var sheet1 = ExcelTool.NPOISheet(path);
        using var stream = path.OpenReadShareStream();
        var sheet2 = ExcelTool.EPPlusSheet(stream);
        var headers1 = ExcelTool.ExcelHeader(sheet1);
        var headers2 = ExcelTool.ExcelHeader(sheet2);
        Assert.True(headers1.SequenceEqual(headers2));
        Assert.True(headers1.SequenceEqual(realHeader));
    }
}
