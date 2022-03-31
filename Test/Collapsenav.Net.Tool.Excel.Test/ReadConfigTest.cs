using System;
using System.Data;
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
    /// 测试自定义Add添加配置
    /// </summary>
    [Fact]
    public async Task AddRequireOptionTest()
    {
        var path = "./RequireExcel.xlsx";
        var read = new ReadConfig<ExcelDefaultDto>()
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field3", item => item.Field3)
        .AddIf(false, "Field3", item => item.Field3)
        ;
        Assert.True(read.FieldOption.Count() == 3);
        var data = await read.EPPlusToEntityAsync(path);
        Assert.True(data.Count() == 20);
        Assert.True(data.Count(item => item.Field0 == "233") == 10);
        Assert.True(data.Count(item => item.Field1 == 23) == 10);
        Assert.True(data.Count(item => item.Field3 == 233.33) == 10);

        read = new ReadConfig<ExcelDefaultDto>()
        .Require("Field0", item => item.Field0)
        .Require("Field1", item => item.Field1)
        .Require("Field3", item => item.Field3)
        .RequireIf(false, "Field3", item => item.Field3)
        ;
        try
        {
            data = await read.EPPlusToEntityAsync(path);
        }
        catch (Exception ex)
        {
            Assert.True(ex.GetType() == typeof(NoNullAllowedException));
        }
    }

    /// <summary>
    /// 通过属性prop添加config
    /// </summary>
    [Fact]
    public async Task AddCellOptionWithPropTest()
    {
        var path = "./TestExcel.xlsx";
        var config = new ReadConfig<ExcelTestDto>()
        .Add("Field0", typeof(ExcelTestDto).GetProperty("Field0"))
        .AddIf(true, "Field1", typeof(ExcelTestDto).GetProperty("Field1"))
        .Add("Field2", typeof(ExcelTestDto).GetProperty("Field2"), item => item == "Male")
        .AddIf(true, "Field3", typeof(ExcelTestDto).GetProperty("Field3"))
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
        .AddIf(false, "Field3", "Field233")
        ;
        Assert.True(config.FieldOption.Count() == 3);
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

        config = new ReadConfig<ExcelTestDto>()
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2, item => item == "Male")
        .Add("Field3", item => item.Field3)
        .AddInitIf(false, item =>
        {
            item.Field0 += item.Field2.ToString();
            item.Field3 *= 100;
            return item;
        })
        ;
        data = await config.ToEntityAsync(path);
        Assert.True(data.Count() == 3000);
        Assert.True(data.Count(item => item.Field0 == "233") == 1500);
        Assert.True(data.Count(item => item.Field1 == 23) == 1500);
        Assert.True(data.Count(item => item.Field2 == true) == 1500);
        Assert.True(data.Count(item => item.Field3 == 123.23) == 1500);
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
    /// 从文件路径读取
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

    /// <summary>
    /// 从文件流读取
    /// </summary>
    [Fact]
    public async Task ReadConfigWithStreamTest()
    {
        var path = "./TestExcel.xlsx";
        using var fs = path.OpenReadShareStream();
        var config = new ReadConfig<ExcelTestDto>(fs)
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

    /// <summary>
    /// 从对应的sheet格式读取
    /// </summary>
    [Fact]
    public async Task ReadConfigWithSheetTest()
    {
        var path = "./TestExcel.xlsx";
        var config = new ReadConfig<ExcelTestDto>()
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2, item => item == "Male")
        .Add("Field3", item => item.Field3)
        ;
        var epplusSheet = EPPlusTool.EPPlusSheet(path);
        var data = await config.EPPlusToEntityAsync(epplusSheet);
        Assert.True(data.Count() == 3000);
        Assert.True(data.Count(item => item.Field0 == "233") == 1500);
        Assert.True(data.Count(item => item.Field1 == 23) == 1500);
        Assert.True(data.Count(item => item.Field2 == true) == 1500);
        Assert.True(data.Count(item => item.Field3 == 123.23) == 1500);

        var npoiSheet = NPOITool.NPOISheet(path);
        data = await config.NPOIToEntityAsync(npoiSheet);
        Assert.True(data.Count() == 3000);
        Assert.True(data.Count(item => item.Field0 == "233") == 1500);
        Assert.True(data.Count(item => item.Field1 == 23) == 1500);
        Assert.True(data.Count(item => item.Field2 == true) == 1500);
        Assert.True(data.Count(item => item.Field3 == 123.23) == 1500);

        var miniSheet = path.OpenReadShareStream();
        data = await config.MiniToEntityAsync(miniSheet);
        Assert.True(data.Count() == 3000);
        Assert.True(data.Count(item => item.Field0 == "233") == 1500);
        Assert.True(data.Count(item => item.Field1 == 23) == 1500);
        Assert.True(data.Count(item => item.Field2 == true) == 1500);
        Assert.True(data.Count(item => item.Field3 == 123.23) == 1500);

        try
        {
            data = await config.EPPlusToEntityAsync();
        }
        catch (Exception ex)
        {
            Assert.True(ex.GetType() == typeof(NullReferenceException));
        }
        try
        {
            data = await config.NPOIToEntityAsync();
        }
        catch (Exception ex)
        {
            Assert.True(ex.GetType() == typeof(NullReferenceException));
        }
        try
        {
            data = await config.MiniToEntityAsync();
        }
        catch (Exception ex)
        {
            Assert.True(ex.GetType() == typeof(NullReferenceException));
        }
    }

    [Fact]
    public async Task StaticReadConfigFuncByPathTest()
    {
        string path = "./TestExcel.xlsx";
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
        var data = await ExcelTool.ExcelToEntityAsync(path, config);

        Assert.True(data.Count() == 3000);
        Assert.True(data.Count(item => item.Field0 == "233True") == 1500);
        Assert.True(data.Count(item => item.Field1 == 23) == 1500);
        Assert.True(data.Count(item => item.Field2 == true) == 1500);
        Assert.True(data.Count(item => item.Field3 == 12323) == 1500);
    }

    [Fact]
    public async Task StaticReadConfigFuncByStreamTest()
    {
        string path = "./TestExcel.xlsx";
        using var fs = path.OpenReadShareStream();
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
        var data = await ExcelTool.ExcelToEntityAsync(fs, config);

        Assert.True(data.Count() == 3000);
        Assert.True(data.Count(item => item.Field0 == "233True") == 1500);
        Assert.True(data.Count(item => item.Field1 == 23) == 1500);
        Assert.True(data.Count(item => item.Field2 == true) == 1500);
        Assert.True(data.Count(item => item.Field3 == 12323) == 1500);
    }

    [Fact]
    public async Task StaticReadConfigFuncByIExcelReadTest()
    {
        string path = "./TestExcel.xlsx";
        using var reader = IExcelReader.GetExcelRead(path);
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
        var data = await ExcelTool.ExcelToEntityAsync(reader, config);

        Assert.True(data.Count() == 3000);
        Assert.True(data.Count(item => item.Field0 == "233True") == 1500);
        Assert.True(data.Count(item => item.Field1 == 23) == 1500);
        Assert.True(data.Count(item => item.Field2 == true) == 1500);
        Assert.True(data.Count(item => item.Field3 == 12323) == 1500);
    }
}
