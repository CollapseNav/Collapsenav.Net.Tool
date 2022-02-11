# IExcelCellRead

虽然已经有 `IExcelRead` 可以比较方便地获取excel数据, 但偶尔会有获取更加详细的信息的需求

所以参照 `IExcelRead` 做了 `IExcelCellRead`

## 接口定义

`IExcelCellRead` 的定义与 `IExcelRead` 大致相同

实现了索引器, 可以方便直观地获取单元格数据

但是不同是 `IExcelCellRead` 返回值为 `IReadCell`

```csharp
public interface IReadCell
{
    int Row { get; }
    int Col { get; }
    string StringValue { get; }
    Type ValueType { get; }
    object Value { get; set; }
}
```




