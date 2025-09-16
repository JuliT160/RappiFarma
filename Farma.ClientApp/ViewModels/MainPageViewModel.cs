using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Farma.Shared.DTOs;
using Farma.Shared.Enums;

namespace farmaClientApp.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    private string _usuarioNombre = "Usuario";
    private bool _tieneDireccionCompleta = false;
    private int _pedidosPendientes = 0;

    public string UsuarioNombre
    {
        get => _usuarioNombre;
        set
        {
            _usuarioNombre = value;
            OnPropertyChanged();
        }
    }

    public bool TieneDireccionCompleta
    {
        get => _tieneDireccionCompleta;
        set
        {
            _tieneDireccionCompleta = value;
            OnPropertyChanged();
        }
    }

    public int PedidosPendientes
    {
        get => _pedidosPendientes;
        set
        {
            _pedidosPendientes = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(TienePedidosPendientes));
        }
    }

    public bool TienePedidosPendientes => PedidosPendientes > 0;

    public string SaludoTexto => DateTime.Now.Hour switch
    {
        < 12 => $"Buenos días, {UsuarioNombre}",
        < 18 => $"Buenas tardes, {UsuarioNombre}",
        _ => $"Buenas noches, {UsuarioNombre}"
    };

    public ICommand NuevoPedidoCommand { get; }
    public ICommand VerPedidosCommand { get; }
    public ICommand ConfigurarPerfilCommand { get; }
    public ICommand MenuCommand { get; }

    public MainPageViewModel()
    {
        NuevoPedidoCommand = new Command(async () => await NuevoPedidoAsync());
        VerPedidosCommand = new Command(async () => await VerPedidosAsync());
        ConfigurarPerfilCommand = new Command(async () => await ConfigurarPerfilAsync());
        MenuCommand = new Command(async () => await MostrarMenuAsync());

        // Simular datos del usuario
        CargarDatosUsuario();
    }

    private void CargarDatosUsuario()
    {
        // TODO: Cargar datos reales del usuario desde API/Storage
        UsuarioNombre = "María";
        TieneDireccionCompleta = true; // Simular que tiene dirección
        PedidosPendientes = 2; // Simular pedidos pendientes
    }

    private async Task NuevoPedidoAsync()
    {
        if (!TieneDireccionCompleta)
        {
            var resultado = await Shell.Current.DisplayAlert(
                "Configurar Perfil", 
                "Necesitas configurar tu dirección de entrega antes de hacer un pedido.", 
                "Configurar", "Cancelar");
            
            if (resultado)
            {
                await Shell.Current.GoToAsync("configurarperfil");
            }
            return;
        }

        await Shell.Current.GoToAsync("nuevopedido");
    }

    private async Task VerPedidosAsync()
    {
        await Shell.Current.GoToAsync("mispedidos");
    }

    private async Task ConfigurarPerfilAsync()
    {
        await Shell.Current.GoToAsync("configurarperfil");
    }

    private async Task MostrarMenuAsync()
    {
        var opciones = new string[] { "Mi Perfil", "Historial", "Ayuda", "Cerrar Sesión" };
        var seleccion = await Shell.Current.DisplayActionSheet("Menú", "Cancelar", null, opciones);

        switch (seleccion)
        {
            case "Mi Perfil":
                await ConfigurarPerfilAsync();
                break;
            case "Historial":
                await VerPedidosAsync();
                break;
            case "Ayuda":
                await Shell.Current.DisplayAlert("Ayuda", "Para soporte contacta: soporte@rapifarma.com", "OK");
                break;
            case "Cerrar Sesión":
                var confirmar = await Shell.Current.DisplayAlert("Cerrar Sesión", "¿Estás seguro?", "Sí", "No");
                if (confirmar)
                {
                    await Shell.Current.GoToAsync("//login");
                }
                break;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
