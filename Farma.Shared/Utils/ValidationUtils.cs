using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Farma.Shared.Constants;

namespace Farma.Shared.Utils;

public static class ValidationUtils
{
    /// <summary>
    /// Valida un objeto usando Data Annotations
    /// </summary>
    /// <param name="obj">Objeto a validar</param>
    /// <returns>Lista de errores de validación</returns>
    public static List<ValidationResult> ValidateObject(object obj)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(obj);
        Validator.TryValidateObject(obj, validationContext, validationResults, true);
        return validationResults;
    }
    
    /// <summary>
    /// Valida si un objeto es válido según Data Annotations
    /// </summary>
    /// <param name="obj">Objeto a validar</param>
    /// <returns>True si es válido, False si tiene errores</returns>
    public static bool IsValidObject(object obj)
    {
        var validationResults = ValidateObject(obj);
        return !validationResults.Any();
    }
    
    /// <summary>
    /// Obtiene los mensajes de error de validación
    /// </summary>
    /// <param name="obj">Objeto a validar</param>
    /// <returns>Lista de mensajes de error</returns>
    public static List<string> GetValidationErrors(object obj)
    {
        var validationResults = ValidateObject(obj);
        return validationResults.Select(vr => vr.ErrorMessage ?? "Error de validación").ToList();
    }
    
    /// <summary>
    /// Valida si un tipo de archivo está permitido
    /// </summary>
    /// <param name="fileName">Nombre del archivo</param>
    /// <returns>True si el tipo está permitido</returns>
    public static bool IsValidFileType(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return false;
            
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return ApiConstants.FileTypes.ALLOWED_EXTENSIONS.Contains(extension);
    }
    
    /// <summary>
    /// Valida si el tamaño del archivo está dentro del límite permitido
    /// </summary>
    /// <param name="fileSize">Tamaño del archivo en bytes</param>
    /// <returns>True si el tamaño está permitido</returns>
    public static bool IsValidFileSize(long fileSize)
    {
        return fileSize > 0 && fileSize <= ApiConstants.Limits.MAX_FILE_SIZE_BYTES;
    }
    
    /// <summary>
    /// Valida coordenadas geográficas
    /// </summary>
    /// <param name="latitud">Latitud</param>
    /// <param name="longitud">Longitud</param>
    /// <returns>True si las coordenadas son válidas</returns>
    public static bool AreValidCoordinates(double? latitud, double? longitud)
    {
        if (!latitud.HasValue || !longitud.HasValue)
            return false;
            
        return latitud >= -90 && latitud <= 90 && longitud >= -180 && longitud <= 180;
    }
    
    /// <summary>
    /// Valida formato de email
    /// </summary>
    /// <param name="email">Email a validar</param>
    /// <returns>True si el formato es válido</returns>
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;
            
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Valida formato de teléfono (básico)
    /// </summary>
    /// <param name="telefono">Teléfono a validar</param>
    /// <returns>True si el formato es válido</returns>
    public static bool IsValidPhone(string telefono)
    {
        if (string.IsNullOrEmpty(telefono))
            return false;
            
        // Patrón básico para teléfonos argentinos
        var pattern = @"^(\+54\s?)?(\d{2,4}[\s-]?)?\d{6,8}$";
        return Regex.IsMatch(telefono, pattern);
    }
    
    /// <summary>
    /// Valida código postal argentino
    /// </summary>
    /// <param name="codigoPostal">Código postal a validar</param>
    /// <returns>True si el formato es válido</returns>
    public static bool IsValidPostalCode(string codigoPostal)
    {
        if (string.IsNullOrEmpty(codigoPostal))
            return false;
            
        // Patrón para códigos postales argentinos (4 dígitos o formato AAAA)
        var pattern = @"^\d{4}$|^[A-Z]\d{4}[A-Z]{3}$";
        return Regex.IsMatch(codigoPostal.ToUpper(), pattern);
    }
    
    /// <summary>
    /// Sanitiza una cadena de texto removiendo caracteres peligrosos
    /// </summary>
    /// <param name="input">Texto a sanitizar</param>
    /// <returns>Texto sanitizado</returns>
    public static string SanitizeString(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;
            
        // Remover caracteres peligrosos para SQL injection y XSS
        var dangerous = new[] { "<", ">", "'", "\"", "&", "script", "javascript", "vbscript" };
        var sanitized = input;
        
        foreach (var danger in dangerous)
        {
            sanitized = sanitized.Replace(danger, "", StringComparison.OrdinalIgnoreCase);
        }
        
        return sanitized.Trim();
    }
    
    /// <summary>
    /// Valida que una cadena no contenga caracteres peligrosos
    /// </summary>
    /// <param name="input">Texto a validar</param>
    /// <returns>True si es seguro</returns>
    public static bool IsSafeString(string input)
    {
        if (string.IsNullOrEmpty(input))
            return true;
            
        var dangerous = new[] { "<script", "</script", "javascript:", "vbscript:", "onload=", "onerror=" };
        return !dangerous.Any(danger => input.Contains(danger, StringComparison.OrdinalIgnoreCase));
    }
}
