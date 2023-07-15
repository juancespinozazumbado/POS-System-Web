
using Inventario.Maui.Modelos;
using Inventario.Maui.Modelos.Dtos;
using Inventario.Maui.Servicios.Iservicios;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using static Inventario.Maui.Modelos.ConfiguracionApi;

namespace Inventario.Maui.Servicios
{
    public class ServicioBase : IServicioBase   
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IProveedorDeToken _proveedorDeToken;

        public ServicioBase(IHttpClientFactory HttpClientFactory, IProveedorDeToken proveedorToker)
        {
            _httpClientFactory = HttpClientFactory;
            _proveedorDeToken = proveedorToker;
        }
        public async Task<RespuestaRest?> SendAsync(ConsultaRest requestDto, bool conBearer = true)
        {
            try
            {
                HttpClient clienteHttp = _httpClientFactory.CreateClient();
                HttpRequestMessage mensaje = new();

                if (requestDto.TipoDeContenido == TipoDeContenido.Json)
                {
                    mensaje.Headers.Add("Accept", "application/json");

                }
                if (conBearer)
                {
                    var token = await  _proveedorDeToken.ObtenerToken();
                    if (token != null)
                    {
                        mensaje.Headers.Add("Authorization", $"Bearer {token}");
                    }
                }

                string url = requestDto.URL == null ? "" : requestDto.URL;
                mensaje.RequestUri = new Uri(url);

                if (requestDto.Cuerpo != null)
                {
                    mensaje.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Cuerpo),
                        Encoding.UTF8, "application/json");
                }

                HttpResponseMessage respuestaApi = new();

                switch (requestDto.MetodoRest)
                {
                    case MetodoREST.POST:
                        mensaje.Method = HttpMethod.Post;
                        break;
                    case MetodoREST.DELETE:
                        mensaje.Method = HttpMethod.Delete;
                        break;
                    case MetodoREST.PUT:
                        mensaje.Method = HttpMethod.Put;
                        break;
                    default:
                        mensaje.Method = HttpMethod.Get;
                        break;
                }

                respuestaApi = await clienteHttp.SendAsync(mensaje);

                switch (respuestaApi.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { esSucces = false, Mensaje = "Not Found : 404" };
                    case HttpStatusCode.Forbidden:
                        return new() { esSucces = false, Mensaje = "Access Denied : 403" };
                    case HttpStatusCode.Unauthorized:
                        return new() { esSucces = false, Mensaje = "Unauthorized : 401" };
                    case HttpStatusCode.InternalServerError:
                        return new() { esSucces = false, Mensaje = "Internal Server Error : 500" };
                    case HttpStatusCode.BadRequest:
                        return new() { esSucces = false, Mensaje = "Bad Request: 400" };
                    default:
                        var apiContent = await respuestaApi.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<RespuestaRest>(apiContent);
                        return apiResponseDto;

                }
            }
            catch (Exception ex)
            {
                return new() { esSucces = false, Mensaje = ex.Message };
            }

        }
    }
}
