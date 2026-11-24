using System.ComponentModel.DataAnnotations;
using TesisApi.Domain.Entities;

namespace TesisApi.DTOs;

public class TesisCreateDto
{
    [Required, StringLength(250, MinimumLength = 5)]
    public string Titulo { get; set; } = string.Empty;

    [Required, StringLength(120, MinimumLength = 3)]
    public string Estudiante { get; set; } = string.Empty;

    public EstadoTesis Estado { get; set; } = EstadoTesis.Pendiente;

    [Range(1, int.MaxValue, ErrorMessage = "idTutor es obligatorio.")]
    public int IdTutor { get; set; }
}

/// <summary>Resultado de buscar tesis: incluye los datos del tutor.</summary>
public class TesisResponseDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Estudiante { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string? Imagen { get; set; }
    public TutorResponseDto Tutor { get; set; } = null!;
}
