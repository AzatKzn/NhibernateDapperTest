// See https://aka.ms/new-console-template for more information

using Dapper;
using NhibernateDapperTest;

/*
 * Тестирование запросов к БД, через Nhibernate и Dapper 
 */

public class Program
{
    public const string ConnectionString = "";
    
    public static void Main(string[] args)
    {
        // NhibernateFoo();
        DapperFoo();
    }

    public static void NhibernateFoo()
    {
        using var session = NhibernateTest.GetSession();
        var sql = "SELECT COUNT(*) FROM fbill_kernel.s_point WHERE flag = :flag";
        var s = session.CreateSQLQuery(sql)
            .SetParameter("flag", 1)
            .UniqueResult<long>();
        Console.WriteLine(s);
    }

    public static void DapperFoo()
    {
        using var connection = DapperTest.GetConnection();
        var sql = "SELECT COUNT(*) FROM fbill_kernel.s_point WHERE flag = @flag";
        var s = connection.ExecuteScalar<int>(sql, new { flag = 1 });
        Console.WriteLine(s);
    }
}