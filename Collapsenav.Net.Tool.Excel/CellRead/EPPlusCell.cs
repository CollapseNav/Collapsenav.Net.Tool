using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public class EPPlusCell : IReadCell<ExcelRangeBase>
{
    private ExcelRangeBase cell;

    public EPPlusCell(ExcelRangeBase excelCell)
    {
        cell = excelCell;
    }

    public ExcelRangeBase Cell { get => cell; set => cell = value; }
    public int Row { get => cell.Start.Row - ExcelTool.EPPlusZero; }
    public int Col { get => cell.Start.Column - ExcelTool.EPPlusZero; }
    public string StringValue => cell.Text?.Trim();
    public Type ValueType => cell.Value?.GetType();
    public object Value { get => cell.Value; set => cell.Value = value; }

    public void CopyCellFrom(IReadCell cell)
    {
        if (cell is not IReadCell<ExcelRangeBase> tcell)
            return;
        Value = tcell.Cell.Value;
    }
}

