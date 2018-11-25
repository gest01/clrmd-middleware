using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Diagnostics.Runtime.Middleware
{
    internal class StacksDiagnosticsMiddleware
    {
        private readonly RequestDelegate _next;

        public StacksDiagnosticsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
        }
    }
}
