using Inventario.WebApp.Areas.Autenticacion.Models;
using Inventario.WebApp.Areas.Autenticacion.Servicio;
using Inventario.WebApp.Servicios.IServicio;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Inventario.WebApp.Areas.Autenticacion.Controllers
{
    [Area("Autenticacion")]
    public class CuentaController : Controller
    {
        
        private readonly IServicioDeAutenticacion _servicioDeAutenticacion;
        private readonly IProveedorDeToken _proveedorDeToken;   

        public CuentaController(IServicioDeAutenticacion servicioDeAutenticacion, IProveedorDeToken proveedorDeToken)
        {
            _servicioDeAutenticacion = servicioDeAutenticacion;
            _proveedorDeToken = proveedorDeToken;   
        }

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Registre( RegistroDto registro)
        {
            var resultado = await _servicioDeAutenticacion.Registro(registro);
            return RedirectToAction(nameof(Login));
           

        }


        [HttpGet]  
        public ActionResult Login()
        {
            LoginDto modeloLogin = new();
            return View(modeloLogin);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var respuesta = await _servicioDeAutenticacion.LoginAsync(request);

            if (respuesta != null && respuesta.esSucces)
            {
                RespuestaLoginDto? respuestaLogin =
                    JsonConvert.DeserializeObject<RespuestaLoginDto>(Convert.ToString(respuesta.Respuesta));
                await inicioDesesion(respuestaLogin);
                _proveedorDeToken.EscribirToken(respuestaLogin.Token);

                return RedirectToAction("Index", "Home" , new {area =""});
            }else
            {
                TempData["error"] = respuesta.Mensaje;
                return View(request);
            }


        }


        [HttpGet]
        public async Task<ActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            _proveedorDeToken.LimpiarToken();
            return RedirectToAction("Index", "Home", new { area = "" });
        }




        [HttpGet]
        public ActionResult CambiarClave()
        {
            var Username = User.Identity.Name;
            return View(new CambioDeClaveDto() {NombreUsario = Username });
        }

        [HttpPost]
        public async Task<IActionResult> CambiarClave(CambioDeClaveDto request)
        {
            var respuesta = await _servicioDeAutenticacion.CambiarClave (request);
            if (respuesta.esSucces && respuesta.Mensaje != null)
            {
                await HttpContext.SignOutAsync();
                _proveedorDeToken.LimpiarToken();
                return RedirectToAction("Index", "Home", new { area = "" });

            }
           

            return RedirectToAction("Index", "Home", new { area = "" });

        }




        private async Task inicioDesesion(RespuestaLoginDto login)
        {
            var administrador = new JwtSecurityTokenHandler();
            var jwt = administrador.ReadJwtToken(login.Token);
            var identidad = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identidad.AddClaim(new Claim(JwtRegisteredClaimNames.Email, 
                jwt.Claims.FirstOrDefault(u=> u.Type == JwtRegisteredClaimNames.Email).Value));
            identidad.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identidad.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identidad.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u=> u.Type == JwtRegisteredClaimNames.Name).Value));
            identidad.AddClaim(new Claim(ClaimTypes.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identidad.AddClaim(new Claim(ClaimTypes.NameIdentifier,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));

            var principal = new ClaimsPrincipal(identidad);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        }

    }
}
