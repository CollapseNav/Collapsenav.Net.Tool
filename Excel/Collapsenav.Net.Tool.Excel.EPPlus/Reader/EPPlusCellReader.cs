using System.Collections;
using OfficeOpenXml;
namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 使用EPPlus获取excel的单元格
/// </summary>
public class EPPlusCellReader : IExcelCellReader
{
    public int Zero => ExcelTool.EPPlusZero;
    protected ExcelWorksheet _sheet;
    protected ExcelPackage _pack;
    protected Stream _stream;
    protected IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> HeaderList;
    protected int rowCount;
    public EPPlusCellReader()
    {
        Init();
    }
    public EPPlusCellReader(string path)
    {
        var fs = path.OpenCreateReadWriteShareStream();
        Init(fs);
    }
    public EPPlusCellReader(Stream stream)
    {
        Init(stream);
    }
    public EPPlusCellReader(ExcelWorksheet sheet)
    {
        Init(sheet);
    }
    private void Init(Stream stream)
    {
        stream.SeekToOrigin();
        // 使用传入的流, 可在 Save 时修改/覆盖
        _stream = stream;
        var sheets = EPPlusTool.EPPlusSheets(_stream);
        // 若传入的文件中无法解析出 sheets ,则使用默认的无参初始化
        if (sheets?.Count > 0)
            Init(EPPlusTool.EPPlusSheet(_stream));
        else
            Init();
    }
    private void Init(ExcelWorksheet sheet)
    {
        _sheet = sheet;
        _pack ??= new ExcelPackage();
        if (_pack.Workbook.Worksheets.Count == 0)
            _sheet = _pack.Workbook.Worksheets.Add("sheet1", sheet);

        rowCount = sheet.Dimension.Rows;
        HeaderIndex = EPPlusTool.HeadersWithIndex(sheet);
        HeaderList = HeaderIndex.Select(item => item.Key).ToList();
    }
    private void Init()
    {
        _pack = new ExcelPackage();
        _sheet = _pack.Workbook.Worksheets.Add("sheet1");
        rowCount = 0;
    }
    public int RowCount { get => rowCount; }
    public IEnumerable<string> Headers { get => HeaderList; }
    public IDictionary<string, int> HeadersWithIndex { get => HeaderIndex; }
    public IEnumerable<IReadCell> this[string field]
    {
        get
        {
            for (var i = Zero; i < rowCount + Zero; i++)
                yield return new EPPlusCell(_sheet.Cells[i, HeaderIndex[field] + Zero]);
        }
    }
    public IEnumerable<IReadCell> this[int row] => _sheet.Cells[(int)row + Zero, Zero, (int)row + Zero, Zero + Headers.Count()]?.Select(item => new EPPlusCell(item));
    public IReadCell this[int row, int col] => new EPPlusCell(_sheet.Cells[(int)row + Zero, (int)col + Zero]);
    public IReadCell this[string field, int row] => new EPPlusCell(_sheet.Cells[(int)row + Zero, HeaderIndex[field] + Zero]);
    public void Dispose()
    {
        _stream?.Dispose();
        _pack?.Dispose();
    }
    public void AutoSize()
    {
        if (HeadersWithIndex.NotEmpty())
            foreach (var col in HeadersWithIndex)
                _sheet.Column(col.Value + Zero).AutoFit();
    }
    public void Save(bool autofit = true)
    {
        SaveTo(_stream, autofit);
    }
    /// <summary>
    /// 保存到流
    /// </summary>
    public void SaveTo(Stream stream, bool autofit = true)
    {
        if (autofit)
            AutoSize();
        stream.SeekToOrigin();
        stream.Clear();
        _pack.SaveAs(stream);
        stream.SeekToOrigin();
    }
    /// <summary>
    /// 保存到文件
    /// </summary>
    public void SaveTo(string path, bool autofit = true)
    {
        using var fs = path.OpenCreateReadWriteShareStream();
        SaveTo(fs, autofit);
    }
    /// <summary>
    /// 获取流
    /// </summary>
    public Stream GetStream()
    {
        var ms = new MemoryStream();
        SaveTo(ms);
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
