

using Inventario.Models.Dominio.Productos;
using Inventario.Models.Dominio.Ventas;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inventario.DA.Database
{
    public class InventarioDBContext : IdentityDbContext<IdentityUser>
    {

        public InventarioDBContext(DbContextOptions<InventarioDBContext> options)
            : base(options)
        {
        }
        public DbSet<AjusteDeInventario> AjusteDeInventarios { get; set; }
        public DbSet<Inventarios> Inventarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaDetalle> VentaDetalles { get; set; }
        public DbSet<AperturaDeCaja> AperturasDeCajas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);


            builder.Entity<Inventarios>()
              .HasMany(e => e.Ajustes)
              .WithOne(e => e.Inventarios)
              .HasForeignKey(e => e.Id_Inventario);

            builder.Entity<Venta>()
                .HasMany(v => v.VentaDetalles)
                .WithOne(v => v.Venta)
                .HasForeignKey(v => v.Id_venta);

            //builder.Entity<VentaDetalle>()
            //    .HasOne(v => v.Inventarios)
            //    .WithMany(i => i.VentasItems)
            //    .HasForeignKey(i => i.Id_inventario);

            /* builder.Entity<AperturaDeCaja>()
                 .HasMany(a => a.Ventas)
                 .WithOne(v => v.AperturaDeCaja)
                 .HasForeignKey(v => v.IdAperturaDeCaja);*/

        }
    }
}