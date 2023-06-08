﻿

using Inventario.Models.Dominio.Productos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Inventario.Models.Dominio.Ventas;
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
        public DbSet<VentaDetalle> Ventas { get; set; }
        public DbSet<VentaDetalle> VentaDetalles { get; set; }
        public DbSet<AperturaDeCaja> AperturasDeCajas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);


            builder.Entity<Inventarios>()
              .HasMany(e => e.Ajustes)
              .WithOne(e => e.Inventarios)
              .HasForeignKey(e => e.Id_Inventario);


        }
    }
}