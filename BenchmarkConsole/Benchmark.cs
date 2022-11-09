using BenchmarkDotNet.Attributes;
using Shared.Extensions;

namespace BenchmarkConsole;

[MemoryDiagnoser(false)]
public class Benchmark
{
    [Params(10, 1000, 10000)] public int Size { get; set; }
    
    [Benchmark]
    public void NormalForLoop()
    {
        for (var i = 0; i <= Size; i++)
        {
            DoSomething();
        }
    }
    
    [Benchmark]
    public void RangeForLoop()
    {
        foreach (var i in Size)
        {
            DoSomething();
        }
    }
    
    private static void DoSomething(){}
}