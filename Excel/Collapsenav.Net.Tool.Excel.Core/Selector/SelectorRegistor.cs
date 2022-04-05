namespace Collapsenav.Net.Tool.Excel;

public class Selector
{
    public static void AddTypeSelector(ExcelType excelType, Func<object, bool> objFunc, Func<Stream, int> streamFunc)
    {
        AddTypeSelector(excelType.ToString(), objFunc, streamFunc);
    }
    public static void AddTypeSelector(string excelType, Func<object, bool> objFunc, Func<Stream, int> streamFunc)
    {
        ExcelTypeSelector.Add(excelType, objFunc);
        ExcelTypeSelector.Add(excelType, streamFunc);
    }

    public static void AddExcelSelector(ExcelType excelType, Func<object, IExcelReader> objFunc, Func<Stream, IExcelReader> streamFunc)
    {
        AddExcelSelector(excelType.ToString(), objFunc, streamFunc);
    }
    public static void AddExcelSelector(string excelType, Func<object, IExcelReader> objFunc, Func<Stream, IExcelReader> streamFunc)
    {
        ExcelReaderSelector.Add(excelType, objFunc);
        ExcelReaderSelector.Add(excelType, streamFunc);
    }

    public static void AddCellSelector(ExcelType excelType, Func<object, IExcelCellReader> objFunc, Func<Stream, IExcelCellReader> streamFunc)
    {
        AddCellSelector(excelType.ToString(), objFunc, streamFunc);
    }
    public static void AddCellSelector(string excelType, Func<object, IExcelCellReader> objFunc, Func<Stream, IExcelCellReader> streamFunc)
    {
        CellReaderSelector.Add(excelType, objFunc);
        CellReaderSelector.Add(excelType, streamFunc);
    }
}