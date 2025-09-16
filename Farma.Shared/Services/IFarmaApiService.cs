using Refit;
using Farma.Shared.DTOs;
using Farma.Shared.Enums;

namespace Farma.Shared.Services;

/// <summary>
/// Interfaz principal para el servicio de API de Farma
/// </summary>
public interface IFarmaApiService
{
    // Autenticación
    [Post("/auth/basic-test")]
    Task<ApiResponse<AuthResponseDto>> TestAuthAsync();
    
    // Health Check
    [Get("/")]
    Task<ApiResponse<HealthCheckDto>> GetHealthAsync();
}

/// <summary>
/// Servicio para gestión de recetas
/// </summary>
public interface IRecetasApiService
{
    [Multipart]
    [Post("/recetas")]
    Task<ApiResponse<CrearRecetaResponseDto>> CrearRecetaAsync(
        [AliasAs("archivo")] StreamPart archivo,
        [AliasAs("calle")] string calle,
        [AliasAs("altura")] string altura,
        [AliasAs("piso")] string? piso,
        [AliasAs("depto")] string? departamento,
        [AliasAs("localidad")] string localidad,
        [AliasAs("provincia")] string provincia,
        [AliasAs("codigoPostal")] string codigoPostal,
        [AliasAs("lat")] double? latitud,
        [AliasAs("lon")] double? longitud,
        [AliasAs("usuarioId")] int usuarioId);
}

/// <summary>
/// Servicio para gestión de pedidos
/// </summary>
public interface IPedidosApiService
{
    [Get("/pedidos/{id}")]
    Task<ApiResponse<PedidoDto>> ObtenerPedidoAsync(int id);
    
    [Get("/pedidos")]
    Task<ApiResponse<PedidosResponseDto>> ObtenerPedidosAsync([Query] BuscarPedidosDto? filtros = null);
    
    [Get("/pedidos")]
    Task<ApiResponse<PedidosResponseDto>> ObtenerPedidosPorEstadoAsync([Query("estado")] EstadoPedido estado);
    
    [Post("/pedidos/{id}/cancelar")]
    Task<ApiResponse<CancelarPedidoResponseDto>> CancelarPedidoAsync(int id);
    
    [Get("/pedidos/estadisticas")]
    Task<ApiResponse<EstadisticasPedidosDto>> ObtenerEstadisticasAsync();
}

/// <summary>
/// Servicio para gestión de farmacias
/// </summary>
public interface IFarmaciasApiService
{
    [Get("/farmacias")]
    Task<ApiResponse<List<FarmaciaDto>>> ObtenerFarmaciasAsync();
    
    [Get("/farmacias/{id}")]
    Task<ApiResponse<FarmaciaDto>> ObtenerFarmaciaAsync(int id);
    
    [Get("/farmacias/cercanas")]
    Task<ApiResponse<FarmaciasResponseDto>> ObtenerFarmaciasCercanasAsync([Query] BuscarFarmaciasDto parametros);
    
    [Get("/farmacias/cercanas")]
    Task<ApiResponse<FarmaciasResponseDto>> ObtenerFarmaciasCercanasAsync(
        [Query("lat")] double? latitud = null,
        [Query("lng")] double? longitud = null,
        [Query("localidad")] string? localidad = null,
        [Query("provincia")] string? provincia = null,
        [Query("codigoPostal")] string? codigoPostal = null,
        [Query("radio")] double radio = 10.0,
        [Query("limite")] int limite = 20);
}

/// <summary>
/// Servicio para gestión de cotizaciones
/// </summary>
public interface ICotizacionesApiService
{
    [Get("/cotizaciones/{id}")]
    Task<ApiResponse<CotizacionDto>> ObtenerCotizacionAsync(int id);
    
    [Get("/cotizaciones")]
    Task<ApiResponse<List<CotizacionDto>>> ObtenerCotizacionesAsync([Query] BuscarCotizacionesDto? filtros = null);
    
    [Post("/cotizaciones")]
    Task<ApiResponse<CotizacionDto>> CrearCotizacionAsync([Body] CreateCotizacionDto cotizacion);
    
    [Put("/cotizaciones/{id}/estado")]
    Task<ApiResponse<CotizacionDto>> ActualizarEstadoCotizacionAsync(int id, [Body] EstadoCotizacion estado);
    
    [Get("/pedidos/{pedidoId}/cotizaciones")]
    Task<ApiResponse<List<CotizacionDto>>> ObtenerCotizacionesPorPedidoAsync(int pedidoId);
}

/// <summary>
/// Servicio para gestión de usuarios
/// </summary>
public interface IUsuariosApiService
{
    [Get("/usuarios")]
    Task<ApiResponse<List<UsuarioDto>>> ObtenerUsuariosAsync();
    
    [Get("/usuarios/{id}")]
    Task<ApiResponse<UsuarioDto>> ObtenerUsuarioAsync(int id);
    
    [Post("/usuarios")]
    Task<ApiResponse<UsuarioDto>> CrearUsuarioAsync([Body] CreateUsuarioDto usuario);
    
    [Put("/usuarios/{id}")]
    Task<ApiResponse<UsuarioDto>> ActualizarUsuarioAsync(int id, [Body] UpdateUsuarioDto usuario);
    
    [Delete("/usuarios/{id}")]
    Task<ApiResponse<object>> EliminarUsuarioAsync(int id);
}

/// <summary>
/// Servicio para gestión de pagos
/// </summary>
public interface IPagosApiService
{
    [Get("/pagos/{id}")]
    Task<ApiResponse<PagoIntentDto>> ObtenerPagoAsync(int id);
    
    [Post("/pagos")]
    Task<ApiResponse<PagoIntentDto>> CrearPagoIntentAsync([Body] CreatePagoIntentDto pagoIntent);
    
    [Put("/pagos/{id}")]
    Task<ApiResponse<PagoIntentDto>> ActualizarPagoAsync(int id, [Body] UpdatePagoIntentDto pagoIntent);
    
    [Get("/pedidos/{pedidoId}/pagos")]
    Task<ApiResponse<List<PagoIntentDto>>> ObtenerPagosPorPedidoAsync(int pedidoId);
}
