// See https://aka.ms/new-console-template for more information

using Application;
using BenchmarkConsole;
using BenchmarkDotNet.Running;
using Infrastructure;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddInfrastructure();
        services.AddApplication();
    })
    .Build();

BenchmarkRunner.Run<Benchmark>();

// await host.RunAsync();

static void Run(IServiceProvider services, string scope)
{
    Console.WriteLine($"Hello, World from scope={scope}!");
}