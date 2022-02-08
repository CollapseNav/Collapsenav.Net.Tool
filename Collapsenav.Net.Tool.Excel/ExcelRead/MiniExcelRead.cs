using MiniExcelLibs;

namespace Collapsenav.Net.Tool.Excel;

public class MiniExcelRead : IExcelRead
{
    public int Zero => ExcelTool.MiniZero;
    protected Stream SheetStream;
    protected IEnumerable<dynamic> sheet;
    protected IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> HeaderList;
    protected int rowCount;
    public MiniExcelRead(string path)
    {
        using var fs = path.OpenReadShareStream();
        Init(fs);
    }
    public MiniExcelRead(Stream stream)
    {
        Init(stream);
    }

    private void Init(Stream stream)
    {
        SheetStream = new MemoryStream();
        stream.SeekAndCopyTo(SheetStream);

        sheet = SheetStream.Query().ToList();

        rowCount = sheet.Count();
        var sheetFirst = sheet.First() as IEnumerable<KeyValuePair<string, object>>;
        HeaderList = sheetFirst.Select(item => item.Value?.ToString() ?? string.Empty);
        HeaderIndex = sheetFirst.Select((item, index) => (item.Value, index)).ToDictionary(item => item.Value?.ToString() ?? item.index.ToString(), item => item.index);
    }


    public long RowCount { get => rowCount; }
    public IEnumerable<string> Headers { get => HeaderList; }
    public IDictionary<string, int> HeadersWithIndex { get => HeaderIndex; }
    public IEnumerable<string> this[string field]
    {
        get
        {
            for (var i = Zero; i < rowCount + Zero; i++)
                yield return (sheet.ElementAt(i) as IEnumerable<KeyValuePair<string, object>>).ElementAt(HeaderIndex[field] + Zero).Value?.ToString() ?? string.Empty;
        }
    }

    public IEnumerable<string> this[long row] => (sheet.ElementAt((int)row) as IEnumerable<KeyValuePair<string, object>>).Select(item => item.Value?.ToString() ?? string.Empty);
    public string this[long row, long col] => (sheet.ElementAt((int)row) as IEnumerable<KeyValuePair<string, object>>).ElementAt((int)col + Zero).Value?.ToString() ?? string.Empty;
    public string this[string field, long row] => (sheet.ElementAt((int)row) as IEnumerable<KeyValuePair<string, object>>).ElementAt(HeaderIndex[field] + Zero).Value?.ToString() ?? string.Empty;

    public void Dispose()
    {
        SheetStream.Dispose();
    }

    public IEnumerator<IEnumerable<string>> GetEnumerator()
    {
        for (var row = 0; row < rowCount; row++)
            yield return (sheet.ElementAt(row) as IEnumerable<KeyValuePair<string, object>>).Select(item => item.Value?.ToString() ?? string.Empty);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
