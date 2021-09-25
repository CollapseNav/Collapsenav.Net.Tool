# [Collection]

## TODO

- [x] [`ContainAnd` 全包含](#containand)
  - [ ] 为复杂类型添加自定义Equal条件
- [x] [`ContainOr` 部分包含](#containor)
  - [ ] 为复杂类型添加自定义Equal条件
- [x] [`Merge` 合并多个集合](#merge)
  - [ ] 可选去重
  - [ ] 为复杂类型添加自定义Equal条件
- [x] [`SpliteCollection` 分割为指定大小的集合](#splitecollection)
- [x] [`WhereIf` 条件查询](#whereif)
- [ ] `Unique` 去重
- [ ] `RemoveRepeat` 移除重复
- [ ] 打乱顺序
- [ ] ...

## How To Use

### ContainAnd

**全部** Contain 则为 `True`

```csharp
string[] strList = { "1", "2", "3", "4", "5", "6" };
strList.ContainAnd(new[] { "2", "6" }); // True
strList.ContainAnd(new[] { "2", "8" }); // False
```

### ContainOr

**至少一个** Contain 则为 `True`

```csharp
string[] strList = { "1", "2", "3", "4", "5", "6" };
strList.ContainOr(new[] { "2", "6" }); // True
strList.ContainOr(new[] { "2", "8" }); // True
strList.ContainOr(new[] { "7", "8" }); // False
```

### Merge

合并 **多个集合**

```csharp
IEnumerable<int[]> nums = new List<int[]>()
{
    new[] {1,2,3},
    new[] {4,5,6},
    new[] {7,8,9},
    new[] {10},
};
var numMergeList = nums.Merge();
numMergeList = nums.Take(2).Merge(nums.TakeLast(2));
numMergeList = nums.Take(2).Merge(new[] { 7, 8, 9, 10 });
// { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
```

### SpliteCollection

将集合分割为传入的大小

```csharp
var nums = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
var numSplit = nums.SpliteCollection(2);
/*
    {1,2}
    {3,4}
    {5,6}
    {7,8}
    {9,10}
*/
numSplit = nums.SpliteCollection(3);
/*
    {1,2,3}
    {4,5,6}
    {7,8,9}
    {10}
*/
```

### WhereIf

**根据条件** 查询

```csharp
int[] intList = { 1, 1, 2, 2, 3, 3, 4 };
intList = intList.WhereIf(true, item => item > 1)
.WhereIf(false, item => item < 3)
.WhereIf("", item => item != 2)
.ToArray();
// {2,2,3,3,4}
```
