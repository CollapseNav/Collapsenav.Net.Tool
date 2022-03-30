namespace Collapsenav.Net.Tool.Data;

public class SqliteConn : Conn
{
    public SqliteConn(string source) : base(source)
    {
    }

    public override string ToString()
    {
        return GetConnString();
    }

    public string GetConnString()
    {
        return $"Data Source = {Source}";
    }
}