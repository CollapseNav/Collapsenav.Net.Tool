namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 尝试使用 IExcelRead 统一 NPOI , EPPlus , MiniExcel 的调用
/// </summary>
public interface IExcelCellRead : IExcelContainer<IReadCell>
{
    void SaveTo(Stream stream);
    void SaveTo(string path);
    Stream GetStream();
}