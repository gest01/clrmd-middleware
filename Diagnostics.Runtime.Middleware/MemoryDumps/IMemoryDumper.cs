using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Diagnostics.Runtime.Middleware.MemoryDumps
{
    internal interface IMemoryDumper
    {
        Task<MemoryStream> CreateMemoryDumpAsync(Process process);
    }
}
