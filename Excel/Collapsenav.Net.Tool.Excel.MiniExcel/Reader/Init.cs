using System.Runtime.CompilerServices;

namespace Collapsenav.Net.Tool.Excel;
internal static class Init
{
#if NETCOREAPP
    [ModuleInitializer]
    public static void InitCellReader()
    {
        CellReaderSelector.Add(ExcelType.MiniExcel, obj =>
        {
            if (obj is Stream stream)
                return new MiniCellReader(stream);
            return null;
        });
        CellReaderSelector.Add(ExcelType.MiniExcel, stream =>
        {
            return new MiniCellReader(stream);
        });
    }
    [ModuleInitializer]
    public static void InitExcelReader()
    {
        ExcelReaderSelector.Add(ExcelType.MiniExcel, obj =>
        {
            if (obj is Stream stream)
                return new MiniExcelReader(stream);
            return null;
        });
        ExcelReaderSelector.Add(ExcelType.MiniExcel, stream =>
        {
            return new MiniExcelReader(stream);
        });
    }
#endif
}