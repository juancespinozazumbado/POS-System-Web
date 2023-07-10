namespace Inventario.SI.Modelos.Dtos.Ventas
{
    public class QuitarItemDeVentaRequest
    {
        public int Id_venta { get; set; }
        public int Id_caja { get; set; }
        public string id_Usuario { get; set; }
        public int id_Item { set; get; }
    }
}
