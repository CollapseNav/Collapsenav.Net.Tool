using System.Collections;
using NPOI.SS.UserModel;
namespace Collapsenav.Net.Tool.Excel;

public class NPOIExcelReader : IExcelReader
{
    public int Zero => ExcelTool.NPOIZero;
    protected ISheet sheet;
    public IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> HeaderList;
    protected int rowCount;
    public int RowCount { get => rowCount; }
    public IEnumerable<string> Headers { get => HeaderList; }
    public IDictionary<string, int> HeadersWithIndex { get => HeaderIndex; }
    public NPOIExcelReader(string path)
    {
        using var fs = path.OpenReadShareStream();
        Init(fs); ;
    }
    public NPOIExcelReader(Stream stream)
    {
        Init(stream);
    }
    public NPOIExcelReader(ISheet sheet)
    {
        Init(sheet);
    }
    private void Init(Stream stream)
    {
        stream.SeekToOrigin();
        Init(NPOITool.NPOISheet(stream));
    }
    private void Init(ISheet sheet)
    {
        this.sheet = sheet;

        rowCount = sheet.LastRowNum + 1;
        HeaderIndex = NPOITool.HeadersWithIndex(sheet);
        HeaderList = HeaderIndex.Select(item => item.Key).ToList();
    }


    public IEnumerable<string> this[string field]
    {
        get
        {
            for (var i = Zero; i < rowCount + Zero; i++)
                yield return sheet.GetRow(i).GetCell(HeaderIndex[field] + Zero).ToString();
        }
    }
    public IEnumerable<string> this[int row] => sheet.GetRow((int)row + Zero).Select(item => item.ToString());
    public string this[int row, int col] => sheet.GetRow((int)row).GetCell((int)col).ToString();
    public string this[string field, int row] => sheet.GetRow((int)row).GetCell(HeaderIndex[field]).ToString();

    public void Dispose()
    {
        sheet.Workbook.Close();
    }

    public IEnumerator<IEnumerable<string>> GetEnumerator()
    {
        for (var row = 0; row < rowCount; row++)
            yield return this[row];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}