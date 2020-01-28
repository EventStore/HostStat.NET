# `HostStat.NET`

`HostStat.NET` is a library intended for use in [Event Store][es] server for collecting statistics from hosts. Since we
use performance counters on Windows, only Linux and macOS are supported by [`libhoststat`][hoststat]. The statistics 
that can be retrieved currently are the following:

- 1, 5 and 15 minute load averages
- Free memory
- Total memory

### Usage Example

```csharp
var stat = new HostStat();

//Get Free Memory
var freeMem = stat.GetFreeMemory();

//Get Total Memory
var totalMem = stat.GetTotalMemory();

var loadAverages = stat.GetLoadAverages();
Console.WriteLine($"Last Minute CPU Load Average {loadAverages.Average1m}");
Console.WriteLine($"Last 5 Minutes CPU Load Average {loadAverages.Average5m}");
Console.WriteLine($"Last 15 Minutes CPU Load Average {loadAverages.Average15m}");
```

[es]: https://github.com/EventStore/EventStore
[hoststat]: https://github.com/EventStore/libHostStat
