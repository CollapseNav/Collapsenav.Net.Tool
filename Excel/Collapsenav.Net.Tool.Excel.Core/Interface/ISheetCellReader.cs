namespace Collapsenav.Net.Tool.Excel;

/// <summary>
/// 多sheet读取, 可以进行修改
/// </summary>
public interface ISheetCellReader : ISheetReader<IExcelCellReader>
{
    /// <summary>
    /// 原地保存
    /// </summary>
    void Save(bool autofit = true);
    /// <summary>
    /// 保存到指定流
    /// </summary>
    void SaveTo(Stream stream, bool autofit = true);
    /// <summary>
    /// 保存到指定路径
    /// </summary>
    void SaveTo(string path, bool autofit = true);
}