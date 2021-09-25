# string

## TODO

- [x] `IsNull` 判空
- [x] `PadLeft` 左填充
- [x] `PadRight` 右填充
- [x] [`ToChinese` 长度16以下的整数字符串转为中文](#tochinese)
- [x] [`ToXXX` 字符串转为基础数据类型](#toxxx)
- [x] [`Join` Join](#join)
- [ ] `IsEmail` 检查是否邮箱格式
  - [x] 仅仅只是检查邮箱格式
  - [ ] 检查邮箱是否存在
- [ ] `IsUrl` 检查是否 Url 格式
  - [x] 检查是否 `http,https` 开头
  - [ ] 检查域名是否可通
- [x] `StartWiths` 支持传入集合的 StartsWith
- [x] `EndWiths` 检查是否 Url 格式
- [ ] 去除如 `\n\r` 的转义字符
- [ ] ...

## How To Use

### ToChinese

将数字字符串转为中文

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

### ToXXX

字符串转基本数据格式

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

### Join

不限 `Join` 的数据类型
可通过委托指定 **Join** 的内容

```csharp
string strJoin = "233.233.233.233";
strJoin.Join("@"); // "2@3@3@.@2@3@3@.@2@3@3@.@2@3@3"
int[] intJoin = { 1, 2, 3 };
intJoin.Join("@"); // "1@2@3"
intJoin.Join("@", item => -item); // "-1@-2@-3"
```


