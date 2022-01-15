# EasyStart

快速介绍如何简单使用

## TL;DR

Excel表格数据
| Field0 | Field1 | Field2 | Field3 |
| ------ | ------ | ------ | ------ |
| 233    | 23     | Male   | 233.33 |
| 1122   | 12     | Female | 123.23 |
| ...    | ...    | ...    | ...    |

代码中定义的数据实体
```csharp
public class ExcelTestDto
{
    public string Field0 { get; set; }
    public int Field1 { get; set; }
    public string Field2 { get; set; }
    public double Field3 { get; set; }
}
```

从excel中 **读取/导入** 数据

```csharp
var entitysFromPath = await ReadConfig<CylinderDto>.ExcelToEntityAsync("******.xlsx");
var entitysFromStream = await ReadConfig<CylinderDto>.ExcelToEntityAsync(stream);
```

## IExcelRead

由于使用了 `NPOI,EPPlus,MiniExcel` 三种实现, 所以简单封装了一个 `IExcelRead`, 试图简化使用(暂时来看是有效果的)




