using MiniExcelLibs;
using MiniExcelLibs.OpenXml;

namespace Collapsenav.Net.Tool.Excel;

public class MiniSheetCellReader : ISheetCellReader
{
    public IEnumerable<IExcelCellReader> Readers { get; protected set; }
    public Stream SheetStream { get; protected set; }
    public IDictionary<string, IExcelCellReader> Sheets { get; protected set; }
    public IExcelCellReader this[string sheetName]
    {
        get
        {
            if (Sheets.ContainsKey(sheetName))
            {
                return Sheets[sheetName];
            }
            else
            {
                Sheets.Add(sheetName, new MiniCellReader(this));
                Readers = Sheets.Select(item => item.Value).ToList();
            }
            return Sheets[sheetName];
        }
    }

    public IExcelCellReader this[int index]
    {
        get
        {
            if (index >= Readers.Count())
                return null;
            return Readers.ElementAt(index);
        }
    }
    public MiniSheetCellReader(Stream stream)
    {
        Init(stream);
    }
    public MiniSheetCellReader(string path)
    {
        var fs = path.OpenReadWriteShareStream();
        Init(fs);
    }
    private void Init(Stream stream)
    {
        SheetStream = stream;
        var sheetNames = MiniExcel.GetSheetNames(SheetStream);
        Sheets = new Dictionary<string, IExcelCellReader>();
        sheetNames.ToDictionary(item => item, item => new MiniCellReader(this, item)).ForEach(item => Sheets.Add(item.Key, item.Value));
        Readers = Sheets.Select(item => item.Value).ToList();
    }

    public void Save(bool autofit = true)
    {
        SaveTo(SheetStream, autofit);
    }

    public void SaveTo(Stream stream, bool autofit = true)
    {
        stream.Clear();
        MiniExcel.SaveAs(stream, Sheets.ToDictionary(item => item.Key, item => (item.Value as MiniCellReader)._sheet as object), printHeader: false, configuration: new OpenXmlConfiguration
        {
            AutoFilter = false,
            TableStyles = TableStyles.None,
        });
        stream.SeekToOrigin();
    }

    public void SaveTo(string path, bool autofit = true)
    {
        using var fs = path.OpenCreateReadWriteShareStream();
        fs.Clear();
        SaveTo(fs, autofit);
    }
}