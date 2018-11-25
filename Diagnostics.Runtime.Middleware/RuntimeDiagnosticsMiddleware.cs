using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Diagnostics.Runtime.Middleware
{
    internal class RuntimeDiagnosticsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDataTargetProvider _dataTargetProvider;

        public RuntimeDiagnosticsMiddleware(RequestDelegate next, IDataTargetProvider dataTargetProvider)
        {
            _next = next;
            _dataTargetProvider = dataTargetProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            var versions = _dataTargetProvider.GetDataTarget().ClrVersions;

            string content = TableBuilder.CreateDataTable("Clr Info", versions.Select(f => new {
                ClrVersion = f.Version.ToString(),
                Flavor = f.Flavor,
                LocalMatchingDac = f.LocalMatchingDac,
                DacInfoFileName = f.DacInfo.FileName,
                DacInfoFileSize = f.DacInfo.FileSize,
                Architecture = f.DacInfo.TargetArchitecture,
            }));

            await context.Response.WriteAsync(content);
        }
    }
}
