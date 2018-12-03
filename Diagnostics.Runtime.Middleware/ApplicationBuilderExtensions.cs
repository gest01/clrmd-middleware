using Diagnostics.Runtime.Middleware.MemoryDumps;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Diagnostics.Runtime.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseClrMd(this IApplicationBuilder builder)
        {
            string basePath = "/diagnostics";

            builder.Map(new PathString($"{basePath}/stacks"), x => x.UseMiddleware<StacksDiagnosticsMiddleware>());
            builder.Map(new PathString($"{basePath}/runtime"), x => x.UseMiddleware<RuntimeDiagnosticsMiddleware>());
            builder.Map(new PathString($"{basePath}/heap"), x => x.UseMiddleware<HeapDiagnosticsMiddleware>());
            builder.Map(new PathString($"{basePath}/threads"), x => x.UseMiddleware<ThreadsDiagnosticsMiddleware>());
            builder.Map(new PathString($"{basePath}/modules"), x => x.UseMiddleware<ModulesDiagnosticsMiddleware>());

            if (PlatformServices.IsLinux || PlatformServices.IsWindows)
            {
                builder.Map(new PathString($"{basePath}/dump"), x => x.UseMiddleware<MemoryDumpMiddleware>());
            }


            builder.Map(new PathString(basePath), x =>
                 
                
                 x.UseMiddleware<HeapDiagnosticsMiddleware>()
                 .UseMiddleware<ThreadsDiagnosticsMiddleware>()
                 .UseMiddleware<StacksDiagnosticsMiddleware>()
                 .UseMiddleware<ModulesDiagnosticsMiddleware>()
                 .UseMiddleware<RuntimeDiagnosticsMiddleware>()
                 );

            return builder;
        }
    }
}
