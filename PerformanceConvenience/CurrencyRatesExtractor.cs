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

        var ratesRow = rows
            .First(s => s.Contains(date.ToString("dd-MMM-yyyy")))
            .Split(',');

        return rows
            .First(s => s.Contains("A$"))
            .Split(',')
            .Select((row, i) => (Code: row.Contains("A$") ? row.Substring(4, 3) : null, Index: i))
            .Select(currencyIndex =>
            {
                var rateString = ratesRow[currencyIndex.Index];
                if (currencyIndex.Code is null || rateString is "")
                    return null;

                return new CurrencyRate(currencyIndex.Code!, decimal.Parse(rateString));
            })
            .Where(rate => rate is not null)
            .ToArray()!;
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
            if (line.Contains("A$", StringComparison.InvariantCultureIgnoreCase))
            {
                codeLine = line;
                line.Split(codeRanges, ',', StringSplitOptions.TrimEntries);
            }

            if (line.Contains(date.ToString("dd-MMM-yyyy"), StringComparison.InvariantCultureIgnoreCase))
            {
                rateLine = line;
                line.Split(rateRanges, ',', StringSplitOptions.TrimEntries);
            }
        }

        for (var i = 0; i < codeRanges.Length; i++)
        {
            var codeChars = codeLine[codeRanges[i]];
            var rateChars = rateLine[rateRanges[i]];

            if (codeChars.Contains("A$", StringComparison.InvariantCultureIgnoreCase) && decimal.TryParse(rateChars, out var rate))
                result.Add(new CurrencyRate(codeChars.Slice(4, 3).ToString(), rate));
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