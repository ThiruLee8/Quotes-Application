using MudBlazor.Services;
using Quotes.UI.Service.HttpHandler;
using Quotes.UI.Service.Services.Implementation;
using Quotes.UI.Service.Services.Interface;

namespace Quotes.UI.Extensions
{
    public static class ExtensionServices
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IQuoteService,QuoteService> ();
            services.AddTransient<ApiRequestHandler>();


            services.AddMudServices();



            return services;
        }

        //public static IServiceCollection AddAppInfrastructure(this IServiceCollection services)
        //{
        //    services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5246/") });

        //    return services;
        //}
    }
}
