namespace farma_back.Services;

public interface IGeoService
{
    double CalculateDistance(double lat1, double lon1, double lat2, double lon2);
}

public class GeoService : IGeoService
{
    /// <summary>
    /// Calcula la distancia entre dos puntos geográficos usando la fórmula de Haversine
    /// </summary>
    /// <param name="lat1">Latitud del primer punto</param>
    /// <param name="lon1">Longitud del primer punto</param>
    /// <param name="lat2">Latitud del segundo punto</param>
    /// <param name="lon2">Longitud del segundo punto</param>
    /// <returns>Distancia en kilómetros</returns>
    public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double earthRadiusKm = 6371.0;

        // Convertir grados a radianes
        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);
        var lat1Rad = ToRadians(lat1);
        var lat2Rad = ToRadians(lat2);

        // Fórmula de Haversine
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1Rad) * Math.Cos(lat2Rad);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return earthRadiusKm * c;
    }

    private static double ToRadians(double degrees)
    {
        return degrees * (Math.PI / 180);
    }
}
