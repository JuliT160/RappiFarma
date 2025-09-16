namespace Farma.Shared.Utils;

public static class GeoUtils
{
    private const double EarthRadiusKm = 6371.0;
    
    /// <summary>
    /// Calcula la distancia entre dos puntos geográficos usando la fórmula de Haversine
    /// </summary>
    /// <param name="lat1">Latitud del primer punto</param>
    /// <param name="lon1">Longitud del primer punto</param>
    /// <param name="lat2">Latitud del segundo punto</param>
    /// <param name="lon2">Longitud del segundo punto</param>
    /// <returns>Distancia en kilómetros</returns>
    public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        // Convertir grados a radianes
        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);
        var lat1Rad = ToRadians(lat1);
        var lat2Rad = ToRadians(lat2);

        // Fórmula de Haversine
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1Rad) * Math.Cos(lat2Rad);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return EarthRadiusKm * c;
    }
    
    /// <summary>
    /// Convierte grados a radianes
    /// </summary>
    /// <param name="degrees">Grados</param>
    /// <returns>Radianes</returns>
    public static double ToRadians(double degrees)
    {
        return degrees * (Math.PI / 180);
    }
    
    /// <summary>
    /// Convierte radianes a grados
    /// </summary>
    /// <param name="radians">Radianes</param>
    /// <returns>Grados</returns>
    public static double ToDegrees(double radians)
    {
        return radians * (180 / Math.PI);
    }
    
    /// <summary>
    /// Determina si un punto está dentro de un radio específico desde otro punto
    /// </summary>
    /// <param name="centerLat">Latitud del centro</param>
    /// <param name="centerLon">Longitud del centro</param>
    /// <param name="pointLat">Latitud del punto a verificar</param>
    /// <param name="pointLon">Longitud del punto a verificar</param>
    /// <param name="radiusKm">Radio en kilómetros</param>
    /// <returns>True si está dentro del radio</returns>
    public static bool IsWithinRadius(double centerLat, double centerLon, double pointLat, double pointLon, double radiusKm)
    {
        var distance = CalculateDistance(centerLat, centerLon, pointLat, pointLon);
        return distance <= radiusKm;
    }
    
    /// <summary>
    /// Calcula los límites de un área rectangular basada en un punto central y un radio
    /// </summary>
    /// <param name="centerLat">Latitud del centro</param>
    /// <param name="centerLon">Longitud del centro</param>
    /// <param name="radiusKm">Radio en kilómetros</param>
    /// <returns>Límites del área (minLat, maxLat, minLon, maxLon)</returns>
    public static (double minLat, double maxLat, double minLon, double maxLon) CalculateBounds(double centerLat, double centerLon, double radiusKm)
    {
        var latDelta = radiusKm / 111.32; // Aproximadamente 111.32 km por grado de latitud
        var lonDelta = radiusKm / (111.32 * Math.Cos(ToRadians(centerLat))); // Ajustado por latitud
        
        return (
            minLat: centerLat - latDelta,
            maxLat: centerLat + latDelta,
            minLon: centerLon - lonDelta,
            maxLon: centerLon + lonDelta
        );
    }
    
    /// <summary>
    /// Formatea coordenadas para mostrar
    /// </summary>
    /// <param name="lat">Latitud</param>
    /// <param name="lon">Longitud</param>
    /// <param name="precision">Precisión decimal</param>
    /// <returns>Coordenadas formateadas</returns>
    public static string FormatCoordinates(double lat, double lon, int precision = 6)
    {
        var latFormatted = Math.Round(lat, precision);
        var lonFormatted = Math.Round(lon, precision);
        var latDirection = lat >= 0 ? "N" : "S";
        var lonDirection = lon >= 0 ? "E" : "W";
        
        return $"{Math.Abs(latFormatted)}°{latDirection}, {Math.Abs(lonFormatted)}°{lonDirection}";
    }
    
    /// <summary>
    /// Valida si las coordenadas están dentro de Argentina (aproximadamente)
    /// </summary>
    /// <param name="lat">Latitud</param>
    /// <param name="lon">Longitud</param>
    /// <returns>True si están dentro de los límites aproximados de Argentina</returns>
    public static bool IsWithinArgentina(double lat, double lon)
    {
        // Límites aproximados de Argentina
        const double minLat = -55.0; // Tierra del Fuego
        const double maxLat = -21.8; // Frontera norte
        const double minLon = -73.6; // Frontera oeste
        const double maxLon = -53.6; // Frontera este
        
        return lat >= minLat && lat <= maxLat && lon >= minLon && lon <= maxLon;
    }
    
    /// <summary>
    /// Obtiene la provincia aproximada basada en coordenadas (muy básico)
    /// </summary>
    /// <param name="lat">Latitud</param>
    /// <param name="lon">Longitud</param>
    /// <returns>Nombre de la provincia o "Desconocida"</returns>
    public static string GetApproximateProvince(double lat, double lon)
    {
        // Implementación muy básica para algunas provincias principales
        if (lat >= -35.0 && lat <= -34.0 && lon >= -59.0 && lon <= -57.5)
            return "Buenos Aires";
        if (lat >= -34.8 && lat <= -34.3 && lon >= -58.8 && lon <= -58.2)
            return "Ciudad Autónoma de Buenos Aires";
        if (lat >= -32.0 && lat <= -31.0 && lon >= -61.0 && lon <= -60.0)
            return "Santa Fe";
        if (lat >= -32.0 && lat <= -31.0 && lon >= -65.0 && lon <= -64.0)
            return "Córdoba";
        if (lat >= -27.0 && lat <= -26.0 && lon >= -66.0 && lon <= -65.0)
            return "Tucumán";
        if (lat >= -35.0 && lat <= -32.0 && lon >= -69.0 && lon <= -66.0)
            return "Mendoza";
        
        return "Desconocida";
    }
}
