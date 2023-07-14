using Inventario.Models.Dominio.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Maui.Modelos.Dtos
{
    public class LoginRespuestaDto
    {
        public AplicationUser Usuario { get; set; }
        public string Token { get; set; }
    }
}
