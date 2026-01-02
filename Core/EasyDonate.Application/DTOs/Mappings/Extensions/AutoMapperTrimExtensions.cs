using AutoMapper;
using System.Reflection;

namespace EasyDonate.Application.DTOs.Mappings.Extensions;

public static class AutoMapperTrimExtensions
{
    public static IMappingExpression<TSource, TDestination> ApplyTrimToStrings<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map)
    {
        return map.AfterMap((src, dest) =>
        {
            var stringProperties = typeof(TDestination)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(string) && p.CanWrite);

            foreach (var prop in stringProperties)
            {
                var value = (string?)prop.GetValue(dest);
                if (value != null)
                {
                    prop.SetValue(dest, value.Trim());
                }
            }
        });
    }
}
