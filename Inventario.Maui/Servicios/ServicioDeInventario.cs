using Inventario.Maui.Modelos;
using Inventario.Maui.Modelos.Dtos;
using Inventario.Maui.Servicios.Iservicios;
using Inventario.Models.Dominio.Productos;
using Newtonsoft.Json;
using static Inventario.Maui.Modelos.ConfiguracionApi;

namespace Inventario.Maui.Servicios
{

    public class ServicioDeInventario : IServicioDeInventario
    {
        private readonly IServicioBase _servicioBase;
        public ServicioDeInventario()
        {

            _servicioBase = App.Current.Servicios.GetRequiredService<IServicioBase>();
        }


        Task<Inventarios> IServicioDeInventario.InvenatrioPorId(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<Inventarios>> IServicioDeInventario.InventariosPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InventarioModelo>> ListarInventarios()
        {
            var respuesta = await _servicioBase.SendAsync(new ConsultaRest()
            {
                MetodoRest = MetodoREST.GET,
                URL = ConfiguracionApi.API_URL + $"/Inventarios",
                TipoDeContenido = TipoDeContenido.Json

            });

            var inventario = JsonConvert.DeserializeObject<List<InventarioModelo>>(Convert.ToString(respuesta.Respuesta));
            return inventario == null ? null : (List<InventarioModelo>)inventario;
        }
    }
}
