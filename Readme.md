### About Performance and Convenience approaches to manipulate with data.

### Result on Fedora Linux
BenchmarkDotNet v0.14.0, Fedora Linux 41 (KDE Plasma)
AMD Ryzen 7 6800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100-rc.2.24474.1
[Host]     : .NET 9.0.0 (9.0.24.47305), X64 RyuJIT AVX2
DefaultJob : .NET 9.0.0 (9.0.24.47305), X64 RyuJIT AVX2


| Method      | Date      | Mean     | Error    | StdDev   | Ratio | Gen0    | Gen1    | Allocated | Alloc Ratio |
|------------ |---------- |---------:|---------:|---------:|------:|--------:|--------:|----------:|------------:|
| Convenience | 1/3/2024  | 46.61 us | 0.454 us | 0.379 us |  1.00 | 25.3296 | 11.3525 | 207.05 KB |        1.00 |
| Performance | 1/3/2024  | 23.45 us | 0.071 us | 0.063 us |  0.50 |  1.7090 |       - |  14.01 KB |        0.07 |
|             |           |          |          |          |       |         |         |           |             |
| Convenience | 3/15/2024 | 47.70 us | 0.458 us | 0.383 us |  1.00 | 25.6348 | 12.7563 | 209.44 KB |        1.00 |
| Performance | 3/15/2024 | 26.22 us | 0.160 us | 0.149 us |  0.55 |  1.9836 |       - |   16.4 KB |        0.08 |
|             |           |          |          |          |       |         |         |           |             |
| Convenience | 11/5/2024 | 60.30 us | 0.229 us | 0.214 us |  1.00 | 26.5503 |  9.4604 | 216.99 KB |        1.00 |
| Performance | 11/5/2024 | 40.88 us | 0.257 us | 0.240 us |  0.68 |  2.9297 |       - |  23.95 KB |        0.11 |

### Result on Windows 11
BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 6800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100
[Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2


| Method       | Date       | Mean     | Error    | StdDev   | Ratio | RatioSD | Gen0    | Gen1    | Allocated | Alloc Ratio |
|------------- |----------- |---------:|---------:|---------:|------:|--------:|--------:|--------:|----------:|------------:|
| Convenience  | 2024-01-03 | 41.47 us | 0.828 us | 0.986 us |  1.00 |    0.03 | 25.3296 | 11.3525 | 207.05 KB |        1.00 |
| Performance  | 2024-01-03 | 23.16 us | 0.445 us | 0.417 us |  0.56 |    0.02 |  1.7090 |       - |  14.01 KB |        0.07 |
|              |            |          |          |          |       |         |         |         |           |             |
| Convenience  | 2024-03-15 | 46.08 us | 0.335 us | 0.279 us |  1.00 |    0.01 | 25.6348 | 12.7563 | 209.44 KB |        1.00 |
| Performance  | 2024-03-15 | 26.74 us | 0.159 us | 0.124 us |  0.58 |    0.00 |  1.9836 |       - |   16.4 KB |        0.08 |
|              |            |          |          |          |       |         |         |         |           |             |
| Convenience  | 2024-11-05 | 54.38 us | 0.531 us | 0.471 us |  1.00 |    0.01 | 26.5503 |  9.4604 | 216.99 KB |        1.00 |
| Performance  | 2024-11-05 | 39.29 us | 0.203 us | 0.190 us |  0.72 |    0.01 |  2.9297 |       - |  23.95 KB |        0.11 |
