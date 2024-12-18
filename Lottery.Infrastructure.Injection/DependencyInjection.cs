using Lottery.Infrastructure.Database.Injection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lottery.Infrastructure.Injection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDatabase(config);
        }
    }
}
