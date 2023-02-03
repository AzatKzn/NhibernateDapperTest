using System.Data;
using Npgsql;

namespace NhibernateDapperTest;

public static class DapperTest
{
    public static IDbConnection GetConnection()
    {
        var connection = new NpgsqlConnection(Program.ConnectionString);
        connection.Open();
        return connection;
    }
}