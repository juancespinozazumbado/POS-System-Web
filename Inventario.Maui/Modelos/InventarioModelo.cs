
namespace Inventario.Maui.Modelos
{
    public class InventarioModelo
    {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public Categoria Categoria { get; set; }
            public int Cantidad { get; set; }
            public decimal Precio { get; set; }

        
    }

    public enum Categoria
    {
        A = 1,
        B = 2,
        C = 3
    }
}
