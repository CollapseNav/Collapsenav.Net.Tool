using System.Runtime.CompilerServices;
using NPOI.SS.UserModel;

namespace Collapsenav.Net.Tool.Excel;

internal class Init
{
#if NETCOREAPP
    [ModuleInitializer]
    public static void InitCellReader()
    {
        CellReaderSelector.Add(ExcelType.NPOI, (obj) =>
        {
            if (obj is ISheet sheet)
                return new NPOICellReader(sheet);
            return null;
        });
        CellReaderSelector.Add(ExcelType.NPOI, (stream) =>
        {
            return new NPOICellReader(stream);
        });
    }
    [ModuleInitializer]
    public static void InitExcelReader()
    {
        ExcelReaderSelector.Add(ExcelType.NPOI, (obj) =>
        {
            if (obj is ISheet sheet)
                return new NPOIExcelReader(sheet);
            return null;
        });
        ExcelReaderSelector.Add(ExcelType.NPOI, (stream) =>
        {
            return new NPOIExcelReader(stream);
        });
    }
#endif
}