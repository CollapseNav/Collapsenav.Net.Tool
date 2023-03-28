namespace Collapsenav.Net.Tool.Excel;

public class EPPlusSheetReader : ISheetReader<IExcelReader>
{
    public IExcelReader this[int index] => Readers.ElementAt(index);

    public IExcelReader this[string sheetName] => Sheets.ContainsKey(sheetName) ? Sheets[sheetName] : null;

    public Stream SheetStream { get; private set; }

    public IEnumerable<IExcelReader> Readers { get; private set; }

    public IDictionary<string, IExcelReader> Sheets { get; private set; }

    public EPPlusSheetReader(string path)
    {
        using var fs = path.OpenReadShareStream();
        Init(fs);
    }

    private void Init(Stream stream)
    {
        using var mem = new MemoryStream();
        stream.SeekAndCopyTo(mem);
        var workSheets = EPPlusTool.EPPlusSheets(mem);
        var sheetNames = workSheets.Select(item => item.Name).ToList();
        Sheets = new Dictionary<string, IExcelReader>();
        sheetNames.ToDictionary(item => item, item => new EPPlusExcelReader(workSheets[item])).ForEach(item => Sheets.Add(item.Key, item.Value));
        Readers = Sheets.Select(item => item.Value).ToList();
    }
}