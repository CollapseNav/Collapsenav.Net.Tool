# Security

## TODO

- [x] `MD5Tool.Encrypt` MD5 计算
- [x] `AESTool.Encrypt` AES 加密
- [x] `AESTool.Decrypt` AES 解密
- [x] `Sha1Tool` sha1 sha256 计算
- [x] `Sha256Tool` sha256 计算
- [x] `MD5Tool.Encrypt` 文件计算 MD5
- [x] `Sha1Tool.Encrypt` 文件计算 sha1

## How To Use

### Aes

```csharp
var msg = "123123123";
var en = AESTool.Encrypt(msg);
var de = AESTool.Decrypt(en)

en = AESTool.Encrypt(msg, "123123123123");
de = AESTool.Decrypt(en, "123123123123")

en = msg.AesEn();
de = msg.AesDe();

en = msg.AesEn("123123123123");
de = msg.AesDe("123123123123");
// de == msg
```


