using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZxVentures.Backend.Model.Entities;

namespace ZxVentures.Backend.DAL.Context
{
    internal class PontoDeVendaConfiguration : IEntityTypeConfiguration<PontoDeVenda>
    {
        public void Configure(EntityTypeBuilder<PontoDeVenda> builder)
        {
            builder
                .Property(q => q.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(q => q.TradingName)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder
                .Property(q => q.OwnerName)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder
                .Property(q => q.Document)
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder
                .OwnsOne(q => q.Address, end =>
                {
                    end.Property(q => q.Type)
                        .HasColumnName("AddressType")
                        .HasColumnType("int")
                        .IsRequired();

                    end.Property(q => q.Coordinates)
                        .HasColumnName("AddressCoordinates")
                        .HasColumnType("nvarchar(max)")
                        .IsRequired();
                });

            builder
                .OwnsOne(q => q.CoverageArea, area =>
                {
                    area.Property(q => q.Coordinates)
                        .HasColumnName("CoverageArea")
                        .HasColumnType("nvarchar(max)")
                        .IsRequired();

                    area.Property(q => q.Type)
                        .HasColumnName("CoverageAreaType")
                        .HasColumnType("int")
                        .IsRequired();
                });
        }
    }
}
