using System.Collections.Generic;

namespace ProAgil.Domain
{
  public class Orador
  {
    public int id { get; set; }
    public string nome { get; set; }
    public string miniCurriculo { get; set; }
    public string imagemURL { get; set; }
    public string telefone { get; set; }
    public string email { get; set; }

    public List<RedeSocial> redesSociais { get; set; }
    public List<OradorEvento> oradoresEventos { get; set; }
  }
}