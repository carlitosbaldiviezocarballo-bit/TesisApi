using Microsoft.EntityFrameworkCore;
using TesisApi.Data;
using TesisApi.Domain.Entities;
using TesisApi.Contracts.Repositories;

namespace TesisApi.Repositories;

public class TutorRepository : ITutorRepository
{
    private readonly AppDbContext _db;
    public TutorRepository(AppDbContext db) => _db = db;

    public Task<Tutor?> GetByIdAsync(int id) =>
        _db.Tutores.Include(t => t.Tesis).FirstOrDefaultAsync(t => t.Id == id);

    public async Task<IReadOnlyList<Tutor>> GetAllAsync() =>
        await _db.Tutores.Include(t => t.Tesis).OrderBy(t => t.Nombre).ToListAsync();

    public Task<bool> ExistsByCorreoAsync(string correo) =>
        _db.Tutores.AnyAsync(t => t.Correo == correo);

    public Task<bool> ExistsByNombreAsync(string nombre) =>
        _db.Tutores.AnyAsync(t => t.Nombre == nombre);

    public async Task<Tutor> AddAsync(Tutor tutor)
    {
        _db.Tutores.Add(tutor);
        await _db.SaveChangesAsync();
        return tutor;
    }
}

public class TesisRepository : ITesisRepository
{
    private readonly AppDbContext _db;
    public TesisRepository(AppDbContext db) => _db = db;

    public Task<Tesis?> GetByIdAsync(int id) =>
        _db.Tesis.Include(t => t.Tutor).FirstOrDefaultAsync(t => t.Id == id);

    public async Task<IReadOnlyList<Tesis>> BuscarAsync(string? termino)
    {
        var q = _db.Tesis.Include(t => t.Tutor).AsQueryable();
        if (!string.IsNullOrWhiteSpace(termino))
        {
            var t = termino.Trim();
            q = q.Where(x => x.Titulo.Contains(t) || x.Estudiante.Contains(t));
        }
        return await q.OrderBy(x => x.Titulo).ToListAsync();
    }

    public Task<bool> ExistsByTituloAsync(string titulo) =>
        _db.Tesis.AnyAsync(t => t.Titulo == titulo);

    public Task<int> CountByTutorAsync(int idTutor) =>
        _db.Tesis.CountAsync(t => t.IdTutor == idTutor);

    public async Task<Tesis> AddAsync(Tesis tesis)
    {
        _db.Tesis.Add(tesis);
        await _db.SaveChangesAsync();
        return tesis;
    }

    public async Task UpdateAsync(Tesis tesis)
    {
        _db.Tesis.Update(tesis);
        await _db.SaveChangesAsync();
    }
}
