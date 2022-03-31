using System.Collections;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public class EPPlusExcelReader : IExcelReader
{
    // static EPPlusExcelReader()
    // {
    //     ExcelReaderSelector.Add(ExcelType.EPPlus, (obj) =>
    //     {
    //         if (obj is ExcelWorksheet sheet)
    //             return new EPPlusExcelReader(sheet);
    //         return null;
    //     });
    //     ExcelReaderSelector.Add(ExcelType.EPPlus, (stream) =>
    //     {
    //         return new EPPlusExcelReader(stream);
    //     });
    // }
    public int Zero => ExcelTool.EPPlusZero;
    protected ExcelWorksheet sheet;
    protected IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> HeaderList;
    protected int rowCount;
    public EPPlusExcelReader(string path)
    {
        using var fs = path.OpenReadShareStream();
        Init(fs);
    }
    public EPPlusExcelReader(Stream stream)
    {
        Init(EPPlusTool.EPPlusSheet(stream));
    }
    public EPPlusExcelReader(ExcelWorksheet sheet)
    {
        Init(sheet);
    }
    private void Init(Stream stream)
    {
        Init(EPPlusTool.EPPlusSheet(stream));
    }
    private void Init(ExcelWorksheet sheet)
    {
        this.sheet = sheet;

        rowCount = sheet.Dimension.Rows;
        HeaderIndex = EPPlusTool.HeadersWithIndex(sheet);
        HeaderList = HeaderIndex.Select(item => item.Key).ToList();
    }
    public IEnumerable<string> this[string field]
    {
        get
        {
            for (var i = Zero; i < rowCount + Zero; i++)
                yield return sheet.Cells[i, HeaderIndex[field] + Zero].Value.ToString();
        }
    }
    public IEnumerable<string> this[long row] => sheet.Cells[(int)row + Zero, Zero, (int)row + Zero, Zero + Headers.Count()]
    .Select(item => item.Value.ToString());
    public string this[long row, long col] => sheet.Cells[(int)row + Zero, (int)col + Zero].Value.ToString();
    public string this[string field, long row] => sheet.Cells[(int)row + Zero, HeaderIndex[field] + Zero].Value.ToString();
    public IEnumerable<string> Headers => HeaderList;
    public IDictionary<string, int> HeadersWithIndex => HeaderIndex;
    public long RowCount => rowCount;
    public void Dispose()
    {
        sheet.Workbook.Dispose();
    }
    public IEnumerator<IEnumerable<string>> GetEnumerator()
    {
        for (var row = 0; row < rowCount; row++)
            yield return this[row];
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}