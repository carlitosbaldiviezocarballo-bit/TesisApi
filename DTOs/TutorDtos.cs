using System.ComponentModel.DataAnnotations;

namespace TesisApi.DTOs;

public class TutorCreateDto
{
    [Required, StringLength(120, MinimumLength = 3)]
    public string Nombre { get; set; } = string.Empty;

    [Required, StringLength(120, MinimumLength = 3)]
    public string Especialidad { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(150)]
    public string Correo { get; set; } = string.Empty;
}

public class TutorResponseDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Especialidad { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public int CantidadTesis { get; set; }
}
