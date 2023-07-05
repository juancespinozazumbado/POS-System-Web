using Inventario.Models.Dominio.Usuarios;
using Inventario.SI.Modelos;
using Inventario.SI.Servicios.Autenticacion.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inventario.SI.Servicios.Autenticacion
{
    public class ServicioDeJWT : IServicioDeJWT
    {
        private readonly UserManager<AplicationUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        public ServicioDeJWT(UserManager<AplicationUser> userManager, IOptions<JwtOptions>  jwtOptions )
        {
            _userManager = userManager; 
            _jwtOptions = jwtOptions.Value;
        }
        public  string GenerarToken(AplicationUser usuario)
        {

            var ProvedorDeToken = new JwtSecurityTokenHandler();

            var Llave = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var ListaDeAclmaciones = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,usuario.Email),
                new Claim(JwtRegisteredClaimNames.Sub,usuario.Id),
                new Claim(JwtRegisteredClaimNames.Name,usuario.UserName)
            };


            var DescriptorDeToken = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(ListaDeAclmaciones),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Llave),
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var token = ProvedorDeToken.CreateToken(DescriptorDeToken);
            return ProvedorDeToken.WriteToken(token);
            
        }

        
    }
}
