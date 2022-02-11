# Collapsenav.Net.Tool


适用于 **netstandard2.1;net6**

由于 **net2.1 LTS 已过**, 所以放弃 **netstandard2.0** 的支持

~~谁有兴趣谁去支持吧,谁有需要谁去支持吧~~

[项目地址\(GitHub\)](https://github.com/CollapseNav/Collapsenav.Net.Tool)

## TL;DR

之前写代码的时候发现有很多实现经常重复的写,比如 `string.IsNullOrEmpty(value)` 这样的

所以就想着稍微封装一下变成 `value.IsEmpty()` `value.NotEmpty()` 这样的调用,大概更加符合"语义"

* [Collapsenav.Net.Tool](./Collapsenav.Net.Tool/README.md)
  * [NuGet 地址](https://www.nuget.org/packages/Collapsenav.Net.Tool/)
  * 一些基础类型的扩展和工具,`string,DateTime,Collection,File,Security....`
* [Collapsenav.Net.Tool.Excel](./Collapsenav.Net.Tool.Excel/Readme.md)
  * [NuGet 地址](https://www.nuget.org/packages/Collapsenav.Net.Tool.Excel/)
  * 简单的Excel操作(基于EPPlus实现)

