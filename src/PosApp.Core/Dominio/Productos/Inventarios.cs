namespace Inventario.Models.Dominio.Productos
{

    public class Inventarios
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public Categoria Categoria { get; set; }

        public int Cantidad { get; set; }


        public decimal Precio { get; set; }

        public List<AjusteDeInventario> Ajustes { get; set; }

        public Inventarios()
        {
            Ajustes = new();
        }

    }
}
