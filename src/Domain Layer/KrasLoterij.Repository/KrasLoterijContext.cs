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
                Id = 1,
                Prize = 25000,
                UserId = null
            };
            lotteries.Add(maxPrizeLottery);

            for (int i = 2; i <= 10000; i++)
            {
                lotteries.Add(new Lottery()
                {
                    Id = i,
                    Prize = i <= 100 ? rand.Next(1000) : null,
                    UserId = null
                });
            }

            return lotteries;
        }

    }
}