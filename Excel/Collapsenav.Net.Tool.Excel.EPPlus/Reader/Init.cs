using System.Runtime.CompilerServices;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

internal class Init
{
#if NETCOREAPP

    [ModuleInitializer]
    public static void InitTypeSelector()
    {
        Selector.AddTypeSelector(ExcelType.EPPlus,
            obj => obj is ExcelWorksheet,
            stream => stream.Length > 5 * 1024 ? 50 : 1
        );
    }
    [ModuleInitializer]
    public static void InitCellReader()
    {
        Selector.AddCellSelector(ExcelType.EPPlus,
            obj => (obj is ExcelWorksheet sheet) ? new EPPlusCellReader(sheet) : null,
            stream => new EPPlusCellReader(stream)
        );
    }
    [ModuleInitializer]
    public static void InitExcelReader()
    {
        Selector.AddExcelSelector(ExcelType.EPPlus,
            obj => (obj is ExcelWorksheet sheet) ? new EPPlusExcelReader(sheet) : null,
            stream => new EPPlusExcelReader(stream)
        );
    }
#endif
}