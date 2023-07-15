using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Maui.Modelos
{
    public class ModeloDeVentas
    {
        public DateTime FechaDeApertura { get; set; }
        public DateTime? FechaDeCierre { get; set; }

        public decimal TotalCaja { get; set; }
        public int TotalDeVentas { get; set; }  

        public string Usuario { get; set; }

        public decimal TotalPorSinpe { get; set; }
        public decimal TotalPorTarjeta { get; set; }
        public decimal TotalPorEfectivo { get; set; }



    }
}
