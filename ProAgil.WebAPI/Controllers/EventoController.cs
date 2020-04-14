using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EventoController : ControllerBase
  {
    private readonly IProAgilRepository repo;
    public EventoController(IProAgilRepository repo)
    {
      this.repo = repo;
    }

    // GET api/evento
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Evento>>> Get()
    {
      // Abertura de uma nova thread
      try
      {
        var results = await repo.GetAllEventoAsync(true); // espera que o processo termine

        return Ok(results);
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
      }
    }

    // GET api/evento/id
    [HttpGet("{eventoId}")]
    public async Task<ActionResult<IEnumerable<Evento>>> Get(int eventoId)
    {
      // Abertura de uma nova thread
      try
      {
        var results = await repo.GetEventoAsyncById(eventoId, true); // espera que o processo termine

        return Ok(results);
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
      }
    }

    // GET api/evento/tema
    [HttpGet("getByTema/{tema}")]
    public async Task<ActionResult<IEnumerable<Evento>>> Get(string tema)
    {
      // Abertura de uma nova thread
      try
      {
        var results = await repo.GetAllEventoAsyncByTema(tema, true); // espera que o processo termine

        return Ok(results);
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
      }
    }

    // POST api/eventos
    [HttpPost]
    public async Task<ActionResult<IEnumerable<Evento>>> Post(Evento model)
    {
      // Abertura de uma nova thread
      try
      {
        this.repo.Add(model);

        if(await this.repo.SaveChangesAsync())
        {
            return Created($"/api/evento/{model.id}", model);
        }
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
      }

      return BadRequest();
    }

    // PUT api/eventos/id
    [HttpPut]
    public async Task<ActionResult<IEnumerable<Evento>>> Put(int eventoId, Evento model)
    {
      // Abertura de uma nova thread
      try
      {
        var evento = await this.repo.GetEventoAsyncById(eventoId, false);

        if(evento == null) return NotFound();

        this.repo.Update(model);

        if (await this.repo.SaveChangesAsync())
        {
          return Created($"/api/evento/{model.id}", model);
        }
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
      }

      return BadRequest();
    }

    // DELETE api/eventos/id
    [HttpDelete]
    public async Task<ActionResult<IEnumerable<Evento>>> Delete(int eventoId)
    {
      // Abertura de uma nova thread
      try
      {
        var evento = await this.repo.GetEventoAsyncById(eventoId, false);

        if (evento == null) return NotFound();

        this.repo.Delete(evento);

        if (await this.repo.SaveChangesAsync())
        {
          return Ok();
        }
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
      }

      return BadRequest();
    }

  }
}