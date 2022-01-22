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
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        fs.CopyTo(this);
        this.SeekToOrigin();
    }
    public override void Close() { }
    public IWorkbook GetWorkBook()
    {
        IWorkbook workbook;
        try
        {
            workbook = new HSSFWorkbook(this);
        }
        catch
        {
            Seek(0, SeekOrigin.Begin);
            workbook = new XSSFWorkbook(this);
        }
        return workbook;
    }
}