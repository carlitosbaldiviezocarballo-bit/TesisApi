using TesisApi.Domain.Entities;
using TesisApi.DTOs;

namespace TesisApi.Contracts.Factories;

public interface ITutorFactory
{
    Tutor Create(TutorCreateDto dto);
    TutorResponseDto ToResponse(Tutor tutor, int cantidadTesis);
}

public interface ITesisFactory
{
    Tesis Create(TesisCreateDto dto);
    TesisResponseDto ToResponse(Tesis tesis, int cantidadTesisTutor);
}
