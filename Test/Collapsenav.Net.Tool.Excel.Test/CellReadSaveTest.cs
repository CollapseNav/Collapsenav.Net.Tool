using System.IO;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;

public class CellReadSaveTest
{
    [Fact]
    public void CellReadSaveToPathTest()
    {
        var path = "./CellRead.xlsx";
        var savePath = "./New-CellRead-Path.xlsx";

        IExcelCellReader reader = new EPPlusCellReader(path);
        reader[1, 0].Value = 9999;
        reader.SaveTo(savePath);
        reader.Dispose();

        reader = new NPOICellReader(savePath);
        Assert.True(reader[1, 0].StringValue == "9999");
        reader[2, 2].Value = 12345;
        reader.SaveTo(savePath);
        reader.Dispose();

        reader = new MiniCellReader(savePath);
        // MiniExcel 的导出Save暂时还有点问题
        Assert.True(reader[2, 2].StringValue == "12345");
        reader[3, 1].Value = "👍";
        reader.SaveTo(savePath);
        reader.Dispose();

        reader = new EPPlusCellReader(savePath);
        Assert.True(reader[3, 1].StringValue == "👍");
        reader.Dispose();
    }

    [Fact]
    public void CellReadSaveToStreamTest()
    {
        var path = "./CellRead.xlsx";
        var savePath = "./New-CellRead-Stream.xlsx";

        var saveStream = savePath.OpenCreateReadWriteShareStream();


        IExcelCellReader reader = new EPPlusCellReader(path);
        reader[1, 0].Value = 9999;
        reader.SaveTo(saveStream);
        reader.Dispose();

        reader = new NPOICellReader(savePath);
        Assert.True(reader[1, 0].StringValue == "9999");
        reader[2, 2].Value = 12345;
        reader.SaveTo(saveStream);

        reader = new MiniCellReader(savePath);
        // MiniExcel 的导出Save暂时还有点问题
        Assert.True(reader[2, 2].StringValue == "12345");
        reader[3, 1].Value = "👍";
        reader.SaveTo(saveStream);
        reader.Dispose();

        reader = new EPPlusCellReader(savePath);
        Assert.True(reader[3, 1].StringValue == "👍");
        reader.Dispose();
    }

    [Fact]
    public void CellReadGetStreamSaveTest()
    {
        var path = "./CellRead.xlsx";
        var savePath = "./New-CellRead-GetStream.xlsx";

        var saveStream = savePath.OpenCreateReadWriteShareStream();

        IExcelCellReader reader = new EPPlusCellReader(path);
        reader[1, 0].Value = 9999;
        var getStream = reader.GetStream();
        getStream.CopyTo(saveStream);
        saveStream.Dispose();
        reader.Dispose();

        saveStream = savePath.OpenCreateReadWriteShareStream();
        reader = new NPOICellReader(savePath);
        Assert.True(reader[1, 0].StringValue == "9999");
        reader[2, 2].Value = 12345;
        getStream = reader.GetStream();
        getStream.CopyTo(saveStream);
        saveStream.Dispose();
        reader.Dispose();

        saveStream = savePath.OpenCreateReadWriteShareStream();
        reader = new MiniCellReader(savePath);
        // MiniExcel 的导出Save暂时还有点问题
        Assert.True(reader[2, 2].StringValue == "12345");
        reader[3, 1].Value = "👍";
        getStream = reader.GetStream();
        getStream.CopyTo(saveStream);
        saveStream.Dispose();
        reader.Dispose();

        reader = new EPPlusCellReader(savePath);
        Assert.True(reader[3, 1].StringValue == "👍");
        reader.Dispose();
    }

    [Fact]
    public void CellReadSaveWithNoParamTest()
    {
        var path = "./CellRead.xlsx";
        var copyPath = "./Copy-CellRead.xlsx";
        File.Copy(path, copyPath);
        IExcelCellReader reader = new EPPlusCellReader(copyPath);
        reader[0, 0].Value = "🗡️";
        reader.Save();
        reader.Dispose();

        reader = new NPOICellReader(copyPath);
        Assert.True(reader[0, 0].StringValue == "🗡️");
        reader[1, 1].Value = "🗡️";
        reader.Save();
        reader.Dispose();

        reader = new MiniCellReader(copyPath);
        Assert.True(reader[1, 1].StringValue == "🗡️");
        reader[2, 2].Value = "🗡️";
        reader.Save();
        reader.Dispose();

        reader = new EPPlusCellReader(copyPath);
        Assert.True(reader[2, 2].StringValue == "🗡️");
        reader.Dispose();
        File.Delete(copyPath);
    }

    [Fact]
    public void NoOriginFileCellReadTest()
    {
        var savePath = "./Save-CellRead.xlsx";
        IExcelCellReader reader = new EPPlusCellReader();
        reader[0, 0].Value = "2333";
        reader.SaveTo(savePath);
        reader.Dispose();
        reader = new EPPlusCellReader(savePath);
        Assert.True(reader[0, 0].StringValue == "2333");
        reader.Dispose();
        File.Delete(savePath);

        reader = new NPOICellReader();
        reader[1, 1].Value = "2333";
        reader.SaveTo(savePath);
        reader.Dispose();
        reader = new NPOICellReader(savePath);
        Assert.True(reader[1, 1].StringValue == "2333");
        reader.Dispose();
        File.Delete(savePath);

        reader = new MiniCellReader();
        reader[2, 2].Value = "2333";
        reader.SaveTo(savePath);
        reader.Dispose();
        reader = new MiniCellReader(savePath);
        Assert.True(reader[2, 2].StringValue == "2333");
        reader.Dispose();
        File.Delete(savePath);
    }
}