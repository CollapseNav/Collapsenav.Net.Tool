using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public class EPPlusExcelRead : IExcelRead
{
    protected int headerRowCount = EPPlusZero;
    protected const int EPPlusZero = 1;
    protected ExcelWorksheet sheet;
    protected IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> Headers;
    protected int rowCount;

    int IExcelRead<string>.Zero => EPPlusZero;
    long IExcelRead<string>.RowCount { get => rowCount; }
    IEnumerable<string> IExcelRead<string>.Headers { get => Headers; }
    IDictionary<string, int> IExcelRead<string>.HeadersWithIndex { get => HeaderIndex; }

    public EPPlusExcelRead(Stream stream, int headerCount = EPPlusZero) : this(ExcelReadTool.GetEPPlusSheet(stream), headerCount)
    { }
    public EPPlusExcelRead(ExcelWorksheet sheet, int headerCount = EPPlusZero)
    {
        this.sheet = sheet;
        headerRowCount += headerCount;

        rowCount = sheet.Dimension.Rows;
        HeaderIndex = ExcelReadTool.HeadersWithIndex(sheet);
        Headers = HeaderIndex.Select(item => item.Key).ToList();

    }
    IEnumerable<string> IExcelRead<string>.this[string field]
    {
        get
        {
            for (var i = headerRowCount; i < rowCount; i++)
                yield return sheet.Cells[i, HeaderIndex[field] + EPPlusZero].Value.ToString();
        }
    }

    IEnumerable<string> IExcelRead<string>.this[long row]
    {
        get
        {
            row += EPPlusZero;
            return sheet.Cells[(int)row, EPPlusZero, (int)row, EPPlusZero + Headers.Count()].Select(item => item.Value.ToString());
        }
    }

    string IExcelRead<string>.this[long row, long col] => sheet.Cells[(int)row + EPPlusZero, (int)col + EPPlusZero].Value.ToString();

    string IExcelRead<string>.this[string field, long row] => sheet.Cells[(int)row + EPPlusZero, HeaderIndex[field] + EPPlusZero].Value.ToString();

    public void Dispose()
    {
        sheet.Workbook.Dispose();
    }
}
