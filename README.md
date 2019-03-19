# Reproduction of issue with string arrays in EventSource.Write

This issue is reported here: https://github.com/dotnet/corefx/issues/36149.

With NuGet package Microsoft.NETCore.UniversalWindowsPlatform version 6.2.8, we get the following exception:

```
    System.NotSupportedException: Arrays of null-terminated string are not supported
```

when we run this code:

```csharp
    var log = new EventSource("Observito-Test-Tack");
    log.Write("Msg", new EventSourceOptions() { Level = EventLevel.Informational, Opcode = EventOpcode.Info },
        new
        {
            Strings = new[]
                {
                    "Foo",
                    "Bar"
                }
        });
```

With NuGet package Microsoft.NETCore.UniversalWindowsPlatform version 6.1.9 we did not get this exception.

The problem also only appears when someone is listening for events of this EventSource. 

## Steps to reproduce

1. Set breakpoint in exception block of ```ArrayRepro.Reproduce```
1. Run App1 (Local Machine)
1. Observe that no exception is thrown
1. Run the following from a command-line as administrator (The Guid corresponding to 
     ```Observito-Test-Tack``` is ```{53487b38-863e-5b17-bdce-7b706a2e81a6}```):
```logman start test_trace -p {53487b38-863e-5b17-bdce-7b706a2e81a6} -ets```  
1. Run App1 again
1. Notice that an exception is thrown
1. Downgrade NuGet package Microsoft.NETCore.UniversalWindowsPlatform to version 6.1.9
1. Restart computer
1. Repeat the test above
1. Notice that now no exception is thrown
