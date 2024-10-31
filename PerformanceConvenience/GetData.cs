namespace PerformanceConvenience;

public class GetData
{
    public async Task<CurrencyRate[]> GetBankOfAustraliaRates(DateOnly date, CancellationToken cancellationToken)
    {
        var csv = await File.ReadAllTextAsync("", cancellationToken);

        var rows = csv.Split("\r\n");
        
        var ratesRow = rows
            .First(s => s.Contains(date.ToString("dd-MMM-yyyy")))
            .Split(',');

        var codeRow = rows[1]
            .Split(',');
        var result = codeRow
            .Select((row, i) => (Code: row.Contains("A$1=") ? row.Substring(4, 3) : null, Index: i))
            .Select(currencyIndex =>
            {
                var rateString = ratesRow[currencyIndex.Index];
                if (currencyIndex.Code is null || rateString is "")
                    return null;

                return new CurrencyRate(currencyIndex.Code!, decimal.Parse(rateString));
            })
            .Where(rate => rate is not null)
            .ToArray();

        return result!;
        
        CurrencyRate[] GetData()
        {
            Span<Range> rowRanges = stackalloc Range[5000];
            csv.AsSpan().Split(rowRanges, "\r\n");
            foreach (var rowRange in rowRanges)
            {
                var row = csv.AsSpan(rowRange);
                if (row.Contains(date.ToString("dd-MMM-yyyy"), StringComparison.CurrentCultureIgnoreCase))
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
        
        CurrencyRate[] GetData1()
        {
            // TODO: Use inline arrays.
            List<(char[] CurrencyCode, int Index)> currencies = [];
            foreach (var row in csv.AsSpan().EnumerateLines())
            {
                if (row.Contains("A$1=", StringComparison.InvariantCultureIgnoreCase))
                {
                    Span<Range> codeRanges = stackalloc Range[18];
                    row.Split(codeRanges, ',', StringSplitOptions.TrimEntries);

                    for (var i = 0; i < codeRanges.Length; i++)
                    {
                        var codeRange = codeRanges[i];
                        if (row[codeRange].Contains("A$1=", StringComparison.InvariantCultureIgnoreCase))
                        {
                            currencies.Add((CurrencyCode: row[codeRange].Slice(4, 3).ToArray(), Index: i));
                        }
                    }
                }
                
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
