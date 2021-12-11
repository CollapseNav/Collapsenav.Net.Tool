# string

## TODO

- [x] [`IsNull` 判空](#nullempty)
- [x] [`PadLeft` 左填充 `PadRight` 右填充](#padleftandright)
- [x] [`ToChinese` 长度16以下的整数字符串转为中文](#tochinese)
- [x] [`ToXXX` 字符串转为基础数据类型](#toxxx)
- [x] [`Join` Join](#join)
- [x] [`CanPing` 检测域名可通](#canping)
- [ ] `IsEmail` 检查是否邮箱格式
  - [x] 仅仅只是检查邮箱格式
  - [ ] 检查邮箱是否存在
- [ ] `IsUrl` 检查是否 Url 格式
  - [x] 检查是否 `http,https` 开头
  - [ ] 检查域名是否可通
- [x] [`StartsWith`&`EndsWith` 支持传入集合判断 StartsWith 和 EndsWith](#startswitendswith)
- [ ] 去除如 `\n\r` 的转义字符
- [ ] [`First` `Last` 取前后固定长度](#firstlast)
- [ ] ...

## How To Use

### Null/Empty

`string`里用到过的最多的判空操作

```csharp
string empty = "";
string notEmpty = "NotEmpty";
string whiteSpace = "   ";
empty.IsNull() && empty.IsEmpty(); //True
notEmpty.NotEmpty() && notEmpty.NotNull(); //True
notEmpty.IsNull(); //False
whiteSpace.IsEmpty(); //True
empty.NotEmpty(); //False
whiteSpace.NotNull(); //False
```

根据实际碰到的情况, 若字符串为空, 则返回传入的值

```csharp
empty.IsNull("233"); // "233"
notEmpty.IsEmpty("233"); // "NotEmpty"
```

### ToChinese

将数字字符串转为中文, 不是很成熟

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

不限 `Join` 的数据类型, 可通过委托指定 **Join** 的内容

```csharp
string strJoin = "233.233.233.233";
strJoin.Join("@"); // "2@3@3@.@2@3@3@.@2@3@3@.@2@3@3"
int[] intJoin = { 1, 2, 3 };
intJoin.Join("@"); // "1@2@3"
intJoin.Join("@", item => -item); // "-1@-2@-3"
```

### CanPing

```csharp
string url = "https://www.bilibili.com/";
url.CanPing(); // 如果联网了,应该为 True
```

### PadLeftAndRight

```csharp
int iValue = 233;
double dValue = 2.33;
long lValue = 23333;

iValue.PadLeft(6); // "   233"
dValue.PadLeft(6); // "  2.33"
lValue.PadLeft(6); // " 23333"

iValue.PadLeft(6, '-'); // "---233"
dValue.PadLeft(6, '-'); // "--2.33"
lValue.PadLeft(6, '-'); // "-23333"

iValue.PadRight(6); // "233   "
dValue.PadRight(6); // "2.33  "
lValue.PadRight(6); // "23333 "

iValue.PadRight(6, '-'); // "233---"
dValue.PadRight(6, '-'); // "2.33--"
lValue.PadRight(6, '-'); // "23333-"

iValue.PadLeft(6, item => item + 1, '-'); // "---234"
dValue.PadLeft(6, item => item + 1, '-'); // "--3.33"
lValue.PadLeft(6, item => item + 1, '-'); // "-23334"

iValue.PadRight(6, item => item + 1, '-'); // "234---"
dValue.PadRight(6, item => item + 1, '-'); // "3.33--"
lValue.PadRight(6, item => item + 1, '-'); // "23334-"
```

### StartsWit&&EndsWith

```csharp
string[] strs = new[] { "2333", "233333333333333", "2332" };
strs.HasStartsWith("233");  // True
strs.HasEndsWith("33");  // True
strs.AllStartsWith("2333");  // False
strs.AllEndsWith("33");  // False

// 可忽略大小写(默认大小写敏感)
strs = new[] { "ABCD", "AbcD", "abcd" };
strs.HasStartsWith("aBcd", true)
strs.AllStartsWith("aBcd", true)
strs.HasEndsWith("aBcd", true)
strs.AllEndsWith("aBcd", true)
```

~~感觉没有什么人会用这种的~~

```csharp
string exampleString = "23333333333333";
exampleString.AllStartsWith("23", "233", "233333"); // True
exampleString.HasStartsWith("223", "233", "233333"); // True
exampleString.AllEndsWith("333333", "33", "3"); // True
exampleString.HasEndsWith("333333", "33", "32"); // True
```

### FirstLast

```csharp
string str = "12345678987654321";
str.First(5); // "12345"
str.Last(5); // "54321"
```
