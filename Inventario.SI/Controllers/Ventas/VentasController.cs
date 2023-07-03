using Microsoft.AspNetCore.Mvc;

namespace Inventario.SI.Controllers.Ventas
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        // GET: api/<InventariosController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
