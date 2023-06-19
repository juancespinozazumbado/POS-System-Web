
using Inventario.BL.Funcionalidades.Inventario;
using Inventario.BL.Funcionalidades.Inventario.Interfaces;
using Inventario.BL.Funcionalidades.Usuarios;
using Inventario.BL.Funcionalidades.Ventas;
using Inventario.BL.ServicioEmail;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Productos;
using Inventario.Models.Dominio.Usuarios;
using Inventario.Models.Dominio.Ventas;
using Microsoft.EntityFrameworkCore;


var contextOPtions = new DbContextOptionsBuilder<InventarioDBContext>()
    .UseSqlServer("Data Source=M2280220322\\SQLEXPRESS;Initial Catalog=COMERCIO;Integrated Security=True; TrustServerCertificate=True")
    .Options;

RepositorioDeVentas RepoVentas;
RepositorioDeAperturaDeCaja RepoCajas;
ReporitorioDeInventarios RepoInventarios;
RepositorioDeUsuarios RepoUsuarios;
RepositorioDeAjusteDeInventario RepoAjusteDeInventario;

AplicationUser UsuarioDePrueba;
Inventarios InventarioDePrueba;
Venta VentaDePrueba;
AperturaDeCaja AperturaDeCajaPrueba;

ServicioDeEmail emailService;




using (var context = new InventarioDBContext(contextOPtions))
{
    RepoVentas = new(context);
    RepoCajas = new(context);
    RepoInventarios = new(context);
    RepoUsuarios = new(context);
    RepoAjusteDeInventario = new(context);
    emailService = new();    

    CrearUnUsuario();
    UsuarioDePrueba = RepoUsuarios.ListeLosUsuarios()[0];
    MuestreLosUsuarios();

    // Cargamos los inventarios
    CargarInventarios();
    InventarioDePrueba = RepoInventarios.listeElInventarios().First();
    AjustarInventario(50, 1);
    AjustarInventario(50, 1);
    AjustarInventario(50, 2);
    AjustarInventario(50, 1);

    CrearUnaAperturaDeCaja();
    AperturaDeCajaPrueba = RepoCajas.ListarAperturasDeCaja().First();

    CrearUnaVenta("Cliente 1");
    VentaDePrueba = RepoVentas.ListeLasVentas().First();

    AñadaItemVenta(3);
    AñadaItemVenta(2);
    AñadaItemVenta(5);
    RepoVentas.ApliqueUnDescuento(VentaDePrueba.Id, 20);
    RepoVentas.TermineLaVenta(VentaDePrueba.Id);

    CrearUnaVenta("Cliente 2");
     List<Venta> ventas = (List<Venta>)RepoVentas.ListeLasVentas();
    VentaDePrueba = ventas[1];

    AñadaItemVenta(3);
    AñadaItemVenta(2);
    AñadaItemVenta(5);
    RepoVentas.ApliqueUnDescuento(VentaDePrueba.Id, 5);
    RepoVentas.TermineLaVenta(VentaDePrueba.Id);

    RepoCajas.CerrarUnaAperturaDeCaja(AperturaDeCajaPrueba.Id);

    mostrarLasCajas((List<AperturaDeCaja>)RepoCajas.ListarAperturasDeCaja());

    fallarIntentodeLogin(UsuarioDePrueba);
    MuestreLosUsuarios();
    fallarIntentodeLogin(UsuarioDePrueba);
    MuestreLosUsuarios();
    fallarIntentodeLogin(UsuarioDePrueba);
    MuestreLosUsuarios();

    BloquearUnusaruo(UsuarioDePrueba);

    string mensaje = "Estimado Usuario: " + UsuarioDePrueba.UserName
        + "\n    Su cuenta ha sido bloqueada devido a que se intento " +
        "ingresar a su cuenta por mas de tres veces."
        + "\n\n Por favor ingrese nuevamente sus credenciales en "
        + UsuarioDePrueba.LockoutEnd.GetValueOrDefault().Subtract(DateTime.Now) + "Minutos.";


    emailService.SendEmailAsync("juan_4002@hotmail.com", "OdiN.7072",
        "Usuario Bloqueado" , mensaje, "espinozajuanki@gmail.com");
    MuestreLosUsuarios();



}


void CrearUnUsuario()
{
    UsuarioDePrueba = new AplicationUser { UserName = "usuarioPrueba" };
    RepoUsuarios.AgregueUnUsuario(UsuarioDePrueba);

}
void MuestreLosUsuarios()
{

    foreach (var u in RepoUsuarios.ListeLosUsuarios())
    {
        Console.WriteLine(u.Id + "  " + u.Email + " " + u.UserName );
        Console.WriteLine("Fallos "+ u.AccessFailedCount + " Block- "+u.LockoutEnd);

    }
}

void CargarInventarios()
{
    InventarioDePrueba = new Inventarios
    {
        Nombre = "CocaCola",
        Cantidad = 0,
        Categoria = Categoria.A,
        Precio = 1000
    };
    RepoInventarios.AgregarInventario(InventarioDePrueba);
    
}

void AjustarInventario(int cant, int tipo)
{
    RepoAjusteDeInventario.AgegarAjusteDeInventario(InventarioDePrueba.Id,
        new AjusteDeInventario 
        { 
            Id_Inventario = InventarioDePrueba.Id,
            CantidadActual = InventarioDePrueba.Cantidad,
            Ajuste = cant,
            Tipo = tipo == 1 ? TipoAjuste.Aumento : TipoAjuste.Disminucion,
            Observaciones = "nuevo ajuste",
            UserId = UsuarioDePrueba.Id,
            Fecha = DateTime.Now
        });
}
void CrearUnaAperturaDeCaja()
{
    RepoCajas.CrearUnaAperturaDeCaja(
        new AperturaDeCaja
        {
            UserId = UsuarioDePrueba.Id,
            FechaDeInicio = DateTime.Now,
            Observaciones = "Caja Abierta",
            estado = EstadoCaja.Abierta
        });
}

void CrearUnaVenta(string cliente)
{
    RepoVentas.CreeUnaVenta(
        new Venta
        {
            NombreCliente = cliente,
            Fecha = DateTime.Now,
            TipoDePago = TipoDePago.Efectivo,
            Total = 0,
            PorcentajeDesCuento = 0,
            MontoDescuento = 0,
            SubTotal = 0,
            UserId = UsuarioDePrueba.Id,
            Estado = EstadoVenta.EnProceso,
            IdAperturaDeCaja = AperturaDeCajaPrueba.Id

        });
}

void AñadaItemVenta(int cant)
{
    RepoVentas.AñadaUnDetalleAlaVenta(VentaDePrueba.Id,
        new VentaDetalle
        {
            Cantidad = cant,    
            Id_venta = VentaDePrueba.Id,
            Id_inventario = InventarioDePrueba.Id, 
            Precio = InventarioDePrueba.Precio,
            Monto = InventarioDePrueba.Precio * cant,
            MontoDescuento = VentaDePrueba.PorcentajeDesCuento

        });
}

void muestreLasVentas(List<Venta> Ventas)
{
     foreach (var v in Ventas)
     {
            Console.WriteLine($"    -Venta" + v.Id + " " + v.IdAperturaDeCaja
                +"fecha: "+v.Fecha + "Subtotal "+v.SubTotal + " Total " +v.Total);
       
            muestreDetalleVenta(v.VentaDetalles);
     }

}

void muestreDetalleVenta(List<VentaDetalle> items)
{
    foreach (var d in items)
    {
        Console.WriteLine($"        --item: " + d.Id + " " + d.Inventarios.Nombre
            + " Cant= " + d.Cantidad + "Precio= " + d.Precio
            + "Monto= " + d.Monto
            + "");
    }
}

    void mostrarLasCajas(List<AperturaDeCaja> cajas)
    {
        foreach(var c in cajas)
        {
            Console.WriteLine("Caja: " + c.Id + "Fecha inicio"+ c.FechaDeInicio 
                +"Fecha cierre "+ c.FechaDeCierre + "Estado "+ (c.estado==EstadoCaja.Cerrada ? "cerrada": "Abierta"));
            Console.WriteLine("Ventas: ");
            muestreLasVentas(c.Ventas);
        }

    }


void fallarIntentodeLogin(AplicationUser user)
{
    RepoUsuarios.AñadirUnAccesoFallido(user.Id);
}

void BloquearUnusaruo(AplicationUser user)
{
    RepoUsuarios.BloquearUnUsuario(user.Id);
}







