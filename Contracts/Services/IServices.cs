using TesisApi.DTOs;

namespace TesisApi.Contracts.Services;

public interface ITutorService
{
    Task<TutorResponseDto> CrearAsync(TutorCreateDto dto);
    Task<IReadOnlyList<TutorResponseDto>> ListarAsync();
}

public interface ITesisService
{
    Task<TesisResponseDto> CrearAsync(TesisCreateDto dto);
    Task<TesisResponseDto> ObtenerAsync(int id);
    Task<IReadOnlyList<TesisResponseDto>> BuscarAsync(string? termino);
    Task<TesisResponseDto> SubirImagenAsync(int id, IFormFile archivo);
}

public interface IImageStorageService
{
    /// <summary>Valida y guarda la imagen; devuelve la ruta pública (ej. /uploads/abc.png).</summary>
    Task<string> GuardarAsync(IFormFile archivo);
}
