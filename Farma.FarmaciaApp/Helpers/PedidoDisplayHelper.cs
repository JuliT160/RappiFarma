using Farma.Shared.Enums;
using Farma.Shared.Utils;
using MudBlazor;

namespace Farma.FarmaciaApp.Helpers;

public static class PedidoDisplayHelper
{
    public static string ToFriendlyText(this EstadoPedido estado) => FormatUtils.FormatEstadoPedido(estado);

    public static Color ToChipColor(this EstadoPedido estado) => estado switch
    {
        EstadoPedido.CREADO => Color.Info,
        EstadoPedido.EN_COTIZACION => Color.Warning,
        EstadoPedido.COTIZADO => Color.Secondary,
        EstadoPedido.CONFIRMADO => Color.Success,
        EstadoPedido.EN_PREPARACION => Color.Info,
        EstadoPedido.LISTO_PARA_ENTREGA => Color.Primary,
        EstadoPedido.EN_ENTREGA => Color.Primary,
        EstadoPedido.ENTREGADO => Color.Success,
        EstadoPedido.CANCELADO => Color.Error,
        _ => Color.Default
    };

    public static string ToEstadoIcon(this EstadoPedido estado) => estado switch
    {
        EstadoPedido.CREADO => Icons.Material.Filled.Search,
        EstadoPedido.EN_COTIZACION => Icons.Material.Filled.Schedule,
        EstadoPedido.COTIZADO => Icons.Material.Filled.RequestQuote,
        EstadoPedido.CONFIRMADO => Icons.Material.Filled.CheckCircle,
        EstadoPedido.EN_PREPARACION => Icons.Material.Filled.LocalPharmacy,
        EstadoPedido.LISTO_PARA_ENTREGA => Icons.Material.Filled.Inventory,
        EstadoPedido.EN_ENTREGA => Icons.Material.Filled.LocalShipping,
        EstadoPedido.ENTREGADO => Icons.Material.Filled.Verified,
        EstadoPedido.CANCELADO => Icons.Material.Filled.Cancel,
        _ => Icons.Material.Filled.Info
    };

    public static int ToProgressPercent(this EstadoPedido estado) => estado switch
    {
        EstadoPedido.CREADO => 10,
        EstadoPedido.EN_COTIZACION => 25,
        EstadoPedido.COTIZADO => 40,
        EstadoPedido.CONFIRMADO => 55,
        EstadoPedido.EN_PREPARACION => 70,
        EstadoPedido.LISTO_PARA_ENTREGA => 85,
        EstadoPedido.EN_ENTREGA => 95,
        EstadoPedido.ENTREGADO => 100,
        _ => 0
    };
}
