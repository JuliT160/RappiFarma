# Farma.Shared

Biblioteca compartida para el sistema de farmacia que contiene DTOs, enumeraciones, constantes y utilidades que pueden ser utilizadas por todas las aplicaciones del ecosistema Farma.

## 📦 Contenido

### Enumeraciones (`Enums/`)
- `EstadoPedido` - Estados de los pedidos
- `EstadoCotizacion` - Estados de las cotizaciones
- `EstadoPago` - Estados de los pagos

### DTOs (`DTOs/`)
- `UsuarioDto` - Datos de usuarios y operaciones CRUD
- `FarmaciaDto` - Datos de farmacias y búsquedas
- `RecetaDto` - Datos de recetas y creación
- `PedidoDto` - Datos de pedidos y operaciones
- `CotizacionDto` - Datos de cotizaciones
- `PagoIntentDto` - Datos de intenciones de pago
- `ApiResponseDto` - Respuestas estándar de la API
- `SearchDto` - Parámetros de búsqueda
- `OperacionesDto` - DTOs para operaciones específicas

### Servicios (`Services/`)
- `IFarmaApiService` - Interfaces para servicios de API usando Refit
- `IRecetasApiService` - Servicio para gestión de recetas
- `IPedidosApiService` - Servicio para gestión de pedidos
- `IFarmaciasApiService` - Servicio para gestión de farmacias
- `ICotizacionesApiService` - Servicio para gestión de cotizaciones
- `IUsuariosApiService` - Servicio para gestión de usuarios
- `IPagosApiService` - Servicio para gestión de pagos

### Constantes (`Constants/`)
- `ApiConstants` - URLs, endpoints, límites y configuraciones

### Utilidades (`Utils/`)
- `ValidationUtils` - Utilidades de validación
- `GeoUtils` - Cálculos geográficos (Haversine, etc.)
- `FormatUtils` - Formateo de datos para mostrar

## 🚀 Uso

### En el Backend (ASP.NET Core)
```csharp
// En Program.cs o Startup.cs
services.AddScoped<IFarmaApiService>();

// En controladores
public class PedidosController : ControllerBase
{
    public async Task<ActionResult<PedidoDto>> Get(int id)
    {
        // Usar DTOs para respuestas
        return Ok(pedidoDto);
    }
}
```

### En aplicaciones cliente (MAUI)
```csharp
// Configurar Refit
services.AddRefitClient<IFarmaApiService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(ApiConstants.BASE_URL));

// Usar en ViewModels o servicios
public class PedidosViewModel
{
    private readonly IFarmaApiService _apiService;
    
    public async Task<List<PedidoDto>> GetPedidosAsync()
    {
        var response = await _apiService.GetPedidosAsync();
        return response.Data?.Pedidos ?? new List<PedidoDto>();
    }
}
```

## 📋 Validaciones

Los DTOs incluyen validaciones usando Data Annotations:

```csharp
var usuario = new CreateUsuarioDto
{
    Nombre = "Juan Pérez",
    Email = "juan@email.com",
    Telefono = "011-1234-5678"
};

// Validar usando utilidades
var errores = ValidationUtils.GetValidationErrors(usuario);
if (errores.Any())
{
    // Manejar errores de validación
}
```

## 🌍 Utilidades Geográficas

```csharp
// Calcular distancia entre dos puntos
var distancia = GeoUtils.CalculateDistance(-34.6037, -58.3816, -34.5875, -58.3974);

// Validar coordenadas
var validas = ValidationUtils.AreValidCoordinates(lat, lng);

// Formatear coordenadas
var coordenadas = GeoUtils.FormatCoordinates(-34.6037, -58.3816);
```

## 💰 Formateo

```csharp
// Formatear moneda
var precio = FormatUtils.FormatCurrency(1250.50m, "ARS"); // "$1,250.50"

// Formatear tamaño de archivo
var tamano = FormatUtils.FormatFileSize(2048576); // "2.0 MB"

// Formatear fecha amigable
var fecha = FormatUtils.FormatFriendlyDate(DateTime.UtcNow.AddHours(-2)); // "Hace 2 horas"

// Formatear distancia
var distancia = FormatUtils.FormatDistance(1.5); // "1.5 km"
```

## 🔧 Configuración

### Constantes importantes
```csharp
ApiConstants.BASE_URL // URL base de la API
ApiConstants.BEARER_TOKEN // Token de autenticación
ApiConstants.Limits.MAX_FILE_SIZE_MB // Tamaño máximo de archivo
ApiConstants.FileTypes.ALLOWED_EXTENSIONS // Extensiones permitidas
```

## 📝 Ejemplos de DTOs

### Crear una receta
```csharp
var recetaRequest = new CreateRecetaRequestDto
{
    Calle = "Av. Corrientes",
    Altura = "1234",
    Localidad = "Buenos Aires",
    Provincia = "Buenos Aires",
    CodigoPostal = "1043",
    Latitud = -34.6037,
    Longitud = -58.3816,
    UsuarioId = 1
};
```

### Buscar farmacias cercanas
```csharp
var busqueda = new BuscarFarmaciasDto
{
    Latitud = -34.6037,
    Longitud = -58.3816,
    Radio = 5.0,
    Limite = 10
};
```

## 🔄 Versionado

Esta biblioteca sigue el versionado semántico. Los cambios breaking en los DTOs requieren incremento de versión mayor.

## 📄 Licencia

Proyecto interno de Farma App.
