using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Collapsenav.Net.Tool.Excel;

public class NPOISheetCellReader : ISheetCellReader
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
                var sheet = workbook.CreateSheet(sheetName);
                Sheets.Add(sheetName, new NPOICellReader(sheet));
                Readers = Sheets.Select(item => item.Value).ToList();
            }
            return Sheets[sheetName];
        }
    }

    public Stream SheetStream { get; private set; }
    public IEnumerable<IExcelCellReader> Readers { get; private set; }
    public IDictionary<string, IExcelCellReader> Sheets { get; private set; }

    protected IWorkbook workbook;
    public NPOISheetCellReader(Stream stream)
    {
        Init(stream);
    }
    public NPOISheetCellReader(string path)
    {
        var fs = path.OpenCreateReadWriteShareStream();
        Init(fs);
    }

    private void Init(Stream stream)
    {
        SheetStream = stream;
        workbook = NPOITool.NPOIWorkbook(SheetStream);
        List<string> sheetNames = new();

        Sheets = new Dictionary<string, IExcelCellReader>();
        foreach (var sheet in workbook)
            Sheets.Add(sheet.SheetName, new NPOICellReader(sheet));
        Readers = Sheets.Select(item => item.Value).ToList();
    }

    public void Save(bool autofit = false)
    {
        SaveTo(SheetStream, autofit);
    }

    public void SaveTo(Stream stream, bool autofit = false)
    {
        stream.Clear();
        workbook.Write(stream, true);
        stream.SeekToOrigin();
    }

    public void SaveTo(string path, bool autofit = false)
    {
        using var fs = path.OpenCreateReadWriteShareStream();
        SaveTo(fs, autofit);
    }
}