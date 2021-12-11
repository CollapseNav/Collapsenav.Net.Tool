# Stream

## TODO

- [x] [`ToBytes` 流转为bytes](#tobytes)
- [x] [`ToStream` bytes转为流](#tostream)
- [x] [`SaveTo` 保存为文件](#saveto)
- [x] [`Seek` 流指针移动](#seek)
- [ ] ...

## How To Use

### ToBytes

```csharp
var filePath = "./XXXXX";
using var fs = new FileStream(filePath, FileMode.OpenOrCreate);
var bytes = fs.ToBytes();
```

### ToStream

```csharp
var filePath = "./XXXXX";
using var fs = new FileStream(filePath, FileMode.OpenOrCreate);
var stream = fs.ToBytes().ToStream();
```

### SaveTo

```csharp
var filePath = "./XXXXX";
using var fs = new FileStream(filePath, FileMode.OpenOrCreate);
string toFile = "./PPPPP";
fs.SaveTo(toFile);
fs.ToBytes().SaveTo(toFile);
```

### Seek

```csharp
var filePath = "./XXXXX";
using var fs = new FileStream(filePath, FileMode.OpenOrCreate);
fs.SeekToEnd();
fs.SeekToOrigin();
fs.SeekTo(0);
```