### About Performance and Convenience approaches to manipulate with data.

### Result on Fedora Linux
BenchmarkDotNet v0.14.0, Fedora Linux 41 (KDE Plasma)
AMD Ryzen 7 6800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.110
[Host]     : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
DefaultJob : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2


| Method      | Date      |     Mean |    Error |   StdDev | Ratio | RatioSD | Gen0    | Gen1    | Allocated | Alloc Ratio |
|------------ |---------- |---------:|---------:|---------:|------:|--------:|--------:|--------:|----------:|------------:|
| Convenience | 1/3/2024  |  47.40us |  0.775us |  0.725us |  1.00 |    0.02 | 25.5737 | 10.8032 |  209.24KB |        1.00 |
| Performance | 1/3/2024  |  22.52us |  0.083us |  0.070us |  0.48 |    0.01 |  1.7090 |       - |   14.01KB |        0.07 |
|             |           |          |          |          |       |         |         |         |           |             |
| Convenience | 3/15/2024 |  50.66us |  0.864us |  0.766us |  1.00 |    0.02 | 25.8789 | 10.4370 |  211.63KB |        1.00 |
| Performance | 3/15/2024 |  28.56us |  0.140us |  0.131us |  0.56 |    0.01 |  1.9836 |       - |    16.4KB |        0.08 |
|             |           |          |          |          |       |         |         |         |           |             |
| Convenience | 11/5/2024 |  60.01us |  1.144us |  1.447us |  1.00 |    0.03 | 26.7944 | 11.8408 |  219.19KB |        1.00 |
| Performance | 11/5/2024 |  38.62us |  0.226us |  0.189us |  0.64 |    0.02 |  2.9297 |       - |   23.95KB |        0.11 |
