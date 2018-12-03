using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Diagnostics.Runtime;
using Microsoft.Extensions.Primitives;

namespace Diagnostics.Runtime.Middleware
{
    internal class StacksDiagnosticsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDataTargetProvider _dataTargetProvider;

        public StacksDiagnosticsMiddleware(RequestDelegate next, IDataTargetProvider dataTargetProvider)
        {
            _next = next;
            _dataTargetProvider = dataTargetProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            ClrInfo runtimeInfo = _dataTargetProvider.GetDataTarget().ClrVersions[0];
            ClrRuntime runtime = runtimeInfo.CreateRuntime();

            if (context.Request.Query.TryGetValue("id", out StringValues value))
            {
                var threadId = value.ToString();
                ClrThread thread = runtime.Threads.FirstOrDefault(f => f.OSThreadId.ToString() == threadId);
                if (thread != null)
                {
                    string content = TableBuilder.CreateDataTable($"Stack for Thread {threadId}", thread.StackTrace.Select(f => new
                    {
                        InstructionPointer = TableColumn.Wrap(f.InstructionPointer).Format("{0,12:X}"),
                        StackPointer = TableColumn.Wrap(f.StackPointer).Format("{0,12:X}"),
                        DisplayString = f.DisplayString,
                        Method = f.Method,
                        ModuleName = f.ModuleName,
                    }));

                    await context.Response.WriteAsync(content);
                }
            }
        }
    }
}
