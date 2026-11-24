using TesisApi.Domain.Entities;
using TesisApi.DTOs;
using TesisApi.Contracts.Factories;

namespace TesisApi.Factories;

public class TutorFactory : ITutorFactory
{
    public Tutor Create(TutorCreateDto dto) => new()
    {
        Nombre = dto.Nombre.Trim(),
        Especialidad = dto.Especialidad.Trim(),
        Correo = dto.Correo.Trim().ToLowerInvariant()
    };

    public TutorResponseDto ToResponse(Tutor t, int cantidadTesis) => new()
    {
        Id = t.Id,
        Nombre = t.Nombre,
        Especialidad = t.Especialidad,
        Correo = t.Correo,
        CantidadTesis = cantidadTesis
    };
}

public class TesisFactory : ITesisFactory
{
    private readonly ITutorFactory _tutorFactory;

    public TesisFactory(ITutorFactory tutorFactory) => _tutorFactory = tutorFactory;

    public Tesis Create(TesisCreateDto dto) => new()
    {
        Titulo = dto.Titulo.Trim(),
        Estudiante = dto.Estudiante.Trim(),
        Estado = dto.Estado,
        IdTutor = dto.IdTutor
    };

    public TesisResponseDto ToResponse(Tesis t, int cantidadTesisTutor) => new()
    {
        Id = t.Id,
        Titulo = t.Titulo,
        Estudiante = t.Estudiante,
        Estado = t.Estado.ToString(),
        Imagen = t.Imagen,
        Tutor = _tutorFactory.ToResponse(t.Tutor, cantidadTesisTutor)
    };
}
