namespace farma_back.Services;

public interface IFileService
{
    Task<string> SaveRecetaFileAsync(IFormFile file, DateTime fecha);
    bool IsValidFileType(string fileName);
    bool IsValidFileSize(long fileSize);
}

public class FileService : IFileService
{
    private readonly ILogger<FileService> _logger;
    private readonly string _baseUploadPath;
    private readonly long _maxFileSize = 10 * 1024 * 1024; // 10MB
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".pdf", ".doc", ".docx" };

    public FileService(ILogger<FileService> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _baseUploadPath = Path.Combine(environment.ContentRootPath, "data", "recetas");
        
        // Crear directorio base si no existe
        if (!Directory.Exists(_baseUploadPath))
        {
            Directory.CreateDirectory(_baseUploadPath);
        }
    }

    public async Task<string> SaveRecetaFileAsync(IFormFile file, DateTime fecha)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Archivo no válido");

        if (!IsValidFileType(file.FileName))
            throw new ArgumentException("Tipo de archivo no permitido");

        if (!IsValidFileSize(file.Length))
            throw new ArgumentException("Tamaño de archivo excede el límite permitido");

        // Crear estructura de carpetas por año/mes
        var yearFolder = fecha.Year.ToString();
        var monthFolder = fecha.Month.ToString("00");
        var targetDirectory = Path.Combine(_baseUploadPath, yearFolder, monthFolder);

        if (!Directory.Exists(targetDirectory))
        {
            Directory.CreateDirectory(targetDirectory);
        }

        // Generar nombre único para el archivo
        var fileExtension = Path.GetExtension(file.FileName);
        var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
        var filePath = Path.Combine(targetDirectory, uniqueFileName);

        try
        {
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            
            _logger.LogInformation("Archivo guardado: {FilePath}", filePath);
            
            // Retornar ruta relativa desde la carpeta data/recetas
            return Path.Combine(yearFolder, monthFolder, uniqueFileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al guardar archivo: {FileName}", file.FileName);
            throw new InvalidOperationException("Error al guardar el archivo");
        }
    }

    public bool IsValidFileType(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return false;

        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return _allowedExtensions.Contains(extension);
    }

    public bool IsValidFileSize(long fileSize)
    {
        return fileSize > 0 && fileSize <= _maxFileSize;
    }
}
