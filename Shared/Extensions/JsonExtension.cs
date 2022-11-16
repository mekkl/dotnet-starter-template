using System.Text.Json;

namespace Shared.Extensions;

public static class JsonExtension
{
    public static T? As<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }
    
    public static string AsJson(this object obj)
    {
        return JsonSerializer.Serialize(obj);
    }
}