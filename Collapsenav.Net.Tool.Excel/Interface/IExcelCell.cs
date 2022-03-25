namespace Collapsenav.Net.Tool.Excel;

public interface IReadCell
{
    int Row { get; }
    int Col { get; }
    string StringValue { get; }
    Type ValueType { get; }
    object Value { get; set; }
    void CopyCellFrom(IReadCell cell);
}
public interface IReadCell<T> : IReadCell
{
    T Cell { get; set; }
}
