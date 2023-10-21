namespace Inventario.SI.Modelos.Dtos.Ventas
{
    public class AgregarItemDeVenatRequest
    {

        public int Id_caja { get; set; }
        public int Id_venta { get; set; }
        public string id_Usuario { get; set; }
        public int Id_Inventario { get; set; }
        public int cantidad { get; set; }
    }
}
