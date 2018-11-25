using Microsoft.Extensions.DependencyInjection;

namespace Diagnostics.Runtime.Middleware
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClrMd(this IServiceCollection services)
        {
            services.AddTransient<IDataTargetProvider, MicrosoftDiagnosticsRuntimeDataTargetProvider>();

            return services;
        }
    }
}
