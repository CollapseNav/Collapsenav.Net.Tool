using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public class EPPlusExcelRead : IExcelRead
{
    protected const int Zero = ExcelReadTool.EPPlusZero;
    protected int headerRowCount = ExcelReadTool.EPPlusZero;
    protected ExcelWorksheet sheet;
    protected IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> HeaderList;
    protected int rowCount;
    public EPPlusExcelRead(Stream stream, int headerCount = Zero)
    {
        Init(ExcelReadTool.EPPlusSheet(stream), headerCount);
    }
    public EPPlusExcelRead(ExcelWorksheet sheet, int headerCount = Zero)
    {
        Init(sheet, headerCount);
    }
    private void Init(ExcelWorksheet sheet, int headerCount = Zero)
    {
        this.sheet = sheet;
        headerRowCount += headerCount;

        rowCount = sheet.Dimension.Rows;
        HeaderIndex = ExcelReadTool.HeadersWithIndex(sheet);
        HeaderList = HeaderIndex.Select(item => item.Key).ToList();
    }



    public IEnumerable<string> this[string field]
    {
        get
        {
            for (var i = headerRowCount; i < rowCount; i++)
                yield return sheet.Cells[i, HeaderIndex[field] + Zero].Value.ToString();
        }
    }
    public IEnumerable<string> this[long row] => sheet.Cells[(int)row + Zero, Zero, (int)row + Zero, Zero + Headers.Count()]
    .Select(item => item.Value.ToString());
    public string this[long row, long col] => sheet.Cells[(int)row + Zero, (int)col + Zero].Value.ToString();
    public string this[string field, long row] => sheet.Cells[(int)row + Zero, HeaderIndex[field] + Zero].Value.ToString();
    public IEnumerable<string> Headers => HeaderList;
    public IDictionary<string, int> HeadersWithIndex => HeaderIndex;
    public long RowCount => rowCount;

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}