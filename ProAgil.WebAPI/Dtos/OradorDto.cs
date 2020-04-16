using System.Collections.Generic;

namespace ProAgil.WebAPI.Dto
{
  public class OradorDto
  {
    public int id { get; set; }
    public string nome { get; set; }
    public string miniCurriculo { get; set; }
    public string imagemURL { get; set; }
    public string telefone { get; set; }
    public string email { get; set; }

    public List<RedeSocialDto> redesSociais { get; set; }
    public List<EventoDto> eventos { get; set; }
  }
}