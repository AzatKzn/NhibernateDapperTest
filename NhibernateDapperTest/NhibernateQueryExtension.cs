using System.Collections;
using NHibernate;

namespace NhibernateDapperTest;

public static class NhibernateQueryExtension
{
    /// <summary>
    /// Установить параметры для запроса
    /// </summary>
    /// <param name="query">Запрос</param>
    /// <param name="parameters">Словарь параметров в виде наименование(ключ) - значение</param>
    public static IQuery SetParameters(this IQuery query, Dictionary<string, object> parameters)
    {
        foreach (var (key, value) in parameters)
        {
            SetParameter(query, key, value);
        }

        return query;
    }
		
    /// <summary>
    /// Установить параметры для запроса
    /// </summary>
    /// <param name="query">Запрос</param>
    /// <param name="parameters">Параметры в виде свойств объекта (примерно как в Dapper)</param>
    public static IQuery SetParameters(this IQuery query, object parameters)
    {
        var properties = parameters.GetType().GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(parameters);
            SetParameter(query, property.Name, value);
        }
			
        return query;
    }

    private static void SetParameter(IQuery query, string name, object value)
    {
        if (!query.NamedParameters?.Contains(name) ?? true)
        {
            return;
        }
			
        if (value is ICollection enumerable)
        {
            query.SetParameterList(name, enumerable);
        }
        else
        {
            query.SetParameter(name, value);					
        }
    } 
}