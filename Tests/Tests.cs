using PerformanceConvenience;

namespace Tests;

public class Tests
{
    private readonly DateOnly _date = new(2024, 03, 15);
    private readonly CancellationTokenSource _cts = new(TimeSpan.FromMinutes(1));
    private string _csv;

    private readonly CurrencyRate[] _expected =
    [
        new("USD", new decimal(0.6567)),
        new("CNY", new decimal(4.7259)),
        new("JPY", new decimal(97.40)),
        new("EUR", new decimal(0.6038)),
        new("KRW", new decimal(873.49)),
        new("GBP", new decimal(0.5155)),
        new("SGD", new decimal(0.8783)),
        new("INR", new decimal(54.45)),
        new("THB", new decimal(23.52)),
        new("NZD", new decimal(1.0760)),
        new("TWD", new decimal(20.79)),
        new("MYR", new decimal(3.0881)),
        new("IDR", new decimal(10262)),
        new("VND", new decimal(16243)),
        new("HKD", new decimal(5.1372)),
        new("CAD", new decimal(0.8890)),
        new("PHP", new decimal(36.50)),
        new("SDR", new decimal(0.4921)),
    ];

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _csv = await CurrencyRatesExtractor.GetCsv(CancellationToken.None);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _cts.Dispose();
    }
    
    [Test]
    public void Convenience()
    {
        var actual = CurrencyRatesExtractor.GetRateConvenience(_csv, _date);
        Assert.That(actual, Is.Not.Empty);
        Assert.That(actual, Is.EqualTo(_expected).AsCollection);
    }
    
    [Test]
    public void Performance()
    {
        var actual = CurrencyRatesExtractor.GetRatePerformance(_csv, _date);
        Assert.That(actual, Is.Not.Empty);
        Assert.That(actual, Is.EqualTo(_expected).AsCollection);
    }    
}