using Farma.Shared.Enums;

namespace Farma.FarmaciaApp.Models;

public record RecetaAdjunto(string NombreArchivo, string TipoArchivo, long TamanoBytes);

public record PharmacyOrder
{
    public int Id { get; init; }
    public string PacienteNombre { get; init; } = string.Empty;
    public string PacienteEmail { get; init; } = string.Empty;
    public string PacienteTelefono { get; init; } = string.Empty;
    public string Direccion { get; init; } = string.Empty;
    public double DistanciaKm { get; init; }
    public EstadoPedido Estado { get; init; }
    public DateTime FechaCreacion { get; init; }
    public DateTime? FechaActualizacion { get; init; }
    public bool RequiereEnvio { get; init; } = true;
    public bool CotizacionEnviada { get; init; }
    public bool CotizacionAceptada { get; init; }
    public decimal? CotizacionMonto { get; init; }
    public TimeSpan? TiempoEntregaEstimado { get; init; }
    public string? Observaciones { get; init; }
    public string? NotasCliente { get; init; }
    public IReadOnlyList<RecetaAdjunto> Recetas { get; init; } = Array.Empty<RecetaAdjunto>();
    public string? MetodoEntregaPreferido { get; init; }
    public string ZonaCobertura { get; init; } = string.Empty;

    public string NumeroPedido => $"#{Id}";
}
