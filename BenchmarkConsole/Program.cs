// See https://aka.ms/new-console-template for more information

using System.Web;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Shared.Common;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddInfrastructure(builder.Configuration);
        services.AddApplication();
    })
    .Build();



// BenchmarkRunner.Run<Benchmark>();

// await host.RunAsync();

// await Run(host.Services, "scope");


static async Task Run(IServiceProvider services, string scope)
{
    Console.WriteLine($"Hello, World from scope={scope}!");
    
    var connection = new HubConnectionBuilder()
        .WithUrl("http://localhost:5021/hubs/ping")
        .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) })
        .Build();
    
    await connection.StartAsync();
    
    connection.On<string, string>("BroadcastMessage", (user, message) =>
    {
        Console.WriteLine(user + message);
    });
    
    await Delay.Seconds(1);
    await connection.InvokeAsync("Send", "user1", "pingping");
    await Delay.Seconds(2);

    await ReceiveStream(connection);
    await SendStream(connection);
}

static async Task ReceiveStream(HubConnection? connection)
{
    // Call "Cancel" on this CancellationTokenSource to send a cancellation message to
    // the server, which will trigger the corresponding token in the hub method.
    var cancellationTokenSource = new CancellationTokenSource();
    var stream = connection.StreamAsync<int>(
        "SendStream", 10, 500, cancellationTokenSource.Token);

    await foreach (var count in stream)
    {
        Console.WriteLine($"{count}");
    }

    Console.WriteLine("Streaming completed");
}

static async Task SendStream(HubConnection? connection)
{
    async IAsyncEnumerable<int> clientStreamData()
    {
        for (var i = 0; i < 5; i++)
        {
            await Delay.Milliseconds(500);
            var data = i;
            yield return data;
        }
        //After the for loop has completed and the local function exits the stream completion will be sent.
    }

    await connection.SendAsync("UploadStream", clientStreamData());
    await Delay.Seconds(5);
}