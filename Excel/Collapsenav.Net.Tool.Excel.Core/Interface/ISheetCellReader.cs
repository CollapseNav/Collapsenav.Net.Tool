namespace Collapsenav.Net.Tool.Excel;

public interface ISheetCellReader : ISheetReader<IExcelCellReader>
{
    void Save(bool autofit = true);
    void SaveTo(Stream stream, bool autofit = true);
    void SaveTo(string path, bool autofit = true);
}