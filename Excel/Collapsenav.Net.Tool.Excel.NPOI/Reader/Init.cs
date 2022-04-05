using System.Runtime.CompilerServices;
using NPOI.SS.UserModel;

namespace Collapsenav.Net.Tool.Excel;

internal class Init
{
#if NETCOREAPP
    [ModuleInitializer]
    public static void InitTypeSelector()
    {
        Selector.AddTypeSelector(ExcelType.NPOI,
            obj => obj is ISheet,
            stream => stream.Length > 5 * 1024 ? 50 : 1
        );
    }
    [ModuleInitializer]
    public static void InitCellReader()
    {
        Selector.AddCellSelector(ExcelType.NPOI,
            obj => (obj is ISheet sheet) ? new NPOICellReader(sheet) : null,
            stream => new NPOICellReader(stream)
        );
    }
    [ModuleInitializer]
    public static void InitExcelReader()
    {
        Selector.AddExcelSelector(ExcelType.NPOI,
            obj => (obj is ISheet sheet) ? new NPOIExcelReader(sheet) : null,
            stream => new NPOIExcelReader(stream)
        );
    }
#endif
}