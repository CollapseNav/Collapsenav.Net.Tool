using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;
public class ReadConfigTest
{
    private readonly string DefaultExcelPath = "./DefaultExcel.xlsx";
    private readonly string TestExcelPath = "./TestConfigExcel.xlsx";

    public ReadConfigTest()
    {
        if (!File.Exists(TestExcelPath))
        {
            File.Copy("./TestExcel.xlsx", TestExcelPath);
        }
    }

    /// <summary>
    /// 测试默认配置
    /// </summary>
    [Fact]
    public async Task DefaultConfigTest()
    {
        var read = ReadConfig<ExcelDefaultDto>.GenDefaultConfig();
        Assert.True(read.FieldOption.Count() == 3);
        var data = await read.EPPlusToEntityAsync(DefaultExcelPath);
        TestData(data);

        data = await read.NPOIToEntityAsync(DefaultExcelPath);
        TestData(data);

        data = await read.MiniToEntityAsync(DefaultExcelPath);
        TestData(data);

        static void TestData(IEnumerable<ExcelDefaultDto> data)
        {
            Assert.True(data.Count() == 20);
            Assert.True(data.Count(item => item.Field0 == "233") == 10);
            Assert.True(data.Count(item => item.Field1 == 23) == 10);
            Assert.True(data.Count(item => item.Field3 == 233.33) == 10);
        }
    }
    /// <summary>
    /// 测试自定义Default配置
    /// </summary>
    [Fact]
    public async Task DefaultCellOptionTest()
    {
        var config = new ReadConfig<ExcelTestDto>()
        .Default(item => item.Field0, "23333")
        .DefaultIf(true, item => item.Field1, 233)
        .Default(item => item.Field2, true)
        .DefaultIf(true, item => item.Field3, 23.3)
        ;
        var data = await config.ToEntityAsync(TestExcelPath);
        Assert.True(data.Count() == 3000);
        Assert.True(data.All(item => item.Field0 == "23333"));
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
        var config = new ReadConfig<ExcelTestDto>()
        .Add("Field0", item => item.Field0)
        .AddIf(true, "Field1", item => item.Field1)
        .Add("Field2", item => item.Field2, item => item == "Male")
        .AddIf(true, "Field3", item => item.Field3)
        ;
        var data = await config.ToEntityAsync(TestExcelPath);
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
        var config = new ReadConfig<ExcelTestDto>()
        .Add("Field0", typeof(ExcelTestDto).GetProperty("Field0"))
        .AddIf(true, "Field1", typeof(ExcelTestDto).GetProperty("Field1"))
        .Add("Field2", typeof(ExcelTestDto).GetProperty("Field2"), item => item == "Male")
        .AddIf(true, "Field3", typeof(ExcelTestDto).GetProperty("Field3"))
        ;
        var data = await config.ToEntityAsync(TestExcelPath);
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
        var data = await config.ToEntityAsync(TestExcelPath);
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
        data = await config.ToEntityAsync(TestExcelPath);
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
        var config = new ReadConfig<ExcelTestDto>(TestExcelPath)
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2, item => item == "Male")
        .Add("Field3", item => item.Field3)
        ;
        var data = await config.EPPlusToEntityAsync();
        TestData(data);

        data = await config.NPOIToEntityAsync();
        TestData(data);

        data = await config.MiniToEntityAsync();
        TestData(data);

        data = await config.ToEntityAsync();
        TestData(data);

        static void TestData(System.Collections.Generic.IEnumerable<ExcelTestDto> data)
        {
            Assert.True(data.Count() == 3000);
            Assert.True(data.Count(item => item.Field0 == "233") == 1500);
            Assert.True(data.Count(item => item.Field1 == 23) == 1500);
            Assert.True(data.Count(item => item.Field2 == true) == 1500);
            Assert.True(data.Count(item => item.Field3 == 123.23) == 1500);
        }
    }

    /// <summary>
    /// 从文件流读取
    /// </summary>
    [Fact]
    public async Task ReadConfigWithStreamTest()
    {
        using var fs = TestExcelPath.OpenReadShareStream();
        var config = new ReadConfig<ExcelTestDto>(fs)
        .Add("Field0", item => item.Field0)
        .Add("Field1", item => item.Field1)
        .Add("Field2", item => item.Field2, item => item == "Male")
        .Add("Field3", item => item.Field3)
        ;
        var data = await config.EPPlusToEntityAsync();
        TestData(data);

        data = await config.NPOIToEntityAsync();
        TestData(data);

        data = await config.MiniToEntityAsync();
        TestData(data);

        data = await config.ToEntityAsync();
        TestData(data);

        static void TestData(System.Collections.Generic.IEnumerable<ExcelTestDto> data)
        {
            Assert.True(data.Count() == 3000);
            Assert.True(data.Count(item => item.Field0 == "233") == 1500);
            Assert.True(data.Count(item => item.Field1 == 23) == 1500);
            Assert.True(data.Count(item => item.Field2 == true) == 1500);
            Assert.True(data.Count(item => item.Field3 == 123.23) == 1500);
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
        });
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
        using var reader = IExcelReader.GetExcelReader(path);
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
    [Fact]
    public void SummaryConfigTest()
    {
        var config = ReadConfig<ExcelTestDto>.GenConfigBySummary();
        Assert.True(config.FieldOption.Count() == 4);
        Assert.True(config.FieldOption.Select(item => item.ExcelField).AllContain("第一个属性", "FirstProp", "随便什么东西", "Field3"));
    }
    [Fact]
    public void StringCellOptionTestWithOutGeneric()
    {
        var config = new ReadConfig("ExcelTestDto", new[] { ("Field0", "Field0", "item=>item+\"23333\"") });
        var data = config.ToEntity(TestExcelPath);
        Assert.True(data.NotEmpty());
        Assert.True(data.All(item => (item as ExcelTestDto).Field0.EndsWith("23333")));
        Assert.True(data.All(item => !(item as ExcelTestDto).Field1.HasValue));
        config = new ReadConfig("ExcelTestDto", new[] { ("Field0", "Field0") });
        data = config.ToEntity(TestExcelPath);
        Assert.True(data.NotEmpty());
        Assert.True(data.All(item => (item as ExcelTestDto).Field0.In("233", "1122")));
        Assert.True(data.All(item => !(item as ExcelTestDto).Field1.HasValue));

        var options = new[]{
            new StringCellOption("Field0", "Field0", "item=>item+\"23333\""),
            new StringCellOption("Field1", "Field1", "item=>int.Parse(item)+23333"),
        };
        config = new ReadConfig("ExcelTestDto", options);
        data = config.ToEntity(TestExcelPath);
        Assert.True(data.NotEmpty());
        Assert.True(data.All(item => (item as ExcelTestDto).Field0.EndsWith("23333")));
        Assert.True(data.All(item => ((item as ExcelTestDto).Field1 - 23333 == 23) || ((item as ExcelTestDto).Field1 - 23333 == 12)));

        config = new ReadConfig("ExcelTestDto");
        config.Add("Field0", "Field0", "item=>item+\"23333\"")
        .AddIf(true, "Field1", "Field1", "item=>int.Parse(item)+23333")
        .AddIf(false, "Field3", "Field3", "item=>double.Parse(item)");

        Assert.True(config.FieldOption.Count() == 2);
        data = config.ToEntity(TestExcelPath);
        Assert.True(data.All(item => (item as ExcelTestDto).Field0.EndsWith("23333")));
        Assert.True(data.All(item => ((item as ExcelTestDto).Field1 - 23333 == 23) || ((item as ExcelTestDto).Field1 - 23333 == 12)));
    }

    [Fact]
    public void ExcelConfigToReadConfigTest()
    {
        var excelConfig = new ExcelConfig<ExcelTestDto, BaseCellOption<ExcelTestDto>>(new[] { ("Field0", "Field0") });
        excelConfig.Add("Field1", typeof(ExcelTestDto).GetProperty("Field1"));
        excelConfig.AddIf(false, "Field2", typeof(ExcelTestDto).GetProperty("Field2"));
        Assert.True(excelConfig.FieldOption.Count() == 2);
        var readConfig = new ReadConfig<ExcelTestDto>(excelConfig);
        Assert.True(excelConfig.FieldOption.Count() == 2);
        var data = readConfig.ToEntity(TestExcelPath);
        Assert.True(data.NotEmpty());
        Assert.True(data.All(item => item.Field0.In("233", "1122")));
        Assert.True(data.All(item => item.Field1.In(23, 12)));
    }
}
