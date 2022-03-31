namespace Collapsenav.Net.Tool.Excel;
public interface IExcelCellReader : IExcelContainer<IReadCell>
{
    void Save();
    void SaveTo(Stream stream);
    void SaveTo(string path);
    Stream GetStream();
#if NETSTANDARD2_0
#else
    public static IExcelCellReader GetCellRead(object sheet)
    {
        return CellReaderSelector.GetCellReader(sheet);
    }
    public static IExcelCellReader GetCellRead(Stream stream, ExcelType? excelType = null)
    {
        return CellReaderSelector.GetCellReader(stream, excelType);
    }
    public static IExcelCellReader GetCellRead(string path, ExcelType? excelType = null)
    {
        using var fs = path.OpenCreateReadWriteShareStream();
        return GetCellRead(fs, excelType);
    }
#endif
}
