using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Sync.Infraestructure.Persistance.Models;

namespace Sync.Infraestructure.Persistance.EntityConfigurations
{
    public class ColaArticulosEntityTypeConfiguration : IEntityTypeConfiguration<ColaArticulos>
    {
        public void Configure(EntityTypeBuilder<ColaArticulos> configuration)
        {
            configuration.ToTable("ColaArticulos", SyncContext.DEFAULT_SCHEMA);
            configuration.HasKey(b => b.Id)
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
            configuration.Property(m => m.SKU)
                .IsRequired(true)
                .HasMaxLength(1000);
            configuration.Property(m => m.Cantidad)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
            configuration.Property<DateTime>(m => m.FechaActualizacion)
                .IsRequired(true);
            configuration.Property<DateTime>(m => m.FechaRegistro)
                .IsRequired(true);
            configuration.Property(m => m.Sincronizado)
                .IsRequired(true);
        }
    }
}
