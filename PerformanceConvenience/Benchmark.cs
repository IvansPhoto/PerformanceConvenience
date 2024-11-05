using BenchmarkDotNet.Attributes;

namespace PerformanceConvenience;

[MemoryDiagnoser]
public class Benchmark
{
    private string _csv = string.Empty;

    [ParamsSource(nameof(DateProvider))] 
    public DateOnly Date = default;

    public IEnumerable<DateOnly> DateProvider =>
    [
        new(2024, 01, 3),
        new(2024, 03, 15),
        new(2024, 11, 05)
    ];

    [GlobalSetup]
    public async Task Setup()
    {
        _csv = await CurrencyRatesExtractor.GetCsv(CancellationToken.None);
    }

    [Benchmark(Baseline = true)]
    public CurrencyRate[] Convenience()
    {
        return CurrencyRatesExtractor.GetRateConvenience(_csv, Date);
    }

    [Benchmark]
    public CurrencyRate[] Performance()
    {
        return CurrencyRatesExtractor.GetRatePerformance(_csv, Date);
    }
}