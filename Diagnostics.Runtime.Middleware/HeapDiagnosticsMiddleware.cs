using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Diagnostics.Runtime;

namespace Diagnostics.Runtime.Middleware
{
    internal class HeapDiagnosticsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDataTargetProvider _dataTargetProvider;

        public HeapDiagnosticsMiddleware(RequestDelegate next, IDataTargetProvider dataTargetProvider)
        {
            _next = next;
            _dataTargetProvider = dataTargetProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            ClrInfo runtimeInfo = _dataTargetProvider.GetDataTarget().ClrVersions[0];
            ClrRuntime runtime = runtimeInfo.CreateRuntime();
            var stats = from o in runtime.Heap.EnumerateObjects()
                        let t = o.Type
                        group o by t into g
                        let size = g.Sum(o => (uint)o.Size)
                        select new
                        {
                            Name = g.Key.Name,
                            Size = size,
                            Count = g.Count()
                        };

            var content = TableBuilder.CreateDataTable("Heap", stats.OrderByDescending(f => f.Size).Select(f => new
            {
                Size = f.Size,
                Count = f.Count.ToString(),
                Name = f.Name
            }));

            await _next(context);
            await context.Response.WriteAsync(content);
        }
    }
}
