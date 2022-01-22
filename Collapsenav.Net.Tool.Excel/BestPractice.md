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
IEnumerable<ExcelTestDto> entitysFromPath = await ReadConfig<ExcelTestDto>.ExcelToEntityAsync("******.xlsx");
IEnumerable<ExcelTestDto> entitysFromStream = await ReadConfig<ExcelTestDto>.ExcelToEntityAsync(stream);
// OR
IEnumerable<ExcelTestDto> entitysFromPath = await ExcelReadTool.ExcelToEntityAsync<ExcelTestDto>("******.xlsx");
IEnumerable<ExcelTestDto> entitysFromStream = await ExcelReadTool.ExcelToEntityAsync<ExcelTestDto>(stream);
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
IEnumerable<ExcelTestDto> entitysFromPath = await ReadConfig<ExcelTestDto>.ExcelToEntityAsync("******.xlsx");
IEnumerable<ExcelTestDto> entitysFromStream = await ReadConfig<ExcelTestDto>.ExcelToEntityAsync(stream);
// OR
IEnumerable<ExcelTestDto> entitysFromPath = await ExcelReadTool.ExcelToEntityAsync<ExcelTestDto>("******.xlsx");
IEnumerable<ExcelTestDto> entitysFromStream = await ExcelReadTool.ExcelToEntityAsync<ExcelTestDto>(stream);
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
IEnumerable<ExcelTestDto> entitys = await config.ToEntityAsync("******.xlsx");
IEnumerable<ExcelTestDto> entitys = await config.ToEntityAsync(stream);
```

#### 自定义-AddInit

有的时候可能需要在所有数据读完之后做个**初始化或者其他什么计算操作**

这时就可以使用 `AddInit`

```csharp
var config = new ReadConfig<ExcelTestDto>()
.Add("Field0", item => item.Field0, item => { return item; })
.Add("Field1", item => item.Field1)
.Add("Field2", item => item.Field2, item => item == "Male" ? "男" : "女")
.Add("Field3", item => item.Field3)
.AddInit(item =>
{
    item.Field2 += $" {item.Field1} 岁";
    return item;
})
;
```

### 数据库中的配置?

偶尔会有把配置存在数据库中的情况, 这时候全都用字符串即可

```csharp
var config = new ReadConfig<ExcelTestDto>()
.Add("Field0", "Field0")
.Add("Field1", "Field1")
.Add("Field2", "Field2")
.Add("Field3", "Field3")
;
// 如果从数据库里读到了配置集合
foreach (var (field, prop) in collection)
    config.Add(field, prop);
```

## 导出

### 简单使用

最简单的使用方式, 实体定义之后只需一行代码即可完成

#### 根据属性(Property)导出

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

```csharp
IEnumerable<ExcelTestDto> exportData = new[] {
    new ExcelTestDto{Field0="Name-1",Field1=1,Field2="Male",Field3= 1},
    new ExcelTestDto{Field0="Name-2",Field1=2,Field2="Male",Field3= 2},
    new ExcelTestDto{Field0="Name-3",Field1=3,Field2="Male",Field3= 3},
    new ExcelTestDto{Field0="Name-4",Field1=4,Field2="Male",Field3= 4},
    new ExcelTestDto{Field0="Name-5",Field1=5,Field2="Male",Field3= 5},
    new ExcelTestDto{Field0="Name-6",Field1=6,Field2="Male",Field3= 6},
    new ExcelTestDto{Field0="Name-7",Field1=7,Field2="Male",Field3= 7},
    new ExcelTestDto{Field0="Name-8",Field1=8,Field2="Male",Field3= 8},
    new ExcelTestDto{Field0="Name-9",Field1=9,Field2="Male",Field3= 9},
    new ExcelTestDto{Field0="Name-10",Field1=10,Field2="Male",Field3= 10}
};
await ExportConfig<ExcelTestDto>.DataExportAsync(filePath, exportData);
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

```csharp
IEnumerable<ExcelTestDto> exportData = new[] {
    new ExcelTestDto{Field0="Name-1",Field1=1,Field2="Male",Field3= 1},
    new ExcelTestDto{Field0="Name-2",Field1=2,Field2="Male",Field3= 2},
    new ExcelTestDto{Field0="Name-3",Field1=3,Field2="Male",Field3= 3},
    new ExcelTestDto{Field0="Name-4",Field1=4,Field2="Male",Field3= 4},
    new ExcelTestDto{Field0="Name-5",Field1=5,Field2="Male",Field3= 5},
    new ExcelTestDto{Field0="Name-6",Field1=6,Field2="Male",Field3= 6},
    new ExcelTestDto{Field0="Name-7",Field1=7,Field2="Male",Field3= 7},
    new ExcelTestDto{Field0="Name-8",Field1=8,Field2="Male",Field3= 8},
    new ExcelTestDto{Field0="Name-9",Field1=9,Field2="Male",Field3= 9},
    new ExcelTestDto{Field0="Name-10",Field1=10,Field2="Male",Field3= 10}
};
await ExportConfig<ExcelTestDto>.DataExportAsync(filePath, exportData);
```

### 自定义导出配置

```csharp
IEnumerable<ExcelTestDto> exportData = new[] {
    new ExcelTestDto{Field0="Name-1",Field1=1,Field2="Male",Field3= 1},
    new ExcelTestDto{Field0="Name-2",Field1=2,Field2="Male",Field3= 2},
    new ExcelTestDto{Field0="Name-3",Field1=3,Field2="Male",Field3= 3},
    new ExcelTestDto{Field0="Name-4",Field1=4,Field2="Male",Field3= 4},
    new ExcelTestDto{Field0="Name-5",Field1=5,Field2="Male",Field3= 5},
    new ExcelTestDto{Field0="Name-6",Field1=6,Field2="Male",Field3= 6},
    new ExcelTestDto{Field0="Name-7",Field1=7,Field2="Male",Field3= 7},
    new ExcelTestDto{Field0="Name-8",Field1=8,Field2="Male",Field3= 8},
    new ExcelTestDto{Field0="Name-9",Field1=9,Field2="Male",Field3= 9},
    new ExcelTestDto{Field0="Name-10",Field1=10,Field2="Male",Field3= 10}
};
var exportConfig = new ExportConfig<ExcelTestDto>(exportData)
.Add("Field0", item => item.Field0)
.Add("Field1", item => item.Field1)
.Add("Field2", item => item.Field2 == "Male" ? "男" : "女")
.Add("Field3", item => item.Field3)
;
await exportConfig.DataExportAsync(filePath);
```

