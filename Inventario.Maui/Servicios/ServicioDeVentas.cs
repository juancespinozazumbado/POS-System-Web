using Inventario.Maui.Modelos;
using Inventario.Maui.Modelos.Dtos;
using Inventario.Maui.Servicios.Iservicios;
using Inventario.Models.Dominio.Ventas;
using Newtonsoft.Json;
using static Inventario.Maui.Modelos.ConfiguracionApi;

namespace Inventario.Maui.Servicios
{
    internal class ServicioDeVentas : IservicioDeVentas
    {
        private readonly IServicioBase _servicioBase;
        public ServicioDeVentas() {

            _servicioBase = App.Current.Servicios.GetRequiredService<IServicioBase>();      
        }   
        public async Task<List<AperturaDeCaja>> AperturasDeCajaPorUsuario(string idUsuario)
        {
            var resultado = await _servicioBase.SendAsync(new ConsultaRest()
            {
                MetodoRest = MetodoREST.GET,
                TipoDeContenido = TipoDeContenido.Json,
                URL = ConfiguracionApi.API_URL + $"/AperturasDeCaja/usuario/cajas?Id_Usuario={idUsuario}",

            });
            if (resultado != null && resultado.esSucces)
            {
                var cajas = JsonConvert.DeserializeObject<List<AperturaDeCaja>>(Convert.ToString(resultado?.Respuesta));
                return cajas == null ? null : (List<AperturaDeCaja>)cajas;
            }

            throw new NotImplementedException();
        }
    }
}
