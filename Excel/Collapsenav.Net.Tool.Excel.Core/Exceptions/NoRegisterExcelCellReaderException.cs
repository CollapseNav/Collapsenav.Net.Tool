namespace Collapsenav.Net.Tool.Excel;

public class NoRegisterExcelCellReaderException : Exception
{
    public NoRegisterExcelCellReaderException(string message) : base(message)
    {
    }
    public NoRegisterExcelCellReaderException() : base("未注册具体的 IExcelCellReader 实现")
    {
    }
}