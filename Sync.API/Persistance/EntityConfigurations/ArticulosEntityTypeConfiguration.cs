using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sync.API.Persistance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Sync.API.Persistance.EntityConfigurations
{
    public class ArticulosEntityTypeConfiguration : IEntityTypeConfiguration<Articulos>
    {
        public void Configure(EntityTypeBuilder<Articulos> configuration)
        {
            configuration.ToTable("Articulos", SyncContext.DEFAULT_SCHEMA);
            configuration.HasKey(b => b.Id)
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
            configuration.Property(m => m.OrdenId)
                .IsRequired(true);
            configuration.Property(m => m.SKU)
                .IsRequired(true)
                .HasMaxLength(1000);
            configuration.Property(m => m.Precio)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
            configuration.Property(m => m.Cantidad)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
            configuration.Property(m => m.SubTotal)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
            configuration.Property(m => m.TotalImpuestos)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
            configuration.Property(m => m.Total)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
            configuration.Property(m => m.Nombre)
                .IsRequired(true)
                .HasMaxLength(1000);

            configuration.HasOne<Orden>()
                    .WithMany()
                    .HasForeignKey("OrdenId")
                    .HasConstraintName("FK_Articulos_Orden")
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
