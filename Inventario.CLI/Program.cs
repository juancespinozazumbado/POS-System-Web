
using Inventario.BL.Funcionalidades.Inventario;
using Inventario.BL.Funcionalidades.Inventario.Interfaces;
using Inventario.BL.Funcionalidades.Usuarios;
using Inventario.BL.Funcionalidades.Ventas;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Ventas;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

var contextOPtions = new DbContextOptionsBuilder<InventarioDBContext>()
    .UseSqlServer("Data Source=localhost;Initial Catalog=Comercio;User Id=sa; Password=$OdiN9%5;TrustServerCertificate=True")
    .Options;

RepositorioDeVentas ventas;
RepositorioDeAperturaDeCaja cajas;
ReporitorioDeInventarios inventarios;
RepositorioDeUsuarios Usuarios;


    using (var context = new InventarioDBContext(contextOPtions))
    {
        ventas = new(context);
        cajas = new(context);
        inventarios = new(context);
    Usuarios = new(context);

    MuestreLosUsuarios(Usuarios);

 


// test

//Venta venta = new Venta
//{
//    IdAperturaDeCaja = 1,
//    NombreCliente = "Juan",
//    TipoDePago = 2,
//    Fecha = DateTime.Now,
//    Total = 0,
//    PorcentajeDesCuento = 0,
//    MontoDescuento = 0,
//    SubTotal = 0,
//    UserId = "8d761c8c-5400-436b-a932-a5e4cf700e5b",
//    Estado = EstadoVenta.EnProceso
//};

//ventas.CreeUnaVenta(venta);
//var inventario = inventarios.ObetenerInevtarioPorId(2005);
//ventas.AñadaUnDetalleAlaVenta(1016, new VentaDetalle
//{
//    Cantidad = 2,
//    Inventarios = inventario,
//    Precio = inventario.Precio,
//    Monto = inventario.Precio * 2,
//    MontoDescuento = 400


//});

//ventas.TermineLaVenta(1016);





//foreach (var v in cajas.ListarAperturasDeCaja())
//{
//    Console.WriteLine(v.Id + " " + v.estado + v.Ventas.Count());
//}

//foreach (var v in ventas.ListeLasVentas())
//{
//    Console.WriteLine(v.Id + " " + v.Fecha + " " + v.VentaDetalles.Count());
//}

muestreLasVentas(ventas);


    }

     static void muestreLasVentas(RepositorioDeVentas ventas)
    {
        foreach (var v in ventas.ListeLasVentas())
        {
            Console.WriteLine($"Venta" + v.Id + " " + v.IdAperturaDeCaja
                +"");
        }

    }
 static void creeUnaAperturaDeCaja()
{

}

static void MuestreLosUsuarios(RepositorioDeUsuarios userss)
{
    var usuarios = userss.ListeLosUsuarios();

    foreach (var u in usuarios)
    {
        Console.WriteLine(u.Id + "  " + u.Email + " " + u.UserName);
       
    }
}


