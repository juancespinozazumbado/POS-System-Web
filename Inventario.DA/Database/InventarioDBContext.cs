

using Inventario.Models.Dominio.Productos;
using Inventario.Models.Dominio.Usuarios;
using Inventario.Models.Dominio.Ventas;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


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
        public DbSet<AperturaDeCaja> AperturasDeCaja { get; set; }

        public DbSet<AplicationUser> Usuarios { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

           builder.Ignore<IdentityUser>();

            builder.Entity<AplicationUser>().ToTable("AspNetUsers");

            builder.Entity<Inventarios>()
              .HasMany(e => e.Ajustes)
              .WithOne(e => e.Inventarios)
              .HasForeignKey(e => e.Id_Inventario);

            builder.Entity<AperturaDeCaja>()

             .HasMany(a => a.Ventas)
             .WithOne(v => v.AperturaDeCaja)
             .HasForeignKey(v => v.IdAperturaDeCaja);

            builder.Entity<Venta>()
                .HasMany(v => v.VentaDetalles)
                .WithOne(v => v.Venta)
                .HasForeignKey(v => v.Id_venta);

            builder.Entity<VentaDetalle>().HasOne(v => v.Inventarios).
                WithMany().HasForeignKey(v => v.Id_inventario).IsRequired();

        }

    }
}