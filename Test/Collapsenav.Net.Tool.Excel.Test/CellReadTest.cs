using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;

public class CellReadTest
{
    [Fact]
    public void ICellReadTest()
    {
        var path = "./CellRead.xlsx";
        var realHeader = new[] { "Field0", "Field1", "Field2", "Field3" };

        IExcelCellRead reader = new EPPlusCellRead(path);
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        var epplusdata = reader[2333, 2].StringValue;
        reader.Dispose();

        reader = new NPOICellRead(path);
        Assert.True(reader.RowCount == 11);
        Assert.True(reader.Headers.SequenceEqual(realHeader));
        var npoidata = reader[2333, 2].StringValue;
        reader.Dispose();

        Assert.True(epplusdata == npoidata);
    }

    [Fact]
    public void ICellReadSaveTest()
    {
        var path = "./CellRead.xlsx";
        var savePath = "./CellRead-Export.xlsx";
        IExcelCellRead reader = new NPOICellRead(path);
        reader[0, 10].Value = "996";
        reader.SaveTo(savePath);
        reader.Dispose();

        reader = new EPPlusCellRead(savePath);
        Assert.True(reader[0, 10].StringValue == "996");
        reader.Dispose();

        var fs = path.OpenReadShareStream();
        reader = new EPPlusCellRead(fs);
        reader[0, 11].Value = "233";
        reader.SaveTo(savePath);
        reader.Dispose();
        fs.Dispose();



        fs = savePath.OpenReadShareStream();
        reader = new EPPlusCellRead(fs);
        Assert.True(reader[0, 11].StringValue == "233");
        reader.Dispose();
        fs.Dispose();
    }
}