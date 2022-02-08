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
    public class OrdenEntityTypeConfiguration : IEntityTypeConfiguration<Orden>
    {
        public void Configure(EntityTypeBuilder<Orden> configuration)
        {
            configuration.ToTable("Orden", SyncContext.DEFAULT_SCHEMA);
            configuration.HasKey(b => b.OrdenId)
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
            configuration.Property(m => m.NumeroOrden)
                .IsRequired(true);
            configuration.Property(m => m.SubTotal)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
            configuration.Property(m => m.TotalImpuestos)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
            configuration.Property(m => m.Total)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
            configuration.Property<DateTime>(m => m.FechaCreacion)
                .IsRequired(true);
            configuration.Property<DateTime>(m => m.FechaOrden)
                .IsRequired(true);
        }
    }
}
