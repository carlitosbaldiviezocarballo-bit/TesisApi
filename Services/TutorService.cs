using TesisApi.Domain.Exceptions;
using TesisApi.DTOs;
using TesisApi.Contracts.Factories;
using TesisApi.Contracts.Repositories;
using TesisApi.Contracts.Services;

namespace TesisApi.Services;

public class TutorService : ITutorService
{
    private readonly ITutorRepository _repo;
    private readonly ITutorFactory _factory;

    public TutorService(ITutorRepository repo, ITutorFactory factory)
    {
        _repo = repo;
        _factory = factory;
    }

    public async Task<TutorResponseDto> CrearAsync(TutorCreateDto dto)
    {
        var tutor = _factory.Create(dto);

        // Regla: no puede duplicarse tutor
        if (await _repo.ExistsByCorreoAsync(tutor.Correo))
            throw new BusinessRuleException($"Ya existe un tutor con el correo '{tutor.Correo}'.");
        if (await _repo.ExistsByNombreAsync(tutor.Nombre))
            throw new BusinessRuleException($"Ya existe un tutor llamado '{tutor.Nombre}'.");

        await _repo.AddAsync(tutor);
        return _factory.ToResponse(tutor, 0);
    }

    public async Task<IReadOnlyList<TutorResponseDto>> ListarAsync()
    {
        var tutores = await _repo.GetAllAsync();
        return tutores.Select(t => _factory.ToResponse(t, t.Tesis.Count)).ToList();
    }
}
