namespace ProAgil.Domain
{
  public class OradorEvento
  {
    public int oradorId { get; set; }
    public Orador orador { get; set; }
    public int eventoId { get; set; }
    public Evento evento { get; set; }
  }
}