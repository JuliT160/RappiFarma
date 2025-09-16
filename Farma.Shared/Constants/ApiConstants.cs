namespace Farma.Shared.Constants;

public static class ApiConstants
{
    // Endpoints base
    public const string BASE_URL = "https://localhost:7097";
    public const string API_VERSION = "v1";
    
    // Autenticación
    public const string BASIC_AUTH_USERNAME = "admin";
    public const string BASIC_AUTH_PASSWORD = "admin123";
    public const string BEARER_TOKEN = "farma-api-key-2024";
    
    // Headers
    public const string AUTHORIZATION_HEADER = "Authorization";
    public const string CONTENT_TYPE_HEADER = "Content-Type";
    public const string ACCEPT_HEADER = "Accept";
    
    // Content Types
    public const string APPLICATION_JSON = "application/json";
    public const string MULTIPART_FORM_DATA = "multipart/form-data";
    
    // Endpoints
    public static class Endpoints
    {
        public const string HEALTH = "/";
        public const string AUTH_TEST = "/auth/basic-test";
        
        public const string RECETAS = "/recetas";
        
        public const string PEDIDOS = "/pedidos";
        public const string PEDIDOS_BY_ID = "/pedidos/{id}";
        public const string PEDIDOS_CANCELAR = "/pedidos/{id}/cancelar";
        public const string PEDIDOS_ESTADISTICAS = "/pedidos/estadisticas";
        
        public const string FARMACIAS = "/farmacias";
        public const string FARMACIAS_BY_ID = "/farmacias/{id}";
        public const string FARMACIAS_CERCANAS = "/farmacias/cercanas";
        
        public const string COTIZACIONES = "/cotizaciones";
        public const string COTIZACIONES_BY_ID = "/cotizaciones/{id}";
        public const string COTIZACIONES_BY_PEDIDO = "/pedidos/{pedidoId}/cotizaciones";
        
        public const string USUARIOS = "/usuarios";
        public const string USUARIOS_BY_ID = "/usuarios/{id}";
        
        public const string PAGOS = "/pagos";
        public const string PAGOS_BY_ID = "/pagos/{id}";
        public const string PAGOS_BY_PEDIDO = "/pedidos/{pedidoId}/pagos";
    }
    
    // Parámetros de consulta
    public static class QueryParams
    {
        public const string ESTADO = "estado";
        public const string USUARIO_ID = "usuarioId";
        public const string FECHA_DESDE = "fechaDesde";
        public const string FECHA_HASTA = "fechaHasta";
        public const string LIMITE = "limite";
        public const string PAGINA = "pagina";
        
        // Farmacias cercanas
        public const string LATITUD = "lat";
        public const string LONGITUD = "lng";
        public const string LOCALIDAD = "localidad";
        public const string PROVINCIA = "provincia";
        public const string CODIGO_POSTAL = "codigoPostal";
        public const string RADIO = "radio";
    }
    
    // Límites y configuraciones
    public static class Limits
    {
        public const int MAX_FILE_SIZE_MB = 10;
        public const long MAX_FILE_SIZE_BYTES = MAX_FILE_SIZE_MB * 1024 * 1024;
        
        public const int MAX_SEARCH_RESULTS = 50;
        public const int DEFAULT_SEARCH_RESULTS = 20;
        
        public const double MAX_SEARCH_RADIUS_KM = 100.0;
        public const double DEFAULT_SEARCH_RADIUS_KM = 10.0;
        
        public const int MAX_PAGINATION_SIZE = 100;
        public const int DEFAULT_PAGINATION_SIZE = 20;
    }
    
    // Tipos de archivo permitidos
    public static class FileTypes
    {
        public static readonly string[] ALLOWED_EXTENSIONS = { ".jpg", ".jpeg", ".png", ".pdf", ".doc", ".docx" };
        
        public static readonly Dictionary<string, string> MIME_TYPES = new()
        {
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" },
            { ".pdf", "application/pdf" },
            { ".doc", "application/msword" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" }
        };
    }
    
    // Mensajes de error comunes
    public static class ErrorMessages
    {
        public const string UNAUTHORIZED = "No autorizado";
        public const string FORBIDDEN = "Acceso prohibido";
        public const string NOT_FOUND = "Recurso no encontrado";
        public const string BAD_REQUEST = "Solicitud inválida";
        public const string INTERNAL_SERVER_ERROR = "Error interno del servidor";
        public const string SERVICE_UNAVAILABLE = "Servicio no disponible";
        
        public const string INVALID_FILE_TYPE = "Tipo de archivo no permitido";
        public const string FILE_TOO_LARGE = "El archivo excede el tamaño máximo permitido";
        public const string INVALID_COORDINATES = "Coordenadas geográficas inválidas";
        public const string PEDIDO_NOT_FOUND = "Pedido no encontrado";
        public const string FARMACIA_NOT_FOUND = "Farmacia no encontrada";
        public const string USUARIO_NOT_FOUND = "Usuario no encontrado";
    }
    
    // Códigos de estado HTTP
    public static class StatusCodes
    {
        public const int OK = 200;
        public const int CREATED = 201;
        public const int NO_CONTENT = 204;
        public const int BAD_REQUEST = 400;
        public const int UNAUTHORIZED = 401;
        public const int FORBIDDEN = 403;
        public const int NOT_FOUND = 404;
        public const int CONFLICT = 409;
        public const int INTERNAL_SERVER_ERROR = 500;
        public const int SERVICE_UNAVAILABLE = 503;
    }
}
