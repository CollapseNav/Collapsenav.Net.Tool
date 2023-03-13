using System.Collections;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public class EPPlusExcelReader : IExcelReader
{
    private object[,] sheetData;
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
        sheetData = sheet.Cells.Value as object[,];
    }
    public IEnumerable<string> this[string field]
    {
        get
        {
            for (var i = Zero; i < rowCount + Zero; i++)
                yield return sheet.Cells[i, HeaderIndex[field] + Zero].Value.ToString();
        }
    }
    public IEnumerable<string> this[int row]
    {
        get
        {
            List<string> _data = new(HeaderList.Count());
            foreach (var h in HeadersWithIndex)
                _data.Add(sheetData[row, h.Value]?.ToString());
            return _data;
        }
    }
    public string this[int row, int col] => sheetData[row, col].ToString();
    public string this[string field, int row] => sheetData[row, HeaderIndex[field]].ToString();
    public IEnumerable<string> Headers => HeaderList;
    public IDictionary<string, int> HeadersWithIndex => HeaderIndex;
    public int RowCount => rowCount;
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