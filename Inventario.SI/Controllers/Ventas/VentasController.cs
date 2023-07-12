using Azure.Core;
using Inventario.BL.Funcionalidades.Inventario.Interfaces;
using Inventario.BL.Funcionalidades.Ventas.Interfaces;
using Inventario.Models.Dominio.Ventas;
using Inventario.SI.Modelos.Dtos.Ventas;
using Microsoft.AspNetCore.Authorization;
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
        public VentasController(IrepositorioDeAperturaDeCaja repositorioDeCajas,
            IRepositorioDeVentas repositorioDeVentas, IRepositorioDeInventarios repositorioDeInventarios)
        {
            _repositorioDeCajas = repositorioDeCajas;
            _repositoriodeVentas = repositorioDeVentas;
            _repositorioDeInventarios = repositorioDeInventarios;
        }
        // GET: api/<InventariosController>
        [HttpPost("nueva/usuario/")]
        public async Task<ActionResult> crearUnaVenta(string Id_Usuario, int Id_caja, [FromBody] CrearVentaRequest request)
        {
            // validar el usuario

            var cajaActual = await _repositorioDeCajas.ObtenerUnaAperturaDeCajaPorId(Id_caja);


            if (cajaActual == null ) return BadRequest("No existe la caja");

            if(cajaActual.estado == EstadoCaja.Cerrada) return BadRequest("Esta caja esta cerrada!");

            var ventasEnProceso = cajaActual.Ventas.Where(v=> v.Estado == EstadoVenta.EnProceso).ToList(); 
            if(ventasEnProceso != null && ventasEnProceso.Count >0) return BadRequest("Hay Ventas en proceso en esta caja");

            Venta nuevaVenta = new()
            {
                Fecha = DateTime.Now,
                UserId = Id_Usuario,
                Estado = EstadoVenta.EnProceso,
                IdAperturaDeCaja = cajaActual.Id,
                NombreCliente = request.Cliente
                

            };
            await _repositoriodeVentas.CreeUnaVenta(nuevaVenta);

            return Ok(nuevaVenta);
        }

   
        // POST api/<InventariosController>
        [HttpPut("{id_venta}/agregarItem")]
        public async Task<ActionResult<Venta>> AgregaUnItemDeVenta(int id_venta, [FromBody] AgregarItemDeVenatRequest request)
        {
            var cajaActual = await _repositorioDeCajas.ObtenerUnaAperturaDeCajaPorId(request.Id_caja);


            if (cajaActual == null) return BadRequest("No existe la caja");

            if (cajaActual.estado == EstadoCaja.Cerrada) return BadRequest("Esta caja esta cerrada!");

            var ventasEnProceso = cajaActual.Ventas.Where(v => v.Estado == EstadoVenta.EnProceso).ToList();
            if (ventasEnProceso == null || ventasEnProceso.Count == 0) return BadRequest("No Hay Ventas en proceso en esta caja");

           
            var venta = await _repositoriodeVentas.ObtengaUnaVentaPorId(id_venta);
            if(venta != null && venta.Estado == EstadoVenta.EnProceso)
            {
                var inventario = await _repositorioDeInventarios.ObetenerInevtarioPorId(request.Id_Inventario);
                VentaDetalle item = new()
                {
                    Id_venta = id_venta,
                    Id_inventario = request.Id_Inventario,
                    Cantidad = request.cantidad,
                    Precio = inventario.Precio,
                    Monto = inventario.Precio * request.cantidad

                };

                await _repositoriodeVentas.AñadaUnDetalleAlaVenta(id_venta, item);
                inventario.Cantidad -= request.cantidad;
                await _repositorioDeInventarios.EditarInventario(inventario);
                return Ok(venta);

            }else return BadRequest("La venta esta terminada");

        }

        // POST api/<InventariosController>
        [HttpPut("{id_venta}/QuitarItem")]
        public async Task<ActionResult<Venta>> QuitarUnItemDeVenta(int id_venta, [FromBody] QuitarItemDeVentaRequest request)
        {
            var cajaActual = await _repositorioDeCajas.ObtenerUnaAperturaDeCajaPorId(request.Id_caja);


            if (cajaActual == null) return BadRequest("No existe la caja");

            if (cajaActual.estado == EstadoCaja.Cerrada) return BadRequest("Esta caja esta cerrada!");

            var ventasEnProceso = cajaActual.Ventas.Where(v => v.Estado == EstadoVenta.EnProceso).ToList();
            if (ventasEnProceso == null || ventasEnProceso.Count == 0) return BadRequest("No Hay Ventas en proceso en esta caja");


            var venta = await _repositoriodeVentas.ObtengaUnaVentaPorId(id_venta);
            if (venta != null && venta.Estado == EstadoVenta.EnProceso)
            {
                var item = venta.VentaDetalles.Find(i => i.Id == request.id_Item);
                var inventario = await _repositorioDeInventarios.ObetenerInevtarioPorId(item.Id_inventario);
                   

                await _repositoriodeVentas.ElimineUnDetalleDeLaVenta(id_venta, item);
                inventario.Cantidad += item.Cantidad;
                await _repositorioDeInventarios.EditarInventario(inventario);
                return Ok(venta);

            }
            else return BadRequest("La venta esta terminada");

        }

        // PUT api/<InventariosController>/5
        [HttpPut("{id_venta}/Descuento")]
        public async Task<ActionResult<Venta>> AplicarUnDescuento(int id_venta, [FromBody] AplicarDescuentoRequest request)
        {
            var venta = await _repositoriodeVentas.ObtengaUnaVentaPorId(id_venta);
            if (venta != null)
            {
                await _repositoriodeVentas.ApliqueUnDescuento(venta.Id, request.descuento);
                venta = await _repositoriodeVentas.ObtengaUnaVentaPorId(id_venta);
                return Ok(venta);
            }
            else return BadRequest("La Venta esta terminada.");

        }

       
        [HttpDelete("{id_venta}/Terminar")]
        public async Task<ActionResult<Venta>> TerminarVenta(int id_venta)
        {
            var venta = await _repositoriodeVentas.ObtengaUnaVentaPorId(id_venta);
            if (venta != null)
            {
                await _repositoriodeVentas.TermineLaVenta(venta.Id);
                venta = await _repositoriodeVentas.ObtengaUnaVentaPorId(id_venta);
                return Ok(venta);
            }
            else return BadRequest("La Venta esta terminada.");

        }
    }
}
