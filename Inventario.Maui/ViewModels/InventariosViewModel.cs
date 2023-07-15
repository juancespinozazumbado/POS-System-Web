
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventario.Maui.Modelos;
using Inventario.Maui.Servicios.Iservicios;

using System.Collections.ObjectModel;

namespace Inventario.Maui.ViewModels
{
    partial class InventariosViewModel :ObservableObject
    {
        public ObservableCollection<InventarioModelo> Lista_inventarios { set; get; } = new ObservableCollection<InventarioModelo>();
       
        private readonly IServicioDeInventario _servicioDeInventario;


        //public InventarioViewModel(IServicioDeInventario servicioDeInventario)
        //{
        //    _servicioDeInventario = servicioDeInventario;   
        //}

        public InventariosViewModel() => _servicioDeInventario = App.Current.Servicios.GetRequiredService<IServicioDeInventario>();

        [RelayCommand]
        public async void ListarInventarios()
        {
            Lista_inventarios.Clear();

            var inventarios = await _servicioDeInventario.ListarInventarios();
            if (inventarios!= null)
            {
                inventarios.ForEach(i => Lista_inventarios.Add(i));

            }

            if (Lista_inventarios != null)
            {

            }
            
            
        }

        [RelayCommand]
        public async void Atras()
        {
            await Shell.Current.GoToAsync($"{nameof(Views.Menu)}");
        }



    }
}
