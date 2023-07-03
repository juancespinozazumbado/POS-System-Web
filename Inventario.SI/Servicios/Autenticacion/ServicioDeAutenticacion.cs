using Inventario.BL.Funcionalidades.Usuarios;
using Inventario.BL.Funcionalidades.Usuarios.Interfaces;
using Inventario.BL.ServicioEmail;
using Inventario.Models.Dominio.Usuarios;
using Inventario.SI.Modelos.Dtos.Usuarios;
using Inventario.SI.Servicios.Autenticacion.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Inventario.SI.Servicios.Autenticacion
{
    public class ServicioDeAutenticacion : IServicioDeAutenticacion
    {
        private readonly IRepositorioDeUsuarios _repositorioDeUsuarios;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly SignInManager<AplicationUser> _signInManager;
        private IServicioDeEmail _servicioDeEmail;


        public ServicioDeAutenticacion(UserManager<AplicationUser> userManager, IRepositorioDeUsuarios repositorioDeUsuarios,
            SignInManager<AplicationUser> signInManager, IServicioDeEmail servicioDeEmail
            )
        { 
            _userManager = userManager; 
            _repositorioDeUsuarios = repositorioDeUsuarios;
            _signInManager = signInManager; 
            _servicioDeEmail = servicioDeEmail;

        }

        public async Task<LoginResponsetDto> Login(LoginRequestDto loginRequest)
        {
           
                
                var Usuario = _repositorioDeUsuarios.ObtengaUnUsuarioPorEmail(loginRequest.Correo);
                if (Usuario != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(Usuario.UserName, loginRequest.Contraseña, isPersistent:false, lockoutOnFailure: true);
                //    if (result.Succeeded)
                //    {
                       

                //        string titulo = "Inicio de sescion del usuario " + Usuario.UserName;
                //        string cuerpo = "Usted inicio sescion el dia " + DateTime.Now.Day
                //            + "/" + DateTime.Now.Month
                //            + "/" + DateTime.Now.Year
                //            + " a las " + DateTime.Now.Hour + " : " + DateTime.Now.Minute;
                //        //_servicioDeEmail.SendEmailAsync("comerciosistema@outlook.com", "OdiN.7072", titulo, cuerpo, Input.Email);
                       
                //    }
                //    if (result.RequiresTwoFactor)
                //    {
                //        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                //    }
                //    if (result.IsLockedOut)
                //    {
                //        _logger.LogWarning("User account locked out.");

                //        //AplicationUser? usuario = _repositorioDeUsuarios.ObtengaUnUsuarioPorEmail(Input.Email);
                //        var lockoutEnd = Usuario.LockoutEnd;
                //        string titulo = "Intento de inicio de sescion del usuario " + Usuario.UserName + "bloqueado";
                //        string cuerpo = "Le informamos que la cuenta del usuario" + Usuario.UserName
                //        + "se encuentra bloqueada por 10 minutos. Por favor ingrese el día "
                //        + lockoutEnd.Value.Day
                //        + "/" + lockoutEnd.Value.Month + "/" + lockoutEnd.Value.Year
                //        + " a las " + lockoutEnd.Value.Hour + " : " + lockoutEnd.Value.Minute;

                //        _emailSender.SendEmailAsync("comerciosistema@outlook.com", "OdiN.7072", titulo, cuerpo, Input.Email);

                //        return RedirectToPage("./Lockout");
                //    }
                //    else
                //    {
                //        ModelState.AddModelError(string.Empty, "Combinacion de correo y contraseña invalidos!");



                //        if (Usuario.AccessFailedCount == 3)
                //        {
                //            _repositorioDeUsuarios.BloquearUnUsuario(Usuario.Id);

                //            var lockoutEnd = Usuario.LockoutEnd;
                //            string titulo = "Usuario bloqueado!";
                //            string cuerpo = "Le informamos que la cuenta del usuario" + Usuario.UserName
                //            + "se encuentra bloqueada por 10 minutos. Por favor ingrese el día "
                //            + lockoutEnd.Value.Day
                //            + "/" + lockoutEnd.Value.Month + "/" + lockoutEnd.Value.Year
                //            + " a las " + lockoutEnd.Value.Hour + " : " + lockoutEnd.Value.Minute;
                //            ModelState.AddModelError(string.Empty, "Su usuario ha sido bloqueado por 10 minutos." +
                //                "\n Revise su correo electronico para mas informacion.");

                //            _emailSender.SendEmailAsync("comerciosistema@outlook.com", "OdiN.7072", titulo, cuerpo, Input.Email);
                //        }
                //        else
                //        {
                //            //_repositorioDeUsuarios.AñadirUnAccesoFallido(Usuario.Id);
                //        }

                //    }

                //    return Page();
                //}
                //else { ModelState.AddModelError(string.Empty, "No se encontro el usuario."); }
            }

       


            return null;
        }

        public async Task<RegistroResponsetDto> Registro(RegistroRequestDto registroRequest)
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
                   
                    RegistroResponsetDto respuesta = new()
                    {
                        Nombre = usuaurio.UserName,
                        Correo = registroRequest.Correo,
                    };

                    return respuesta;
                }
               

            }catch(Exception ex)
            {
                Console.Write(ex.ToString());       
            }

            return null;
        }
        
    }
}
