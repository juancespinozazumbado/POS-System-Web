using Inventario.SI.Modelos.Dtos.Usuarios;
using Inventario.SI.Servicios.Autenticacion;
using Inventario.SI.Servicios.Autenticacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inventario.SI.Controllers.Usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {

        private readonly IServicioDeAutenticacion _servicioDeAutenticacion;

        public AutenticacionController(IServicioDeAutenticacion servicioDeAutenticacion)
        {
            _servicioDeAutenticacion = servicioDeAutenticacion;
        }


        // PUT api/<Autenticacion>/5
        [HttpPost("Registro")]
        public async Task<ActionResult<RegistroResponsetDto>> Registro([FromBody] RegistroRequestDto request)
        {

            try
            {
                var respuesta =  await _servicioDeAutenticacion.Registro(request);
                if(respuesta != null) 
                {
                    return Ok(respuesta);   
                }else
                {
                    return BadRequest(respuesta);
                }



            }catch (Exception ex)
            { 
                Console.Write(ex.ToString());   
            }
            return null;
        } 


        // POST api/<Autenticacion>
        [HttpPost("Login")]
        public void Login([FromBody] string value)
        {
        }

        // PUT api/<Autenticacion>/5
        [HttpPost("Logoutfc")]
        public void Logout([FromBody] string value)
        {
        }

    }
}
