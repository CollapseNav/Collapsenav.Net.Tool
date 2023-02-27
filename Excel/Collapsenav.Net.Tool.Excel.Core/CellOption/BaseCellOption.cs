using System.Reflection;

namespace Collapsenav.Net.Tool.Excel;
public class BaseCellOption<T> : ICellOption<T>
{
    /// <summary>
    /// 对应excel中的表头字段
    /// </summary>
    public virtual string ExcelField { get; set; }
    /// <summary>
    /// 对应字段的属性(实际上包含PropName)
    /// </summary>
    public virtual PropertyInfo Prop
    {
        get
        {
            return prop;
        }
        set
        {
            prop = value;
            propName = prop.Name;
        }
    }
    private PropertyInfo prop;
    /// <summary>
    /// 就是一个看起来比较方便的标识
    /// </summary>
    public virtual string PropName
    {
        get
        {
            return propName;
        }
        set
        {
            propName = value;
            prop = typeof(T).GetProperty(propName);
        }
    }
    private string propName;
}