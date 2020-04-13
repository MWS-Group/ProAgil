using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
  public interface IProAgilRepository
  {
    // Geral
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;

    Task<bool> SaveChangesAsync();

    // Eventos
    Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includeOradores);
    Task<Evento[]> GetAllEventoAsync(bool includeOradores);
    Task<Evento> GetEventoAsyncById(int eventoId, bool includeOradores);

    // Oradores
    Task<Orador[]> GetAllOradorAsyncByName(string nome, bool includeEventos);
    Task<Orador> GetOradorAsyncById(int oradorId, bool includeEventos);

  }
}