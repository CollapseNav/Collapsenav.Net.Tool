using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public class MiniCell : IReadCell<KeyValuePair<string, object>>
{
    private KeyValuePair<string, object> cell;
    private readonly int _row;
    private readonly int _col;

    public MiniCell(KeyValuePair<string, object> excelCell, int row, int col)
    {
        cell = excelCell;
        _row = row;
        _col = col;
    }

    public KeyValuePair<string, object> Cell => cell;
    public int Row { get => _row; }
    public int Col { get => _col; }
    public string StringValue => cell.Value.ToString().Trim();
    public Type ValueType => cell.Value?.GetType();
    public object Value { get => cell.Value; set => cell = new(cell.Key, Value); }
}

