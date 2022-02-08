using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 专门为NPOI做的不关闭流
/// </summary>
public class NPOINotCloseStream : MemoryStream
{
    public NPOINotCloseStream() { }
    public NPOINotCloseStream(Stream stream)
    {
        stream.CopyTo(this);
        stream.SeekToOrigin();
        this.SeekToOrigin();
    }
    public NPOINotCloseStream(string path)
    {
        using var fs = path.OpenReadShareStream();
        fs.CopyTo(this);
        fs.Dispose();
        this.SeekToOrigin();
    }
    public override void Close() { }
    public IWorkbook GetWorkBook()
    {
        IWorkbook workbook;
        try
        {
            workbook = Length > 0 ? new HSSFWorkbook(this) : new HSSFWorkbook();
        }
        catch
        {
            Seek(0, SeekOrigin.Begin);
            workbook = Length > 0 ? new XSSFWorkbook(this) : new XSSFWorkbook();
        }
        return workbook;
    }
}