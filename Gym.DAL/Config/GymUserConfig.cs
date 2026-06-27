using Gym.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Gym.DAL.Config
{
    public class GymUserConfig<T> : IEntityTypeConfiguration <T> where T :GymUser
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p=>p.Name).HasMaxLength(100);
            builder.Property(p => p.Email).HasMaxLength(150);
            builder.Property(p => p.Phone).IsUnicode().HasMaxLength(11);
            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_GymUser_Phone", "[Phone] LIKE '010%' or [Phone] LIKE '011%' or [Phone] LIKE '012%' or [Phone] LIKE '015%' or [Phone] LIKE '011%' ");
                t.HasCheckConstraint("CK_GymUser_Email", "[Email] LIKE '_%@_%._%'");
            }
            );
           

        }
    }
}
