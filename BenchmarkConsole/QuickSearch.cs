using System.Diagnostics;
using System.Web;
using Shared.Extensions;

namespace BenchmarkConsole;

public static class QuickSearch
{
    private const string EngineSeparator = ":";
    private static readonly Dictionary<string, string> Engines = new()
    {
        { "gh", "https://github.com/search?q=" },
        { "g", "https://www.google.com/search?q=" },
    };

    public static void OpenSearch(string search)
    {
        var usedEngine = search[..search.IndexOf(EngineSeparator, StringComparison.Ordinal)].Trim();
        if (!Engines.TryGetValue(usedEngine, out var engine))
            throw new ArgumentException($"Engine={usedEngine} not found! Valid engines is {Engines.Keys.AsJson()}");
        
        var query = search[(search.IndexOf(EngineSeparator, StringComparison.Ordinal) + 1)..];
        var encodedQuery = HttpUtility.UrlEncode(query);
        OpenBrowser(new Uri($"{engine}{encodedQuery}"));
    }
    
    private static void OpenBrowser(Uri uri)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = uri.ToString(),
            UseShellExecute = true
        });
    }
}