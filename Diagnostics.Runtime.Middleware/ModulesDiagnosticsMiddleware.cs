using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Diagnostics.Runtime;

namespace Diagnostics.Runtime.Middleware
{
    internal class ModulesDiagnosticsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDataTargetProvider _dataTargetProvider;

        public ModulesDiagnosticsMiddleware(RequestDelegate next, IDataTargetProvider dataTargetProvider)
        {
            _next = next;
            _dataTargetProvider = dataTargetProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            ClrInfo runtimeInfo = _dataTargetProvider.GetDataTarget().ClrVersions[0];
            ClrRuntime runtime = runtimeInfo.CreateRuntime();
            var content = TableBuilder.CreateDataTable("Modules", runtime.Modules.Select(f => new
            {
                AssemblyId = f.AssemblyId,
                AssemblyName = f.AssemblyName,
                DebuggingMode = f.DebuggingMode,
                FileName = f.FileName,
                ImageBase = f.ImageBase,
                MetadataAddress = f.MetadataAddress,
                Name = f.Name,
                Runtime = f.Runtime,
            }));

            await _next(context);
            await context.Response.WriteAsync(content);
        }
    }
}
