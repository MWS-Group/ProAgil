namespace ProAgil.Domain
{
  public class RedeSocial
  {
    public int id { get; set; }
    public string nome { get; set; }
    public string URL { get; set; }

    public int? enventoId { get; set; }
    public Evento envento { get; set; }
    public int? oradorId { get; set; }
    public Orador ordador { get; set; }
  }
}