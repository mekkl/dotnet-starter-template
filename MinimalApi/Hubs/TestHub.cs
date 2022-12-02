using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.SignalR;
using Shared.Common;

namespace MinimalApi.Hubs;

public class TestHub : Hub
{
    private readonly ILogger<TestHub> _logger;
    
    public TestHub(ILogger<TestHub> logger)
    {
        _logger = logger;
    }

    public async Task Send(string name, string message)
    {
        // Call the broadcastMessage method to update clients.
        _logger.LogInformation("message name{Name} with message={Message}", name, message);
        await Clients.All.SendAsync(HubMethods.BroadcastMessage, name, message);
    }
    
    public async IAsyncEnumerable<int> SendStream(
        int count, int delay, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        for (var i = 0; i < count; i++)
        {
            // Check the cancellation token regularly so that the server will stop
            // producing items if the client disconnects.
            cancellationToken.ThrowIfCancellationRequested();

            yield return i;

            // Use the cancellationToken in other APIs that accept cancellation
            // tokens so the cancellation can flow down to them.
            await Delay.Milliseconds(delay, cancellationToken);
        }
    }
    
    public async Task UploadStream(IAsyncEnumerable<int> stream)
    {
        await foreach (var item in stream)
        {
            Console.WriteLine(item);
        }
    }
}

public static class HubMethods
{
    public const string BroadcastMessage = "BroadcastMessage";
}