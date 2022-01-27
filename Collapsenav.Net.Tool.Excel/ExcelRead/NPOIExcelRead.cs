using NPOI.SS.UserModel;
namespace Collapsenav.Net.Tool.Excel;

public class NPOIExcelRead : IExcelRead
{
    protected int headerRowCount = Zero;
    protected const int Zero = ExcelTool.NPOIZero;
    protected ISheet sheet;
    public IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> HeaderList;
    protected int rowCount;
    public long RowCount { get => rowCount; }
    public IEnumerable<string> Headers { get => HeaderList; }
    public IDictionary<string, int> HeadersWithIndex { get => HeaderIndex; }
    public NPOIExcelRead(Stream stream, int headerCount = Zero)
    {
        Init(ExcelTool.NPOISheet(stream), headerCount);
    }
    public NPOIExcelRead(ISheet sheet, int headerCount = Zero)
    {
        Init(sheet, headerCount);
    }
    private void Init(ISheet sheet, int headerCount = Zero)
    {
        this.sheet = sheet;
        headerRowCount += headerCount;

        rowCount = sheet.LastRowNum + 1;
        HeaderIndex = ExcelReadTool.HeadersWithIndex(sheet);
        HeaderList = HeaderIndex.Select(item => item.Key).ToList();
    }


    public IEnumerable<string> this[string field]
    {
        get
        {
            for (var i = headerRowCount; i < rowCount; i++)
                yield return sheet.GetRow(i).GetCell(HeaderIndex[field] + Zero).ToString();
        }
    }
    public IEnumerable<string> this[long row] => sheet.GetRow((int)row + Zero).Select(item => item.ToString());
    public string this[long row, long col] => sheet.GetRow((int)row).GetCell((int)col).ToString();
    public string this[string field, long row] => sheet.GetRow((int)row).GetCell(HeaderIndex[field]).ToString();

    public void Dispose()
    {
        sheet.Workbook.Close();
    }
}