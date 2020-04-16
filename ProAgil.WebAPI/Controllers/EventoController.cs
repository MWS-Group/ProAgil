using System.Collections.Generic;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.WebAPI.Dto;

namespace ProAgil.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EventoController : ControllerBase
  {
    private readonly IProAgilRepository repo;
    private readonly IMapper mapper;

    public EventoController(IProAgilRepository repo, IMapper mapper)
    {
      this.mapper = mapper;
      this.repo = repo;
    }

    // GET api/evento
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Evento>>> Get()
    {
      // Abertura de uma nova thread
      try
      {
        var eventos = await repo.GetAllEventoAsync(true); // espera que o processo termine

        var results = this.mapper.Map<EventoDto[]>(eventos);

        return Ok(results);
      }
      catch (System.Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou {ex.Message}");
      }
    }

    // GET api/evento/id
    [HttpGet("{eventoId}")]
    public async Task<ActionResult<IEnumerable<Evento>>> Get(int eventoId)
    {
      // Abertura de uma nova thread
      try
      {
        var evento = await repo.GetEventoAsyncById(eventoId, true); // espera que o processo termine

        var results = this.mapper.Map<EventoDto>(evento);

        return Ok(results);
      }
      catch (System.Exception e)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, new { erro = e });
      }
    }

    // GET api/evento/tema
    [HttpGet("getByTema/{tema}")]
    public async Task<ActionResult<IEnumerable<Evento>>> Get(string tema)
    {
      // Abertura de uma nova thread
      try
      {
        var eventos = await repo.GetAllEventoAsyncByTema(tema, true); // espera que o processo termine

        var results = this.mapper.Map<EventoDto[]>(eventos);

        return Ok(results);
      }
      catch (System.Exception e)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, new { erro = e });
      }
    }

    // POST api/eventos
    [HttpPost]
    public async Task<ActionResult<IEnumerable<Evento>>> Post(EventoDto model)
    {
      // Abertura de uma nova thread
      try
      {
        var evento = this.mapper.Map<Evento>(model);

        this.repo.Add(evento);

        if (await this.repo.SaveChangesAsync())
        {
          return Created($"/api/evento/{model.id}", this.mapper.Map<EventoDto>(evento));
        }
      }
      catch (System.Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou {ex.Message}");
      }

      return BadRequest();
    }

    // PUT api/eventos/id
    [HttpPut("{eventoId}")]
    public async Task<ActionResult<IEnumerable<Evento>>> Put(int eventoId, EventoDto model)
    {
      // Abertura de uma nova thread
      try
      {
        var evento = await this.repo.GetEventoAsyncById(eventoId, false);

        if (evento == null) return NotFound();

        this.mapper.Map(model, evento);

        this.repo.Update(evento);

        if (await this.repo.SaveChangesAsync())
        {
          return Created($"/api/evento/{model.id}", this.mapper.Map<EventoDto>(evento));
        }
      }
      catch (System.Exception e)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, new { erro = e });
      }

      return BadRequest();
    }

    // DELETE api/eventos/id
    [HttpDelete("{eventoId}")]
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
      catch (System.Exception e)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, new { erro = e });
      }

      return BadRequest();
    }

  }
}