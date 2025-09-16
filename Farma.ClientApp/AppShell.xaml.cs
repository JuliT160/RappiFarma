using farmaClientApp.Views;

namespace farmaClientApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            // Registrar rutas para navegación
            Routing.RegisterRoute("login", typeof(ClienteLoginPage));
            Routing.RegisterRoute("nuevopedido", typeof(NuevoPedidoPage));
            Routing.RegisterRoute("mispedidos", typeof(MisPedidosPage));
            Routing.RegisterRoute("configurarperfil", typeof(ConfigurarPerfilPage));
        }
    }
}
