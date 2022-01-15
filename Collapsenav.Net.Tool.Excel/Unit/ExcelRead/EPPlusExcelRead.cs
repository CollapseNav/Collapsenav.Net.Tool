using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public class EPPlusExcelRead : IExcelRead
{
    protected int headerRowCount = ExcelReadTool.EPPlusZero;
    protected ExcelWorksheet sheet;
    protected IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> Headers;
    protected int rowCount;

    long IExcelRead<string>.RowCount { get => rowCount; }
    IEnumerable<string> IExcelRead<string>.Headers { get => Headers; }
    IDictionary<string, int> IExcelRead<string>.HeadersWithIndex { get => HeaderIndex; }

    public EPPlusExcelRead(Stream stream, int headerCount = ExcelReadTool.EPPlusZero) : this(ExcelReadTool.EPPlusSheet(stream), headerCount)
    { }
    public EPPlusExcelRead(ExcelWorksheet sheet, int headerCount = ExcelReadTool.EPPlusZero)
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
                yield return sheet.Cells[i, HeaderIndex[field] + ExcelReadTool.EPPlusZero].Value.ToString();
        }
    }

    IEnumerable<string> IExcelRead<string>.this[long row]
    {
        get
        {
            row += ExcelReadTool.EPPlusZero;
            return sheet.Cells[(int)row, ExcelReadTool.EPPlusZero, (int)row, ExcelReadTool.EPPlusZero + Headers.Count()].Select(item => item.Value.ToString());
        }
    }

    string IExcelRead<string>.this[long row, long col] => sheet.Cells[(int)row + ExcelReadTool.EPPlusZero, (int)col + ExcelReadTool.EPPlusZero].Value.ToString();

    string IExcelRead<string>.this[string field, long row] => sheet.Cells[(int)row + ExcelReadTool.EPPlusZero, HeaderIndex[field] + ExcelReadTool.EPPlusZero].Value.ToString();

    public void Dispose()
    {
        sheet.Workbook.Dispose();
    }
}
