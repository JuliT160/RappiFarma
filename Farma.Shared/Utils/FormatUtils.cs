using Farma.Shared.Enums;

namespace Farma.Shared.Utils;

public static class FormatUtils
{
    /// <summary>
    /// Formatea un tamaño de archivo en bytes a una representación legible
    /// </summary>
    /// <param name="bytes">Tamaño en bytes</param>
    /// <returns>Tamaño formateado (ej: "1.5 MB")</returns>
    public static string FormatFileSize(long bytes)
    {
        if (bytes == 0) return "0 B";
        
        string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
        int counter = 0;
        decimal number = bytes;
        
        while (Math.Round(number / 1024) >= 1)
        {
            number /= 1024;
            counter++;
        }
        
        return $"{number:n1} {suffixes[counter]}";
    }
    
    /// <summary>
    /// Formatea un monto de dinero
    /// </summary>
    /// <param name="amount">Monto</param>
    /// <param name="currency">Moneda</param>
    /// <returns>Monto formateado</returns>
    public static string FormatCurrency(decimal amount, string currency = "ARS")
    {
        var symbol = currency switch
        {
            "ARS" => "$",
            "USD" => "US$",
            "EUR" => "€",
            _ => currency
        };
        
        return $"{symbol} {amount:N2}";
    }
    
    /// <summary>
    /// Formatea una fecha de manera amigable
    /// </summary>
    /// <param name="date">Fecha</param>
    /// <returns>Fecha formateada</returns>
    public static string FormatFriendlyDate(DateTime date)
    {
        var now = DateTime.UtcNow;
        var diff = now - date;
        
        if (diff.TotalMinutes < 1)
            return "Hace un momento";
        if (diff.TotalMinutes < 60)
            return $"Hace {(int)diff.TotalMinutes} minutos";
        if (diff.TotalHours < 24)
            return $"Hace {(int)diff.TotalHours} horas";
        if (diff.TotalDays < 7)
            return $"Hace {(int)diff.TotalDays} días";
        if (diff.TotalDays < 30)
            return $"Hace {(int)(diff.TotalDays / 7)} semanas";
        if (diff.TotalDays < 365)
            return $"Hace {(int)(diff.TotalDays / 30)} meses";
        
        return $"Hace {(int)(diff.TotalDays / 365)} años";
    }
    
    /// <summary>
    /// Formatea una distancia en kilómetros
    /// </summary>
    /// <param name="distanceKm">Distancia en kilómetros</param>
    /// <returns>Distancia formateada</returns>
    public static string FormatDistance(double distanceKm)
    {
        if (distanceKm < 1)
            return $"{(int)(distanceKm * 1000)} m";
        
        return $"{distanceKm:F1} km";
    }
    
    /// <summary>
    /// Formatea un estado de pedido para mostrar
    /// </summary>
    /// <param name="estado">Estado del pedido</param>
    /// <returns>Estado formateado</returns>
    public static string FormatEstadoPedido(EstadoPedido estado)
    {
        return estado switch
        {
            EstadoPedido.CREADO => "Creado",
            EstadoPedido.EN_COTIZACION => "En Cotización",
            EstadoPedido.COTIZADO => "Cotizado",
            EstadoPedido.CONFIRMADO => "Confirmado",
            EstadoPedido.EN_PREPARACION => "En Preparación",
            EstadoPedido.LISTO_PARA_ENTREGA => "Listo para Entrega",
            EstadoPedido.EN_ENTREGA => "En Entrega",
            EstadoPedido.ENTREGADO => "Entregado",
            EstadoPedido.CANCELADO => "Cancelado",
            _ => estado.ToString()
        };
    }
    
    /// <summary>
    /// Formatea un estado de cotización para mostrar
    /// </summary>
    /// <param name="estado">Estado de la cotización</param>
    /// <returns>Estado formateado</returns>
    public static string FormatEstadoCotizacion(EstadoCotizacion estado)
    {
        return estado switch
        {
            EstadoCotizacion.PENDIENTE => "Pendiente",
            EstadoCotizacion.ENVIADA => "Enviada",
            EstadoCotizacion.ACEPTADA => "Aceptada",
            EstadoCotizacion.RECHAZADA => "Rechazada",
            EstadoCotizacion.EXPIRADA => "Expirada",
            _ => estado.ToString()
        };
    }
    
    /// <summary>
    /// Formatea un estado de pago para mostrar
    /// </summary>
    /// <param name="estado">Estado del pago</param>
    /// <returns>Estado formateado</returns>
    public static string FormatEstadoPago(EstadoPago estado)
    {
        return estado switch
        {
            EstadoPago.PENDIENTE => "Pendiente",
            EstadoPago.PROCESANDO => "Procesando",
            EstadoPago.COMPLETADO => "Completado",
            EstadoPago.FALLIDO => "Fallido",
            EstadoPago.CANCELADO => "Cancelado",
            EstadoPago.REEMBOLSADO => "Reembolsado",
            _ => estado.ToString()
        };
    }
    
    /// <summary>
    /// Formatea una dirección completa
    /// </summary>
    /// <param name="calle">Calle</param>
    /// <param name="altura">Altura</param>
    /// <param name="piso">Piso (opcional)</param>
    /// <param name="departamento">Departamento (opcional)</param>
    /// <param name="localidad">Localidad</param>
    /// <param name="provincia">Provincia</param>
    /// <param name="codigoPostal">Código postal</param>
    /// <returns>Dirección formateada</returns>
    public static string FormatAddress(string calle, string altura, string? piso, string? departamento, 
        string localidad, string provincia, string codigoPostal)
    {
        var address = $"{calle} {altura}";
        
        if (!string.IsNullOrEmpty(piso))
            address += $", Piso {piso}";
            
        if (!string.IsNullOrEmpty(departamento))
            address += $", Depto {departamento}";
            
        address += $", {localidad}, {provincia} ({codigoPostal})";
        
        return address;
    }
    
    /// <summary>
    /// Trunca un texto a una longitud específica agregando "..."
    /// </summary>
    /// <param name="text">Texto a truncar</param>
    /// <param name="maxLength">Longitud máxima</param>
    /// <returns>Texto truncado</returns>
    public static string TruncateText(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
            return text;
            
        return text.Substring(0, maxLength - 3) + "...";
    }
    
    /// <summary>
    /// Capitaliza la primera letra de cada palabra
    /// </summary>
    /// <param name="text">Texto a capitalizar</param>
    /// <returns>Texto capitalizado</returns>
    public static string ToTitleCase(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;
            
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
    }
    
    /// <summary>
    /// Formatea un número de teléfono argentino
    /// </summary>
    /// <param name="phone">Teléfono</param>
    /// <returns>Teléfono formateado</returns>
    public static string FormatPhoneNumber(string phone)
    {
        if (string.IsNullOrEmpty(phone))
            return phone;
            
        // Remover todos los caracteres no numéricos excepto el +
        var cleaned = new string(phone.Where(c => char.IsDigit(c) || c == '+').ToArray());
        
        // Formatear números argentinos básicos
        if (cleaned.StartsWith("+54"))
        {
            cleaned = cleaned.Substring(3);
        }
        
        if (cleaned.Length == 10) // Formato: 1112345678
        {
            return $"({cleaned.Substring(0, 3)}) {cleaned.Substring(3, 4)}-{cleaned.Substring(7)}";
        }
        
        return phone; // Retornar original si no se puede formatear
    }
}
