using Microsoft.Extensions.Configuration;

namespace NederlandseLoterij.KrasLoterij.Service.Extensions
{
    public static class Extensions
    {
        public static T BindSettings<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var implementation = new T();
            configuration.GetSection(sectionName).Bind(implementation);
            return implementation;
        }
    }
}