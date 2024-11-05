using BenchmarkDotNet.Attributes;

namespace PerformanceConvenience;

[MemoryDiagnoser]
public class Benchmark
{
    private readonly DateOnly _date = new(2024, 03, 15);
    private string _csv = string.Empty;
    
    [GlobalSetup]
    public async Task Setup()
    {
        _csv = await CurrencyRatesExtractor.GetCsv(CancellationToken.None);
    }

    [Benchmark]
    public CurrencyRate[] Convenience()
    {
        return CurrencyRatesExtractor.GetRateConvenience(_csv, _date);
    }
    
    [Benchmark]
    public CurrencyRate[] Performance()
    {
        return CurrencyRatesExtractor.GetRatePerformance(_csv, _date);
    }
}