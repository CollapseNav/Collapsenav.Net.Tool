using System.Runtime.CompilerServices;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public class Init
{
#if NETCOREAPP
    [ModuleInitializer]
    public static void InitTypeSelector()
    {
        Selector.AddTypeSelector(ExcelType.EPPlus,
            obj => obj is ExcelWorksheet,
            stream => stream.Length > 5 * 1024 ? 50 : 1
        );
        Selector.AddCellSelector(ExcelType.EPPlus,
            obj => (obj is ExcelWorksheet sheet) ? new EPPlusCellReader(sheet) : null,
            stream => new EPPlusCellReader(stream)
        );
        Selector.AddExcelSelector(ExcelType.EPPlus,
            obj => (obj is ExcelWorksheet sheet) ? new EPPlusExcelReader(sheet) : null,
            stream => new EPPlusExcelReader(stream)
        );
    }
#endif
}