namespace Collapsenav.Net.Tool.Excel;

public interface IExcelContainer<out T> : IDisposable, IEnumerable<IEnumerable<T>>
{
    /// <summary>
    /// 获取列数据
    /// </summary>
    IEnumerable<T> this[string field] { get; }
    /// <summary>
    /// 获取行数据
    /// </summary>
    IEnumerable<T> this[int row] { get; }
    /// <summary>
    /// 通过row col获取单元格
    /// </summary>
    T this[int row, int col] { get; }
    /// <summary>
    /// 通过表头和row获取单元格
    /// </summary>
    T this[string field, int row] { get; }
    /// <summary>
    /// 行统计
    /// </summary>
    int RowCount { get; }
    /// <summary>
    /// 适配不同实现的0值
    /// </summary>
    int Zero { get; }
}