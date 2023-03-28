using MiniExcelLibs;

namespace Collapsenav.Net.Tool.Excel;

public class MiniSheetReader : ISheetReader<IExcelReader>
{
    public IEnumerable<IExcelReader> Readers { get; protected set; }
    public Stream SheetStream { get; protected set; }
    public IDictionary<string, IExcelReader> Sheets { get; protected set; }
    public IExcelReader this[string sheetName] => Sheets.ContainsKey(sheetName) ? Sheets[sheetName] : null;

    public IExcelReader this[int index] => Readers.ElementAt(index);
    public MiniSheetReader(string path)
    {
        using var fs = path.OpenReadShareStream();
        Init(fs);
    }
    private void Init(Stream stream)
    {
        SheetStream = new MemoryStream();
        stream.SeekAndCopyTo(SheetStream);
        var sheetNames = MiniExcel.GetSheetNames(stream);
        Sheets = new Dictionary<string, IExcelReader>();
        sheetNames.ToDictionary(item => item, item => new MiniExcelReader(this, item)).ForEach(item => Sheets.Add(item.Key, item.Value));
        Readers = Sheets.Select(item => item.Value).ToList();
    }
}