using System.Runtime.CompilerServices;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

internal class Init
{
#if NETCOREAPP
    [ModuleInitializer]
    public static void InitCellReader()
    {
        CellReaderSelector.Add(ExcelType.EPPlus, (obj) =>
        {
            if (obj is ExcelWorksheet sheet)
                return new EPPlusCellReader(sheet);
            return null;
        });
        CellReaderSelector.Add(ExcelType.EPPlus, (stream) =>
        {
            return new EPPlusCellReader(stream);
        });
    }
    [ModuleInitializer]
    public static void InitExcelReader()
    {
        ExcelReaderSelector.Add(ExcelType.EPPlus, (obj) =>
        {
            if (obj is ExcelWorksheet sheet)
                return new EPPlusExcelReader(sheet);
            return null;
        });
        ExcelReaderSelector.Add(ExcelType.EPPlus, (stream) =>
        {
            return new EPPlusExcelReader(stream);
        });
    }
#endif
}