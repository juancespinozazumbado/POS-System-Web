using Inventario.SI.Modelos.Dtos.Usuarios;
using Inventario.SI.Servicios.Autenticacion;
using Inventario.SI.Servicios.Autenticacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

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


        // PUT api/Autenticacion/Registro
        [HttpPost("Registro")]
        public async Task<ActionResult<RegistroResponseDto>> Registro([FromBody] RegistroRequestDto request)
        {

            try
            {
                var respuesta =  await _servicioDeAutenticacion.Registro(request);
                if(respuesta.EntidadDto != null) 
                {
                    return Ok(respuesta.EntidadDto);   
                }else
                {
                    return BadRequest(respuesta.Error);
                }



            }catch (Exception ex)
            { 
                Console.Write(ex.ToString());
                return BadRequest(ex);
            }
            
        } 

        // POST api/Autenticacion/Login/
        [HttpPost("Login")]
        public async Task<ActionResult<LoginRequestDto>> Login ([FromBody] LoginRequestDto request)
        {
             try
            {
                var respuesta =  await _servicioDeAutenticacion.Login(request);
                if (respuesta.EntidadDto != null)
                {
                    return Ok(respuesta.EntidadDto);
                }
                else
                {
                    return BadRequest(respuesta.Mensaje);
                }

            }catch (Exception ex)
            { 
                Console.Write(ex.ToString());
                return BadRequest(ex);
            }
        }

    }
}
