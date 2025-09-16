using farma_back.Models;

namespace farma_back.DTOs;

public class PedidoResponseDto
{
    public int Id { get; set; }
    public EstadoPedido Estado { get; set; }
    public string Calle { get; set; } = string.Empty;
    public string Altura { get; set; } = string.Empty;
    public string? Piso { get; set; }
    public string? Departamento { get; set; }
    public string Localidad { get; set; } = string.Empty;
    public string Provincia { get; set; } = string.Empty;
    public string CodigoPostal { get; set; } = string.Empty;
    public double? Latitud { get; set; }
    public double? Longitud { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    
    public int UsuarioId { get; set; }
    public string UsuarioNombre { get; set; } = string.Empty;
    public string UsuarioEmail { get; set; } = string.Empty;
    
    public List<RecetaResponseDto> Recetas { get; set; } = new();
    public List<CotizacionResponseDto> Cotizaciones { get; set; } = new();
}

public class RecetaResponseDto
{
    public int Id { get; set; }
    public string NombreArchivoOriginal { get; set; } = string.Empty;
    public string TipoArchivo { get; set; } = string.Empty;
    public long TamanoArchivo { get; set; }
    public DateTime FechaCreacion { get; set; }
}

public class CotizacionResponseDto
{
    public int Id { get; set; }
    public decimal MontoTotal { get; set; }
    public decimal MontoMedicamentos { get; set; }
    public decimal MontoEnvio { get; set; }
    public EstadoCotizacion Estado { get; set; }
    public string? Observaciones { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaExpiracion { get; set; }
    
    public int FarmaciaId { get; set; }
    public string FarmaciaNombre { get; set; } = string.Empty;
    public string FarmaciaDireccion { get; set; } = string.Empty;
}
