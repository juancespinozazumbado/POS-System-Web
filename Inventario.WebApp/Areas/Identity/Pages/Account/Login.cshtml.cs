// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Inventario.Models.Dominio.Usuarios;
using Inventario.BL.Funcionalidades.Usuarios;
using Inventario.BL.ServicioEmail;
using System.Security.Claims;
using Inventario.DA.Database;
using Inventario.BL.Funcionalidades.Usuarios.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Inventario.WebApp.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AplicationUser> _signInManager;
        //private readonly UserManager<AplicationUser> _userManager;   
        private readonly ILogger<LoginModel> _logger;
        

        private readonly IServicioDeEmail _emailSender = new ServicioDeEmail();
        private readonly RepositorioDeUsuarios _repositorioDeUsuarios;  
       


        public LoginModel(SignInManager<AplicationUser> signInManager, ILogger<LoginModel> logger, InventarioDBContext dbContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            //_userManager = userManager; 
            _repositorioDeUsuarios = new(dbContext);
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var Usuario = _repositorioDeUsuarios.ObtengaUnUsuarioPorEmail(Input.Email);
                if(Usuario != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(Usuario.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");

                        string titulo = "Inicio de sescion del usuario " + Usuario.UserName ;
                        string cuerpo = "Usted inicio sescion el dia " + DateTime.Now.Day
                            +"/"+ DateTime.Now.Month 
                            +"/" + DateTime.Now.Year    
                            + " a las " + DateTime.Now.Hour + " : " + DateTime.Now.Minute;
                        _emailSender.SendEmailAsync("comerciosistema@outlook.com", "OdiN.7072", titulo, cuerpo, Input.Email);
                        return LocalRedirect(returnUrl);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");

                        //AplicationUser? usuario = _repositorioDeUsuarios.ObtengaUnUsuarioPorEmail(Input.Email);
                        var lockoutEnd = Usuario.LockoutEnd;
                        string titulo = "Intento de inicio de sescion del usuario " + Usuario.UserName +"bloqueado";
                        string cuerpo = "Le informamos que la cuenta del usuario" + Usuario.UserName
                        + "se encuentra bloqueada por 10 minutos. Por favor ingrese el día "
                        + lockoutEnd.Value.Day
                        + "/" + lockoutEnd.Value.Month + "/" + lockoutEnd.Value.Year
                        + " a las " + lockoutEnd.Value.Hour + " : " + lockoutEnd.Value.Minute;

                        _emailSender.SendEmailAsync("comerciosistema@outlook.com", "OdiN.7072", titulo, cuerpo, Input.Email);
                    
                    return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Combinacion de correo y contraseña invalidos!");

                       
                       
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
                            ModelState.AddModelError(string.Empty, "Su usuario ha sido bloqueado por 10 minutos." +
                                "\n Revise su correo electronico para mas informacion.");

                            _emailSender.SendEmailAsync("comerciosistema@outlook.com", "OdiN.7072", titulo, cuerpo, Input.Email);
                            }
                            else
                            {
                                //_repositorioDeUsuarios.AñadirUnAccesoFallido(Usuario.Id);
                            }

                    }
               
                    return Page();
                }
                else { ModelState.AddModelError(string.Empty, "No se encontro el usuario."); }
            } 

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
