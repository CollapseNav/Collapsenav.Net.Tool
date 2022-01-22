using NPOI.SS.UserModel;
using NPOI.XSSF.Streaming;
namespace Collapsenav.Net.Tool.Excel;

/// <summary>
/// 使用NPOI获取excel的单元格
/// </summary>
public class NPOICellRead : IExcelCellRead
{
    protected const int Zero = ExcelReadTool.NPOIZero;
    protected int headerRowCount = ExcelReadTool.NPOIZero;
    protected ISheet _sheet;
    protected IWorkbook _workbook;
    protected Stream _stream;
    public IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> HeaderList;
    protected int rowCount;
    public NPOICellRead()
    {
        _workbook = new SXSSFWorkbook();
        _sheet = _workbook.CreateSheet("sheet1");
        rowCount = 0;
    }
    public NPOICellRead(string path, int headerCount = Zero)
    {
        var fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        Init(fs, headerCount);
    }

    public NPOICellRead(Stream stream, int headerCount = Zero)
    {
        Init(stream, headerCount);
    }
    public NPOICellRead(ISheet sheet, int headerCount = Zero)
    {
        Init(sheet, headerCount);
    }
    private void Init(Stream stream, int headerCount = Zero)
    {
        _stream = stream;
        Init(ExcelReadTool.NPOISheet(_stream), headerCount);
    }
    private void Init(ISheet sheet, int headerCount = Zero)
    {
        _sheet = sheet;
        _workbook = sheet.Workbook;
        headerRowCount += headerCount;

        rowCount = sheet.LastRowNum + 1;
        HeaderIndex = ExcelReadTool.HeadersWithIndex(sheet);
        HeaderList = HeaderIndex.Select(item => item.Key).ToList();
    }


    public long RowCount { get => rowCount; }
    public IEnumerable<string> Headers { get => HeaderList; }
    public IDictionary<string, int> HeadersWithIndex { get => HeaderIndex; }
    public IEnumerable<IReadCell> this[string field]
    {
        get
        {
            for (var i = headerRowCount; i < rowCount; i++)
            {
                yield return new NPOICell(GetRow(i).GetCell(HeaderIndex[field] + Zero, MissingCellPolicy.CREATE_NULL_AS_BLANK));
            }
        }
    }
    public IEnumerable<IReadCell> this[long row] => GetRow(row + Zero).Cells.Select(item => new NPOICell(item));
    public IReadCell this[long row, long col]
    {
        get
        {
            return new NPOICell(GetRow(row).GetCell((int)col, MissingCellPolicy.CREATE_NULL_AS_BLANK));
        }
    }
    public IReadCell this[string field, long row] => new NPOICell(_sheet.GetRow((int)row).GetCell(HeaderIndex[field], MissingCellPolicy.CREATE_NULL_AS_BLANK));

    public void Dispose()
    {
        _stream.Dispose();
        _sheet.Workbook.Close();
    }
    private IRow GetRow(long row)
    {
        var excelRow = _sheet.GetRow((int)row);
        excelRow ??= _sheet.CreateRow((int)row);
        return excelRow;
    }

    /// <summary>
    /// 保存到流
    /// </summary>
    public void SaveTo(Stream stream)
    {
        _sheet.Workbook.Write(stream);
        stream.SeekToOrigin();
    }

    /// <summary>
    /// 保存到文件
    /// </summary>
    public void SaveTo(string path)
    {
        using var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
        _sheet.Workbook.Write(fs);
        fs.SeekToOrigin();
        fs.Dispose();
    }

    /// <summary>
    /// 获取流
    /// </summary>
    public Stream GetStream()
    {
        var ms = new NPOINotCloseStream();
        _sheet.Workbook.Write(ms);
        ms.SeekToOrigin();
        return ms;
    }
}