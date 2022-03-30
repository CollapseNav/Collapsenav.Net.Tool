# Dictionary

## TODO

- [x] `AddOrUpdate` 添加或更新
- [x] `AddRange` 添加多个字典项
- [x] `ToDictionary` 转为字典
- [x] `GetAndRemove` 获取值并且移除字典项
- [ ] ...

## How To Use

### AddOrUpdate

添加或更新, 支持传入 `KeyValuePair` 和 单独的key+value

```csharp
Dictionary<int, string> dict = new();
dict.AddOrUpdate(new KeyValuePair<int, string>(1, "1"))
.AddOrUpdate(new KeyValuePair<int, string>(1, "1"))
.AddOrUpdate(1, "2");
// 1 : "2"
```

### AddRange

添加多个字典项, 也支持传入 `KeyValuePair` 集合

```csharp
Dictionary<int, string> dict = new();
Dictionary<int, string> dict2 = new()
{
    { 1, "1" },
    { 2, "2" },
    { 3, "3" },
};
dict.AddRange(dict2);
// 1 : "1"
// 2 : "2"
// 3 : "3"
```

### ToDictionary

将集合转为字典

```csharp
string[] nums = new[] { "1", "2", "3", "4", "5" };
IDictionary<int, string> dict = nums.ToDictionary(item => int.Parse(item));
```

### GetAndRemove

获取值并且移除字典项

```csharp

Dictionary<int, string> dict = new()
{
    { 1, "1" },
    { 2, "2" },
    { 3, "3" },
};
var value = dict.GetAndRemove(1);
```






