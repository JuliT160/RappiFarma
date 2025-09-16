using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Farma.Shared.DTOs;

namespace farmaClientApp.ViewModels;

public class ClienteLoginViewModel : INotifyPropertyChanged
{
    private string _email = string.Empty;
    private string _password = string.Empty;
    private bool _isLoading = false;
    private string _errorMessage = string.Empty;
    private bool _rememberMe = false;

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
            ErrorMessage = string.Empty;
            (LoginCommand as Command)?.ChangeCanExecute();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
            ErrorMessage = string.Empty;
            (LoginCommand as Command)?.ChangeCanExecute();
        }
    }

    public bool RememberMe
    {
        get => _rememberMe;
        set
        {
            _rememberMe = value;
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
            (LoginCommand as Command)?.ChangeCanExecute();
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoginCommand { get; private set; }
    public ICommand GoogleLoginCommand { get; private set; }
    public ICommand RegisterCommand { get; private set; }
    public ICommand ForgotPasswordCommand { get; private set; }

    public ClienteLoginViewModel()
    {
        LoginCommand = new Command(async () => await LoginAsync(), () => CanLogin());
        GoogleLoginCommand = new Command(async () => await GoogleLoginAsync());
        RegisterCommand = new Command(async () => await NavigateToRegisterAsync());
        ForgotPasswordCommand = new Command(async () => await ForgotPasswordAsync());
    }

    private bool CanLogin()
    {
        return !IsLoading && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
    }

    private async Task LoginAsync()
    {
        if (!CanLogin()) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            // Simular validación de login
            await Task.Delay(1500); // Simular llamada a API

            // Validación simple para demostración
            if (Email.Contains("@") && Password.Length >= 6)
            {
                // Login exitoso - navegar a la página principal
                await Shell.Current.GoToAsync("//main");
            }
            else
            {
                ErrorMessage = "Credenciales inválidas. Verifique su email y contraseña.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error de conexión. Intente nuevamente.";
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task GoogleLoginAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            // TODO: Implementar Google Sign-In cuando esté disponible
            await Shell.Current.DisplayAlert("Próximamente", "Google Sign-In estará disponible pronto", "OK");
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error con Google Sign-In. Intente nuevamente.";
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task NavigateToRegisterAsync()
    {
        await Shell.Current.GoToAsync("register");
    }

    private async Task ForgotPasswordAsync()
    {
        await Shell.Current.DisplayAlert("Recuperar Contraseña", "Se enviará un email con instrucciones para recuperar su contraseña.", "OK");
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
