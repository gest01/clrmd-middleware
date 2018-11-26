using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Diagnostics.Runtime.Middleware.MemoryDumps
{
    internal class MemoryDumpMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryDumper _memoryDumper;

        public MemoryDumpMiddleware(RequestDelegate next, IMemoryDumper memoryDumper)
        {
            _next = next;
            _memoryDumper = memoryDumper;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var process = Process.GetCurrentProcess();
            using (var dump = await _memoryDumper.CreateMemoryDumpAsync(process))
            {
                string filename = $"{process.ProcessName}-{process.Id}.dmp";

                context.Response.Headers.Add("Content-Disposition", new Microsoft.Extensions.Primitives.StringValues($"attachment; filename='{filename}'"));
                await dump.CopyToAsync(context.Response.Body);
            }
        }
    }
}
