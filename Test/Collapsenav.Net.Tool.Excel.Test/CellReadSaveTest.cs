using System.IO;
using System.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;

public class CellReadSaveTest
{
    private readonly string path = "./CellRead.xlsx";
    string PathCellReadPath = "./New-CellRead-Path.xlsx";
    string StreamCellReadPath = "./New-CellRead-Stream.xlsx";
    string GetStreamCellReadPath = "./New-CellRead-GetStream.xlsx";
    string CopyCellReadPath = "./Copy-CellRead.xlsx";
    string SaveCellReadPath = "./Save-CellRead.xlsx";

    [Fact]
    public void CellReadSaveToPathTest()
    {

        IExcelCellReader reader = new EPPlusCellReader(path);
        reader[1, 0].Value = 9999;
        reader.SaveTo(PathCellReadPath);
        reader.Dispose();

        reader = new NPOICellReader(PathCellReadPath);
        Assert.True(reader[1, 0].StringValue == "9999");
        reader[2, 2].Value = 12345;
        reader.SaveTo(PathCellReadPath);
        reader.Dispose();

        reader = new MiniCellReader(PathCellReadPath);
        // MiniExcel 的导出Save暂时还有点问题
        Assert.True(reader[2, 2].StringValue == "12345");
        reader[3, 1].Value = "👍";
        reader.SaveTo(PathCellReadPath);
        reader.Dispose();

        reader = new EPPlusCellReader(PathCellReadPath);
        Assert.True(reader[3, 1].StringValue == "👍");
        reader.Dispose();
    }

    [Fact]
    public void CellReadSaveToStreamTest()
    {
        var saveStream = StreamCellReadPath.OpenCreateReadWriteShareStream();
        IExcelCellReader reader = new EPPlusCellReader(path);
        reader[1, 0].Value = 9999;
        reader.SaveTo(saveStream);
        reader.Dispose();

        reader = new NPOICellReader(StreamCellReadPath);
        Assert.True(reader[1, 0].StringValue == "9999");
        reader[2, 2].Value = 12345;
        reader.SaveTo(saveStream);

        reader = new MiniCellReader(StreamCellReadPath);
        // MiniExcel 的导出Save暂时还有点问题
        Assert.True(reader[2, 2].StringValue == "12345");
        reader[3, 1].Value = "👍";
        reader.SaveTo(saveStream);
        reader.Dispose();

        reader = new EPPlusCellReader(StreamCellReadPath);
        Assert.True(reader[3, 1].StringValue == "👍");
        reader.Dispose();
    }

    [Fact]
    public void CellReadGetStreamSaveTest()
    {
        var saveStream = GetStreamCellReadPath.OpenCreateReadWriteShareStream();
        IExcelCellReader reader = new EPPlusCellReader(path);
        reader[1, 0].Value = 9999;
        var getStream = reader.GetStream();
        getStream.CopyTo(saveStream);
        saveStream.Dispose();
        reader.Dispose();

        saveStream = GetStreamCellReadPath.OpenCreateReadWriteShareStream();
        reader = new NPOICellReader(GetStreamCellReadPath);
        Assert.True(reader[1, 0].StringValue == "9999");
        reader[2, 2].Value = 12345;
        getStream = reader.GetStream();
        getStream.CopyTo(saveStream);
        saveStream.Dispose();
        reader.Dispose();

        saveStream = GetStreamCellReadPath.OpenCreateReadWriteShareStream();
        reader = new MiniCellReader(GetStreamCellReadPath);
        // MiniExcel 的导出Save暂时还有点问题
        Assert.True(reader[2, 2].StringValue == "12345");
        reader[3, 1].Value = "👍";
        getStream = reader.GetStream();
        getStream.CopyTo(saveStream);
        saveStream.Dispose();
        reader.Dispose();

        reader = new EPPlusCellReader(GetStreamCellReadPath);
        Assert.True(reader[3, 1].StringValue == "👍");
        reader.Dispose();
    }

    [Fact]
    public void CellReadSaveWithNoParamTest()
    {
        File.Copy(path, CopyCellReadPath);
        IExcelCellReader reader = new EPPlusCellReader(CopyCellReadPath);
        reader[0, 0].Value = "🗡️";
        reader.Save();
        reader.Dispose();

        reader = new NPOICellReader(CopyCellReadPath);
        Assert.True(reader[0, 0].StringValue == "🗡️");
        reader[1, 1].Value = "🗡️";
        reader.Save();
        reader.Dispose();

        reader = new MiniCellReader(CopyCellReadPath);
        Assert.True(reader[1, 1].StringValue == "🗡️");
        reader[2, 2].Value = "🗡️";
        reader.Save();
        reader.Dispose();

        reader = new EPPlusCellReader(CopyCellReadPath);
        Assert.True(reader[2, 2].StringValue == "🗡️");
        reader.Dispose();
        File.Delete(CopyCellReadPath);
    }

    [Fact]
    public void NoOriginFileCellReadTest()
    {
        IExcelCellReader reader = new EPPlusCellReader();
        reader[0, 0].Value = "2333";
        reader.SaveTo(SaveCellReadPath);
        reader.Dispose();
        reader = new EPPlusCellReader(SaveCellReadPath);
        Assert.True(reader[0, 0].StringValue == "2333");
        reader.Dispose();
        File.Delete(SaveCellReadPath);

        reader = new NPOICellReader();
        reader[1, 1].Value = "2333";
        reader.SaveTo(SaveCellReadPath);
        reader.Dispose();
        reader = new NPOICellReader(SaveCellReadPath);
        Assert.True(reader[1, 1].StringValue == "2333");
        reader.Dispose();
        File.Delete(SaveCellReadPath);

        reader = new MiniCellReader();
        reader[2, 2].Value = "2333";
        reader.SaveTo(SaveCellReadPath);
        reader.Dispose();
        reader = new MiniCellReader(SaveCellReadPath);
        Assert.True(reader[2, 2].StringValue == "2333");
        reader.Dispose();
        File.Delete(SaveCellReadPath);
    }
}