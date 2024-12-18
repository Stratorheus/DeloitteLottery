using Lottery.Application.Configuration.Options;
using Lottery.Application.Services;
using Lottery.Domain.Abstract.Application;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lottery.Application.Injection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<GeneratorOptions>(config.GetSection(GeneratorOptions.ConfigSection));
            services.AddMemoryCache( );
            services.AddScoped<INumberService, NumberService>( );
            services.AddScoped<ILotteryService, LotteryService>( );
        }
    }
}
