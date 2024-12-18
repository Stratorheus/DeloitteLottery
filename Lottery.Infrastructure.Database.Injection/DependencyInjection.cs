﻿using Lottery.Domain.Abstract.Infrastructure.Database;
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
