using System.Collections.Concurrent;

namespace Collapsenav.Net.Tool.Excel;

public class ExcelTypeSelector
{
    private static ConcurrentDictionary<string, Func<Stream, int>> StreamSelector = new();
    private static ConcurrentDictionary<string, Func<object, bool>> ObjSelector = new();
    public static void Add(ExcelType excelType, Func<Stream, int> func)
    {
        StreamSelector.AddOrUpdate(excelType.ToString(), func);
    }

    public static void Add(ExcelType excelType, Func<object, bool> func)
    {
        ObjSelector.AddOrUpdate(excelType.ToString(), func);
    }
    public static void Add(string excelType, Func<Stream, int> func)
    {
        StreamSelector.AddOrUpdate(excelType, func);
    }

    public static void Add(string excelType, Func<object, bool> func)
    {
        ObjSelector.AddOrUpdate(excelType, func);
    }

    public static string GetExcelType(object obj, string excelType = null)
    {
        if (excelType.NotEmpty() && (ObjSelector?.ContainsKey(excelType) ?? false))
            return ObjSelector[excelType](obj) ? excelType : string.Empty;
        return ObjSelector?.Where(item => item.Value(obj)).Select(item => item.Key).FirstOrDefault();
    }

    public static string GetExcelType(Stream stream, string excelType = null)
    {
        if (excelType.NotEmpty() && (StreamSelector?.ContainsKey(excelType) ?? false))
            return excelType;
        return StreamSelector?.Select(item => new { weight = item.Value(stream), value = item.Key }).OrderByDescending(item => item.weight).FirstOrDefault()?.value;
    }

}