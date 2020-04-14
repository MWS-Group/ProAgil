namespace ProAgil.Domain
{
  public class RedeSocial
  {
    public int id { get; set; }
    public string nome { get; set; }
    public string URL { get; set; }

    public int? eventoId { get; set; }
    public Evento evento { get; }
    public int? oradorId { get; set; }
    public Orador ordador { get; }
  }
}