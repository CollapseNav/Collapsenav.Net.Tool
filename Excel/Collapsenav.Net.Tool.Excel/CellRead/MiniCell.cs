namespace Collapsenav.Net.Tool.Excel;

public class MiniCell : IReadCell<KeyValuePair<string, object>>
{
    private KeyValuePair<string, object> cell;
    private IDictionary<string, object> cellRow;
    private readonly int _row;
    private readonly int _col;
    private readonly string _scol;

    public MiniCell(KeyValuePair<string, object> excelCell, IDictionary<string, object> excelRow, int row, int col)
    {
        cell = excelCell;
        cellRow = excelRow;
        _row = row;
        _col = col;
        _scol = GetSCol(col);
    }

    public static string GetSCol(int col)
    {
        string scol = "";
        do
        {
            var temp = col % 26;
            scol += (char)(temp + 'A');
            col -= 26;
        } while (col >= 0);
        return scol;
    }

    public KeyValuePair<string, object> Cell { get => cell; set => cell = value; }
    public int Row { get => _row - ExcelTool.MiniZero; }
    public int Col { get => _col - ExcelTool.MiniZero; }
    public string StringValue => cell.Value.ToString().Trim();
    public Type ValueType => cell.Value?.GetType();
    public object Value
    {
        get => cell.Value; set => cellRow.AddOrUpdate(_scol, value);
    }
    public void CopyCellFrom(IReadCell cell)
    {
        if (cell is not IReadCell<KeyValuePair<string, object>> tcell)
            return;
        Value = cell.Value;
    }
}

