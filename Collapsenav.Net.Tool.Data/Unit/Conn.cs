namespace Collapsenav.Net.Tool.Data;

public abstract class Conn
{
    protected Conn(string source)
    {
        Source = source;
    }
    protected Conn(string source, int? port, string dataBase, string user, string pwd)
    {
        Source = source;
        Port = port;
        DataBase = dataBase;
        User = user;
        Pwd = pwd;
    }

    public string Source { get; set; }
    public int? Port { get; set; }
    public string DataBase { get; set; }
    public string User { get; set; }
    public string Pwd { get; set; }
}