using Microsoft.Extensions.Configuration;

namespace Shared.Extensions;

public static class ConfigurationExtension
{
    public static T? GetOrDefault<T>(this IConfiguration configuration, string name)
    {
        var config = configuration[name];
        if (config is null) 
            return default;

        try
        {
            return (T)Convert.ChangeType(config, typeof(T));
        }
        catch (FormatException _)
        {
            return default;
        }
    }
}
