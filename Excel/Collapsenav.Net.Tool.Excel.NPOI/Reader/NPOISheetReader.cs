namespace Collapsenav.Net.Tool.Excel;

public class NPOISheetReader : ISheetReader<IExcelReader>
{
    public IExcelReader this[int index] => Readers.ElementAt(index);

    public IExcelReader this[string sheetName] => Sheets.ContainsKey(sheetName) ? Sheets[sheetName] : null;

    public Stream SheetStream { get; private set; }
    public IEnumerable<IExcelReader> Readers { get; private set; }
    public IDictionary<string, IExcelReader> Sheets { get; private set; }

    public NPOISheetReader(string path)
    {
        using var fs = path.OpenReadShareStream();
        Init(fs);
    }

    private void Init(Stream stream)
    {
        using var mem = new MemoryStream();
        stream.SeekAndCopyTo(mem);
        var workBook = NPOITool.NPOIWorkbook(mem);
        List<string> sheetNames = new();

        Sheets = new Dictionary<string, IExcelReader>();
        foreach (var sheet in workBook)
            Sheets.Add(sheet.SheetName, new NPOIExcelReader(sheet));
        Readers = Sheets.Select(item => item.Value).ToList();
    }
}