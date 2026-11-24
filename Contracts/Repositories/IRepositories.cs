using TesisApi.Domain.Entities;

namespace TesisApi.Contracts.Repositories;

public interface ITutorRepository
{
    Task<Tutor?> GetByIdAsync(int id);
    Task<IReadOnlyList<Tutor>> GetAllAsync();
    Task<bool> ExistsByCorreoAsync(string correo);
    Task<bool> ExistsByNombreAsync(string nombre);
    Task<Tutor> AddAsync(Tutor tutor);
}

public interface ITesisRepository
{
    Task<Tesis?> GetByIdAsync(int id);
    Task<IReadOnlyList<Tesis>> BuscarAsync(string? termino);
    Task<bool> ExistsByTituloAsync(string titulo);
    Task<int> CountByTutorAsync(int idTutor);
    Task<Tesis> AddAsync(Tesis tesis);
    Task UpdateAsync(Tesis tesis);
}
