using System.Reflection;

namespace Collapsenav.Net.Tool.Excel;

public interface ICellOption<T>
{
    string ExcelField { get; set; }
    PropertyInfo Prop { get; set; }
    string PropName { get; set; }
}