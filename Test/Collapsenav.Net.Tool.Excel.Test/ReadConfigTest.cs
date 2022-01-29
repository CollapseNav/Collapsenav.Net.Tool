using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;
public class ReadConfigTest
{
    /// <summary>
    /// 测试默认配置
    /// </summary>
    [Fact]
    public async Task DefaultConfigTest()
    {
        var path = "./DefaultExcel.xlsx";
        var read = ReadConfig<ExcelDefaultDto>.GenDefaultConfig();
        Assert.True(read.FieldOption.Count() == 3);
        var data = await read.EPPlusToEntityAsync(path);
        Assert.True(data.Count() == 20);
        Assert.True(data.Count(item => item.Field0 == "233") == 10);
        Assert.True(data.Count(item => item.Field1 == 23) == 10);
        Assert.True(data.Count(item => item.Field3 == 233.33) == 10);

        data = await read.NPOIToEntityAsync(path);
        Assert.True(data.Count() == 20);
        Assert.True(data.Count(item => item.Field0 == "233") == 10);
        Assert.True(data.Count(item => item.Field1 == 23) == 10);
        Assert.True(data.Count(item => item.Field3 == 233.33) == 10);

        data = await read.MiniToEntityAsync(path);
        Assert.True(data.Count() == 20);
        Assert.True(data.Count(item => item.Field0 == "233") == 10);
        Assert.True(data.Count(item => item.Field1 == 23) == 10);
        Assert.True(data.Count(item => item.Field3 == 233.33) == 10);
    }
    /// <summary>
    /// 测试自定义Default配置
    /// </summary>
    [Fact]
    public async Task DefaultCellOptionTest()
    {
        var path = "./TestExcel.xlsx";
        var config = new ReadConfig<ExcelTestDto>()
        .Default(item => item.Field0, "233")
        .DefaultIf(true, item => item.Field1, 233)
        .Default(item => item.Field2, true)
        .DefaultIf(true, item => item.Field3, 23.3)
        ;
        var data = await config.ToEntityAsync(path);
        Assert.True(data.Count() == 3000);
        Assert.True(data.All(item => item.Field0 == "233"));
        Assert.True(data.All(item => item.Field1 == 233));
        Assert.True(data.All(item => item.Field2 == true));
        Assert.True(data.All(item => item.Field3 == 23.3));
    }

    /// <summary>
    /// 测试自定义Add添加配置
    /// </summary>
    [Fact]
    public async Task AddCellOptionTest()
    {
        var path = "./TestExcel.xlsx";
        var config = new ReadConfig<ExcelTestDto>()
        .Add("Field0", item => item.Field0)
        .AddIf(true, "Field1", item => item.Field1)
        .Add("Field2", item => item.Field2, item => item == "Male")
        .AddIf(true, "Field3", item => item.Field3)
        ;
        var data = await config.ToEntityAsync(path);
        Assert.True(data.Count() == 3000);
        Assert.True(data.Count(item => item.Field0 == "233") == 1500);
        Assert.True(data.Count(item => item.Field1 == 23) == 1500);
        Assert.True(data.Count(item => item.Field2 == true) == 1500);
        Assert.True(data.Count(item => item.Field3 == 123.23) == 1500);
    }

    /// <summary>
    /// 测试完全使用字符串添加的配置
    /// </summary>
    [Fact]
    public async Task StringCellOptionTest()
    {
        var path = "./DefaultExcel.xlsx";
        var config = new ReadConfig<ExcelDefaultDto>()
        .Add("Field0", "Field0")
        .Add("Field1", "Field1")
        .Add("Field3", "Field3")
        ;
        var data = await config.ToEntityAsync(path);
        Assert.True(data.Count() == 20);
        Assert.True(data.Count(item => item.Field0 == "233") == 10);
        Assert.True(data.Count(item => item.Field1 == 23) == 10);
        Assert.True(data.Count(item => item.Field3 == 233.33) == 10);
    }

    /// <summary>
    /// 测试使用了Init初始化的配置
    /// </summary>
    [Fact]
    public async Task AddInitOptionTest()
    {
        var path = "./TestExcel.xlsx";
        var config = new ReadConfig<ExcelTestDto>()
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2, item => item == "Male")
        .Add("Field3", item => item.Field3)
        .AddInit(item =>
        {
            item.Field0 += item.Field2.ToString();
            item.Field3 *= 100;
            return item;
        })
        ;
        var data = await config.ToEntityAsync(path);
        Assert.True(data.Count() == 3000);
        Assert.True(data.Count(item => item.Field0 == "233True") == 1500);
        Assert.True(data.Count(item => item.Field1 == 23) == 1500);
        Assert.True(data.Count(item => item.Field2 == true) == 1500);
        Assert.True(data.Count(item => item.Field3 == 12323) == 1500);
    }

    /// <summary>
    /// 测试根据注解生成的配置
    /// </summary>
    [Fact]
    public async Task AttributeConfigTest()
    {
        var path = "./AttributeExcel.xlsx";
        var config = ReadConfig<ExcelAttrDto>.GenDefaultConfig();
        Assert.True(config.FieldOption.Count() == 3);
        var data = await config.ToEntityAsync(path);
        Assert.True(data.Count() == 20);
        Assert.True(data.Count(item => item.Field0 == "233") == 10);
        Assert.True(data.Count(item => item.Field1 == 23) == 10);
        Assert.True(data.Count(item => item.Field3 == 233.33) == 10);
    }

    /// <summary>
    /// 测试根据注解生成的配置
    /// </summary>
    [Fact]
    public async Task ReadConfigWithFileTest()
    {
        var path = "./TestExcel.xlsx";
        var config = new ReadConfig<ExcelTestDto>(path)
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2, item => item == "Male")
        .Add("Field3", item => item.Field3)
        ;
        var data = await config.EPPlusToEntityAsync();
        Assert.True(data.Count() == 3000);
        Assert.True(data.Count(item => item.Field0 == "233") == 1500);
        Assert.True(data.Count(item => item.Field1 == 23) == 1500);
        Assert.True(data.Count(item => item.Field2 == true) == 1500);
        Assert.True(data.Count(item => item.Field3 == 123.23) == 1500);

        data = await config.NPOIToEntityAsync();
        Assert.True(data.Count() == 3000);
        Assert.True(data.Count(item => item.Field0 == "233") == 1500);
        Assert.True(data.Count(item => item.Field1 == 23) == 1500);
        Assert.True(data.Count(item => item.Field2 == true) == 1500);
        Assert.True(data.Count(item => item.Field3 == 123.23) == 1500);

        data = await config.MiniToEntityAsync();
        Assert.True(data.Count() == 3000);
        Assert.True(data.Count(item => item.Field0 == "233") == 1500);
        Assert.True(data.Count(item => item.Field1 == 23) == 1500);
        Assert.True(data.Count(item => item.Field2 == true) == 1500);
        Assert.True(data.Count(item => item.Field3 == 123.23) == 1500);

        data = await config.ToEntityAsync();
        Assert.True(data.Count() == 3000);
        Assert.True(data.Count(item => item.Field0 == "233") == 1500);
        Assert.True(data.Count(item => item.Field1 == 23) == 1500);
        Assert.True(data.Count(item => item.Field2 == true) == 1500);
        Assert.True(data.Count(item => item.Field3 == 123.23) == 1500);
    }

}
