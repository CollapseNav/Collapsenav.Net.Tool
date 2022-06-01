namespace Collapsenav.Net.Tool.Excel;

public class NoRegisterExcelReaderException : Exception
{
    public NoRegisterExcelReaderException(string message) : base(message)
    {
    }
    public NoRegisterExcelReaderException() : base("未注册具体的 IExcelReader 实现")
    {
    }
}