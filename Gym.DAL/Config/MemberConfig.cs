using Gym.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Gym.DAL.Config
{
    internal class MemberConfig:GymUserConfig<Member>,IEntityTypeConfiguration<Member>
    {
        public override void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(m => m.CreatedAt).HasColumnName("JoinDate").HasDefaultValueSql("GETDATE()");
            builder.HasOne(m => m.HealthRecord)
                .WithOne(h => h.Member)
                .HasForeignKey<HealthRecord>(h => h.MemberId);
        }

    }
}
