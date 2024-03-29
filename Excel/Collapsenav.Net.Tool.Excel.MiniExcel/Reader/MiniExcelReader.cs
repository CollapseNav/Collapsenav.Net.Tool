using MiniExcelLibs;

namespace Collapsenav.Net.Tool.Excel;

public class MiniExcelReader : IExcelReader
{
    public int Zero => ExcelTool.MiniZero;
    protected Stream SheetStream;
    protected IEnumerable<dynamic> sheet;
    protected IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> HeaderList;
    protected int rowCount;
    protected ISheetReader<IExcelReader> SheetReader;
    public MiniExcelReader(ISheetReader<IExcelReader> sheetReader, string sheetName)
    {
        SheetReader = sheetReader;
        SheetStream = SheetReader.SheetStream;
        Init(sheetName);
    }
    public MiniExcelReader(string path)
    {
        using var fs = path.OpenReadShareStream();
        Init(fs);
    }
    public MiniExcelReader(Stream stream)
    {
        Init(stream);
    }

    private void Init(string sheetName)
    {
        Init(SheetStream.Query(sheetName: sheetName).ToList());
    }
    private void Init(Stream stream)
    {
        SheetStream = new MemoryStream();
        stream.SeekAndCopyTo(SheetStream);
        Init(SheetStream.Query().ToList());
    }

    private void Init(List<dynamic> sheetList)
    {
        sheet = sheetList;

        rowCount = sheet.Count();
        var sheetFirst = sheet.First() as IEnumerable<KeyValuePair<string, object>>;
        HeaderList = sheetFirst.Select(item => item.Value?.ToString() ?? string.Empty);
        HeaderIndex = sheetFirst.Select((item, index) => (item.Value, index)).ToDictionary(item => item.Value?.ToString() ?? item.index.ToString(), item => item.index);
    }


    public int RowCount { get => rowCount; }
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

    public IEnumerable<string> this[int row] => (sheet.ElementAt(row) as IEnumerable<KeyValuePair<string, object>>).Select(item => item.Value?.ToString() ?? string.Empty);
    public string this[int row, int col] => (sheet.ElementAt(row) as IEnumerable<KeyValuePair<string, object>>).ElementAt(col + Zero).Value?.ToString() ?? string.Empty;
    public string this[string field, int row] => (sheet.ElementAt(row) as IEnumerable<KeyValuePair<string, object>>).ElementAt(HeaderIndex[field] + Zero).Value?.ToString() ?? string.Empty;

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
