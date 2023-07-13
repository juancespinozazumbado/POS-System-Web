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
    public class AperturasDeCajaController : ControllerBase
    {
        private readonly IrepositorioDeAperturaDeCaja _repositorioDeAperturasDeCaja;
        private readonly IRepositorioDeVentas _repositoriodeVentas;
        private readonly UserManager<AplicationUser> _userManager;

        public AperturasDeCajaController(IRepositorioDeVentas repositoriodeVentas,
            IrepositorioDeAperturaDeCaja repositorioDeAperturaDeCaja, UserManager<AplicationUser> userManagger)
        {
            _repositoriodeVentas = repositoriodeVentas;
            _repositorioDeAperturasDeCaja = repositorioDeAperturaDeCaja;
            _userManager = userManagger;
        }

        // GET: api/<InventariosController>
        [HttpGet("usuario/cajas")]
        public async Task<ActionResult<RespuestaDto>> verCajasPorUsuario(string Id_Usuario)
        {
            var usuario = await _userManager.FindByIdAsync(Id_Usuario);
            if (usuario == null) return BadRequest("Usuario no encontrado");
            var cajas = await _repositorioDeAperturasDeCaja.AperturasDeCajaPorUsuario(Id_Usuario);
            if (cajas != null)
            {
                return Ok( new RespuestaDto() { Respuesta = cajas });
            } else return BadRequest("No existe el usuario");
            
        }

        // GET api/<InventariosController>/5
        [HttpGet("usuario/cajas/{id}")]
        public async Task<ActionResult<RespuestaDto>> VerUnaCajaDelUsuario( string Id_Usuario, int id)
        {
            var usuario = await _userManager.FindByIdAsync(Id_Usuario);
            if (usuario == null) return BadRequest("Usuario no encontrado");
            var cajas = await _repositorioDeAperturasDeCaja.AperturasDeCajaPorUsuario(Id_Usuario);
            if (cajas != null)
            {
               var caja = await _repositorioDeAperturasDeCaja.ObtenerUnaAperturaDeCajaPorId(id);
                if (caja != null)
                {
                    return Ok(new RespuestaDto() { Respuesta = caja });
                }
                else return BadRequest("No se enconrtro la caja");
               
            }
            else return BadRequest("No existe el usuario");

        }

        // POST api/<InventariosController>
        [HttpPost("usuario/cajas/")]
        public async Task<ActionResult<RespuestaDto>> CrearAperturaDeCaja([FromBody] CrearAperturaCajaRequest request)
        {
            var usuario = await _userManager.FindByIdAsync(request.Id_Usuario);
            if (usuario == null) return BadRequest("Usuario no encontrado");

            AperturaDeCaja nuevaAperturaDeCaja = new()
            {
                FechaDeInicio = DateTime.Now,
                UserId = usuario.Id,
                estado = EstadoCaja.Abierta,
                Observaciones = request.Observaciones

            };
            await _repositorioDeAperturasDeCaja.CrearUnaAperturaDeCaja(nuevaAperturaDeCaja);
            return Ok(new RespuestaDto() { Respuesta = nuevaAperturaDeCaja });
        }

        // PUT api/<InventariosController>/5
        [HttpPut("usuario/cajas/{id_caja}")]
        public async Task<ActionResult<RespuestaDto>> CerrarLaCaja( string Id_Usuario, int id_caja)
        {
            var usuario = await _userManager.FindByIdAsync(Id_Usuario);
            if (usuario == null) return BadRequest("Usuario no encontrado");
            var cajas = await _repositorioDeAperturasDeCaja.AperturasDeCajaPorUsuario(Id_Usuario);
            if (cajas != null)
            {
                var caja = await _repositorioDeAperturasDeCaja.ObtenerUnaAperturaDeCajaPorId(id_caja);
                if (caja != null)
                {
                   var resultado =  await _repositorioDeAperturasDeCaja.CerrarUnaAperturaDeCaja(id_caja);
                    if(caja.estado == EstadoCaja.Cerrada) return BadRequest("La caja esta cerrada");
                    if (!resultado) return BadRequest("La caja no tiene ventas terminadas");
                    return Ok(new RespuestaDto() { Respuesta = caja });
                }
                else return BadRequest("No se enconrtro la caja");

            }
            else return BadRequest("No existe el usuario");
        }

    }
}
