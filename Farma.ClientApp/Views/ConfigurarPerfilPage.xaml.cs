using farmaClientApp.ViewModels;

namespace farmaClientApp.Views;

public partial class ConfigurarPerfilPage : ContentPage
{
    public ConfigurarPerfilPage()
    {
        InitializeComponent();
        BindingContext = new ConfigurarPerfilViewModel();
    }
}
