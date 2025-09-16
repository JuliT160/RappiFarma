using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Farma.Shared.DTOs;

namespace farmaClientApp.ViewModels;

public class ConfigurarPerfilViewModel : INotifyPropertyChanged
{
    private string _nombre = string.Empty;
    private string _email = string.Empty;
    private string _telefono = string.Empty;
    private string _calle = string.Empty;
    private string _altura = string.Empty;
    private string _piso = string.Empty;
    private string _departamento = string.Empty;
    private string _localidad = string.Empty;
    private string _provincia = string.Empty;
    private string _codigoPostal = string.Empty;
    private string _obraSocialNombre = string.Empty;
    private string _obraSocialNumero = string.Empty;
    private bool _isLoading = false;

    public string Nombre
    {
        get => _nombre;
        set { _nombre = value; OnPropertyChanged(); }
    }

    public string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged(); }
    }

    public string Telefono
    {
        get => _telefono;
        set { _telefono = value; OnPropertyChanged(); }
    }

    public string Calle
    {
        get => _calle;
        set { _calle = value; OnPropertyChanged(); }
    }

    public string Altura
    {
        get => _altura;
        set { _altura = value; OnPropertyChanged(); }
    }

    public string Piso
    {
        get => _piso;
        set { _piso = value; OnPropertyChanged(); }
    }

    public string Departamento
    {
        get => _departamento;
        set { _departamento = value; OnPropertyChanged(); }
    }

    public string Localidad
    {
        get => _localidad;
        set { _localidad = value; OnPropertyChanged(); }
    }

    public string Provincia
    {
        get => _provincia;
        set { _provincia = value; OnPropertyChanged(); }
    }

    public string CodigoPostal
    {
        get => _codigoPostal;
        set { _codigoPostal = value; OnPropertyChanged(); }
    }

    public string ObraSocialNombre
    {
        get => _obraSocialNombre;
        set { _obraSocialNombre = value; OnPropertyChanged(); }
    }

    public string ObraSocialNumero
    {
        get => _obraSocialNumero;
        set { _obraSocialNumero = value; OnPropertyChanged(); }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set { _isLoading = value; OnPropertyChanged(); }
    }

    public ICommand GuardarCommand { get; }
    public ICommand ObtenerUbicacionCommand { get; }

    public ConfigurarPerfilViewModel()
    {
        GuardarCommand = new Command(async () => await GuardarAsync());
        ObtenerUbicacionCommand = new Command(async () => await ObtenerUbicacionAsync());
        
        // Cargar datos existentes
        CargarDatosUsuario();
    }

    private void CargarDatosUsuario()
    {
        // TODO: Cargar datos reales del usuario
        Nombre = "María González";
        Email = "maria.gonzalez@email.com";
        Telefono = "11-1234-5678";
        Calle = "Av. Corrientes";
        Altura = "1234";
        Piso = "5";
        Departamento = "A";
        Localidad = "CABA";
        Provincia = "Buenos Aires";
        CodigoPostal = "1043";
        ObraSocialNombre = "OSDE";
        ObraSocialNumero = "123456789";
    }

    private async Task GuardarAsync()
    {
        IsLoading = true;
        
        try
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Email))
            {
                await Shell.Current.DisplayAlert("Error", "Nombre y email son obligatorios", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Calle) || string.IsNullOrWhiteSpace(Altura))
            {
                await Shell.Current.DisplayAlert("Error", "Dirección completa es obligatoria para recibir pedidos", "OK");
                return;
            }

            // Simular guardado
            await Task.Delay(1500);

            // TODO: Guardar datos reales en API
            
            await Shell.Current.DisplayAlert("Éxito", "Perfil actualizado correctamente", "OK");
            await Shell.Current.GoToAsync("//main");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo guardar el perfil", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task ObtenerUbicacionAsync()
    {
        try
        {
            // TODO: Implementar geolocalización real
            await Shell.Current.DisplayAlert("Ubicación", "Funcionalidad de geolocalización próximamente disponible", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo obtener la ubicación", "OK");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
