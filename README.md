# Collapsenav.Net.Tool

为了自己以后开发方便做了工具包

之前写代码的时候发现有很多实现经常重复的写,比如 `string.IsNullOrEmpty(value)` 这样的

所以就想着稍微封装一下变成 `value.IsEmpty()` `value.NotEmpty()` 这样的调用,大概更加符合"语义"

支持 **netstandard2.0;netstandard2.1;net6**

~~屈辱地给netstandard2.0做了兼容~~

[文档](http://doc.collapsenav.cn/)

* [Collapsenav.Net.Tool](./Tool/README.mdx)
  * [NuGet 地址](https://www.nuget.org/packages/Collapsenav.Net.Tool/)
  * 一些基础类型的扩展和工具
* [Collapsenav.Net.Tool.Excel](./Excel/Readme.mdx)
  * [NuGet 地址](https://www.nuget.org/packages/Collapsenav.Net.Tool.Excel/)
  * 简单的Excel操作



## TODO-1.3.*

### Tool

- [ ] 添加一个国内区划信息的包
- [ ] 优化代码结构(合并tool和ext)

### Excel

- [ ] 优化MiniCellReader的性能

### Ext

- [ ] 优化代码结构
- [ ] 丰富任务的状态管理功能

### Demo

- [ ] Data包的使用demo
- [ ] Webapi包的使用demo
- [ ] Swagger包的单独使用demo
- [ ] Excel包的使用demo

## TODO-1.4.*

### Tool

- [ ] RSA加解密
- [ ] 基于httpclient的文件上传下载

### Excel

- [ ] 实现模板导出
- [ ] 考虑修改 IExcelReader 静态接口方法的选择策略问题

### Data

- [ ] 研究基于 Freesql 的封装

### WebApi

- [ ] 研究动态API

### Ext

- [ ] 可以传参的定时任务

