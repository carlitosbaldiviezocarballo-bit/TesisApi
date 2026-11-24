namespace TesisApi.Domain.Entities;

public class Tutor
{
    public const int MaxTesisPorTutor = 3;

    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Especialidad { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;

    public ICollection<Tesis> Tesis { get; set; } = new List<Tesis>();
}
