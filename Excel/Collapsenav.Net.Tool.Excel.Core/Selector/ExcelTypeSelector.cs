namespace Collapsenav.Net.Tool.Excel;

public class ExcelTypeSelector
{
    public static Dictionary<string, Func<Stream, int>> StreamSelector = null;
    public static Dictionary<string, Func<object, bool>> ObjSelector = null;
    public static void Add(ExcelType excelType, Func<Stream, int> func)
    {
        StreamSelector ??= new();
        StreamSelector.AddOrUpdate(excelType.ToString(), func);
    }

    public static void Add(ExcelType excelType, Func<object, bool> func)
    {
        ObjSelector ??= new();
        ObjSelector.AddOrUpdate(excelType.ToString(), func);
    }
    public static void Add(string excelType, Func<Stream, int> func)
    {
        StreamSelector ??= new();
        StreamSelector.AddOrUpdate(excelType, func);
    }

    public static void Add(string excelType, Func<object, bool> func)
    {
        ObjSelector ??= new();
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