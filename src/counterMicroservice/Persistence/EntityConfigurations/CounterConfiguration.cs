using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CounterConfiguration : IEntityTypeConfiguration<Counter>
{
    public void Configure(EntityTypeBuilder<Counter> builder)
    {
        builder.ToTable("Counters").HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
        builder.Property(c => c.SerialNumber).HasColumnName("SerialNumber");
        builder.Property(c => c.MeasurementTime).HasColumnName("MeasurementTime");
        builder.Property(c => c.LatestIndexInfo).HasColumnName("LatestIndexInfo");
        builder.Property(c => c.VoltageDuringMeasurement).HasColumnName("VoltageDuringMeasurement");
        builder.Property(c => c.CurrentDuringMeasurement).HasColumnName("CurrentDuringMeasurement");
        builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(c => c.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(c => !c.DeletedDate.HasValue);
    }
}