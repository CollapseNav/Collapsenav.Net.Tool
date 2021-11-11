# StringBuilder

## TODO

- [x] [`AddIf` 条件拼接](#addif)
- [x] [`AndIf` 提供 Dapper使用](#andif)
- [x] [`OrIf` 提供 Dapper使用](#orif)

## How To Use

### AddIf

```csharp
StringBuilder sb = new StringBuilder();
.AddIf(true, "1")
.AddIf(true, "2")
.AddIf("3")
.AddIf(false, "4")
.AddIf("5", "6")
;// 1236
```

### AndIf

我自己在使用 `Dapper` 的时候会这样拼接 `sql`

```csharp
StringBuilder sb = new StringBuilder();
.AndIf(true, "1")
.AndIf(true, "2")
.AndIf("3")
.AndIf(false, "4")
.AndIf("5", "6")
;//  AND 1 AND 3 AND 6
```

### OrIf

我自己在使用 `Dapper` 的时候会这样拼接 `sql`

```csharp
StringBuilder sb = new StringBuilder();
.OrIf(true, "1")
.OrIf(true, "2")
.OrIf("3")
.OrIf(false, "4")
.OrIf("5", "6")
;//  OR 1 OR 3 OR 6
```
