using Sync.Infraestructure.Persistance.EntityConfigurations;
using Sync.Infraestructure.Persistance.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sync.Infraestructure.Persistance
{
    public class SyncContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";
        public DbSet<ColaArticulos> ColaArticulo { get; set; }
        public DbSet<Articulos> Articulo { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Orden> Orden { get; set; }

        public SyncContext(DbContextOptions<SyncContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ColaArticulosEntityTypeConfiguration());
            builder.ApplyConfiguration(new OrdenEntityTypeConfiguration());
            builder.ApplyConfiguration(new ArticulosEntityTypeConfiguration());
            builder.ApplyConfiguration(new ClienteEntityTypeConfiguration());
        }

    }
}
