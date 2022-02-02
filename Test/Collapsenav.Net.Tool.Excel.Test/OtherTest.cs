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
        Assert.True(readConfig.FieldOption.Select(item => item.ExcelField).ContainAnd("Field0", "Field2", "Field3"));
        Assert.True(readConfig.FieldOption.Select(item => item.PropName).ContainAnd("Field0", "Field2", "Field3"));
        readConfig.Remove("Field0");
        Assert.True(readConfig.FieldOption.Count() == 2);
        Assert.True(readConfig.FieldOption.Select(item => item.Prop).Select(item => item.Name).ContainAnd("Field2", "Field3"));

        var exportConfig = new ExportConfig<ExcelTestDto>()
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2)
        .Add("Field3", item => item.Field3)
        ;

        Assert.True(exportConfig.FieldOption.Count() == 4);
        exportConfig.FilterOptionByHeaders(new[] { "Field0", "Field0", "Field2", "Field3" });
        Assert.True(exportConfig.FieldOption.Count() == 3);
        Assert.True(exportConfig.FieldOption.Select(item => item.ExcelField).ContainAnd("Field0", "Field2", "Field3"));
    }
}
