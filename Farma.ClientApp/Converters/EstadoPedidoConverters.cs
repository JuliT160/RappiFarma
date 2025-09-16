using System.Globalization;
using Farma.Shared.Enums;

namespace farmaClientApp.Converters;

public class EstadoToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is EstadoPedido estado)
        {
            return estado switch
            {
                EstadoPedido.CREADO => "Buscando farmacia",
                EstadoPedido.EN_COTIZACION => "Pendiente de aceptar",
                EstadoPedido.COTIZADO => "Cotización recibida",
                EstadoPedido.CONFIRMADO => "Confirmado",
                EstadoPedido.EN_PREPARACION => "En preparación",
                EstadoPedido.LISTO_PARA_ENTREGA => "Listo para envío",
                EstadoPedido.EN_ENTREGA => "En camino",
                EstadoPedido.ENTREGADO => "Recibido",
                EstadoPedido.CANCELADO => "Cancelado",
                _ => "Estado desconocido"
            };
        }
        return "Estado desconocido";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class EstadoToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is EstadoPedido estado)
        {
            return estado switch
            {
                EstadoPedido.CREADO => Colors.Orange,
                EstadoPedido.EN_COTIZACION => Colors.Blue,
                EstadoPedido.COTIZADO => Colors.Green,
                EstadoPedido.CONFIRMADO => Colors.Green,
                EstadoPedido.EN_PREPARACION => Colors.Purple,
                EstadoPedido.LISTO_PARA_ENTREGA => Colors.DarkGreen,
                EstadoPedido.EN_ENTREGA => Colors.DarkBlue,
                EstadoPedido.ENTREGADO => Colors.Gray,
                EstadoPedido.CANCELADO => Colors.Red,
                _ => Colors.Gray
            };
        }
        return Colors.Gray;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class EstadoToIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is EstadoPedido estado)
        {
            return estado switch
            {
                EstadoPedido.CREADO => "🔍",
                EstadoPedido.EN_COTIZACION => "⏳",
                EstadoPedido.COTIZADO => "💰",
                EstadoPedido.CONFIRMADO => "✅",
                EstadoPedido.EN_PREPARACION => "⚗️",
                EstadoPedido.LISTO_PARA_ENTREGA => "📦",
                EstadoPedido.EN_ENTREGA => "🚚",
                EstadoPedido.ENTREGADO => "✅",
                EstadoPedido.CANCELADO => "❌",
                _ => "❓"
            };
        }
        return "❓";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class EstadoToProgressConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is EstadoPedido estado)
        {
            return estado switch
            {
                EstadoPedido.CREADO => 0.1,
                EstadoPedido.EN_COTIZACION => 0.2,
                EstadoPedido.COTIZADO => 0.4,
                EstadoPedido.CONFIRMADO => 0.5,
                EstadoPedido.EN_PREPARACION => 0.7,
                EstadoPedido.LISTO_PARA_ENTREGA => 0.8,
                EstadoPedido.EN_ENTREGA => 0.9,
                EstadoPedido.ENTREGADO => 1.0,
                EstadoPedido.CANCELADO => 0.0,
                _ => 0.0
            };
        }
        return 0.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class EstadoIsActiveConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is EstadoPedido estado)
        {
            return estado is not (EstadoPedido.ENTREGADO or EstadoPedido.CANCELADO);
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class EstadoCanCancelConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is EstadoPedido estado)
        {
            return estado is EstadoPedido.CREADO or EstadoPedido.EN_COTIZACION or EstadoPedido.COTIZADO;
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class CountToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int count)
        {
            return count > 0;
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class PluralConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int count)
        {
            return count == 1 ? "" : "s";
        }
        return "s";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
