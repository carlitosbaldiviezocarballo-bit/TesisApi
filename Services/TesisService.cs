using TesisApi.Domain.Entities;
using TesisApi.Domain.Exceptions;
using TesisApi.DTOs;
using TesisApi.Contracts.Factories;
using TesisApi.Contracts.Repositories;
using TesisApi.Contracts.Services;

namespace TesisApi.Services;

public class TesisService : ITesisService
{
    private readonly ITesisRepository _tesisRepo;
    private readonly ITutorRepository _tutorRepo;
    private readonly ITesisFactory _factory;
    private readonly IImageStorageService _storage;

    public TesisService(
        ITesisRepository tesisRepo,
        ITutorRepository tutorRepo,
        ITesisFactory factory,
        IImageStorageService storage)
    {
        _tesisRepo = tesisRepo;
        _tutorRepo = tutorRepo;
        _factory = factory;
        _storage = storage;
    }

    public async Task<TesisResponseDto> CrearAsync(TesisCreateDto dto)
    {
        var tutor = await _tutorRepo.GetByIdAsync(dto.IdTutor)
            ?? throw new NotFoundException($"No existe el tutor con id {dto.IdTutor}.");

        var tesis = _factory.Create(dto);

        // Regla: no puede duplicarse tesis
        if (await _tesisRepo.ExistsByTituloAsync(tesis.Titulo))
            throw new BusinessRuleException($"Ya existe una tesis con el título '{tesis.Titulo}'.");

        // Regla: un tutor solo puede tener 3 tesis
        var cantidad = await _tesisRepo.CountByTutorAsync(tutor.Id);
        if (cantidad >= Tutor.MaxTesisPorTutor)
            throw new BusinessRuleException(
                $"El tutor '{tutor.Nombre}' ya tiene {Tutor.MaxTesisPorTutor} tesis; no puede tener más.");

        await _tesisRepo.AddAsync(tesis);
        tesis.Tutor = tutor;
        return _factory.ToResponse(tesis, cantidad + 1);
    }

    public async Task<TesisResponseDto> ObtenerAsync(int id)
    {
        var tesis = await _tesisRepo.GetByIdAsync(id)
            ?? throw new NotFoundException($"No existe la tesis con id {id}.");
        var cantidad = await _tesisRepo.CountByTutorAsync(tesis.IdTutor);
        return _factory.ToResponse(tesis, cantidad);
    }

    public async Task<IReadOnlyList<TesisResponseDto>> BuscarAsync(string? termino)
    {
        var lista = await _tesisRepo.BuscarAsync(termino);
        var result = new List<TesisResponseDto>();
        foreach (var t in lista)
        {
            var cantidad = await _tesisRepo.CountByTutorAsync(t.IdTutor);
            result.Add(_factory.ToResponse(t, cantidad));
        }
        return result;
    }

    public async Task<TesisResponseDto> SubirImagenAsync(int id, IFormFile archivo)
    {
        var tesis = await _tesisRepo.GetByIdAsync(id)
            ?? throw new NotFoundException($"No existe la tesis con id {id}.");

        tesis.Imagen = await _storage.GuardarAsync(archivo);
        await _tesisRepo.UpdateAsync(tesis);

        var cantidad = await _tesisRepo.CountByTutorAsync(tesis.IdTutor);
        return _factory.ToResponse(tesis, cantidad);
    }
}
