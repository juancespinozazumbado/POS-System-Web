using Inventario.BL.Funcionalidades.Inventario.Interfaces;
using Inventario.BL.Funcionalidades.Usuarios.Interfaces;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Usuarios;
using Inventario.SI.Modelos;
using Inventario.SI.Modelos.Dtos.Usuarios;
using Inventario.SI.Servicios.Autenticacion;
using Inventario.SI.Servicios.Autenticacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.SI.Controllers.Usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CuentaController : ControllerBase
    {

        private readonly IServicioDeAutenticacion _servicioDeAutenticacion;
        private readonly IRepositorioDeUsuarios _repositorioDeUsuarios;
        UserManager<AplicationUser> _userManager;

        public CuentaController(IServicioDeAutenticacion servicioDeAutenticacion,
            IRepositorioDeUsuarios repositorioDeUsuarios, UserManager<AplicationUser> userManager)
        {
            _servicioDeAutenticacion = servicioDeAutenticacion;
            _repositorioDeUsuarios = repositorioDeUsuarios;
            _userManager = userManager;
        }


        // PUT api/Autenticacion/Registro
        [HttpGet("Usuario")]
        public async Task<ActionResult<RegistroResponseDto>> Obtenerusuario(string username)
        {

            try
            {
                var usuario =  await _repositorioDeUsuarios.ObtengaUnUsuarioPorUserName(username);
                if(usuario != null) 
                {
                    return Ok(new RespuestaDto() { Respuesta = usuario });   
                }else
                {
                    return BadRequest("No se encontro el usuario");
                }



            }catch (Exception ex)
            { 
                Console.Write(ex.ToString());
                return BadRequest(ex);
            }
            
        } 

        // POST api/Autenticacion/Login/
        [HttpPost("CambioDeClave")]
        public async Task<ActionResult<RespuestaDto>> CambiarContraseña ([FromBody] CambioDeContraseñaRequestDto request)
        {
             try
            {
                var usuario = await _userManager.FindByNameAsync(request.username);

                if (usuario != null)
                {
                    var validarContraseñaActual = await _userManager.CheckPasswordAsync(usuario, request.Contraseña);
                    if (validarContraseñaActual)
                    {
                        var result = await _userManager.ChangePasswordAsync(usuario, request.Contraseña, request.NuevaContraseña);
                        if (result.Succeeded)
                            return Ok(new RespuestaDto() { Mensaje = "Contraseña cambiada con exito" });
                        return Ok(new RespuestaDto() { Mensaje = "Fallo al cambimbiar la contraseña" });
                    } else return Ok(new RespuestaDto() { Mensaje = "Contraseña es incorrecta" });

                }
                else
                {
                    return BadRequest("Fallo");
                }

            }catch (Exception ex)
            { 
                Console.Write(ex.ToString());
                return BadRequest(ex);
            }
        }

        // POST api/Autenticacion/Login/
        [HttpPost("usuario/cambioDeContraseña")]
        public async Task<ActionResult<LoginRequestDto>> CambioDeContraseña([FromBody] CambioDeContraseñaRequestDto request)
        {
            try
            {
                var respuesta =  await _servicioDeAutenticacion.CambiarContraseña(request);
                if (respuesta.Error == null)
                {
                    return Ok(respuesta.Mensaje);
                }
                else
                {
                    return BadRequest(respuesta.Error);
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return BadRequest(ex);
            }
        }

    }
}
