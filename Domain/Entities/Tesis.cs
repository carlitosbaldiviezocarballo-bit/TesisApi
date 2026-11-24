namespace TesisApi.Domain.Entities;

public enum EstadoTesis
{
    Pendiente = 0,
    EnProgreso = 1,
    Aprobada = 2,
    Rechazada = 3
}

public class Tesis
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Estudiante { get; set; } = string.Empty;
    public EstadoTesis Estado { get; set; } = EstadoTesis.Pendiente;
    public string? Imagen { get; set; }

    public int IdTutor { get; set; }
    public Tutor Tutor { get; set; } = null!;
}
