# [Collection]

## TODO

- [x] [`ContainAnd` 全包含](#containand)
  - [x] 为复杂类型添加自定义Equal条件
- [x] [`ContainOr` 部分包含](#containor)
  - [x] 为复杂类型添加自定义Equal条件
- [x] [`Merge` 合并多个集合](#merge)
  - [x] 可选去重
  - [x] 为复杂类型添加自定义Equal条件
- [x] [`SpliteCollection` 分割为指定大小的集合](#splitecollection)
- [x] [`WhereIf` 条件查询](#whereif)
- [x] `Unique` 去重
- [x] `SequenceEqual` 判断集合相等
- [x] `Empty` 集合判空
- [ ] `RemoveRepeat` 移除重复
- [ ] 打乱顺序
- [ ] ...

## How To Use

### Empty

```csharp
string[] nullArray = null;
List<string> emptyList = new();
int[] notEmptyArray = new[] { 1, 2, 3, 4 };
nullArray.IsEmpty(); //true
emptyList.NotEmpty(); // false
emptyList.IsEmpty(); // true
notEmptyArray.NotEmpty(); // true
notEmptyArray.IsEmpty(); // false

```

### ContainAnd

**全部** Contain 则为 `True`

```csharp
string[] strList = { "1", "2", "3", "4", "5", "6" };
strList.ContainAnd(new[] { "2", "6" }); // True
strList.ContainAnd(new[] { "2", "8" }); // False

strList.ContainAnd((x, y) => x == y, new[] { "2", "6" }); // True
strList.ContainAnd((x, y) => x == y, new[] { "2", "8" }); // False
```

### ContainOr

**至少一个** Contain 则为 `True`

```csharp
string[] strList = { "1", "2", "3", "4", "5", "6" };
strList.ContainOr(new[] { "2", "6" }); // True
strList.ContainOr(new[] { "2", "8" }); // True
strList.ContainOr(new[] { "7", "8" }); // False

strList.ContainOr((x, y) => x == y, new[] { "2", "6" }); // True
strList.ContainOr((x, y) => x == y, new[] { "2", "8" }); // True
strList.ContainOr((x, y) => x == y, new[] { "7", "8" }); // False
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

numMergeList = nums.Merge((x, y) => x == y, item => item.GetHashCode());
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

### Unique

```csharp
int[] intList = { 1, 1, 2, 2, 3, 3, 4 };
intList.Unique();
intList.Unique(item => item);
intList.Unique((x, y) => x == y);
// 1,2,3,4
```

### SequenceEqual

稍微扩展了原来的 `IEnumerable.SequenceEqual` , 方便了调用

```csharp
IEnumerable<UserInfo> userInfos = new List<UserInfo> {
    new UserInfo{UserName="23331",Age=231 },
    new UserInfo{UserName="23332",Age=232 },
    new UserInfo{UserName="23333",Age=233 },
    new UserInfo{UserName="23334",Age=234 },
};
IEnumerable<UserInfo> userInfosCopy = new List<UserInfo> {
    new UserInfo{UserName="23331",Age=231 },
    new UserInfo{UserName="23332",Age=232 },
    new UserInfo{UserName="23333",Age=233 },
    new UserInfo{UserName="23334",Age=234 },
};
IEnumerable<UserInfo> userInfosNotCopy = new List<UserInfo> {
    new UserInfo{UserName="23331",Age=23 },
    new UserInfo{UserName="23332",Age=23 },
    new UserInfo{UserName="23333",Age=23 },
    new UserInfo{UserName="23334",Age=23 },
};
userInfos.SequenceEqual(userInfosCopy); // false
userInfos.SequenceEqual(userInfosCopy, (x, y) => x.UserName == y.UserName); // true
userInfos.SequenceEqual(userInfosCopy, item => item.Age.GetHashCode()); // true

userInfos.SequenceEqual(userInfosNotCopy); // false
userInfos.SequenceEqual(userInfosNotCopy, (x, y) => x.UserName == y.UserName); // true
userInfos.SequenceEqual(userInfosNotCopy, item => item.Age.GetHashCode()); // false
```



