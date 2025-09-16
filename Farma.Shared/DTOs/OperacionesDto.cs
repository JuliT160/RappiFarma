using System.ComponentModel.DataAnnotations;
using Farma.Shared.Enums;

namespace Farma.Shared.DTOs;

/// <summary>
/// Respuesta de creación de receta
/// </summary>
public class CrearRecetaResponseDto
{
    public int PedidoId { get; set; }
    public int RecetaId { get; set; }
    public EstadoPedido Estado { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public DireccionEntregaDto DireccionEntrega { get; set; } = new();
    public ArchivoInfoDto Archivo { get; set; } = new();
}

/// <summary>
/// Información de dirección de entrega
/// </summary>
public class DireccionEntregaDto
{
    public string Calle { get; set; } = string.Empty;
    public string Altura { get; set; } = string.Empty;
    public string? Piso { get; set; }
    public string? Departamento { get; set; }
    public string Localidad { get; set; } = string.Empty;
    public string Provincia { get; set; } = string.Empty;
    public string CodigoPostal { get; set; } = string.Empty;
    public double? Latitud { get; set; }
    public double? Longitud { get; set; }
    
    public string DireccionCompleta => 
        $"{Calle} {Altura}" + 
        (!string.IsNullOrEmpty(Piso) ? $", Piso {Piso}" : "") +
        (!string.IsNullOrEmpty(Departamento) ? $", Depto {Departamento}" : "") +
        $", {Localidad}, {Provincia} ({CodigoPostal})";
}

/// <summary>
/// Información de archivo
/// </summary>
public class ArchivoInfoDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public long Tamano { get; set; }
    
    public string TamanoLegible
    {
        get
        {
            string[] suffixes = { "B", "KB", "MB", "GB" };
            int counter = 0;
            decimal number = Tamano;
            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }
            return $"{number:n1} {suffixes[counter]}";
        }
    }
}

/// <summary>
/// Respuesta de cancelación de pedido
/// </summary>
public class CancelarPedidoResponseDto
{
    public int PedidoId { get; set; }
    public EstadoPedido EstadoAnterior { get; set; }
    public EstadoPedido EstadoActual { get; set; }
    public DateTime FechaCancelacion { get; set; }
    public string Mensaje { get; set; } = string.Empty;
}

/// <summary>
/// Estadísticas de pedidos
/// </summary>
public class EstadisticasPedidosDto
{
    public int TotalPedidos { get; set; }
    public int PedidosCreados { get; set; }
    public int PedidosEnCotizacion { get; set; }
    public int PedidosCotizados { get; set; }
    public int PedidosConfirmados { get; set; }
    public int PedidosEnPreparacion { get; set; }
    public int PedidosListos { get; set; }
    public int PedidosEnEntrega { get; set; }
    public int PedidosEntregados { get; set; }
    public int PedidosCancelados { get; set; }
    
    public Dictionary<EstadoPedido, int> PorEstado { get; set; } = new();
    public DateTime FechaConsulta { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Información de salud del servicio
/// </summary>
public class HealthCheckDto
{
    public string Service { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public Dictionary<string, object> Details { get; set; } = new();
}

/// <summary>
/// Configuración de archivo
/// </summary>
public class ConfiguracionArchivoDto
{
    public List<string> TiposPermitidos { get; set; } = new();
    public long TamanoMaximoBytes { get; set; }
    public string TamanoMaximoLegible { get; set; } = string.Empty;
    public string DirectorioBase { get; set; } = string.Empty;
}
