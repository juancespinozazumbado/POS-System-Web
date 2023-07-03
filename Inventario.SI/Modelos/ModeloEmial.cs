namespace Inventario.SI.Modelos
{
    public class ModeloEmial
    {
        public string Titulo { set; get; } 
        public string Cuerpo { set; get; }  
        public string Destinatario { set; get; }

        public string Emisor => "comerciosistema@outlook.com";
        public string Password => "OdiN.7072";
    }
}
