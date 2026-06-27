using Gym.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Gym.DAL.Config
{
    public class PlanConfig : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
            builder.Property(p => p.Name).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(p => p.Description).HasColumnType("varchar").HasMaxLength(200);
            builder.Property(p => p.Price).HasPrecision(10, 2);
            builder.ToTable(tb => tb.HasCheckConstraint("CK_Plan_DurationDays", "DurationDays between 1 and 365"));
        }
    }
}
