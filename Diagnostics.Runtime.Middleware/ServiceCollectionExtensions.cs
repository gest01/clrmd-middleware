using Diagnostics.Runtime.Middleware.MemoryDumps;
using Microsoft.Extensions.DependencyInjection;

namespace Diagnostics.Runtime.Middleware
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClrMd(this IServiceCollection services)
        {
            services.AddTransient<IDataTargetProvider, MicrosoftDiagnosticsRuntimeDataTargetProvider>();

            if (PlatformServices.IsWindows)
            {
                services.AddTransient<IMemoryDumper, WindowsMemoryDumper>();
            }

            else if (PlatformServices.IsLinux)
            {
                services.AddTransient<IMemoryDumper, LinuxMemoryDumper>();
            }


            return services;
        }
    }
}
