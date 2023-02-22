namespace Collapsenav.Net.Tool.Excel;
public interface IExcelCellReader : IExcelContainer<IReadCell>
{
    void Save(bool autofit = true);
    void SaveTo(Stream stream, bool autofit = true);
    void SaveTo(string path, bool autofit = true);
    Stream GetStream();
#if NET6_0_OR_GREATER && NETCOREAPP
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
