using System.Text;

namespace Collapsenav.Net.Tool.Data;

public class MariaDbConn : Conn
{
    public MariaDbConn(string source, int? port, string dataBase, string user, string pwd) : base(source, port, dataBase, user, pwd)
    {
    }
    public override string ToString()
    {
        return GetConnString();
    }

    public override string GetConnString()
    {
        StringBuilder sb = new();
        sb.Append($"Server = {Source}; Port = {Port.ToString()}; Database = {DataBase}; Uid = {User}; Pwd = {Pwd};");
        return sb.ToString();
    }
}