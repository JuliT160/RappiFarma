using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using farma_back.Data;
using farma_back.DTOs;
using farma_back.Services;

namespace farma_back.Controllers;

[ApiController]
[Route("farmacias")]
public class FarmaciasController : ControllerBase
{
    private readonly FarmaContext _context;
    private readonly IGeoService _geoService;
    private readonly ILogger<FarmaciasController> _logger;

    public FarmaciasController(FarmaContext context, IGeoService geoService, ILogger<FarmaciasController> logger)
    {
        _context = context;
        _geoService = geoService;
        _logger = logger;
    }

    /// <summary>
    /// Obtener farmacias cercanas a una ubicación
    /// </summary>
    /// <param name="lat">Latitud</param>
    /// <param name="lng">Longitud</param>
    /// <param name="localidad">Localidad (fallback si no hay coordenadas)</param>
    /// <param name="provincia">Provincia (fallback si no hay coordenadas)</param>
    /// <param name="codigoPostal">Código postal (fallback si no hay coordenadas)</param>
    /// <param name="radio">Radio de búsqueda en km (default: 10)</param>
    /// <param name="limite">Número máximo de resultados (default: 20)</param>
    /// <returns>Lista de farmacias cercanas ordenadas por distancia</returns>
    [HttpGet("cercanas")]
    public async Task<IActionResult> ObtenerFarmaciasCercanas(
        [FromQuery] double? lat,
        [FromQuery] double? lng,
        [FromQuery] string? localidad = null,
        [FromQuery] string? provincia = null,
        [FromQuery] string? codigoPostal = null,
        [FromQuery] double radio = 10.0,
        [FromQuery] int limite = 20)
    {
        try
        {
            var farmacias = await _context.Farmacias
                .Where(f => f.Activa)
                .ToListAsync();

            List<FarmaciaResponseDto> farmaciasCercanas;

            // Si tenemos coordenadas, usar cálculo Haversine
            if (lat.HasValue && lng.HasValue)
            {
                _logger.LogInformation("Buscando farmacias cercanas usando coordenadas: {Lat}, {Lng}", lat.Value, lng.Value);

                farmaciasCercanas = farmacias
                    .Select(f => new FarmaciaResponseDto
                    {
                        Id = f.Id,
                        Nombre = f.Nombre,
                        Direccion = f.Direccion,
                        Localidad = f.Localidad,
                        Provincia = f.Provincia,
                        CodigoPostal = f.CodigoPostal,
                        Latitud = f.Latitud,
                        Longitud = f.Longitud,
                        Telefono = f.Telefono,
                        Email = f.Email,
                        DistanciaKm = _geoService.CalculateDistance(lat.Value, lng.Value, f.Latitud, f.Longitud)
                    })
                    .Where(f => f.DistanciaKm <= radio)
                    .OrderBy(f => f.DistanciaKm)
                    .Take(limite)
                    .ToList();
            }
            else
            {
                // Fallback: buscar por localidad, provincia y código postal
                _logger.LogInformation("Buscando farmacias usando fallback: {Localidad}, {Provincia}, {CodigoPostal}", 
                    localidad, provincia, codigoPostal);

                var query = farmacias.AsQueryable();

                // Aplicar filtros de fallback
                if (!string.IsNullOrEmpty(localidad))
                {
                    query = query.Where(f => f.Localidad.ToLower().Contains(localidad.ToLower()));
                }

                if (!string.IsNullOrEmpty(provincia))
                {
                    query = query.Where(f => f.Provincia.ToLower().Contains(provincia.ToLower()));
                }

                if (!string.IsNullOrEmpty(codigoPostal))
                {
                    query = query.Where(f => f.CodigoPostal == codigoPostal);
                }

                farmaciasCercanas = query
                    .Take(limite)
                    .Select(f => new FarmaciaResponseDto
                    {
                        Id = f.Id,
                        Nombre = f.Nombre,
                        Direccion = f.Direccion,
                        Localidad = f.Localidad,
                        Provincia = f.Provincia,
                        CodigoPostal = f.CodigoPostal,
                        Latitud = f.Latitud,
                        Longitud = f.Longitud,
                        Telefono = f.Telefono,
                        Email = f.Email,
                        DistanciaKm = null // Sin coordenadas no podemos calcular distancia
                    })
                    .ToList();
            }

            var response = new
            {
                total = farmaciasCercanas.Count,
                parametros = new
                {
                    latitud = lat,
                    longitud = lng,
                    localidad = localidad,
                    provincia = provincia,
                    codigoPostal = codigoPostal,
                    radioKm = radio,
                    limite = limite
                },
                metodo = lat.HasValue && lng.HasValue ? "Haversine" : "Filtro por ubicación",
                farmacias = farmaciasCercanas
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener farmacias cercanas");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtener todas las farmacias activas
    /// </summary>
    /// <returns>Lista de todas las farmacias</returns>
    [HttpGet]
    public async Task<IActionResult> ObtenerFarmacias()
    {
        try
        {
            var farmacias = await _context.Farmacias
                .Where(f => f.Activa)
                .OrderBy(f => f.Nombre)
                .Select(f => new FarmaciaResponseDto
                {
                    Id = f.Id,
                    Nombre = f.Nombre,
                    Direccion = f.Direccion,
                    Localidad = f.Localidad,
                    Provincia = f.Provincia,
                    CodigoPostal = f.CodigoPostal,
                    Latitud = f.Latitud,
                    Longitud = f.Longitud,
                    Telefono = f.Telefono,
                    Email = f.Email
                })
                .ToListAsync();

            return Ok(new
            {
                total = farmacias.Count,
                farmacias = farmacias
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener farmacias");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }
}
