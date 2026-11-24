using TesisApi.Domain.Exceptions;
using TesisApi.Contracts.Services;

namespace TesisApi.Services;

public class ImageStorageService : IImageStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly long _maxBytes;
    private readonly HashSet<string> _extensiones;

    public ImageStorageService(IWebHostEnvironment env, IConfiguration config)
    {
        _env = env;
        _maxBytes = config.GetValue<long>("Uploads:MaxBytes", 2 * 1024 * 1024);
        var ext = config.GetSection("Uploads:AllowedExtensions").Get<string[]>()
                  ?? new[] { ".jpg", ".jpeg", ".png", ".webp" };
        _extensiones = new HashSet<string>(ext, StringComparer.OrdinalIgnoreCase);
    }

    public async Task<string> GuardarAsync(IFormFile archivo)
    {
        if (archivo is null || archivo.Length == 0)
            throw new ValidationAppException("Debe enviar un archivo de imagen.");
        if (archivo.Length > _maxBytes)
            throw new ValidationAppException($"La imagen supera el máximo de {_maxBytes / 1024 / 1024} MB.");

        var extension = Path.GetExtension(archivo.FileName);
        if (!_extensiones.Contains(extension))
            throw new ValidationAppException(
                $"Formato no permitido. Use: {string.Join(", ", _extensiones)}.");

        var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
        var carpeta = Path.Combine(webRoot, "uploads");
        Directory.CreateDirectory(carpeta);

        var nombre = $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";
        await using var stream = File.Create(Path.Combine(carpeta, nombre));
        await archivo.CopyToAsync(stream);

        return $"/uploads/{nombre}";
    }
}
