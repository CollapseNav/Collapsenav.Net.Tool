using System.Text;

namespace Collapsenav.Net.Tool.Data;

public class PgsqlConn : Conn
{
    public PgsqlConn(string source, int? port, string dataBase, string user, string pwd) : base(source, port, dataBase, user, pwd)
    {
    }

    public override string ToString()
    {
        return GetConnString();
    }

    public override string GetConnString()
    {
        StringBuilder sb = new();
        sb.Append($"Host = {Source}; Port = {Port.ToString()}; Database = {DataBase}; Username = {User}; Password = {Pwd};");
        return sb.ToString();
    }
}