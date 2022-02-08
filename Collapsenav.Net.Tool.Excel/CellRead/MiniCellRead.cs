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
    public IEnumerable<string> Headers
    {
        get
        {
            var sheetFirst = _sheet.First() as IDictionary<string, object>;
            return sheetFirst.Select(item => item.Value?.ToString() ?? string.Empty);
        }
    }
    public IDictionary<string, int> HeadersWithIndex
    {
        get
        {
            var sheetFirst = _sheet.First() as IDictionary<string, object>;
            return sheetFirst.Select((item, index) => (item.Value, index)).ToDictionary(item => item.Value?.ToString() ?? item.index.ToString(), item => item.index);
        }
    }
    public IEnumerable<IReadCell> this[string field]
    {
        get
        {
            for (var i = Zero; i < rowCount + Zero; i++)
                yield return new MiniCell(GetCell(i, HeadersWithIndex[field] + Zero), GetRow(i), i, HeadersWithIndex[field] + Zero);
        }
    }

    public IEnumerable<IReadCell> this[long row] => GetRow((int)row).Select((item, index) => new MiniCell(item, GetRow((int)row), (int)row + Zero, index + Zero));
    public IReadCell this[long row, long col] => new MiniCell(GetCell((int)row, (int)col + Zero), GetRow((int)row), (int)row + Zero, (int)col + Zero);
    public IReadCell this[string field, long row] => new MiniCell(GetCell((int)row, HeadersWithIndex[field] + Zero), GetRow((int)row), (int)row, HeadersWithIndex[field] + Zero);

    private KeyValuePair<string, object> GetCell(int row, int col)
    {
        var existRow = GetRow(row);
        if (col >= existRow.Count)
        {
            ExpandRow(col);
            existRow = GetRow(row);
        }
        return existRow.ElementAt(col);
    }
    private IDictionary<string, object> GetRow(int row)
    {
        if (row < _sheet.Count())
            return _sheet.ElementAt(row);
        for (var i = _sheet.Count(); i <= row; i++)
        {
            IDictionary<string, object> newRow = new ExpandoObject();
            for (var col = 0; col < colCount; col++)
                newRow.Add(MiniCell.GetSCol(col), "");
            _sheet = _sheet.Append(newRow);
        }
        return _sheet.ElementAt(row);
    }
    private void ExpandRow(int col)
    {
        colCount = col;
        for (var row = Zero; row < _sheet.Count(); row++)
        {
            var existRow = GetRow(row);
            for (var i = existRow.Count; i <= col; i++)
                existRow.Add(MiniCell.GetSCol(i), "");
        }
    }

    public void Dispose()
    {
        _stream.Dispose();
    }

    public void Save()
    {
        SaveTo(_stream);
    }

    /// <summary>
    /// 保存到流
    /// </summary>
    public void SaveTo(Stream stream)
    {
        stream.SeekToOrigin();
        stream.Clear();
        stream.SaveAs(_sheet, printHeader: false);
        stream.SeekToOrigin();
    }
    /// <summary>
    /// 保存到文件
    /// </summary>
    public void SaveTo(string path)
    {
        using var fs = path.OpenCreateReadWriteShareStream();
        SaveTo(fs);
        fs.Dispose();
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
        for (var row = 0; row < rowCount + Zero; row++)
            yield return this[row];
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
