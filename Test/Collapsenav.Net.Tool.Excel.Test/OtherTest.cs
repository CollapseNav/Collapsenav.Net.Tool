using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;
public class OtherTest
{
    /// <summary>
    /// 测试默认配置
    /// </summary>
    [Fact]
    public void ConfigFilterTest()
    {
        var readConfig = new ReadConfig<ExcelTestDto>()
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2)
        .Add("Field3", item => item.Field3)
        ;
        Assert.True(readConfig.FieldOption.Count() == 4);
        readConfig.FilterOptionByHeaders(new[] { "Field0", "Field0", "Field2", "Field3" });
        Assert.True(readConfig.FieldOption.Count() == 3);
        Assert.True(readConfig.FieldOption.Select(item => item.ExcelField).AllContain("Field0", "Field2", "Field3"));
        Assert.True(readConfig.FieldOption.Select(item => item.PropName).AllContain("Field0", "Field2", "Field3"));
        readConfig.Remove("Field0");
        Assert.True(readConfig.FieldOption.Count() == 2);
        Assert.True(readConfig.FieldOption.Select(item => item.Prop).Select(item => item.Name).AllContain("Field2", "Field3"));

        var exportConfig = new ExportConfig<ExcelTestDto>()
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2)
        .Add("Field3", item => item.Field3)
        ;

        Assert.True(exportConfig.FieldOption.Count() == 4);
        exportConfig.FilterOptionByHeaders(new[] { "Field0", "Field0", "Field2", "Field3" });
        Assert.True(exportConfig.FieldOption.Count() == 3);
        Assert.True(exportConfig.FieldOption.Select(item => item.ExcelField).AllContain("Field0", "Field2", "Field3"));
    }

    [Fact]
    public void GetCellReadTest()
    {
        string path = "./TestExcel.xlsx";
        var epplusSheet = EPPlusTool.EPPlusSheet(path);
        var npoiSheet = NPOITool.NPOISheet(path);
        using var reader1 = IExcelCellReader.GetCellRead(epplusSheet);
        using var reader2 = IExcelCellReader.GetCellRead(npoiSheet);
        Assert.True(reader1[0, 0].StringValue == reader2[0, 0].StringValue);
        Assert.True(reader1[0, 1].StringValue == reader2[0, 1].StringValue);
        Assert.True(reader1[2, 2].StringValue == reader2[2, 2].StringValue);
        Assert.True(reader1[4, 2].StringValue == reader2[4, 2].StringValue);
    }

    [Fact]
    public void GetExcelReadTest()
    {
        string path = "./TestExcel.xlsx";
        var epplusSheet = EPPlusTool.EPPlusSheet(path);
        var npoiSheet = NPOITool.NPOISheet(path);
        using var reader1 = IExcelReader.GetExcelRead(epplusSheet);
        using var reader2 = IExcelReader.GetExcelRead(npoiSheet);
        Assert.True(reader1[0, 0] == reader2[0, 0]);
        Assert.True(reader1[0, 1] == reader2[0, 1]);
        Assert.True(reader1[2, 2] == reader2[2, 2]);
        Assert.True(reader1[4, 2] == reader2[4, 2]);
    }
}
