using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NederlandseLoterij.KrasLoterij.Repository.Entity;

namespace NederlandseLoterij.KrasLoterij.Repository.EtConfiguration
{
    public class LotteryConfiguration : IEntityTypeConfiguration<Lottery>
    {
        public void Configure(EntityTypeBuilder<Lottery> modelBuilder)
        {
            // Key
            modelBuilder.HasKey(k => k.Id);

            modelBuilder.Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Property(p => p.UserId);

            modelBuilder.Property(p => p.Prize);
        }
    }
}