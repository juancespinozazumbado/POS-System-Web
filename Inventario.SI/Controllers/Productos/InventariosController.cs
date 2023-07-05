using Inventario.BL.Funcionalidades.Inventario.Interfaces;
using Inventario.Models.Dominio.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.SI.Controllers.Productos
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class InventariosController : ControllerBase
    {
        private readonly IRepositorioDeInventarios _repositorioDeInventarios;   

        public InventariosController( IRepositorioDeInventarios repositorioDeInventarios)
        {
            _repositorioDeInventarios = repositorioDeInventarios;
        }

        // GET: api/<InventariosController>
        [HttpGet]
        public async Task<ActionResult<List<Inventarios>>> ListaDeInvetarios()
        {
            List<Inventarios> lista = await _repositorioDeInventarios.listeElInventarios();
            return  Ok(lista);
        }

        // GET api/<InventariosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InventariosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<InventariosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InventariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
