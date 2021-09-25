# Collapsenav.Net.Tool

## Intro

一些可能会方便日常使用的操作

现阶段都做了扩展方法\(未来可能会有不方便做扩展方法的\)

## TODO

* [x] `DateTime`
  * [x] `ToTimestamp` 时间转为时间戳
  * [x] `ToDateTime` 时间戳转为时间
  * [x] `ToYear` 等只保留时间到指定的单位
  * [ ] 计算 几月几日 是哪一周的星期几/星期几/一年的第几天......
  * [ ] 计算 两个日期之间的时间差\(各种单位\)
  * [ ] 获取 时间段内的所有日期\(返回有序的时间集合\)
  * [ ] 判断是否为闰年
  * [ ] ...
* [ ] `Type`
  * [ ] 反射赋值
  * [ ] 判断是否基本数据类型
* [ ] `string`
  * [x] `IsNull` 判空
  * [x] `PadLeft` 左填充
  * [x] `PadRight` 右填充
  * [x] `ToChinese` 长度16以下的整数字符串转为中文
  * [x] `ToInt` 等字符串转为基础数据类型
  * [x] `Join` Join
  * [ ] `IsEmail` 检查是否邮箱格式
  * [x] 仅仅只是检查邮箱格式
  * [ ] 检查邮箱是否存在
  * [ ] `IsUrl` 检查是否 Url 格式
  * [x] 检查是否 `http,https` 开头
  * [ ] 检查域名是否可通
  * [x] `StartWiths` 支持传入集合的 StartsWith
  * [x] `EndWiths` 检查是否 Url 格式
  * [ ] 去除如 `\n\r` 的转义字符
  * [ ] ...
* [ ] `Collection`
  * [x] `ContainAnd` 全包含
  * [x] `ContainOr` 部分包含
  * [x] `SpliteCollection` 分割为指定大小的集合
  * [x] `WhereIf` 条件查询
  * [ ] `Unique` 去重
  * [ ] `RemoveRepeat` 移除重复
  * [ ] 打乱顺序
  * [ ] 合并集合
  * [ ] ...
* [x] `Json`
  * [x] `ToObj` 字符串转对象
  * [x] `ToObjCollection` 字符串转对象集合
  * [x] `ToJson` 对象转字符串
  * [ ] ...
* [ ] `StringBuilder`
  * [x] `AddIf` 条件拼接
  * [x] `AndIf` 提供 Dapper使用
  * [x] `OrIf` 提供 Dapper使用
* [ ] 生成假数据
* [ ] 加密解密
  * [x] `MD5Tool.Encrypt` MD5 计算
  * [x] `AESTool.Encrypt` AES 加密
  * [x] `AESTool.Decrypt` AES 解密
  * [x] `Sha1Tool` sha1 sha256 计算
  * [x] `Sha256Tool` sha256 计算
  * [x] `MD5Tool.Encrypt` 文件计算 MD5
  * [x] `Sha1Tool.Encrypt` 文件计算 sha1

## Feature

### [CollectionTool](https://github.com/CollapseNav/Collapsenav.Net.Tool/tree/8505bf913004442539cc2c0371265727b95f8b69/Collapsenav.Net.Tool/Tools/Collection/CollectionTool.cs)

[CollectionExt](https://github.com/CollapseNav/Collapsenav.Net.Tool/tree/8505bf913004442539cc2c0371265727b95f8b69/Collapsenav.Net.Tool/Extensions/CollectionExt.cs)

### Json操作

#### Json字符串转为对象

```csharp
string userInfo = $@"
{
    "userName": "ABCD",
    "age": 23
}
";
UserInfo user = userInfo.ToObj<UserInfo>();
```

#### Json字符串转为对象集合

```csharp
string userInfo = $@"
[
    {
        "userName": "ABCD",
        "age": 23
    },
    {
        "userName": "EFGH",
        "age": 32
    }
]
";
IEnumerable<UserInfo> user = userInfo.ToObjCollection<UserInfo>();
```

#### 对象转为Json字符串

```csharp
List<UserInfo> userInfos = new List<UserInfo>();
string userInfoString = userInfos.ToJson();
```

### 日期操作

```csharp
DateTime date = DateTime.Now;
date.ToTimestamp(); // 时间戳
date.ToYear(); // 2021-0-0 ...
date.ToMonth(); // 2021-8-0 ...
date.ToDay(); // 2021-8-29 ...
// ......
```

### 集合操作

#### 集合ContainAnd

```csharp
string[] strList = { "1", "2", "3", "4", "5", "6" };
strList.ContainAnd(new[] { "2", "6" }); // True
strList.ContainAnd(new[] { "2", "8" }); // False
```

#### 集合ContainOr

```csharp
string[] strList = { "1", "2", "3", "4", "5", "6" };
strList.ContainOr(new[] { "2", "6" }); // True
strList.ContainOr(new[] { "2", "8" }); // True
strList.ContainOr(new[] { "7", "8" }); // False
```

### String字符串操作

#### 数字转中文

```csharp
string num = "10510001010";
num.ToChinese(); // "一百零五亿一千万零一千零一十"
num = "1010";
num.ToChinese(); // "一千零一十"
num = "99999";
num.ToChinese(); // "九万九千九百九十九"
num = "203300010001";
num.ToChinese(); // "二千零三十三亿零一万零一"
num = "9999999999999999";
num.ToChinese(); // "九千九百九十九万九千九百九十九亿九千九百九十九万九千九百九十九"
```

#### 字符串转基本数据格式

```csharp
// 日期
string timeString = "2021-08-28 23:23:23";
timeString.ToDateTime();
// 整数Int32
string intString = "2333";
intString.ToInt();
// 浮点数
string doubleString = "233.333";
doubleString.ToDouble();
// Guid
string guidString = "00000000-0000-0000-0000-000000000000";
guidString.ToGuid();
// 长整型
string longString = "233333333333333";
longString.ToLong();
```

#### 字符串Join操作

不限 `Join` 的数据类型 我的 `Join` 定义为 `Join<T>(this IEnumerable<T> query, string separate, Func<T, object> getStr = null)` 可将任意类型通过 `getStr` 传入的委托转为字符串

```csharp
string strJoin = "233.233.233.233";
strJoin.Join("@"); // "2@3@3@.@2@3@3@.@2@3@3@.@2@3@3"
int[] intJoin = { 1, 2, 3 };
intJoin.Join("@"); // "1@2@3"
intJoin.Join("@", item => -item); // "-1@-2@-3"
```

