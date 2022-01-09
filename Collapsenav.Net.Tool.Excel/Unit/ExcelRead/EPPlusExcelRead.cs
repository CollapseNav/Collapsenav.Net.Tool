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

    int IExcelRead.Zero => EPPlusZero;
    long IExcelRead.RowCount { get => rowCount; }
    IEnumerable<string> IExcelRead.Headers { get => Headers; }
    IDictionary<string, int> IExcelRead.HeadersWithIndex { get => HeaderIndex; }

    public EPPlusExcelRead(ExcelWorksheet sheet, int headerCount = EPPlusZero)
    {
        this.sheet = sheet;
        headerRowCount += headerCount;

        rowCount = sheet.Dimension.Rows;
        HeaderIndex = ExcelReadTool.HeadersWithIndex(sheet);
        Headers = HeaderIndex.Select(item => item.Key).ToList();

    }
    IEnumerable<string> IExcelRead.this[string field]
    {
        get
        {
            for (var i = headerRowCount; i < rowCount; i++)
                yield return sheet.Cells[i, HeaderIndex[field] + EPPlusZero].Value.ToString();
        }
    }

    IEnumerable<string> IExcelRead.this[long col]
    {
        get
        {
            for (var i = headerRowCount; i < rowCount; i++)
                yield return sheet.Cells[i, (int)col + EPPlusZero].Value.ToString();
        }
    }

    string IExcelRead.this[long row, long col] => sheet.Cells[(int)row + EPPlusZero, (int)col + EPPlusZero].Value.ToString();

    string IExcelRead.this[string field, long row] => sheet.Cells[(int)row + EPPlusZero, HeaderIndex[field] + EPPlusZero].Value.ToString();

    public void Dispose()
    {
        sheet.Workbook.Dispose();
    }
}
