
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventario.Maui.Modelos;
using Inventario.Maui.Modelos.Ventas;
using Inventario.Maui.Servicios.Iservicios;

using System.Collections.ObjectModel;

namespace Inventario.Maui.ViewModels
{
    partial class VentasViewModel : ObservableObject
    {
        public ObservableCollection<ModeloDeVentas> Lista_Ventas { set; get; } = new ObservableCollection<ModeloDeVentas>();
       
        private readonly IservicioDeVentas _servicioDeVentas;

        public VentasViewModel() => _servicioDeVentas = App.Current.Servicios.GetRequiredService<IservicioDeVentas>();

        [RelayCommand]
        public async void ListarVentas()
        {
            Lista_Ventas.Clear();

            var Id_usuario = await SecureStorage.Default.GetAsync("Id_Usuario");
            var nombredeUsario = await SecureStorage.Default.GetAsync("Nombre_Usuario");

            var Cajas = await _servicioDeVentas.AperturasDeCajaPorUsuario(Id_usuario);
            if (Cajas != null)
            {
                Cajas.ForEach(c =>
                {
                    var Venta = new ModeloDeVentas()
                    {
                        FechaDeApertura = c.FechaDeInicio,
                        FechaDeCierre = c.FechaDeCierre,
                        Usuario = nombredeUsario,
                        TotalDeVentas = c.Ventas.Count(),
                        TotalCaja = c.Ventas.Sum(v => v.Total),
                        TotalPorEfectivo = c.Ventas.Where(v=> v.TipoDePago == TipoDePago.Efectivo).ToList().Sum(v=>v.Total),
                        TotalPorTarjeta = c.Ventas.Where(v => v.TipoDePago == TipoDePago.Tarjeta).ToList().Sum(v => v.Total),
                        TotalPorSinpe = c.Ventas.Where(v => v.TipoDePago == TipoDePago.SinpeMovil).ToList().Sum(v => v.Total)
                    };
                    Lista_Ventas.Add(Venta);
                });

            }

            if (Lista_Ventas != null)
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
