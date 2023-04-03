namespace Collapsenav.Net.Tool.Excel;

public interface IExcelHeader
{
    /// <summary>
    /// 获取表头数据
    /// </summary>
    IEnumerable<string> Headers { get; }
    /// <summary>
    /// 带index的表头数据
    /// </summary>
    IDictionary<string, int> HeadersWithIndex { get; }
}
