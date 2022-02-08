namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 表格读取设置
/// </summary>
public partial class ReadConfig<T> : ExcelConfig<T, ReadCellOption<T>>
{
    private readonly Stream ExcelStream;
    /// <summary>
    /// 读取成功之后调用的针对T的委托
    /// </summary>
    public Func<T, T> Init { get; protected set; }
    public ReadConfig()
    {
        Init = null;
    }
    /// <summary>
    /// 根据文件路径的初始化
    /// </summary>
    /// <param name="filepath"> 文件路径 </param>
    public ReadConfig(string filepath) : this()
    {
        ExcelStream = new MemoryStream();
        filepath.IsXls();
        using var fs = filepath.OpenReadShareStream();
        fs.CopyTo(ExcelStream);
    }
    /// <summary>
    /// 根据文件流的初始化
    /// </summary>
    /// <param name="stream">文件流</param>
    public ReadConfig(Stream stream) : this()
    {
        ExcelStream = new MemoryStream();
        stream.CopyTo(ExcelStream);
    }
}