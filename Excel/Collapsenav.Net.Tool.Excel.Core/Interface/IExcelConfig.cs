using System.Reflection;

namespace Collapsenav.Net.Tool.Excel;

public interface IExcelConfig : IExcelHeader
{
    /// <summary>
    /// 对应的转化数据类型
    /// </summary>
    Type DtoType { get; }
}

public interface IExcelConfig<out T, CellOption> : IExcelConfig where CellOption : ICellOption, new()
{
    /// <summary>
    /// 依据表头的设置
    /// </summary>
    IEnumerable<CellOption> FieldOption { get; }
    /// <summary>
    /// 表格数据
    /// </summary>
    IEnumerable<T> Data { get; }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    IExcelConfig<T, CellOption> Add(string field, PropertyInfo prop);
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    IExcelConfig<T, CellOption> AddIf(bool check, string field, PropertyInfo prop);
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    IExcelConfig<T, CellOption> Add(string field, string propName);
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="check">判断结果</param>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    IExcelConfig<T, CellOption> AddIf(bool check, string field, string propName);
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    IExcelConfig<T, CellOption> Add(CellOption option);
    /// <summary>
    /// check条件为True时添加普通单元格设置
    /// </summary>
    IExcelConfig<T, CellOption> AddIf(bool check, CellOption option);
    /// <summary>
    /// 移除指定单元格配置
    /// </summary>
    IExcelConfig<T, CellOption> Remove(string field);
    /// <summary>
    /// 清除数据
    /// </summary>
    void ClearData();
    /// <summary>
    /// 清除字段配置
    /// </summary>
    void ClearFieldOption();
}