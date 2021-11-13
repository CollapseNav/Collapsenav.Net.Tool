# Excel操作

基于 `EPPlus` 和 `NPOI` 做了一些针对 `Excel` 的导入导出功能

本文档基于 `EPPlus` 所作

把 `EPPlus` 全局替换为 `NPOI`

除了 [ConvertExcel](#convertexcel) 中 `EPPlus` 的专有类型之外(换为 `NPOI` 的对应类型)

就是 `NPOI` 的文档了


## Excel Data

使用的表格数据Demo, 暂时只能处理单行表头的简单excel

| Field0 | Field1 | Field2 | Field3 |
| ------ | ------ | ------ | ------ |
| 233    | 23     | Male   | 233.33 |
| 1122   | 12     | Female | 123.23 |
| 233    | 23     | Male   | 233.33 |
| 1122   | 12     | Female | 123.23 |
| 233    | 23     | Male   | 233.33 |
| 1122   | 12     | Female | 123.23 |
| 233    | 23     | Male   | 233.33 |
| 1122   | 12     | Female | 123.23 |
| ...    | ...    | ...    | ...    |

## How To Use

### 导入(Import/Read/...)

在 **我碰到的使用场景中**, 一般需要将Excel的数据转为系统中的某个实体

比如导入一个商品列表Excel,将这个列表转为 `Goods` 对象, 然后使用现成的 `AddRange(IEnumerable<Goods> datas)` 方法存到数据库中

基于以上这种 `Excel-->Entitys` 的使用场景设计了这一套东西

~~性能怎么样我就没测试了, 可能很拉了~~

假设我的实体长这样

```csharp
public class ExcelTestDto
{
    public string Field0 { get; set; }
    public int Field1 { get; set; }
    public bool Field2 { get; set; }
    public double Field3 { get; set; }
}
```

#### BuildReadConfig

第一步需要创建对应的读取配置, 这个配置决定了以什么方式读取哪些列

先创建一个 `ReadConfig`

```csharp
var config = new ReadConfig<ExcelTestDto>();
```

#### AddCellOptions

有了 `ReadConfig` 之后就需要添加具体的配置了

提供了 `Default` `Require` `Add` 添加对 **单个实体字段** 的读取设置

* `Default`
  * 不依赖表格数据,对 `ExcelTestDto` 中的属性统一添加默认值
  * ```csharp
    config.Default(item => item.Field0, "233");
    ```
* `Require`
  * 被 Require 的单元格不可为空, 否则在读取时会主动抛出异常
  * ```csharp
    config.Require("Field1", item => item.Field1);
    ```
* `Add`
  * 普通的添加单元格设置, 相对来说更加灵活一些
  * ```csharp
    config.Add("Field3", item => item.Field3)
    ```

`Require` `Add` 都可以使用 `Func<string, object>` 委托自定义对读取单元格的处理

~~由于是委托, 你可以做很多操作, 但比较容易影响性能~~

```csharp
config.Add("Field1", item => item.Field1, item =>
{
    var numCellData = int.Parse(item);
    numCellData += 2333;
    return numCellData;
});
```

以上操作都会返回 `ReadConfig` , 所以推荐写成以下的调用

```csharp
var config = new ReadConfig<ExcelTestDto>()
.Default(item => item.Field0, "233")
.Require("Field1", item => item.Field1)
.Add("Field3", item => item.Field3)
;
```

同时提供了对应的 `DefaultIf` `RequireIf` `AddIf` 方法, 用来根据不同的条件添加不同的配置

#### AddInit

偶尔会有需要在一行数据读取完之后计算点什么的需求, 所以提供一个  `AddInit` 方法, 通过传入一个 `Func<T, T>` 来搞点事情

```csharp
config.AddInit(item =>
{
    item.Field0 += "23333";
    // 一些属性的初始化也可以在这边做
    item.Field2 = false;
    return item;
});
```

#### ConvertExcel

有了配置之后就可以使用 `EPPlusExcelToEntityAsync` 将配套的excel转为实体集合

```csharp
// 如果excel是个文件流
IEnumerable<ExcelTestDto> data = await config.EPPlusExcelToEntityAsync(excelStream);
```

除了流, 也支持其他的参数

* `string filepath`
  * 简单质朴的物理文件路径
* `ExcelPackage pack`
  * `EPPlus` 的 `ExcelPackage`
* `ExcelWorksheet sheet`
  * `EPPlus` 的 `ExcelWorksheet`

#### Other

还有一些获取 表头 数据 之类的方法, 不是很关键 就不细说了

### 导出(Export/...)

有的时候总是会有人需要把列表数据导出成 Excel

~~然后像个傻逼一样再导回来~~

所以相对导入功能做了个导出功能, 两者相似度比较高

#### BuildExportConfig

```csharp
// datas 为 ExcelTestDto集合
var config = new ExportConfig<ExcelTestDto>(datas);
```

由于导出比较简单粗暴, 所以提供了一个 `GenDefaultConfig` 可以直接 **根据泛型生成 Config**

#### AddCellOption

由于导出比较简单粗暴, 所以就只有一个 `Add` 和 `AddIf` 方法添加单元格设置(~~虽然是两个~~)

```csharp
config
.Add("Field0", item => item.Field0)
.Add("Field1", item => item.Field1)
.Add("Field2", item => item.Field2 ? "Male" : "Female")
.Add("Field3", item => item.Field3);
```

#### GenerateExcel

使用 `EPPlusExportAsync` 生成 Excel

```csharp
// someStream 是个流, 本来也可以做成不需要传流的, 但我忘了, 暂时还不想改, 等下个版本
Stream stream = await exportConfig.EPPlusExportAsync(someStream);
```

方法会返回一个流, 拿到流之后可以去做你们想做的事情...

也可以传入一个物理路径(string 类型), 这样就会在指定的位置生成excel



## TODO

- [ ] 无参导出
- [ ] 合并相同的导入配置
- [ ] 根据泛型生成默认的导入配置
- [ ] 考虑添加错误处理
- [ ] 测试性能问题
- [ ] 根据配置生成导入导出配置
  * 可以将对应导入导出配置存入数据库

