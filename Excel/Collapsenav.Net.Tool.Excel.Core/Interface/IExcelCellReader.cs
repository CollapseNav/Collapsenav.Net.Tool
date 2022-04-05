namespace Collapsenav.Net.Tool.Excel;
public interface IExcelCellReader : IExcelContainer<IReadCell>
{
    void Save();
    void SaveTo(Stream stream);
    void SaveTo(string path);
    Stream GetStream();
#if NETSTANDARD2_0
#else
    public static IExcelCellReader GetCellReader(object sheet)
    {
        return CellReaderSelector.GetCellReader(sheet);
    }
    public static IExcelCellReader GetCellReader(Stream stream, ExcelType? excelType = null)
    {
        return CellReaderSelector.GetCellReader(stream, excelType.ToString());
    }
    public static IExcelCellReader GetCellReader(string path, ExcelType? excelType = null)
    {
        using var fs = path.OpenReadWriteShareStream();
        return GetCellReader(fs, excelType);
    }
#endif
}
