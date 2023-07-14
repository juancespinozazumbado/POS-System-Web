
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventario.Maui.Servicios.Iservicios;
using Inventario.Models.Dominio.Productos;
using System.Collections.ObjectModel;

namespace Inventario.Maui.ViewModels
{
    partial class InventarioViewModel :ObservableObject
    {
        public ObservableCollection<Inventarios> Lista_inventarios { set; get; } = new ObservableCollection<Inventarios>();     


        private readonly IServicioDeInventario _servicioDeInventario;


        //public InventarioViewModel(IServicioDeInventario servicioDeInventario)
        //{
        //    _servicioDeInventario = servicioDeInventario;   
        //}

        public InventarioViewModel() => _servicioDeInventario = App.Current.Servicios.GetRequiredService<IServicioDeInventario>();

        [RelayCommand]
        public void ListeLosInventarios()
        {
            SecureStorage.SetAsync("Token", "jaja!");
            //Lista_inventarios = _servicioDeInventario.ListarInventarios();
            
        }
    }
}
