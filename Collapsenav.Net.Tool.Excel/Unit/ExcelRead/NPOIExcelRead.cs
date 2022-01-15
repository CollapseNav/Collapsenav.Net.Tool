using NPOI.SS.UserModel;
namespace Collapsenav.Net.Tool.Excel;

public class NPOIExcelRead : IExcelRead
{
    protected int headerRowCount = ExcelReadTool.NPOIZero;
    protected ISheet sheet;
    public IDictionary<string, int> HeaderIndex;
    protected int rowCount;
    protected IEnumerable<string> Headers;

    long IExcelRead<string>.RowCount { get => rowCount; }
    IEnumerable<string> IExcelRead<string>.Headers { get => Headers; }
    IDictionary<string, int> IExcelRead<string>.HeadersWithIndex { get => HeaderIndex; }

    public NPOIExcelRead(Stream stream, int headerCount = ExcelReadTool.NPOIZero) : this(ExcelReadTool.NPOISheet(stream), headerCount)
    { }
    public NPOIExcelRead(ISheet sheet, int headerCount = ExcelReadTool.NPOIZero)
    {
        this.sheet = sheet;
        headerRowCount += headerCount;

        rowCount = sheet.LastRowNum + 1;
        HeaderIndex = ExcelReadTool.HeadersWithIndex(sheet);
        Headers = HeaderIndex.Select(item => item.Key).ToList();
    }
    IEnumerable<string> IExcelRead<string>.this[string field]
    {
        get
        {
            for (var i = headerRowCount; i < rowCount; i++)
                yield return sheet.GetRow(i).GetCell(HeaderIndex[field] + ExcelReadTool.NPOIZero).ToString();
        }
    }
    IEnumerable<string> IExcelRead<string>.this[long row]
    {
        get
        {
            return sheet.GetRow((int)row + ExcelReadTool.NPOIZero).Select(item => item.ToString());
        }
    }
    string IExcelRead<string>.this[long row, long col] => sheet.GetRow((int)row).GetCell((int)col).ToString();
    string IExcelRead<string>.this[string field, long row] => sheet.GetRow((int)row).GetCell(HeaderIndex[field]).ToString();

    public void Dispose()
    {
        sheet.Workbook.Close();
    }
}