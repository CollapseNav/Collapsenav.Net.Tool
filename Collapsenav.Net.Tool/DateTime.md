# DateTime

## TODO

- [x] `ToTimestamp` 时间转为时间戳
- [x] `ToDateTime` 时间戳转为时间
- [x] `ToYear` 等只保留时间到指定的单位
- [ ] `DefaultString` 返回默认格式字符串
- [ ] 计算 几月几日 是哪一周的星期几/星期几/一年的第几天......
- [ ] 计算 两个日期之间的时间差(各种单位)
- [ ] 获取 时间段内的所有日期(返回有序的时间集合)
- [ ] 判断是否为闰年
- [ ] ...

## How To Use

### ToTimestamp

```csharp
DateTime.Now.ToUniversalTime();
```

### ToDateTime

~~会碰到精度问题,不知道现在的处理方式是否合理~~

```csharp
now.ToTimestamp().ToDateTime();
```

### ToYear,ToMonthXXX

有时候会有 只保留到年/月/日 之类的需求

```csharp
DateTime now = new DateTime(2010, 10, 10, 10, 10, 10, 10);
now.ToMillisecond();
now.ToSecond();
now.ToMinute();
now.ToHour();
now.ToDay();
now.ToMonth();
now.To(DateLevel.Year);
```

### DefaultString

返回常用的时间格式字符串

后面可能会尝试添加更多的格式

```csharp
DateTime date = new(2021, 11, 11, 11, 11, 11);
date.ToDefaultDateString(); // 2021-11-11
date.ToDefaultTimeString(); // 2021-11-11 11:11:11
date.ToDefaultDateString("WTF"); // 2021WTF11WTF11
date.ToDefaultTimeString("WTF"); // 2021WTF11WTF11 11:11:11
```






