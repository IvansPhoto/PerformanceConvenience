### About Performance and Convenience approaches to manipulate with data.

### Result on Fedora Linux
BenchmarkDotNet v0.14.0, Fedora Linux 41 (KDE Plasma)
AMD Ryzen 7 6800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100-rc.2.24474.1
[Host]     : .NET 9.0.0 (9.0.24.47305), X64 RyuJIT AVX2
DefaultJob : .NET 9.0.0 (9.0.24.47305), X64 RyuJIT AVX2


| Method      | Date      | Mean     |    Error | StdDev   | Ratio | Gen0    | Gen1    | Allocated | Alloc Ratio |
|------------ |---------- |---------:|---------:|---------:|------:|--------:|--------:|----------:|------------:|
| Convenience | 1/3/2024  | 46.61us |  0.454us | 0.379us |  1.00 | 25.3296 | 11.3525 |  207.05KB |        1.00 |
| Performance | 1/3/2024  | 23.45us |  0.071us | 0.063us |  0.50 |  1.7090 |       - |   14.01KB |        0.07 |
|             |           |          |          |          |       |         |         |           |             |
| Convenience | 3/15/2024 | 47.70us |  0.458us | 0.383us |  1.00 | 25.6348 | 12.7563 |  209.44KB |        1.00 |
| Performance | 3/15/2024 | 26.22us |  0.160us | 0.149us |  0.55 |  1.9836 |       - |    16.4KB |        0.08 |
|             |           |          |          |          |       |         |         |           |             |
| Convenience | 11/5/2024 | 60.30us |  0.229us | 0.214us |  1.00 | 26.5503 |  9.4604 |  216.99KB |        1.00 |
| Performance | 11/5/2024 | 40.88us |  0.257us | 0.240us |  0.68 |  2.9297 |       - |   23.95KB |        0.11 |

### Result on Windows 11
BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 6800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100
[Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2


| Method       | Date       | Mean     | Error    | StdDev   | Ratio | RatioSD | Gen0    | Gen1    | Allocated | Alloc Ratio |
|------------- |----------- |---------:|---------:|---------:|------:|--------:|--------:|--------:|----------:|------------:|
| Convenience  | 2024-01-03 | 41.47us | 0.828us | 0.986us |  1.00 |    0.03 | 25.3296 | 11.3525 |  207.05KB |        1.00 |
| Performance  | 2024-01-03 | 23.16us | 0.445us | 0.417us |  0.56 |    0.02 |  1.7090 |       - |   14.01KB |        0.07 |
|              |            |          |          |          |       |         |         |         |           |             |
| Convenience  | 2024-03-15 | 46.08us | 0.335us | 0.279us |  1.00 |    0.01 | 25.6348 | 12.7563 |  209.44KB |        1.00 |
| Performance  | 2024-03-15 | 26.74us | 0.159us | 0.124us |  0.58 |    0.00 |  1.9836 |       - |    16.4KB |        0.08 |
|              |            |          |          |          |       |         |         |         |           |             |
| Convenience  | 2024-11-05 | 54.38us | 0.531us | 0.471us |  1.00 |    0.01 | 26.5503 |  9.4604 |  216.99KB |        1.00 |
| Performance  | 2024-11-05 | 39.29us | 0.203us | 0.190us |  0.72 |    0.01 |  2.9297 |       - |   23.95KB |        0.11 |
