using System.Collections.Concurrent;

namespace Collapsenav.Net.Tool.Excel;

public class CellReaderSelector
{
    private static ConcurrentDictionary<string, Func<Stream, IExcelCellReader>> StreamSelectorDict = new();
    private static ConcurrentDictionary<string, Func<object, IExcelCellReader>> ObjSelectorDict = new();
    public static void Add(string excelType, Func<object, IExcelCellReader> func)
    {
        ObjSelectorDict.AddOrUpdate(excelType, func);
    }
    public static void Add(string excelType, Func<Stream, IExcelCellReader> func)
    {
        StreamSelectorDict.AddOrUpdate(excelType, func);
    }
    public static void Add(ExcelType excelType, Func<object, IExcelCellReader> func)
    {
        ObjSelectorDict.AddOrUpdate(excelType.ToString(), func);
    }
    public static void Add(ExcelType excelType, Func<Stream, IExcelCellReader> func)
    {
        StreamSelectorDict.AddOrUpdate(excelType.ToString(), func);
    }
    public static IExcelCellReader GetCellReader(object obj)
    {
        return GetCellReader(obj, null);
    }
    public static IExcelCellReader GetCellReader(object obj, string excelType)
    {
        if (ObjSelectorDict.IsEmpty())
            throw new NoRegisterExcelCellReaderException();
        excelType = ExcelTypeSelector.GetExcelType(obj, excelType);
        if (excelType.NotWhite() && !ObjSelectorDict.ContainsKey(excelType))
            throw new Exception($"未注册 {excelType} 的 IExcelReader 实现");
        else if (excelType.IsWhite())
            throw new NoRegisterExcelCellReaderException();
        return ObjSelectorDict[excelType](obj);
    }
    public static IExcelCellReader GetCellReader(Stream stream)
    {
        return GetCellReader(stream, null);
    }
    public static IExcelCellReader GetCellReader(Stream stream, string excelType)
    {
        if (StreamSelectorDict.IsEmpty())
            throw new NoRegisterExcelCellReaderException();
        excelType = ExcelTypeSelector.GetExcelType(stream, excelType);
        if (excelType.NotWhite() && !StreamSelectorDict.ContainsKey(excelType))
            throw new NoRegisterExcelCellReaderException();
        else if (excelType.IsWhite())
            throw new NoRegisterExcelCellReaderException();
        return StreamSelectorDict[excelType](stream);
    }
}