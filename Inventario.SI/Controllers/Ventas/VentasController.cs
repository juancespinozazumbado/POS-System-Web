using Azure.Core;
using Inventario.BL.Funcionalidades.Inventario.Interfaces;
using Inventario.BL.Funcionalidades.Ventas.Interfaces;
using Inventario.Models.Dominio.Usuarios;
using Inventario.Models.Dominio.Ventas;
using Inventario.SI.Modelos;
using Inventario.SI.Modelos.Dtos.Ventas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.SI.Controllers.Ventas
{ 
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IRepositorioDeVentas _repositoriodeVentas;
        private readonly IrepositorioDeAperturaDeCaja _repositorioDeCajas;
        private readonly IRepositorioDeInventarios _repositorioDeInventarios;
        private readonly UserManager<AplicationUser> _userManager;
        public VentasController(IrepositorioDeAperturaDeCaja repositorioDeCajas,
            IRepositorioDeVentas repositorioDeVentas, 
            IRepositorioDeInventarios repositorioDeInventarios, UserManager<AplicationUser> userManagger)
        {
            _repositorioDeCajas = repositorioDeCajas;
            _repositoriodeVentas = repositorioDeVentas;
            _repositorioDeInventarios = repositorioDeInventarios;
            _userManager = userManagger;
        }


        // GET: api/<InventariosController>
        [HttpPost("nueva/")]
        public async Task<ActionResult<RespuestaDto>> crearUnaVenta([FromBody] CrearVentaRequest request)
        {
            // validar el usuario
            var usuario = await _userManager.FindByIdAsync(request.Id_Usuario);
            if (usuario == null) return BadRequest("Usuario no encontrado");

            var cajaActual = await _repositorioDeCajas.ObtenerUnaAperturaDeCajaPorId(request.Id_caja);


            if (cajaActual == null ) return BadRequest("No existe la caja");

            if(cajaActual.estado == EstadoCaja.Cerrada) return BadRequest("Esta caja esta cerrada!");

            if(ExistenVentasEnProceso(cajaActual)) return BadRequest("Esta caja Tiene ventas en proceso!");

            var ventasEnProceso = cajaActual.Ventas.Where(v=> v.Estado == EstadoVenta.EnProceso).ToList(); 
            if(ventasEnProceso != null && ventasEnProceso.Count >0) return BadRequest("Hay Ventas en proceso en esta caja");

            Venta nuevaVenta = new()
            {
                Fecha = DateTime.Now,
                UserId = usuario.Id,
                Estado = EstadoVenta.EnProceso,
                IdAperturaDeCaja = cajaActual.Id,
                NombreCliente = request.Cliente
                

            };
            await _repositoriodeVentas.CreeUnaVenta(nuevaVenta);

            return Ok(new RespuestaDto() { Respuesta = nuevaVenta });
        }

   
        // POST api/<InventariosController>
        [HttpPut("{id_venta}/agregarItem")]
        public async Task<ActionResult<RespuestaDto>> AgregaUnItemDeVenta(int id_venta, [FromBody] AgregarItemDeVenatRequest request)
        {
            var usuario = await _userManager.FindByIdAsync(request.id_Usuario);
            if (usuario == null) return BadRequest("No existe el usuario");

            var cajaActual = await _repositorioDeCajas.ObtenerUnaAperturaDeCajaPorId(request.Id_caja);


            if (cajaActual == null) return BadRequest("No existe la caja");

            if (cajaActual.estado == EstadoCaja.Cerrada) return BadRequest("Esta caja esta cerrada!");

            var ventasEnProceso = cajaActual.Ventas.Where(v => v.Estado == EstadoVenta.EnProceso).ToList();
            if (ventasEnProceso == null || ventasEnProceso.Count == 0) return BadRequest("No Hay Ventas en proceso en esta caja");

           
            var venta = await _repositoriodeVentas.ObtengaUnaVentaPorId(id_venta);
            if(venta != null && venta.Estado == EstadoVenta.EnProceso)
            {
                var inventario = await _repositorioDeInventarios.ObetenerInevtarioPorId(request.Id_Inventario);
                if(inventario == null) return BadRequest("No existe el inventario");
                VentaDetalle item = new()
                {
                    Id_venta = id_venta,
                    Id_inventario = request.Id_Inventario,
                    Cantidad = request.cantidad,
                    Precio = inventario.Precio,
                    Monto = inventario.Precio * request.cantidad

                };
             
                await _repositoriodeVentas.AñadaUnDetalleAlaVenta(id_venta, item);
                // Actualizar la cantidad en el inventario
                inventario.Cantidad -= request.cantidad;
                await _repositorioDeInventarios.EditarInventario(inventario);
                return Ok(new RespuestaDto() { Respuesta = venta });

            }else return BadRequest("La venta esta terminada");

        }

        // POST api/<InventariosController>
        [HttpPut("{id_venta}/QuitarItem")]
        public async Task<ActionResult<RespuestaDto>> QuitarUnItemDeVenta(int id_venta, [FromBody] QuitarItemDeVentaRequest request)
        {

            var usuario = await _userManager.FindByIdAsync(request.id_Usuario);
            if (usuario == null) return BadRequest("No existe el usuario");

            var cajaActual = await _repositorioDeCajas.ObtenerUnaAperturaDeCajaPorId(request.Id_caja);


            if (cajaActual == null) return BadRequest("No existe la caja");

            if (cajaActual.estado == EstadoCaja.Cerrada) return BadRequest("Esta caja esta cerrada!");

            var ventasEnProceso = cajaActual.Ventas.Where(v => v.Estado == EstadoVenta.EnProceso).ToList();
            if (ventasEnProceso == null || ventasEnProceso.Count == 0) return BadRequest("No Hay Ventas en proceso en esta caja");


            var venta = await _repositoriodeVentas.ObtengaUnaVentaPorId(id_venta);
            if (venta != null && venta.Estado == EstadoVenta.EnProceso)
            {
                var item = venta.VentaDetalles.Find(i => i.Id == request.id_Item);
                if(item == null) return BadRequest("No existe el item");
                var inventario = await _repositorioDeInventarios.ObetenerInevtarioPorId(item.Id_inventario);
                if (inventario == null) return BadRequest("No existe el inventario");


                await _repositoriodeVentas.ElimineUnDetalleDeLaVenta(id_venta, item);
                inventario.Cantidad += item.Cantidad;
                await _repositorioDeInventarios.EditarInventario(inventario);
                return Ok(new RespuestaDto() { Respuesta = venta });

            }
            else return BadRequest("La venta esta terminada");

        }

        // PUT api/<InventariosController>/5
        [HttpPut("{id_venta}/Descuento")]
        public async Task<ActionResult<RespuestaDto>> AplicarUnDescuento(int id_venta, [FromBody] AplicarDescuentoRequest request)
        {
            
            var venta = await _repositoriodeVentas.ObtengaUnaVentaPorId(id_venta);
            if (venta != null)
            {
                if (venta.Estado == EstadoVenta.Terminada) return BadRequest("La Venta esta terminada.");
                await _repositoriodeVentas.ApliqueUnDescuento(venta.Id, request.descuento);
                venta = await _repositoriodeVentas.ObtengaUnaVentaPorId(id_venta);
                return Ok(new RespuestaDto() { Respuesta = venta });
            }
            else return BadRequest("La Venta esta terminada.");

        }

       
        [HttpPut("{id_venta}/Terminar")]
        public async Task<ActionResult<RespuestaDto>> TerminarVenta(int id_venta, [FromBody] TerminarVentaRequest cuerpo)
        {
            var venta = await _repositoriodeVentas.ObtengaUnaVentaPorId(id_venta);
            if (venta != null)
            {
                if(venta.Estado == EstadoVenta.Terminada) return BadRequest("La Venta esta terminada.");
                await _repositoriodeVentas.EstablescaElTipoDePago(venta.Id, cuerpo.TipoDePago);
                await _repositoriodeVentas.TermineLaVenta(venta.Id);
                venta = await _repositoriodeVentas.ObtengaUnaVentaPorId(id_venta);
                return Ok(new RespuestaDto() { Respuesta = venta });
            }
            else return BadRequest("No existe la venta");

        }



        private bool ExistenVentasEnProceso(AperturaDeCaja caja)
        {
            return caja.Ventas.Find(v => v.Estado == EstadoVenta.EnProceso) != null;

        }
    }
}
