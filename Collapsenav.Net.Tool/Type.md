# Type

## TODO

- [x] [`IsBuildInType/IsBaseType` 判断是否基本数据类型](#isbuildintypeisbasetype)
- [x] [`PropNames` 获取属性名称](#propnames)
- [ ] 反射赋值

```csharp
public class PropTest1
{
    public string Prop1 { get; set; }
    public string Prop2 { get; set; }
    public string Prop3 { get; set; }
}
public class PropTest0
{
    public string Prop0 { get; set; }
    public PropTest1 Prop { get; set; }
}
```

## How To Use

### IsBuildInType/IsBaseType

内建类型/基础类型/内置类型(差不多意思的其他说法)

`Boolean,Byte,SByte,Char,Decimal,Double,Single,Int32`
`UInt32,IntPtr,UIntPtr,Int64,UInt64,Int16,UInt16,String`

```csharp
int intValue = 2333;
long longValue = 2333;
double doubleValue = 2333.2333;
string stringValue = "23333";
bool boolValue = true;
...

intValue.IsBuildInType(); // True
longValue.IsBuildInType(); // True
doubleValue.IsBuildInType(); // True
stringValue.IsBuildInType(); // True
boolValue.IsBuildInType(); // True
boolValue.IsBaseType(); // True
TypeTool.IsBuildInType<TypeTest>(); // False
TypeTool.IsBaseType<TypeTest>(); // False
...
```

### PropNames

获取属性的名称,可设定递归深度

```csharp
var props = typeof(PropTest1).PropNames();
// {"Prop1", "Prop2", "Prop3"}
props = new PropTest0().PropNames();
// {Prop0", "Prop"}
props = new PropTest0().PropNames(1);
// {"Prop0", "Prop.Prop1", "Prop.Prop2", "Prop.Prop3"}
props = typeof(PropTest0).PropNames(0);
// {Prop0", "Prop"}
```

