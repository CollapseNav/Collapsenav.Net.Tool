using MiniExcelLibs;
using OfficeOpenXml;
namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 使用MiniExcel获取excel的单元格
/// </summary>
public class MiniCellRead : IExcelCellRead
{
    public int Zero => ExcelTool.MiniZero;
    protected IEnumerable<dynamic> sheet;
    protected Stream _stream;
    protected IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> HeaderList;
    protected int rowCount;
    public MiniCellRead()
    {
        _stream = new MemoryStream();
        sheet = _stream.Query().ToList();
        rowCount = 0;
    }
    public MiniCellRead(string path)
    {
        var fs = path.OpenCreateReadWirteShareStream();
        Init(fs);
    }
    public MiniCellRead(Stream stream)
    {
        Init(stream);
    }
    private void Init(Stream stream)
    {
        _stream = stream;
        sheet = _stream.Query().ToList();

        rowCount = sheet.Count();
        var sheetFirst = sheet.First() as IEnumerable<KeyValuePair<string, object>>;
        HeaderList = sheetFirst.Select(item => item.Value?.ToString() ?? string.Empty);
        HeaderIndex = sheetFirst.Select((item, index) => (item.Value, index)).ToDictionary(item => item.Value.ToString(), item => item.index);
    }


    public long RowCount { get => rowCount; }
    public IEnumerable<string> Headers { get => HeaderList; }
    public IDictionary<string, int> HeadersWithIndex { get => HeaderIndex; }
    public IEnumerable<IReadCell> this[string field]
    {
        get
        {
            for (var i = Zero; i < rowCount + Zero; i++)
                yield return new MiniCell((sheet.ElementAt(i) as IEnumerable<KeyValuePair<string, object>>).ElementAt(HeaderIndex[field] + Zero), i, HeaderIndex[field] + Zero);
        }
    }

    public IEnumerable<IReadCell> this[long row] => (sheet.ElementAt((int)row) as IEnumerable<KeyValuePair<string, object>>).Select((item, index) => new MiniCell(item, (int)row + Zero, index + Zero));
    public IReadCell this[long row, long col] => new MiniCell((sheet.ElementAt((int)row) as IEnumerable<KeyValuePair<string, object>>).ElementAt((int)col + Zero), (int)row + Zero, (int)col + Zero);
    public IReadCell this[string field, long row] => new MiniCell((sheet.ElementAt((int)row) as IEnumerable<KeyValuePair<string, object>>).ElementAt(HeaderIndex[field] + Zero), (int)row, HeaderIndex[field] + Zero);

    public void Dispose()
    {
        _stream.Dispose();
    }

    public void Save()
    {
        _stream.SeekToOrigin();
        _stream.Clear();
        _stream.SaveAs(sheet);
        _stream.SeekToOrigin();
    }

    /// <summary>
    /// 保存到流
    /// </summary>
    public void SaveTo(Stream stream)
    {
        stream.SeekToOrigin();
        stream.SaveAs(sheet);
        stream.SeekToOrigin();
    }
    /// <summary>
    /// 保存到文件
    /// </summary>
    public void SaveTo(string path)
    {
        using var fs = path.OpenCreateReadWirteShareStream();
        fs.SaveAs(sheet);
        fs.Dispose();
    }
    /// <summary>
    /// 获取流
    /// </summary>
    public Stream GetStream()
    {
        var ms = new MemoryStream();
        ms.SaveAs(sheet);
        ms.SeekToOrigin();
        return ms;
    }

    public IEnumerator<IEnumerable<IReadCell>> GetEnumerator()
    {
        for (var row = 0; row < rowCount + Zero; row++)
            yield return this[row];
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
