using System.Globalization;

namespace PerformanceConvenience;

public static class CurrencyRatesExtractor
{
    public static async Task<string> GetCsv(CancellationToken cancellationToken)
    {
        // The CSV file is available on The Reserve Bank of Australia site https://www.rba.gov.au/statistics/tables/csv/f11.1-data.csv?v=2024-03-15";
        return await File.ReadAllTextAsync($"{Directory.GetCurrentDirectory()}/f11.1-data.csv", cancellationToken);
    }

    public static CurrencyRate[] GetRateConvenience(string csv, DateOnly date)
    {
        var rows = csv.Split("\r\n");

        var rates = rows
            .First(s => s.StartsWith(date.ToString("dd-MMM-yyyy")))
            .Split(',')
            .Select((rateStr, index) =>
            {
                var isDecimal = decimal.TryParse(rateStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var rate);
                return (Rate: isDecimal ? rate : -1, Index: index);
            })
            .Where(rates => rates.Rate > 0)
            .ToArray();
        
        var codes = rows
            .First(s => s.StartsWith("Title"))
            .Split(',')
            .Select((row, i) => (Code: row.StartsWith("A$1=") ? row.Substring(4, 3) : string.Empty, Index: i))
            .Where(codes => codes.Code is not "")
            .ToArray();
        
        return codes
            .Join(rates, code => code.Index, rate => rate.Index, (code, rate) => new CurrencyRate(code.Code, rate.Rate))
            .ToArray();
    }

    public static CurrencyRate[] GetRatePerformance(string csv, DateOnly date)
    {
        const int currencyNumber = 25;
        var result = new List<CurrencyRate>(currencyNumber);
        
        Span<Range> codeRanges = stackalloc Range[currencyNumber];
        ReadOnlySpan<char> codeLine = default;
        
        Span<Range> rateRanges = stackalloc Range[currencyNumber];
        ReadOnlySpan<char> rateLine = default;
        
        foreach (var line in csv.AsSpan().EnumerateLines())
        {
            if (line.StartsWith("Title", StringComparison.InvariantCultureIgnoreCase))
            {
                codeLine = line;
                line.Split(codeRanges, ',', StringSplitOptions.TrimEntries);
            }

            if (line.StartsWith(date.ToString("dd-MMM-yyyy"), StringComparison.InvariantCultureIgnoreCase))
            {
                rateLine = line;
                line.Split(rateRanges, ',', StringSplitOptions.TrimEntries);
                break;
            }
        }

        for (var i = 0; i < codeRanges.Length || i < rateRanges.Length; i++)
        {
            var rateRange = rateRanges[i];
            if (rateRange.End.Value - rateRange.Start.Value == 0)
                continue;
            
            var codeChars = codeLine[codeRanges[i]];
            if (codeChars.StartsWith("A$", StringComparison.InvariantCultureIgnoreCase))
                result.Add(new CurrencyRate(codeChars.Slice(4, 3).ToString(), decimal.Parse(rateLine[rateRange], NumberStyles.Any, CultureInfo.InvariantCulture)));
        }

        return result.ToArray();
    }
}
public sealed record CurrencyRate(string CurrencyCode, decimal Rate);

