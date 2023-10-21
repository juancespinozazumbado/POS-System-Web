namespace Inventario.SI.Modelos.Dtos.Ventas
{
    public class CrearVentaRequest
    {
        public string Id_Usuario { get; set; }  
        public int Id_caja { get; set; }        
        public string Cliente { get; set; }     
    }
}
