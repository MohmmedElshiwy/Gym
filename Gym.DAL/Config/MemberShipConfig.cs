
using Gym.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gym.DAL.Config
{
    internal class MemberShipConfig:IEntityTypeConfiguration<MemberShip>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MemberShip> builder)
        {
            builder.Property(ms => ms.CreatedAt).HasColumnName("StartDate").HasDefaultValueSql("getdate()");
            builder.HasOne(ms=>ms.Plan)
                .WithMany(p => p.MemberShips)
                .HasForeignKey(ms => ms.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m=> m.Member)
                .WithMany(m => m.MemberShips)
                .HasForeignKey(ms => ms.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
        } 
    }
}
