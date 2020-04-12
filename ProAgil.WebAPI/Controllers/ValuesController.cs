﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.WebAPI.Data;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {
    public readonly DataContext context;
    public ValuesController(DataContext context)
    {
      this.context = context;
    }

    // GET api/values
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Evento>>> Get()
    {
      // Abertura de uma nova thread
      try
      {
        var results = await context.Eventos.ToListAsync(); // espera que o processo termine

        return Ok(results);
      }
      catch (System.Exception)
      {
          return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
      }
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Evento>> Get(int id)
    {
      try
      {
        var results = await context.Eventos.FirstOrDefaultAsync(x => x.eventoId == id); // espera que o processo termine

        return Ok(results);
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
      }
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
