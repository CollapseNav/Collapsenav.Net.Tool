using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Collapsenav.Net.Tool.Excel
{
    /// <summary>
    /// 专门为NPOI做的不关闭流
    /// </summary>
    public class NPOINotCloseStream : MemoryStream
    {
        public NPOINotCloseStream(Stream stream)
        {
            stream.CopyTo(this);
            stream.Seek(0, SeekOrigin.Begin);
            this.Seek(0, SeekOrigin.Begin);
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
}