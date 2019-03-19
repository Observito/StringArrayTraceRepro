using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Fails with:
//    System.NotSupportedException: Arrays of null-terminated string are not supported.
// when listening with:
//    logman start test_trace -p {53487b38-863e-5b17-bdce-7b706a2e81a6} -ets
// using NuGet package Microsoft.NETCore.UniversalWindowsPlatform 6.2.8.
// Downgrading to Microsoft.NETCore.UniversalWindowsPlatform 6.1.9 solves the problem

namespace App1
{
    public class ArrayRepro
    {
        public ArrayRepro()
        {
        }

        public void Reproduce()
        {
            try
            {
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
            }
            catch (Exception ex)
            { // set breakpoint here
                throw;
            }
        }
    }
}

