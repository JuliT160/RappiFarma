using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Farma.Shared.DTOs;
using Farma.Shared.Enums;

namespace farmaClientApp.ViewModels;

public class MisPedidosViewModel : INotifyPropertyChanged
{
    private bool _isRefreshing = false;
    private bool _isLoading = true;

    public ObservableCollection<PedidoDto> MisPedidos { get; } = new();

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set
        {
            _isRefreshing = value;
            OnPropertyChanged();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public ICommand RefreshCommand { get; }
    public ICommand VerDetallePedidoCommand { get; }
    public ICommand CancelarPedidoCommand { get; }

    public MisPedidosViewModel()
    {
        RefreshCommand = new Command(async () => await RefreshPedidosAsync());
        VerDetallePedidoCommand = new Command<PedidoDto>(async (pedido) => await VerDetallePedidoAsync(pedido));
        CancelarPedidoCommand = new Command<PedidoDto>(async (pedido) => await CancelarPedidoAsync(pedido));
        
        // Cargar pedidos inicialmente
        Task.Run(async () => await LoadPedidosAsync());
    }

    private async Task LoadPedidosAsync()
    {
        IsLoading = true;
        
        try
        {
            // Simular carga de datos
            await Task.Delay(1200);
            
            // Datos de ejemplo con diferentes estados tipo delivery
            var pedidosEjemplo = new List<PedidoDto>
            {
                new PedidoDto
                {
                    Id = 1,
                    Estado = EstadoPedido.EN_ENTREGA,
                    Calle = "Av. Corrientes",
                    Altura = "1234",
                    Piso = "5",
                    Departamento = "A",
                    Localidad = "CABA",
                    Provincia = "Buenos Aires",
                    CodigoPostal = "1043",
                    FechaCreacion = DateTime.Now.AddHours(-2),
                    UsuarioNombre = "María González",
                    UsuarioEmail = "maria.gonzalez@email.com",
                    Recetas = new List<RecetaResumenDto>
                    {
                        new() { Id = 1, NombreArchivoOriginal = "receta_1.jpg", TipoArchivo = "image/jpeg" }
                    },
                    Cotizaciones = new List<CotizacionDto>
                    {
                        new() { Id = 1, MontoTotal = 2500, FarmaciaNombre = "Farmacia Central" }
                    }
                },
                new PedidoDto
                {
                    Id = 2,
                    Estado = EstadoPedido.EN_PREPARACION,
                    Calle = "San Martín",
                    Altura = "567",
                    Localidad = "Palermo",
                    Provincia = "Buenos Aires",
                    CodigoPostal = "1425",
                    FechaCreacion = DateTime.Now.AddHours(-4),
                    UsuarioNombre = "María González",
                    UsuarioEmail = "maria.gonzalez@email.com",
                    Recetas = new List<RecetaResumenDto>
                    {
                        new() { Id = 2, NombreArchivoOriginal = "receta_2.pdf", TipoArchivo = "application/pdf" }
                    },
                    Cotizaciones = new List<CotizacionDto>
                    {
                        new() { Id = 2, MontoTotal = 1800, FarmaciaNombre = "Farmacia del Barrio" }
                    }
                },
                new PedidoDto
                {
                    Id = 3,
                    Estado = EstadoPedido.COTIZADO,
                    Calle = "Rivadavia",
                    Altura = "890",
                    Localidad = "Flores",
                    Provincia = "Buenos Aires",
                    CodigoPostal = "1406",
                    FechaCreacion = DateTime.Now.AddHours(-6),
                    UsuarioNombre = "María González",
                    UsuarioEmail = "maria.gonzalez@email.com",
                    Recetas = new List<RecetaResumenDto>
                    {
                        new() { Id = 3, NombreArchivoOriginal = "receta_3.jpg", TipoArchivo = "image/jpeg" }
                    },
                    Cotizaciones = new List<CotizacionDto>
                    {
                        new() { Id = 3, MontoTotal = 3200, FarmaciaNombre = "Farmacia Moderna" }
                    }
                },
                new PedidoDto
                {
                    Id = 4,
                    Estado = EstadoPedido.ENTREGADO,
                    Calle = "Belgrano",
                    Altura = "445",
                    Piso = "2",
                    Localidad = "Recoleta",
                    Provincia = "Buenos Aires",
                    CodigoPostal = "1092",
                    FechaCreacion = DateTime.Now.AddDays(-2),
                    UsuarioNombre = "María González",
                    UsuarioEmail = "maria.gonzalez@email.com",
                    Recetas = new List<RecetaResumenDto>
                    {
                        new() { Id = 4, NombreArchivoOriginal = "receta_4.jpg", TipoArchivo = "image/jpeg" }
                    },
                    Cotizaciones = new List<CotizacionDto>
                    {
                        new() { Id = 4, MontoTotal = 1500, FarmaciaNombre = "Farmacia Express" }
                    }
                }
            };

            MainThread.BeginInvokeOnMainThread(() =>
            {
                MisPedidos.Clear();
                foreach (var pedido in pedidosEjemplo.OrderByDescending(p => p.FechaCreacion))
                {
                    MisPedidos.Add(pedido);
                }
            });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "No se pudieron cargar los pedidos", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task RefreshPedidosAsync()
    {
        IsRefreshing = true;
        await LoadPedidosAsync();
        IsRefreshing = false;
    }

    private async Task VerDetallePedidoAsync(PedidoDto pedido)
    {
        if (pedido == null) return;
        
        // TODO: Navegar a página de detalle del pedido
        await Shell.Current.DisplayAlert("Detalle", $"Ver detalles del pedido #{pedido.Id}", "OK");
    }

    private async Task CancelarPedidoAsync(PedidoDto pedido)
    {
        if (pedido == null) return;

        // Solo se pueden cancelar pedidos en ciertos estados
        if (pedido.Estado is not (EstadoPedido.CREADO or EstadoPedido.EN_COTIZACION or EstadoPedido.COTIZADO))
        {
            await Shell.Current.DisplayAlert("No se puede cancelar", 
                "Este pedido ya está en proceso y no puede ser cancelado.", "OK");
            return;
        }

        var result = await Shell.Current.DisplayAlert(
            "Cancelar Pedido", 
            $"¿Está seguro que desea cancelar el pedido #{pedido.Id}?", 
            "Sí", "No");

        if (result)
        {
            // TODO: Cancelar pedido en el backend
            pedido.Estado = EstadoPedido.CANCELADO;
            await Shell.Current.DisplayAlert("Cancelado", "El pedido ha sido cancelado exitosamente.", "OK");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

// Extension methods para mejorar la UI
public static class PedidoExtensions
{
    public static string GetDireccionCompleta(this PedidoDto pedido)
    {
        var direccion = $"{pedido.Calle} {pedido.Altura}";
        if (!string.IsNullOrEmpty(pedido.Piso))
            direccion += $", Piso {pedido.Piso}";
        if (!string.IsNullOrEmpty(pedido.Departamento))
            direccion += $", Depto {pedido.Departamento}";
        return direccion;
    }
}