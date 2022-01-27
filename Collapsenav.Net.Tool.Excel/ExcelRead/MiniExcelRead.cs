using MiniExcelLibs;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public class MiniExcelRead : IExcelRead
{
    protected int headerRowCount = MiniZero;
    protected const int MiniZero = ExcelTool.MiniZero;
    protected Stream SheetStream;
    protected IEnumerable<dynamic> sheet;
    protected IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> HeaderList;
    protected int rowCount;
    public MiniExcelRead(Stream stream, int headerCount = MiniZero)
    {
        SheetStream = new MemoryStream();
        stream.SeekAndCopyTo(SheetStream);
        headerRowCount += headerCount;

        sheet = SheetStream.Query().ToList();

        rowCount = sheet.Count();
        var sheetFirst = sheet.First() as IEnumerable<KeyValuePair<string, object>>;
        HeaderList = MiniExcelReadTool.ExcelHeader(SheetStream);
        HeaderIndex = sheetFirst.Select((item, index) => (item.Value, index)).ToDictionary(item => item.Value.ToString(), item => item.index);
    }



    public long RowCount { get => rowCount; }
    public IEnumerable<string> Headers { get => HeaderList; }
    public IDictionary<string, int> HeadersWithIndex { get => HeaderIndex; }
    public IEnumerable<string> this[string field]
    {
        get
        {
            for (var i = headerRowCount; i < rowCount; i++)
                yield return (sheet.ElementAt(i) as IEnumerable<KeyValuePair<string, object>>).ElementAt(HeaderIndex[field] + MiniZero).Value?.ToString() ?? string.Empty;
        }
    }

    public IEnumerable<string> this[long row] => (sheet.ElementAt((int)row) as IEnumerable<KeyValuePair<string, object>>).Select(item => item.Value?.ToString() ?? string.Empty);
    public string this[long row, long col] => (sheet.ElementAt((int)row) as IEnumerable<KeyValuePair<string, object>>).ElementAt((int)col + MiniZero).Value?.ToString() ?? string.Empty;
    public string this[string field, long row] => (sheet.ElementAt((int)row) as IEnumerable<KeyValuePair<string, object>>).ElementAt(HeaderIndex[field] + MiniZero).Value?.ToString() ?? string.Empty;

    public void Dispose()
    {
        SheetStream.Dispose();
    }
}
