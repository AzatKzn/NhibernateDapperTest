using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace NhibernateDapperTest;

public static class NhibernateTest
{
    private static readonly Configuration _cfg;
    
    static NhibernateTest()
    {
        var cfg = new Configuration();
        cfg.DataBaseIntegration(x =>
        {
            x.Dialect<PostgreSQLDialect>();
            x.Driver<NpgsqlDriver>();
            x.ConnectionString = Program.ConnectionString;
        });
			
        cfg.AddAssembly(Assembly.GetExecutingAssembly());

        _cfg = cfg;
    }

    public static ISessionFactory GetSessionFactory()
    {
        return _cfg.BuildSessionFactory();
    }

    public static ISession GetSession()
    {
        return GetSessionFactory().OpenSession();
    }
}