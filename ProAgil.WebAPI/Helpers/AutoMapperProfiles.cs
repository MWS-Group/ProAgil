using System.Linq;
using AutoMapper;
using ProAgil.Domain;
using ProAgil.Domain.Identity;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<Evento, EventoDto>()
        .ForMember(destinatario => destinatario.oradores, options => {
          options.MapFrom(src => src.oradoresEventos.Select(x => x.orador).ToList());
        }).ReverseMap();

      CreateMap<Orador, OradorDto>()
        .ForMember(destinatario => destinatario.eventos, options => {
          options.MapFrom(src => src.oradoresEventos.Select(x => x.evento).ToList());
        }).ReverseMap();

      CreateMap<Lote, LoteDto>().ReverseMap();
      CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();

      CreateMap<User, UserDto>().ReverseMap();
      CreateMap<User, UserLoginDto>().ReverseMap();
    }
  }
}