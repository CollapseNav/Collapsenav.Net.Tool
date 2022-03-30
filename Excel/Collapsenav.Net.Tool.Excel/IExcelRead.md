# IExcelRead

由于使用了 `NPOI,EPPlus,MiniExcel` 三种实现, 所以简单封装了一个 `IExcelRead`, 试图简化使用(暂时来看是有效果的)

## 接口定义

`IExcelRead` 的定义类似下面这样

```csharp
public interface IExcelRead<T> : IDisposable
{
    IEnumerable<T> this[string field] { get; }
    IEnumerable<T> this[long row] { get; }
    T this[long row, long col] { get; }
    T this[string field, long row] { get; }
    IEnumerable<string> Headers { get; }
    IDictionary<string, int> HeadersWithIndex { get; }
    long RowCount { get; }
}
```

为了方便使用, 又有

```csharp
public interface IExcelRead : IExcelRead<string> { }
```

会将excel中的所有数据都ToString

## 获取实例

现阶段拥有 `NPOIExcelRead EPPlusExcelRead MiniExcelRead` 三种实现

同时为了降低心智负担, `IExcelRead` 有静态方法 `GetExcelRead`, 用以自动生成 `IExcelRead` 的实例

```csharp
using IExcelRead reader = IExcelRead.GetExcelRead(filePath);
```

5M 以上的自动使用 `MiniExcel` 作为实现, 其他使用 EPPlus 或 NPOI

## 使用

### 获取单元格的值

```csharp
// 第 1 行 第 6 列
string value = reader[0, 5];
```

### 获取行

```csharp
var row = read[0];
```

### 获取列

我认为"列"与"行"不同, 是不适合直接用下标去获取的, 所以设计成传有"含义"的列名称

```csharp
IEnumerable<string> col = read["Column"];
```

为了降低损耗, 使用了 `yield return`, 但是有没有效果, 有多大效果, 这我就不知道了, 暂时也没有能力去验证

