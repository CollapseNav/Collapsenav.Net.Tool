using MiniExcelLibs;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public class MiniExcelRead : IExcelRead
{
    protected int headerRowCount = MiniZero;
    protected const int MiniZero = 0;
    protected Stream SheetStream;
    protected IEnumerable<dynamic> sheet;
    protected IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> Headers;
    protected int rowCount;



    long IExcelRead<string>.RowCount { get => rowCount; }
    IEnumerable<string> IExcelRead<string>.Headers { get => Headers; }
    IDictionary<string, int> IExcelRead<string>.HeadersWithIndex { get => HeaderIndex; }

    public MiniExcelRead(Stream stream, int headerCount = MiniZero)
    {
        SheetStream = new MemoryStream();
        stream.SeekAndCopyTo(SheetStream);
        headerRowCount += headerCount;

        sheet = SheetStream.Query().ToList();

        rowCount = sheet.Count();
        var sheetFirst = sheet.First() as IEnumerable<KeyValuePair<string, object>>;
        Headers = MiniExcelReadTool.ExcelHeader(SheetStream);
        HeaderIndex = sheetFirst.Select((item, index) => (item.Value, index)).ToDictionary(item => item.Value.ToString(), item => item.index);
    }
    IEnumerable<string> IExcelRead<string>.this[string field]
    {
        get
        {
            for (var i = headerRowCount; i < rowCount; i++)
                yield return (sheet.ElementAt(i) as IEnumerable<KeyValuePair<string, object>>).ElementAt(HeaderIndex[field] + MiniZero).Value?.ToString() ?? string.Empty;
        }
    }

    IEnumerable<string> IExcelRead<string>.this[long row]
    {
        get
        {
            return (sheet.ElementAt((int)row) as IEnumerable<KeyValuePair<string, object>>).Select(item => item.Value?.ToString() ?? string.Empty);
        }
    }

    string IExcelRead<string>.this[long row, long col] => (sheet.ElementAt((int)row) as IEnumerable<KeyValuePair<string, object>>).ElementAt((int)col + MiniZero).Value?.ToString() ?? string.Empty;

    string IExcelRead<string>.this[string field, long row] => (sheet.ElementAt((int)row) as IEnumerable<KeyValuePair<string, object>>).ElementAt(HeaderIndex[field] + MiniZero).Value?.ToString() ?? string.Empty;

    public void Dispose()
    {
        SheetStream.Dispose();
    }
}
