# FileStream

`FileStream` 相关的扩展暂时基于 `string`

之前创建文件流的方式大概是

```csharp
using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
```

然后感觉太麻烦了, 然后就写了扩展

```csharp
using var fs = path.OpenStream();
using var fs = path.OpenReadStream();
using var fs = path.OpenReadShareStream();
using var fs = path.OpenWriteStream();
using var fs = path.OpenWriteShareStream();
using var fs = path.OpenReadWirteStream();
using var fs = path.OpenReadWirteShareStream();

using var fs = path.CreateStream();
using var fs = path.CreateReadStream();
using var fs = path.CreateReadShareStream();
using var fs = path.CreateWriteStream();
using var fs = path.CreateWriteShareStream();
using var fs = path.CreateReadWirteStream();
using var fs = path.CreateReadWirteShareStream();

using var fs = path.OpenCreateStream();
using var fs = path.OpenCreateReadStream();
using var fs = path.OpenCreateReadShareStream();
using var fs = path.OpenCreateWriteStream();
using var fs = path.OpenCreateWriteShareStream();
using var fs = path.OpenCreateReadWirteStream();
using var fs = path.OpenCreateReadWirteShareStream();
```

感觉用起来大概会方便一点点
