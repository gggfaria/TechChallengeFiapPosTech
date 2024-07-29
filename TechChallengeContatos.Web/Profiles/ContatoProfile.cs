using AutoMapper;
using TechChallengeContatos.Domain.Contatos;
using TechChallengeContatos.Domain.Entities.ValueObjects;
using TechChallengeContatos.Service.DTOs;

namespace TechChallengeContatos.Web.Profiles
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            CreateMap<CadastroContatoDto, Contato>()
                .ConstructUsing(src => new Contato(new Ddd(src.Ddd), src.Telefone, src.Nome, src.Email));

            CreateMap<AtualizaContatoDto, Contato>()
                .ConstructUsing(src => new Contato(new Ddd(src.Ddd), src.Telefone, src.Nome, src.Email));

            CreateMap<Contato, ViewContatoDto>()
                .ForMember(
                    dest => dest.Ddd,
                    opt => opt.MapFrom(src => src.Ddd.Codigo)
                );
        }
    }
}
