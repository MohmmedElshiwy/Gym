using Gym.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.DAL.Config
{
    internal class TranierConfig : GymUserConfig<Traniner>, IEntityTypeConfiguration<Traniner>
    {
        public override void Configure(EntityTypeBuilder<Traniner> builder)


        {
            builder.Property(t => t.CreatedAt).HasColumnName("HireDate").HasDefaultValueSql("getdate()");
        }
    }
}