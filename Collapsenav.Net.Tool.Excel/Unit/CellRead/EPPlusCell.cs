using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public class EPPlusCell : IReadCell<ExcelRangeBase>
{
    private readonly ExcelRangeBase cell;

    public EPPlusCell(ExcelRangeBase excelCell)
    {
        cell = excelCell;
    }

    public ExcelRangeBase Cell => cell;
    public int Row { get => cell.Start.Row; }
    public int Col { get => cell.Start.Column; }
    public string StringValue => cell.Text?.Trim();
    public Type ValueType => cell.Value?.GetType();
    public object Value { get => cell.Value; set => cell.Value = value; }
}

