using System;
using System.Collections;
using System.Collections.Generic;

namespace Collapsenav.Net.Tool.Excel.Test;
public class ExcelTestDto
{
    public ExcelTestDto() { }
    public ExcelTestDto(string field0, int field1, bool field2, double field3)
    {
        Field0 = field0;
        Field1 = field1;
        Field2 = field2;
        Field3 = field3;
    }

    public string Field0 { get; set; }
    public int Field1 { get; set; }
    public bool Field2 { get; set; }
    public double Field3 { get; set; }

    public static IEnumerable<ExcelTestDto> ExportData(int len = 10)
    {
        var list = new List<ExcelTestDto>();
        for (; len > 0; len--)
            list.Add(new ExcelTestDto($"Name-{len}", len, len / 2 == 0, len / 2.0));
        return list;
    }
}

public class ExcelDefaultDto
{
    public ExcelDefaultDto()
    {
    }

    public ExcelDefaultDto(string field0, int field1, double field3)
    {
        Field0 = field0;
        Field1 = field1;
        Field3 = field3;
    }

    public string Field0 { get; set; }
    public int Field1 { get; set; }
    public double Field3 { get; set; }
    public static IEnumerable<ExcelDefaultDto> ExportData(int len = 10)
    {
        var list = new List<ExcelDefaultDto>();
        for (; len > 0; len--)
            list.Add(new ExcelDefaultDto($"Name-{len}", len, len / 2.0));
        return list;
    }
}


public class ExcelAttrDto
{
    public ExcelAttrDto()
    {
    }

    public ExcelAttrDto(string field0, int field1, double field3)
    {
        Field0 = field0;
        Field1 = field1;
        Field3 = field3;
    }

    [ExcelExport("字段1"), ExcelRead("字段1")]
    public string Field0 { get; set; }
    [ExcelExport("字段2"), ExcelRead("字段2")]
    public int Field1 { get; set; }
    [ExcelExport("字段3"), ExcelRead("字段3")]
    public double Field3 { get; set; }
    public static IEnumerable<ExcelAttrDto> ExportData(int len = 10)
    {
        var list = new List<ExcelAttrDto>();
        for (; len > 0; len--)
            list.Add(new ExcelAttrDto($"Name-{len}", len, len / 2.0));
        return list;
    }
}

public class ExcelConfigDto
{
    public ExcelConfigDto()
    {
    }

    public ExcelConfigDto(int num, string name, bool flag, DateTime time)
    {
        Num = num;
        Name = name;
        Flag = flag;
        Time = time;
    }

    public int Num { get; set; }
    public string Name { get; set; }
    public bool Flag { get; set; }
    public DateTime Time { get; set; }
    public static IEnumerable<ExcelConfigDto> ExportData(int len = 10)
    {
        var list = new List<ExcelConfigDto>();
        for (; len > 0; len--)
            list.Add(new ExcelConfigDto(len, $"Name-{len}", len / 2 == 0, DateTime.Now.AddDays(len + 1)));
        return list;
    }
}