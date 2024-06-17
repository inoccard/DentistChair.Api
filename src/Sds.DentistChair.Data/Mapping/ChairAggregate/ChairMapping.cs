using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sds.DentistChair.Domain.Models.ChairAggregate.Entities;

namespace Sds.DentistChair.Data.Mapping.ChairAggregate;

public class ChairMapping : IEntityTypeConfiguration<Chair>
{
    public void Configure(EntityTypeBuilder<Chair> builder)
    {
        builder.ToTable("Chair");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnType("bigint")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Number);
        builder.Property(x => x.Description);
        builder.Property(x => x.AdditionalInfo);
    }
}

public class AllocationMapping : IEntityTypeConfiguration<Allocation>
{
    public void Configure(EntityTypeBuilder<Allocation> builder)
    {
        builder.ToTable("Allocation");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnType("bigint")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ChairId);
        builder.Property(x => x.StartTime);
        builder.Property(x => x.EndTime);

        builder.HasOne(x => x.Chair)
            .WithMany()
            .HasForeignKey(x => x.ChairId);
    }
}
