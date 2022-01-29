using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;

public class CellReadSaveTest
{
    [Fact]
    public void CellReadSaveToPathTest()
    {
        var path = "./CellRead.xlsx";
        var savePath = "./New-CellRead-Path.xlsx";

        IExcelCellRead reader = new EPPlusCellRead(path);
        reader[1, 0].Value = 9999;
        reader.SaveTo(savePath);
        reader.Dispose();

        reader = new NPOICellRead(savePath);
        Assert.True(reader[1, 0].StringValue == "9999");
        reader[2, 2].Value = 12345;
        reader.SaveTo(savePath);
        reader.Dispose();

        reader = new MiniCellRead(savePath);
        // MiniExcel 的导出Save暂时还有点问题
        Assert.True(reader[2, 2].StringValue == "12345");
        reader.Dispose();
    }

    [Fact]
    public void CellReadSaveToStreamTest()
    {
        var path = "./CellRead.xlsx";
        var savePath = "./New-CellRead-Stream.xlsx";

        var saveStream = savePath.OpenCreateReadWirteShareStream();


        IExcelCellRead reader = new EPPlusCellRead(path);
        reader[1, 0].Value = 9999;
        reader.SaveTo(saveStream);
        reader.Dispose();

        reader = new NPOICellRead(savePath);
        Assert.True(reader[1, 0].StringValue == "9999");
        reader[2, 2].Value = 12345;
        reader.SaveTo(saveStream);

        reader = new MiniCellRead(savePath);
        // MiniExcel 的导出Save暂时还有点问题
        Assert.True(reader[2, 2].StringValue == "12345");
        reader.Dispose();
    }

    [Fact]
    public void CellReadGetStreamSaveTest()
    {
        var path = "./CellRead.xlsx";
        var savePath = "./New-CellRead-GetStream.xlsx";

        var saveStream = savePath.OpenCreateReadWirteShareStream();

        IExcelCellRead reader = new EPPlusCellRead(path);
        reader[1, 0].Value = 9999;
        var getStream = reader.GetStream();
        getStream.CopyTo(saveStream);
        saveStream.Dispose();
        reader.Dispose();

        saveStream = savePath.OpenCreateReadWirteShareStream();
        reader = new NPOICellRead(savePath);
        Assert.True(reader[1, 0].StringValue == "9999");
        reader[2, 2].Value = 12345;
        getStream = reader.GetStream();
        getStream.CopyTo(saveStream);
        saveStream.Dispose();
        reader.Dispose();

        reader = new MiniCellRead(savePath);
        // MiniExcel 的导出Save暂时还有点问题
        Assert.True(reader[2, 2].StringValue == "12345");
        reader.Dispose();
    }
}