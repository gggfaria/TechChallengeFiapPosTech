using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TechChallengeContatos.Domain.Contatos;
using TechChallengeContatos.Domain.Entities.ValueObjects;
using TechChallengeContatos.Service.DTOs;

namespace TechChallengeContatos.Web.Profiles;

public class ContatoProfile: Profile
{ 
    public ContatoProfile()
    {
        CreateMap<CadastroContatoDto, Contato>()
            .ForMember(
                dest => dest.Ddd,
                opt => opt.MapFrom(src => new Ddd(src.Ddd))
            );
        
        CreateMap<AtualizaContatoDto, Contato>()
            .ForMember(
                dest => dest.Ddd,
                opt => opt.MapFrom(src => new Ddd(src.Ddd))
            );
        
        
                
        CreateMap<Contato, ViewContatoDto>()
            .ForMember(
                dest => dest.Ddd,
                opt => opt.MapFrom(src => src.Ddd.Codigo)
            );

    }
}