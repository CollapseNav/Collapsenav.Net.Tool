using NPOI.SS.UserModel;

namespace Collapsenav.Net.Tool.Excel;
public class NPOICell : IReadCell<ICell>
{
    private ICell cell;

    public NPOICell(ICell excelCell)
    {
        cell = excelCell;
    }

    public ICell Cell { get => cell; set => cell = value; }
    public int Row { get => cell.RowIndex - ExcelTool.NPOIZero; }
    public int Col { get => cell.ColumnIndex - ExcelTool.NPOIZero; }
    public string StringValue => cell.ToString();
    public Type ValueType => cell.CellType switch
    {
        CellType.String => typeof(string),
        CellType.Numeric => typeof(double),
        CellType.Blank => typeof(string),
        CellType.Boolean => typeof(bool),
        _ => typeof(object)
    };
    public object Value
    {
        get => cell; set
        {
            var typeName = value.GetType().Name.Split('.')[^1];
            var strValue = value.ToString().Trim();
            if (typeName.HasContain(nameof(Int16), nameof(Int32), nameof(Int64), nameof(UInt16), nameof(UInt32), nameof(UInt64), nameof(Double), nameof(Byte), nameof(Decimal)))
                cell.SetCellValue(double.Parse(strValue));
            else if (DateTime.TryParse(strValue, out DateTime date))
                cell.SetCellValue(date);
            else
                cell.SetCellValue(strValue);
        }
    }
    public void CopyCellFrom(IReadCell cell)
    {
        if (cell is not IReadCell<ICell> tcell)
            return;
        Value = tcell.StringValue;
        var style = Cell.Sheet.Workbook.CreateCellStyle();
        style.CloneStyleFrom(tcell.Cell.CellStyle);
        Cell.CellStyle = style;
    }
}