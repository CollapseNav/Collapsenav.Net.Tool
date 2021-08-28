# Collapsenav.Net.Tool

## Intro

一些可能会方便日常使用的操作

现阶段都做了扩展方法(未来可能会有不方便做扩展方法的)

## TODO

### Json操作

#### Json字符串转为对象

```csharp
string userInfo = $@"
{
    "userName": "ABCD",
    "age": 23
}
";
var user = userInfo.ToObj<UserInfo>();
```
