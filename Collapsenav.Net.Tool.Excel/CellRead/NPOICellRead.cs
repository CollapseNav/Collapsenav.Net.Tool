using System.Collections;
using NPOI.SS.UserModel;
using NPOI.XSSF.Streaming;
namespace Collapsenav.Net.Tool.Excel;

/// <summary>
/// 使用NPOI获取excel的单元格
/// </summary>
public class NPOICellRead : IExcelCellRead
{
    public int Zero => ExcelReadTool.NPOIZero;
    protected ISheet _sheet;
    protected IWorkbook _workbook;
    protected Stream _stream;
    protected NPOINotCloseStream notCloseStream;
    public IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> HeaderList;
    protected int rowCount;
    public NPOICellRead()
    {
        Init();
    }
    public NPOICellRead(string path)
    {
        var fs = path.OpenCreateReadWriteShareStream();
        Init(fs);
    }

    public NPOICellRead(Stream stream)
    {
        Init(stream);
    }
    public NPOICellRead(ISheet sheet)
    {
        Init(sheet);
    }
    private void Init(Stream stream)
    {
        _stream = stream;
        notCloseStream = new NPOINotCloseStream(stream);
        var sheet = ExcelTool.NPOISheet(_stream);
        if (sheet == null)
            Init();
        else
            Init(sheet);
    }
    private void Init(ISheet sheet)
    {
        _sheet = sheet;
        _workbook = sheet.Workbook;

        rowCount = sheet.LastRowNum + 1;
        HeaderIndex = ExcelReadTool.HeadersWithIndex(sheet);
        HeaderList = HeaderIndex.Select(item => item.Key).ToList();
    }
    private void Init()
    {
        _workbook = new SXSSFWorkbook();
        _sheet = _workbook.CreateSheet("sheet1");
        rowCount = 0;
    }


    public long RowCount { get => rowCount; }
    public IEnumerable<string> Headers { get => HeaderList; }
    public IDictionary<string, int> HeadersWithIndex { get => HeaderIndex; }
    public IEnumerable<IReadCell> this[string field]
    {
        get
        {
            for (var i = Zero; i < rowCount + Zero; i++)
                yield return new NPOICell(GetCell(GetRow(i), HeaderIndex[field] + Zero));
        }
    }
    public IEnumerable<IReadCell> this[long row] => GetRow(row + Zero).Cells.Select(item => new NPOICell(item));
    public IReadCell this[long row, long col]
    {
        get
        {
            return new NPOICell(GetCell(GetRow(row), (int)col));
        }
    }
    public IReadCell this[string field, long row] => new NPOICell(GetCell(GetRow(row), HeaderIndex[field]));

    public void Dispose()
    {
        _stream.Dispose();
        notCloseStream.Dispose();
        _workbook.Close();
    }
    private IRow GetRow(long row)
    {
        var excelRow = _sheet.GetRow((int)row);
        excelRow ??= _sheet.CreateRow((int)row);
        return excelRow;
    }
    private ICell GetCell(IRow row, int col)
    {
        var cell = row.GetCell(col, MissingCellPolicy.RETURN_NULL_AND_BLANK);
        if (cell == null)
            cell = row.CreateCell(col);
        return cell;
    }

    public void Save()
    {
        _stream.Clear();
        notCloseStream.Clear();
        _workbook.Write(notCloseStream);
        notCloseStream.SeekToOrigin();
        notCloseStream.CopyTo(_stream);
    }
    /// <summary>
    /// 保存到流
    /// </summary>
    public void SaveTo(Stream stream)
    {
        stream.Clear();
        using var fs = new NPOINotCloseStream();
        _sheet.Workbook.Write(fs);
        fs.SeekToOrigin();
        fs.CopyTo(stream);
        stream.SeekToOrigin();
    }

    /// <summary>
    /// 保存到文件
    /// </summary>
    public void SaveTo(string path)
    {
        using var fs = path.OpenCreateReadWriteShareStream();
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

    public IEnumerator<IEnumerable<IReadCell>> GetEnumerator()
    {
        for (var row = 0; row < rowCount; row++)
            yield return this[row];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}