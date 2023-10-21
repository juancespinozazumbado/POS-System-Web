using Inventario.BL.Funcionalidades.Inventario.Interfaces;
using Inventario.Models.Dominio.Productos;
using Inventario.SI.Modelos;
using Inventario.SI.Modelos.Dtos.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.SI.Controllers.Productos
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AjustesDeInventariosController : ControllerBase
    {
        private readonly IRepositorioDeAjusteDeInventarios _repositorioDeAjusteDeInventarios;
        private readonly IRepositorioDeInventarios _repositorioDeInventarios;
        public AjustesDeInventariosController(IRepositorioDeAjusteDeInventarios repositorioDeAjustesDeInventarios, 
            IRepositorioDeInventarios repositorioDeInventarios)
        {
            _repositorioDeInventarios = repositorioDeInventarios;
            _repositorioDeAjusteDeInventarios = repositorioDeAjustesDeInventarios;
        }


        // GET: api/<InventariosController>
        [HttpGet("{id}")]
        public  async Task<ActionResult<RespuestaDto>> listarAjustesdeInentario(int id)
        {
            var inventario = await _repositorioDeInventarios.ObetenerInevtarioPorId(id);
            if(inventario != null)
            {
                
                return Ok(new RespuestaDto() { Respuesta = inventario });
            }else { return BadRequest("No existe el inventario" ); }  
            
        }

        // GET api/<InventariosController>/5
        [HttpGet("{id}/Detalle")]
        public async Task<ActionResult<RespuestaDto>> VerElDetalleDeUnAjuste(int id, int id_detalle)
        {
            var inventario = await _repositorioDeInventarios.ObetenerInevtarioPorId(id);
            if(inventario.Ajustes.Count ==0) return BadRequest("El inventario no tiene ajustes");
            if (inventario != null)
            {
                var ajuste = inventario.Ajustes.Find( a => a.Id == id_detalle);
                if (ajuste != null) { return Ok(new RespuestaDto() { Respuesta = ajuste });  
                } else return BadRequest("No existe este ajuste");
                
            }
            else { return BadRequest("No existe el inventario"); }

        }

        // POST api/<InventariosController>
        [HttpPost("{id}")]
        public async Task<ActionResult<RespuestaDto>> CrearUnAjusteDeInventario(int id, [FromBody] CrearAjusteDeInventarioRequest request)
        {
            var inventario = await _repositorioDeInventarios.ObetenerInevtarioPorId(id);
            
            if (inventario != null)
            {
                AjusteDeInventario ajusteDeInventario = new()
                {
                    Id_Inventario = id,
                    UserId = request.Id_Usuario,
                    Fecha = DateTime.Now,
                    CantidadActual = inventario.Cantidad,
                    Tipo = (TipoAjuste)request.TipoAjuste,
                    Ajuste = request.Ajuste,
                    Observaciones = request.Observaciones

                };
                await _repositorioDeAjusteDeInventarios.AgegarAjusteDeInventario(id, ajusteDeInventario);
                return Ok(new RespuestaDto() { Respuesta = inventario });
            }
            else { return BadRequest("No existe el inventario"); }
        }
    }
}
