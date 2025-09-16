using farmaClientApp.ViewModels;

namespace farmaClientApp.Views;

public partial class MisPedidosPage : ContentPage
{
    public MisPedidosPage()
    {
        InitializeComponent();
        BindingContext = new MisPedidosViewModel();
    }

    private async void OnNuevoPedidoClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//main");
    }
}
