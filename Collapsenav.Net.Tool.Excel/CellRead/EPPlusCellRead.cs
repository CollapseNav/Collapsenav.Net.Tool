using OfficeOpenXml;
namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 使用EPPlus获取excel的单元格
/// </summary>
public class EPPlusCellRead : IExcelCellRead
{
    protected const int Zero = ExcelTool.EPPlusZero;
    protected int headerRowCount = Zero;
    protected ExcelWorksheet _sheet;
    protected ExcelPackage _pack;
    protected Stream _stream;
    protected IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> HeaderList;
    protected int rowCount;
    public EPPlusCellRead()
    {
        Init();
    }
    public EPPlusCellRead(string path, int headerCount = Zero)
    {
        var fs = path.OpenCreateReadWirteShareStream();
        Init(fs, headerCount);
    }
    public EPPlusCellRead(Stream stream, int headerCount = Zero)
    {
        Init(stream, headerCount);
    }
    public EPPlusCellRead(ExcelWorksheet sheet, int headerCount = Zero)
    {
        Init(sheet, headerCount);
    }
    private void Init(Stream stream, int headerCount = Zero)
    {
        // 使用传入的流, 可在 Save 时修改/覆盖
        _stream = stream;
        var sheets = ExcelTool.EPPlusSheets(_stream);
        // 若传入的文件中无法解析出 sheets ,则使用默认的无参初始化
        if (sheets?.Count > 0)
            Init(ExcelTool.EPPlusSheet(_stream), headerCount);
        else
            Init();
    }
    private void Init(ExcelWorksheet sheet, int headerCount = Zero)
    {
        _sheet = sheet;
        _pack ??= new ExcelPackage();
        if (_pack.Workbook.Worksheets.Count == 0)
            _sheet = _pack.Workbook.Worksheets.Add("sheet1", sheet);

        headerRowCount += headerCount;

        rowCount = sheet.Dimension.Rows;
        HeaderIndex = ExcelReadTool.HeadersWithIndex(sheet);
        HeaderList = HeaderIndex.Select(item => item.Key).ToList();
    }
    private void Init()
    {
        _pack = new ExcelPackage();
        _sheet = _pack.Workbook.Worksheets.Add("sheet1");
        rowCount = 0;
    }


    public long RowCount { get => rowCount; }
    public IEnumerable<string> Headers { get => HeaderList; }
    public IDictionary<string, int> HeadersWithIndex { get => HeaderIndex; }
    public IEnumerable<IReadCell> this[string field]
    {
        get
        {
            for (var i = headerRowCount; i < rowCount; i++)
                yield return new EPPlusCell(_sheet.Cells[i, HeaderIndex[field] + Zero]);
        }
    }

    public IEnumerable<IReadCell> this[long row] => _sheet.Cells[(int)row, Zero, (int)row + Zero, Zero + Headers.Count()]?.Select(item => new EPPlusCell(item));
    public IReadCell this[long row, long col] => new EPPlusCell(_sheet.Cells[(int)row + Zero, (int)col + Zero]);
    public IReadCell this[string field, long row] => new EPPlusCell(_sheet.Cells[(int)row + Zero, HeaderIndex[field] + Zero]);

    public void Dispose()
    {
        _stream.Dispose();
        _pack.Dispose();
    }
    public void Save()
    {
        _stream.SeekToOrigin();
        _stream.Clear();
        _pack.SaveAs(_stream);
        _stream.SeekToOrigin();
    }

    /// <summary>
    /// 保存到流
    /// </summary>
    public void SaveTo(Stream stream)
    {
        stream.SeekToOrigin();
        stream.Clear();
        _pack.SaveAs(stream);
        stream.SeekToOrigin();
    }

    /// <summary>
    /// 保存到文件
    /// </summary>
    public void SaveTo(string path)
    {
        using var fs = path.OpenCreateReadWirteShareStream();
        _pack.SaveAs(fs);

    }
    /// <summary>
    /// 获取流
    /// </summary>
    public Stream GetStream()
    {
        var ms = new MemoryStream();
        _pack.SaveAs(ms);
        ms.SeekToOrigin();
        return ms;
    }
}
