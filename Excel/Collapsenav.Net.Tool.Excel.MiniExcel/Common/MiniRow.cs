namespace Collapsenav.Net.Tool.Excel;

public class MiniRow
{
    public MiniCell this[int col]
    {
        get
        {
            if (col <= CellCount)
                return Cells[col];
            ExpandRow(col);
            return Cells[col];
        }
    }
    public int RowNum { get; private set; }
    public List<MiniCell> Cells { get; set; }
    public IDictionary<string, object> ExcelRow { get; private set; }
    private int CellCount = -1;
    public int Count { get => CellCount; }
    public MiniRow(IDictionary<string, object> excelRow, int rowNum)
    {
        Cells = new List<MiniCell>();
        ExcelRow = excelRow;
        RowNum = rowNum;
    }

    public void ExpandRow(int len)
    {
        if (len <= CellCount)
            return;
        CellCount = len;
        for (var colNum = ExcelRow.Count; colNum <= CellCount; colNum++)
        {
            var cell = new KeyValuePair<string, object>(MiniCell.GetSCol(colNum), "");
            ExcelRow.Add(cell);
            Cells.Add(new MiniCell(this, cell, RowNum, colNum));
        }
    }
}