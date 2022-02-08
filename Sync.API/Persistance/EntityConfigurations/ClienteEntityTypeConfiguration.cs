using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Sync.API.Persistance.Models;

namespace Sync.API.Persistance.EntityConfigurations
{
    public class ClienteEntityTypeConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> configuration)
        {
            configuration.ToTable("Cliente", SyncContext.DEFAULT_SCHEMA);
            configuration.HasKey(b => b.Id)
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
            configuration.Property(m => m.OrdenId)
                .IsRequired(true);
            configuration.Property(m => m.Nombre)
                .IsRequired(true)
                .HasMaxLength(1000);
            configuration.Property(m => m.Apellido)
                .IsRequired(true)
                .HasMaxLength(1000);
            configuration.Property(m => m.Email)
                .IsRequired(true)
                .HasMaxLength(1000);
            configuration.Property(m => m.Telefono)
                .IsRequired(true)
                .HasMaxLength(1000);
            configuration.Property(m => m.Direccion)
                .IsRequired(true)
                .HasMaxLength(1000);

            configuration.HasOne<Orden>()
                    .WithMany()
                    .HasForeignKey("OrdenId")
                    .HasConstraintName("FK_Cliente_Orden")
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
