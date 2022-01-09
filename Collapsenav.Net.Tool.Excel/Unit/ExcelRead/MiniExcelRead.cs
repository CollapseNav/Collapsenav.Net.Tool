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



    int IExcelRead<string>.Zero => MiniZero;
    long IExcelRead<string>.RowCount { get => rowCount; }
    IEnumerable<string> IExcelRead<string>.Headers { get => Headers; }
    IDictionary<string, int> IExcelRead<string>.HeadersWithIndex { get => HeaderIndex; }

    public MiniExcelRead(Stream stream, int headerCount = MiniZero)
    {
        SheetStream = stream;
        headerRowCount += headerCount;

        sheet = SheetStream.Query().ToList();

        rowCount = sheet.Count();
        var sheetFirst = DynamicToDict(sheet.First() as object);
        Headers = DynamicToStringList(sheetFirst);
        HeaderIndex = sheetFirst.Select((item, index) => (item.Value, index)).ToDictionary(item => item.Value.ToString(), item => item.index);
    }
    private IDictionary<string, object> DynamicToDict(object dy) => dy.JsonMap<IDictionary<string, object>>();
    private IEnumerable<string> DynamicToStringList(object dy) => DynamicToDict(dy).Select(item => item.Value?.ToString() ?? string.Empty);
    IEnumerable<string> IExcelRead<string>.this[string field]
    {
        get
        {
            for (var i = headerRowCount; i < rowCount; i++)
                yield return DynamicToStringList(sheet.ElementAt(i) as object).ElementAt(HeaderIndex[field] + MiniZero);
        }
    }

    IEnumerable<string> IExcelRead<string>.this[long row]
    {
        get
        {
            return DynamicToStringList(sheet.ElementAt((int)row) as object);
        }
    }

    string IExcelRead<string>.this[long row, long col] => DynamicToStringList(sheet.ElementAt((int)row) as object).ElementAt((int)col + MiniZero);

    string IExcelRead<string>.this[string field, long row] => DynamicToStringList(sheet.ElementAt((int)row) as object).ElementAt(HeaderIndex[field] + MiniZero);

    public void Dispose()
    {
        SheetStream.Dispose();
    }
}
