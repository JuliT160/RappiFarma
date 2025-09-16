using farmaClientApp.ViewModels;

namespace farmaClientApp.Views;

public partial class ClienteLoginPage : ContentPage
{
    public ClienteLoginPage()
    {
        InitializeComponent();
        BindingContext = new ClienteLoginViewModel();
    }
}
