namespace Collapsenav.Net.Tool.Excel;

public class MiniCell : IReadCell<KeyValuePair<string, object>>
{
    private static string[] ScolArray;
    static MiniCell()
    {
        ScolArray = Enumerable.Range(0, 702).ToList().Select(item =>
        {
            string scol = string.Empty;
            var temp = item % 26;
            item /= 26;
            if (item > 0)
                return $"{(char)(item - 1 + 'A')}{(char)(temp + 'A')}";
            return ((char)(temp + 'A')).ToString();
        }).ToArray();
    }
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
        if (col > ScolArray.Length)
            return string.Empty;
        return ScolArray[col];
    }
    public KeyValuePair<string, object> Cell { get => cell; set => cell = value; }
    public int Row { get => _row - ExcelTool.MiniZero; }
    public int Col { get => _col - ExcelTool.MiniZero; }
    public string StringValue => cell.Value.ToString().Trim();
    public Type ValueType => cell.Value?.GetType();
    public object Value
    {
        get => cell.Value; set => cellRow[_scol] = value;
    }
    public void CopyCellFrom(IReadCell cell)
    {
        if (cell is not IReadCell<KeyValuePair<string, object>> tcell)
            return;
        Value = cell.Value;
    }
}

