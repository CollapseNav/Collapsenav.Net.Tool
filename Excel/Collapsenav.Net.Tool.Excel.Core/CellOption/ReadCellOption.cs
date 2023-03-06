using System.Reflection;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// Excel 表格-单元格 读取设置
/// </summary>
public class ReadCellOption<T> : BaseCellOption<T>
{
    public ReadCellOption() { }
    public ReadCellOption(ICellOption cellOption)
    {
        ExcelField = cellOption.ExcelField;
        PropName = cellOption.PropName;
        Prop = cellOption.Prop;
    }

    public ReadCellOption(string excelField, string propName, Func<string, object> action = null) : base(excelField, propName)
    {
        Action = action;
    }

    public ReadCellOption(string excelField, PropertyInfo prop, Func<string, object> action = null) : base(excelField, prop)
    {
        Action = action;
    }

    /// <summary>
    /// 转换 表格 数据的方法
    /// </summary>
    public Func<string, object> Action
    {
        get
        {
            if (action != null)
                return action;
            action = Prop?.PropertyType.Name switch
            {
                nameof(String) => (item) => item,
                nameof(Int16) => (item) => short.Parse(item),
                nameof(Int32) => (item) => int.Parse(item),
                nameof(Int64) => (item) => long.Parse(item),
                nameof(Double) => (item) => double.Parse(item),
                nameof(Single) => (item) => float.Parse(item),
                nameof(Decimal) => (item) => decimal.Parse(item),
                nameof(Boolean) => (item) => bool.Parse(item),
                nameof(DateTime) => (item) => DateTime.Parse(item),
                _ => (item) => Prop.PropertyType.Name.Contains(nameof(Nullable)) ? ParseNullableValueFunc(Prop.PropertyType, item) : item,
            };
            return action;
        }
        set => action = value;
    }
    private Func<string, object> action;

    protected object ParseNullableValueFunc(Type type, string input)
    {
        if (type == typeof(Nullable<Int16>))
            return Int16.TryParse(input, out Int16 value) ? value : default;
        if (type == typeof(Nullable<Int32>))
            return Int32.TryParse(input, out Int32 value) ? value : default;
        if (type == typeof(Nullable<Int64>))
            return Int64.TryParse(input, out Int64 value) ? value : default;
        if (type == typeof(Nullable<Double>))
            return Double.TryParse(input, out Double value) ? value : default;
        if (type == typeof(Nullable<Single>))
            return Single.TryParse(input, out Single value) ? value : default;
        if (type == typeof(Nullable<Decimal>))
            return Decimal.TryParse(input, out Decimal value) ? value : default;
        if (type == typeof(Nullable<Boolean>))
            return Boolean.TryParse(input, out Boolean value) ? value : default;
        if (type == typeof(Nullable<DateTime>))
            return DateTime.TryParse(input, out DateTime value) ? value : default;
        return input;
    }

}
