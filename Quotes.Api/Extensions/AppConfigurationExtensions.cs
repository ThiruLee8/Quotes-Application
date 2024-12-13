using Microsoft.EntityFrameworkCore;
using Quotes.Data.DatabaseContext;
using Quotes.Data.Repositories.Implementation;
using Quotes.Data.Repositories.Interface;
using Quotes.Service.Implementations;
using Quotes.Service.Infrastructure.AutoMapper;
using Quotes.Service.Interfaces;

namespace Quotes.Api.Extensions
{
    public static class AppConfigurationExtensions
    {
        public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QuoteDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IServiceCollection AddInfrastructures(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(AppAutoMapperProfile));

            return services;
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<IQuoteService,QuoteService> ();


            #endregion

            #region Repo
            services.AddScoped<IQuoteRepo, QuoteRepo>();

            #endregion

            return services;
        }

        public static IServiceCollection ConfigureCutomCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(x =>
                {
                    x.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
                });
            });
            return services;
        }
    }
}
