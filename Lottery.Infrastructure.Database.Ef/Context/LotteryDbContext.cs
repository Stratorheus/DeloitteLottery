using System;
using System.Collections.Generic;
using System.Linq;
using Lottery.Domain.Models.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lottery.Infrastructure.Database.Ef.Context
{
    public sealed class LotteryDbContext : DbContext
    {
        /// <summary>
        /// Draw logs storage
        /// </summary>
        /// <remarks>
        /// Ideally the DbSet/Table names should be plural of the entity name. Chose DrawHistory instead of DrawLogs to align with the assignment
        /// </remarks>
        public DbSet<DrawLog> DrawHistory { get; set; }
        public DbSet<DrawNumber> DrawNumbers { get; set; }
        public LotteryDbContext(DbContextOptions<LotteryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DrawNumber>( )
                .HasOne(dn => dn.DrawLog)
                .WithMany(dl => dl.DrawNumbers)
                .HasForeignKey(dn => dn.DrawLogId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
