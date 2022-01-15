# BestPractice-最佳实践

一些方便好用的 **"最佳实践"**

## 读取/导入

Excel表格数据
| Field0 | Field1 | Field2 | Field3 |
| ------ | ------ | ------ | ------ |
| 233    | 23     | Male   | 233.33 |
| 1122   | 12     | Female | 123.23 |
| ...    | ...    | ...    | ...    |

### 简单使用

最简单的使用方式, 实体定义之后只需一行代码即可完成

#### 根据属性(Property)读取

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

不需要额外配置, 直接传入 Excel文件 的**文件路径或者流**即可

```csharp
IEnumerable<CylinderDto> entitysFromPath = await ReadConfig<CylinderDto>.ExcelToEntityAsync("******.xlsx");
IEnumerable<CylinderDto> entitysFromStream = await ReadConfig<CylinderDto>.ExcelToEntityAsync(stream);
```

#### 根据注解(Attribute)读取

代码中定义的数据实体
```csharp
public class ExcelTestDto
{
    [ExcelRead("Field0")]
    public string Name { get; set; }
    [ExcelRead("Field1")]
    public int Age { get; set; }
    [ExcelRead("Field2")]
    public string Gender { get; set; }
    [ExcelRead("Field3")]
    public double Height { get; set; }
}
```

不需要额外配置, 直接传入 Excel文件 的**文件路径或者流**即可

```csharp
IEnumerable<CylinderDto> entitysFromPath = await ReadConfig<CylinderDto>.ExcelToEntityAsync("******.xlsx");
IEnumerable<CylinderDto> entitysFromStream = await ReadConfig<CylinderDto>.ExcelToEntityAsync(stream);
```

与上方使用属性读取的代码相同, **自动优先使用注解读取, 若无注解, 则使用属性**

### 自定义读取配置

"**简单使用**是**有极限**的!"

"我从短暂的撸代码生涯中学到一件事......"

"越是想要搞骚操作, 就会发现**简单使用**适用范围不够......"

"除非**自定义读取配置**"

```csharp
var config = new ReadConfig<ExcelTestDto>()
.Add("Field0", item => item.Field0, item => { return item; })
.Add("Field1", item => item.Field1)
.Add("Field2", item => item.Field2, item => item == "Male" ? "男" : "女")
.Add("Field3", item => item.Field3)
;
```

然后传入 Excel文件 的**文件路径或者流**即可

```csharp
IEnumerable<CylinderDto> entitys = await config.ToEntityAsync("******.xlsx");
IEnumerable<CylinderDto> entitys = await config.ToEntityAsync(stream);
```

