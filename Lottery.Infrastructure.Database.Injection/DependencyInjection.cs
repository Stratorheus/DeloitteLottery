using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lottery.Domain.Abstract.Infrastructure.Database;
using Lottery.Domain.Configuration;
using Lottery.Infrastructure.Database.Ef.Context;
using Lottery.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lottery.Infrastructure.Database.Injection
{
    public static class DependencyInjection
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<LotteryDbContext>(opt => opt.UseSqlServer(config.GetConnectionString(ConfigKeys.DbConnectionString)));
            services.AddScoped<IDrawHistoryRepository, DrawHistoryRepository>( );
        }
    }
}
