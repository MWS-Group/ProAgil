using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.Dtos
{
  public class EventoDto
  {
    public int id { get; set; }

    [Required (ErrorMessage="Campo obrigatório")]
    [StringLength (100, MinimumLength=3, ErrorMessage="Local é entre 3 e 100 Ch")]
    public string local { get; set; }
    public string dataEvento { get; set; }

    [Required (ErrorMessage="O campo {0} deve ser preenchido")]
    public string tema { get; set; }

    [Range(2, 120000, ErrorMessage="Quantidade fora do intervalo")]
    public int qtdPessoas { get; set; }
    public string imagemURL { get; set; }

    [Phone]
    public string telefone { get; set; }

    [EmailAddress]
    public string email { get; set; }
    public List<LoteDto> lotes { get; set; }
    public List<RedeSocialDto> redesSociais { get; set; }
    public List<OradorDto> oradores { get; private set; }
  }
}