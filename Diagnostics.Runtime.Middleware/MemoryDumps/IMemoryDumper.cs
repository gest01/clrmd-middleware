using System.Diagnostics;
using System.IO;

namespace Diagnostics.Runtime.Middleware.MemoryDumps
{
    internal interface IMemoryDumper
    {
        Stream CreateMemoryDump(Process process);
    }
}
