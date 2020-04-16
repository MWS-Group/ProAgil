using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
  public class ProAgilRepository : IProAgilRepository
  {
    private readonly ProAgilContext context;

    public ProAgilRepository(ProAgilContext context)
    {
      this.context = context;
      this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    // Geral
    public void Add<T>(T entity) where T : class
    {
      this.context.Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
      this.context.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      this.context.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
      return (await this.context.SaveChangesAsync()) > 0;
    }

    // Evento
    public async Task<Evento[]> GetAllEventoAsync(bool includeOradores = false)
    {

      IQueryable<Evento> query = this.context.Eventos.Include(c => c.lotes).Include(c => c.redesSociais);

      if (includeOradores)
      {
        query = query.Include(oe => oe.oradoresEventos).ThenInclude(o => o.orador);
      }

      query = query.AsNoTracking().OrderByDescending(c => c.dataEvento);

      return await query.ToArrayAsync();
    }

    public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includeOradores)
    {
      IQueryable<Evento> query = this.context.Eventos.Include(c => c.lotes).Include(c => c.redesSociais);

      if (includeOradores)
      {
        query = query.Include(oe => oe.oradoresEventos).ThenInclude(o => o.orador);
      }

      query = query.AsNoTracking().OrderByDescending(c => c.dataEvento).Where(c => c.tema.Contains(tema));

      return await query.ToArrayAsync();
    }

    public async Task<Evento> GetEventoAsyncById(int eventoId, bool includeOradores)
    {
      IQueryable<Evento> query = this.context.Eventos.Include(c => c.lotes).Include(c => c.redesSociais);

      if (includeOradores)
      {
        query = query.Include(oe => oe.oradoresEventos).ThenInclude(o => o.orador);
      }

      query = query.AsNoTracking().OrderByDescending(c => c.dataEvento).Where(c => c.id == eventoId);

      return await query.FirstOrDefaultAsync();
    }

    // Orador
    public async Task<Orador[]> GetAllOradorAsyncByName(string nome, bool includeEventos = false)
    {
      IQueryable<Orador> query = this.context.Oradores.Include(c => c.redesSociais);

      if (includeEventos)
      {
        query = query.AsNoTracking().Include(oe => oe.oradoresEventos).ThenInclude(e => e.evento);
      }

      query = query.AsNoTracking().Where(o => o.nome.ToLower().Contains(nome.ToLower()));

      return await query.ToArrayAsync();
    }

    public async Task<Orador> GetOradorAsyncById(int oradorId, bool includeEventos = false)
    {
      IQueryable<Orador> query = this.context.Oradores.Include(c => c.redesSociais);

      if (includeEventos)
      {
        query = query.Include(oe => oe.oradoresEventos).ThenInclude(e => e.evento);
      }

      query = query.AsNoTracking().OrderBy(o => o.nome).Where(o => o.id == oradorId);

      return await query.FirstOrDefaultAsync();
    }
  }
}