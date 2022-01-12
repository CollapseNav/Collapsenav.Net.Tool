using System;

namespace Collapsenav.Net.Tool.Excel.Test;
public class ExcelTestDto
{
    public string Field0 { get; set; }
    public int Field1 { get; set; }
    public bool Field2 { get; set; }
    public double Field3 { get; set; }
}

public class ExcelDefaultDto
{
    public string Field0 { get; set; }
    public int Field1 { get; set; }
    public double Field3 { get; set; }
}


public class ExcelAttrDto
{
    [ExcelExport("字段1"), ExcelRead("字段1")]
    public string Field0 { get; set; }
    [ExcelExport("字段2"), ExcelRead("字段2")]
    public int Field1 { get; set; }
    [ExcelExport("字段3"), ExcelRead("字段3")]
    public double Field3 { get; set; }
}

public class ExcelConfigDto
{
    public int Num { get; set; }
    public string Name { get; set; }
    public bool Flag { get; set; }
    public DateTime Time { get; set; }
}