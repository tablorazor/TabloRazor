using Microsoft.Extensions.DependencyInjection;
using TabloRazor.Components.Tables;
using TabloRazor.Services;

namespace TabloRazor
{
    public static class TablerExtensions
    {
        public static IServiceCollection AddTabler(this IServiceCollection services, Action<TablerOptions> tablerOptions = null)
        {
            if (tablerOptions is null)
            {
                tablerOptions = _ => {};
            }

            services.Configure(tablerOptions);
            
            return services
                .AddScoped<ToastService>()
                .AddScoped<TablerService>()
                .AddScoped<IModalService, ModalService>()
                .AddScoped<TableFilterService>()
                .AddScoped<IFormValidator, TablerDataAnnotationsValidator>()
             .AddSingleton<FlagService>();
        }
        
        public static TabloRazorBuilder AddTabloRazor(this IServiceCollection services, Action<TablerOptions> tablerOptions = null)
        {
            services
                .AddTabler(tablerOptions);

            return new TabloRazorBuilder(services);
        }
    }
}