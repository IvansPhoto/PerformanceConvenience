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

        return rows
            .First(s => s.StartsWith("Title"))
            .Split(',')
            .Select((row, i) => (Code: row.StartsWith("A$1=") ? row.Substring(4, 3) : string.Empty, Index: i))
            .Where(codes => codes.Code is not "")
            .ToArray()
            .Join(
                inner: rows
                    .First(s => s.StartsWith(date.ToString("dd-MMM-yyyy")))
                    .Split(',')
                    .Select((rateStr, index) => (Rate: decimal.TryParse(rateStr, out var rate1) ? rate1 : -1, Index: index))
                    .Where(rates => rates.Rate > 0)
                    .ToArray(), 
                outerKeySelector: code => code.Index, 
                innerKeySelector: rate => rate.Index, 
                resultSelector: (code, rate) => new CurrencyRate(code.Code, rate.Rate))
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

        for (var i = 0; i < codeRanges.Length; i++)
        {
            var rateRange = rateRanges[i];
            if (rateRange.End.Value - rateRange.Start.Value == 0)
                continue;
            
            var codeChars = codeLine[codeRanges[i]];
            if (codeChars.Contains("A$", StringComparison.InvariantCultureIgnoreCase))
                result.Add(new CurrencyRate(codeChars.Slice(4, 3).ToString(), decimal.Parse(rateLine[rateRange])));
        }

        return result.ToArray();
    }

    public static CurrencyRate[] Get1(string csv, DateOnly date)
    {
        Span<Range> rowRanges = stackalloc Range[5000];
        csv.AsSpan().Split(rowRanges, "\r\n");
        
        
        Span<Range> codeRanges = stackalloc Range[18];
        csv.AsSpan(rowRanges[1]).Split(codeRanges, ',', StringSplitOptions.TrimEntries);
        foreach (var codeRange in codeRanges)
        {
            var row = csv.AsSpan(codeRange);
            if (row.Contains("A$1=", StringComparison.InvariantCultureIgnoreCase))
            {
                
            }
        }
        
        foreach (var rowRange in rowRanges)
        {
            var row = csv.AsSpan(rowRange);
            if (row.Contains(date.ToString("dd-MMM-yyyy"), StringComparison.InvariantCultureIgnoreCase))
            {
                Span<Range> rateRanges = stackalloc Range[18];
                row.Split(rateRanges, ',', StringSplitOptions.TrimEntries);

                return
                [
                    new CurrencyRate(Currency.Codes.USD, decimal.Parse(row[rateRanges[1]])),
                    new CurrencyRate(Currency.Codes.CNY, decimal.Parse(row[rateRanges[3]]))
                ];
            }
        }

        return [];
    }
}

public sealed record CurrencyRate(string CurrencyCode, decimal Rate);

public static class Currency
{
    public static class Codes
    {
        public const string USD = "USD";
        public const string CNY = "USD";
    }
}