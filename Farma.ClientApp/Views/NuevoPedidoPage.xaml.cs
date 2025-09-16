using farmaClientApp.ViewModels;

namespace farmaClientApp.Views;

public partial class NuevoPedidoPage : ContentPage
{
    public NuevoPedidoPage()
    {
        InitializeComponent();
        BindingContext = new NuevoPedidoViewModel();
    }
}
