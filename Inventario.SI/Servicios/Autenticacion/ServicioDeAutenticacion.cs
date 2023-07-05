using Inventario.BL.Funcionalidades.Usuarios;
using Inventario.BL.Funcionalidades.Usuarios.Interfaces;
using Inventario.BL.ServicioEmail;
using Inventario.Models.Dominio.Usuarios;
using Inventario.SI.Modelos;
using Inventario.SI.Modelos.Dtos.Usuarios;
using Inventario.SI.Servicios.Autenticacion.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Inventario.SI.Servicios.Autenticacion
{
    public class ServicioDeAutenticacion : IServicioDeAutenticacion
    {
        private readonly IRepositorioDeUsuarios _repositorioDeUsuarios;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly SignInManager<AplicationUser> _signInManager;
        private readonly IServicioDeJWT _servicioDeJWT;
        private IServicioDeEmail _servicioDeEmail;


        public ServicioDeAutenticacion(UserManager<AplicationUser> userManager, 
            IRepositorioDeUsuarios repositorioDeUsuarios,
            SignInManager<AplicationUser> signInManager, 
            IServicioDeEmail servicioDeEmail, IServicioDeJWT servicioDeJWT
            )
        { 
            _userManager = userManager; 
            _repositorioDeUsuarios = repositorioDeUsuarios;
            _signInManager = signInManager; 
            _servicioDeEmail = servicioDeEmail;
            _servicioDeJWT = servicioDeJWT; 

        }

        public async Task<RespuestaDeAutenticacion<LoginResponsetDto>> Login(LoginRequestDto loginRequest)
        {      
            var Usuario = _repositorioDeUsuarios.ObtengaUnUsuarioPorEmail(loginRequest.Correo);
            if (Usuario != null)
            {
                    var resultado = await _signInManager.CheckPasswordSignInAsync(Usuario, loginRequest.Contraseña, false);

                if (resultado.Succeeded)
                {
                    LoginResponsetDto respuesta = new()
                    {
                        Usuario = Usuario.UserName,
                        Token = _servicioDeJWT.GenerarToken(Usuario)

                    };

                    string titulo = "Inicio de sescion del usuario " + Usuario.UserName;
                    string cuerpo = "Usted inicio sescion el dia " + DateTime.Now.Day
                        + "/" + DateTime.Now.Month
                        + "/" + DateTime.Now.Year
                        + " a las " + DateTime.Now.Hour + " : " + DateTime.Now.Minute;
                    _servicioDeEmail.SendEmailAsync(
                        "comerciosistema@outlook.com", "OdiN.7072", titulo, cuerpo, Usuario.Email);

                    return new RespuestaDeAutenticacion<LoginResponsetDto>()
                    {
                        Mensaje = "Credenciales correctos",
                        EntidadDto = respuesta
                    };

                }
                if (resultado.IsLockedOut)
                {

                    var lockoutEnd = Usuario.LockoutEnd;
                    string titulo = "Intento de inicio de sescion del usuario " + Usuario.UserName + "bloqueado";
                    string cuerpo = "Le informamos que la cuenta del usuario" + Usuario.UserName
                    + "se encuentra bloqueada por 10 minutos. Por favor ingrese el día "
                    + lockoutEnd.Value.Day
                    + "/" + lockoutEnd.Value.Month + "/" + lockoutEnd.Value.Year
                    + " a las " + lockoutEnd.Value.Hour + " : " + lockoutEnd.Value.Minute;

                    _servicioDeEmail.SendEmailAsync("comerciosistema@outlook.com", "OdiN.7072", titulo, cuerpo, Usuario.Email);

                    return new RespuestaDeAutenticacion<LoginResponsetDto>()
                    {
                        Mensaje = "Su usuario esta bloqueado bloqueado!",

                    };

                }
                else
                {

                    if (Usuario.AccessFailedCount == 3)
                    {
                        _repositorioDeUsuarios.BloquearUnUsuario(Usuario.Id);

                        var lockoutEnd = Usuario.LockoutEnd;
                        string titulo = "Usuario bloqueado!";
                        string cuerpo = "Le informamos que la cuenta del usuario" + Usuario.UserName
                        + "se encuentra bloqueada por 10 minutos. Por favor ingrese el día "
                        + lockoutEnd.Value.Day
                        + "/" + lockoutEnd.Value.Month + "/" + lockoutEnd.Value.Year
                        + " a las " + lockoutEnd.Value.Hour + " : " + lockoutEnd.Value.Minute;

                        _servicioDeEmail.SendEmailAsync(
                            "comerciosistema@outlook.com", "OdiN.7072", titulo, cuerpo, Usuario.Email);

                        return new RespuestaDeAutenticacion<LoginResponsetDto>()
                        {
                            Mensaje = "Su usuario ha sido bloqueado bloqueado!"
                        };


                    }
                    else
                    {
                        _repositorioDeUsuarios.AñadirUnAccesoFallido(Usuario.Id);
                        return new RespuestaDeAutenticacion<LoginResponsetDto>()
                        {
                            Mensaje = "Credenciales invalidos!"
                        };
                    }
                }

            }
            else{
                return new RespuestaDeAutenticacion<LoginResponsetDto>()
                {
                    Mensaje = "Usuario no encontrado"
                };

            }
            
        }

        public async Task<RespuestaDeAutenticacion<RegistroResponseDto>> Registro(RegistroRequestDto registroRequest)
        {

            AplicationUser usuaurio = new()
            {
                UserName = registroRequest.Nombre,
                Email = registroRequest.Correo
            };

            try
            { 
                var resultado = await _userManager.CreateAsync(usuaurio, registroRequest.Contraseña);

                if (resultado.Succeeded)
                {
                    usuaurio = _repositorioDeUsuarios.ObtengaUnUsuarioPorEmail(registroRequest.Correo);

                    var userId = await _userManager.GetUserIdAsync((AplicationUser)usuaurio);

                    RegistroResponseDto respuesta = new()
                    {
                        Nombre = usuaurio.UserName,
                        Correo = registroRequest.Correo,
                    };

                    return new RespuestaDeAutenticacion<RegistroResponseDto>() { EntidadDto = respuesta };
                }
                else return new RespuestaDeAutenticacion<RegistroResponseDto>() { Error = resultado.Errors.ToList() };
               

            }catch(Exception ex)
            {
                Console.Write(ex.ToString());
                return new RespuestaDeAutenticacion<RegistroResponseDto> {Mensaje = ex.Message };
            }
        }
        
    }
}
