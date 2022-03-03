using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using NederlandseLoterij.KrasLoterij.Repository.Entity;
using NederlandseLoterij.KrasLoterij.Repository.EtConfiguration;

namespace NederlandseLoterij.KrasLoterij.Repository
{
    public class KrasLoterijContext : BaseContext
    {
        public KrasLoterijContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<Lottery> Lottery { get; set; }

        public DbSet<RequestLog> RequestLog { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LotteryConfiguration());

            modelBuilder.Entity<Lottery>().HasData(GenerateLotteries());
            base.OnModelCreating(modelBuilder);
        }

        private List<Lottery> GenerateLotteries()
        {
            var rand = new Random();
            var lotteries = new List<Lottery>();
            var maxPrizeLottery = new Lottery()
            {
                Prize = 25000,
                UserId = null
            };
            lotteries.Add(maxPrizeLottery);

            var maxLottery = 10000;
            for (int i = 2; i <= maxLottery; i++)
            {
                lotteries.Add(new Lottery()
                {
                    Prize = i <= 100 ? rand.Next(1000) : null,
                    UserId = null
                });
            }

            var randomLotteries = lotteries.OrderBy(item => rand.Next()).ToList();

            for (var i = 1; i <= maxLottery; i++)
            {
                randomLotteries[i-1].Id = i;
            }

            return randomLotteries;
        }

    }
}