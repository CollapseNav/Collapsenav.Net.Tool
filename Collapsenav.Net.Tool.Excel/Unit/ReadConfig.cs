namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 表格读取设置
/// </summary>
public partial class ReadConfig<T>
{
    private readonly Stream ExcelStream;
    /// <summary>
    /// 一行数据的读取设置
    /// </summary>
    public virtual IEnumerable<ReadCellOption<T>> FieldOption { get; protected set; }
    /// <summary>
    /// 读取成功之后调用的针对T的委托
    /// </summary>
    public Func<T, T> Init { get; protected set; }
    public ReadConfig()
    {
        FieldOption = new List<ReadCellOption<T>>();
        Init = null;
        ExcelStream = new MemoryStream();
    }
    /// <summary>
    /// 根据文件路径的初始化
    /// </summary>
    /// <param name="filepath"> 文件路径 </param>
    /// <returns></returns>
    public ReadConfig(string filepath) : this()
    {
        filepath.IsXls();
        using var fs = new FileStream(filepath, FileMode.Open);
        fs.CopyTo(ExcelStream);
    }
    /// <summary>
    /// 根据文件流的初始化
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <returns></returns>
    public ReadConfig(Stream stream) : this()
    {
        stream.CopyTo(ExcelStream);
    }
}
