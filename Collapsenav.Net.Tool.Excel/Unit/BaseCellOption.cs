using System.Reflection;

namespace Collapsenav.Net.Tool.Excel;
public class BaseCellOption<T>
{
    /// <summary>
    /// 对应excel中的表头字段
    /// </summary>
    public string ExcelField { get; set; }
    /// <summary>
    /// 对应字段的属性(实际上包含PropName)
    /// </summary>
    public PropertyInfo Prop
    {
        get
        {
            if (prop == null && PropName.NotEmpty())
                prop = typeof(T).GetProperty(PropName);
            return prop;
        }
        set => prop = value;
    }
    protected PropertyInfo prop;
    /// <summary>
    /// 就是一个看起来比较方便的标识
    /// </summary>
    public string PropName
    {
        get
        {
            if (propName.IsEmpty() && Prop != null)
                propName = Prop.Name;
            return propName;
        }
        set => propName = value;
    }
    protected string propName { get; set; }
}