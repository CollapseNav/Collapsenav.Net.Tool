using System.Dynamic;
using MiniExcelLibs;
namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 使用MiniExcel获取excel的单元格
/// </summary>
public class MiniCellRead : IExcelCellRead
{
    public int Zero => ExcelTool.MiniZero;
    protected IEnumerable<dynamic> _sheet;
    protected Stream _stream;
    protected IDictionary<string, int> HeaderIndex;
    protected IEnumerable<string> HeaderList;
    protected int rowCount;
    protected int colCount;
    public MiniCellRead()
    {
        Init();
    }
    public MiniCellRead(string path)
    {
        var fs = path.OpenCreateReadWriteShareStream();
        Init(fs);
    }
    public MiniCellRead(Stream stream)
    {
        Init(stream);
    }
    private void Init(Stream stream)
    {
        try
        {
            _ = stream.Query().First();
            _stream = stream;
            _sheet = _stream.Query().ToList();
            rowCount = _sheet.Count();
            var sheetFirst = _sheet.First() as IDictionary<string, object>;
            colCount = sheetFirst.Count;
            HeaderList = sheetFirst.Select(item => item.Value?.ToString() ?? string.Empty);
            HeaderIndex = sheetFirst.Select((item, index) => (item.Value, index)).ToDictionary(item => item.Value.ToString(), item => item.index);
        }
        catch (Exception ex)
        {
            Init();
            _stream = stream;
        }
    }
    private void Init()
    {
        _stream = new MemoryStream();
        _sheet = new List<dynamic>();
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
                yield return new MiniCell(GetCell(i, HeaderIndex[field] + Zero), GetRow(i), i, HeaderIndex[field] + Zero);
        }
    }

    public IEnumerable<IReadCell> this[long row] => GetRow((int)row).Select((item, index) => new MiniCell(item, GetRow((int)row), (int)row + Zero, index + Zero));
    public IReadCell this[long row, long col] => new MiniCell(GetCell((int)row, (int)col + Zero), GetRow((int)row), (int)row + Zero, (int)col + Zero);
    public IReadCell this[string field, long row] => new MiniCell(GetCell((int)row, HeaderIndex[field] + Zero), GetRow((int)row), (int)row, HeaderIndex[field] + Zero);

    private KeyValuePair<string, object> GetCell(int row, int col)
    {
        var existRow = GetRow(row);
        if (col < existRow.Count())
            return existRow.ElementAt(col);

        // for (var i = existRow.Count(); i <= colCount; i++)
        //     existRow.Add(new KeyValuePair<string, object>(MiniCell.GetSCol(i), ""));
        return existRow.ElementAt(col);
    }
    private IDictionary<string, object> GetRow(int row)
    {
        // _sheet = _sheet.Concat(new dynamic[] { new Dictionary<string, object>() });
        if (row < _sheet.Count())
            return _sheet.ElementAt(row);
        for (var i = _sheet.Count(); i <= row; i++)
        {
            IDictionary<string, object> dd = new ExpandoObject();
            for (var col = 0; col < colCount; col++)
                dd.Add(MiniCell.GetSCol(col), "");
            _sheet = _sheet.Append(dd);
        }
        // _sheet = _sheet.Concat(new dynamic[] { new Dictionary<string, object>() });
        return _sheet.ElementAt(row);
    }

    public void Dispose()
    {
        _stream.Dispose();
    }

    public void Save()
    {
        // throw new Exception();
        _stream.SeekToOrigin();
        _stream.Clear();
        _stream.SaveAs(_sheet, printHeader: false);
        _stream.SeekToOrigin();
    }

    /// <summary>
    /// 保存到流
    /// </summary>
    public void SaveTo(Stream stream)
    {
        stream.SeekToOrigin();
        stream.SaveAs(_sheet, printHeader: false);
        stream.SeekToOrigin();
    }
    /// <summary>
    /// 保存到文件
    /// </summary>
    public void SaveTo(string path)
    {
        using var fs = path.OpenCreateReadWriteShareStream();
        fs.SaveAs(_sheet, printHeader: false);
        fs.Dispose();
    }
    /// <summary>
    /// 获取流
    /// </summary>
    public Stream GetStream()
    {
        var ms = new MemoryStream();
        ms.SaveAs(_sheet, printHeader: false);
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
