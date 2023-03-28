namespace Collapsenav.Net.Tool.Excel;

public class EPPlusSheetCellReader : ISheetCellReader
{
    public IExcelCellReader this[int index] => Readers.ElementAt(index);

    public IExcelCellReader this[string sheetName]
    {
        get
        {
            if (Sheets.ContainsKey(sheetName))
                return Sheets[sheetName];
            else
            {
                Sheets.Add(sheetName, new EPPlusCellReader(this));
                Readers = Sheets.Select(item => item.Value).ToList();
            }
            return Sheets[sheetName];
        }
    }

    public Stream SheetStream { get; private set; }

    public IEnumerable<IExcelCellReader> Readers { get; private set; }

    public IDictionary<string, IExcelCellReader> Sheets { get; private set; }

    public EPPlusSheetCellReader(Stream stream)
    {
        Init(stream);
    }
    public EPPlusSheetCellReader(string path)
    {
        var fs = path.OpenCreateReadWriteShareStream();
        Init(fs);
    }

    private void Init(Stream stream)
    {
        SheetStream = stream;
        var workSheets = EPPlusTool.EPPlusSheets(SheetStream);
        var sheetNames = workSheets.Select(item => item.Name).ToList();
        Sheets = new Dictionary<string, IExcelCellReader>();
        sheetNames.ToDictionary(item => item, item => new EPPlusCellReader(workSheets[item])).ForEach(item => Sheets.Add(item.Key, item.Value));
        Readers = Sheets.Select(item => item.Value).ToList();
    }

    public void Save(bool autofit = true)
    {
        SheetStream.Clear();
        SaveTo(SheetStream);
    }

    public void SaveTo(Stream stream, bool autofit = true)
    {
        stream.Clear();
        var pack = EPPlusTool.EPPlusPackage(stream);
        Sheets.Select(item =>
        {
            var reader = item.Value as EPPlusCellReader;
            if (autofit)
                reader.AutoSize();
            return new KeyValuePair<string, EPPlusCellReader>(item.Key, reader);
        }).ToDictionary(item => item.Key, item => item.Value).ForEach(item => pack.Workbook.Worksheets.Add(item.Key, item.Value._sheet));
        pack.SaveAs(stream);
        stream.SeekToOrigin();
    }

    public void SaveTo(string path, bool autofit = true)
    {
        using var fs = path.OpenCreateReadWriteShareStream();
        SaveTo(fs, autofit);
    }
}