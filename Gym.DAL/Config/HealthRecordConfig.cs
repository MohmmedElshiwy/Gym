using Gym.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


internal class HealthRecordConfig : IEntityTypeConfiguration<HealthRecord>
{
    public void Configure(EntityTypeBuilder<HealthRecord> builder)
    {
        builder.Property(h => h.BloodType).HasMaxLength(5);
        builder.Property(h=>h.Note).HasMaxLength(500);
    }
}



