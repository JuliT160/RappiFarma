using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Media;

namespace farmaClientApp.ViewModels;

public class NuevoPedidoViewModel : INotifyPropertyChanged
{
    private List<string> _fotosRecetas = new();
    private string _observaciones = string.Empty;
    private bool _consentimientoAceptado = false;
    private bool _isLoading = false;
    private bool _mostrarConsentimiento = false;

    public List<string> FotosRecetas
    {
        get => _fotosRecetas;
        set
        {
            _fotosRecetas = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(TieneFotos));
            OnPropertyChanged(nameof(CantidadFotos));
            (EnviarPedidoCommand as Command)?.ChangeCanExecute();
        }
    }

    public string Observaciones
    {
        get => _observaciones;
        set
        {
            _observaciones = value;
            OnPropertyChanged();
        }
    }

    public bool ConsentimientoAceptado
    {
        get => _consentimientoAceptado;
        set
        {
            _consentimientoAceptado = value;
            OnPropertyChanged();
            (EnviarPedidoCommand as Command)?.ChangeCanExecute();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
            (EnviarPedidoCommand as Command)?.ChangeCanExecute();
        }
    }

    public bool MostrarConsentimiento
    {
        get => _mostrarConsentimiento;
        set
        {
            _mostrarConsentimiento = value;
            OnPropertyChanged();
        }
    }

    public bool TieneFotos => FotosRecetas.Count > 0;
    public string CantidadFotos => FotosRecetas.Count == 1 ? "1 receta" : $"{FotosRecetas.Count} recetas";

    public ICommand TomarFotoCommand { get; }
    public ICommand SeleccionarFotoCommand { get; }
    public ICommand EliminarFotoCommand { get; }
    public ICommand MostrarConsentimientoCommand { get; }
    public ICommand EnviarPedidoCommand { get; }

    public NuevoPedidoViewModel()
    {
        TomarFotoCommand = new Command(async () => await TomarFotoAsync());
        SeleccionarFotoCommand = new Command(async () => await SeleccionarFotoAsync());
        EliminarFotoCommand = new Command<string>(EliminarFoto);
        MostrarConsentimientoCommand = new Command(() => MostrarConsentimiento = true);
        EnviarPedidoCommand = new Command(async () => await EnviarPedidoAsync(), () => PuedeEnviar());
    }

    private bool PuedeEnviar()
    {
        return !IsLoading && TieneFotos && ConsentimientoAceptado;
    }

    private async Task TomarFotoAsync()
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var foto = await MediaPicker.Default.CapturePhotoAsync();
                if (foto != null)
                {
                    var nuevaLista = new List<string>(FotosRecetas) { foto.FullPath };
                    FotosRecetas = nuevaLista;
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "La cámara no está disponible en este dispositivo", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo tomar la foto. Intenta nuevamente.", "OK");
        }
    }

    private async Task SeleccionarFotoAsync()
    {
        try
        {
            var foto = await MediaPicker.Default.PickPhotoAsync();
            if (foto != null)
            {
                var nuevaLista = new List<string>(FotosRecetas) { foto.FullPath };
                FotosRecetas = nuevaLista;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo seleccionar la foto. Intenta nuevamente.", "OK");
        }
    }

    private void EliminarFoto(string rutaFoto)
    {
        if (!string.IsNullOrEmpty(rutaFoto))
        {
            var nuevaLista = new List<string>(FotosRecetas);
            nuevaLista.Remove(rutaFoto);
            FotosRecetas = nuevaLista;
        }
    }

    private async Task EnviarPedidoAsync()
    {
        if (!PuedeEnviar()) return;

        IsLoading = true;

        try
        {
            // Simular envío del pedido
            await Task.Delay(2000);

            // TODO: Enviar pedido real al backend
            // - Subir fotos
            // - Crear pedido con observaciones
            // - Obtener ID del pedido

            await Shell.Current.DisplayAlert("¡Pedido Enviado!", 
                "Tu pedido ha sido enviado exitosamente. Las farmacias cercanas lo revisarán y te enviarán cotizaciones.", 
                "OK");

            // Navegar a seguimiento de pedidos
            await Shell.Current.GoToAsync("//main");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo enviar el pedido. Intenta nuevamente.", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
