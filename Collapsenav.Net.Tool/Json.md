# Json

## TODO

- [x] [`ToObj` 字符串转对象](#toobj)
- [x] [`ToObjCollection` 字符串转对象集合](#toobjcollection)
- [x] [`ToJson` 对象转字符串](#tojson)
- [ ] ...

```csharp
public class UserInfo
{
    public string UserName { get; set; }
    public int Age { get; set; }
}
```

## How To Use

### ToObj

```csharp
string userInfo = @"
{
    ""userName"": ""ABCD"",
    ""age"": 23
}";
var user = userInfo.ToObj<UserInfo>();
// user.UserName == "ABCD";
// user.Age == 23;
```

### ToObjCollection

```csharp
string userInfo = @"
[
    {
        ""userName"": ""ABCD"",
        ""age"": 23
    }
]";
IEnumerable<UserInfo> user = userInfo.ToObjCollection<UserInfo>();
// user.First().UserName = "ABCD";
// user.First().Age = 23;
```

### ToJson

```csharp
UserInfo user = new()
{
    UserName = "ABCD",
    Age = 23
};
var jsonString = user.ToJson().Trim();
// "{\"userName\":\"ABCD\",\"age\":23}"
```

### JsonMap


