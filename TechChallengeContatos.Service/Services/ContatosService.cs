using AutoMapper;
using Microsoft.Extensions.Options;
using TechChallengeContatos.Domain.Commands;
using TechChallengeContatos.Domain.Contatos;
using TechChallengeContatos.Domain.Extensions;
using TechChallengeContatos.Domain.Repositories;
using TechChallengeContatos.Domain.Settings;
using TechChallengeContatos.Service.DTOs;
using TechChallengeContatos.Service.Interfaces;
using TechChallengeContatos.Service.Reponses;

namespace TechChallengeContatos.Service.Services;

public class ContatoService : ServicePublisherBase, IContatoService
{
    private readonly IContatoRepository _contatoRepository;
    private readonly IMapper _mapper;

    public ContatoService(IContatoRepository contatoRepository, IMapper mapper,
        IOptions<RabbitMQSettings> rabbitMqSetting) : base(rabbitMqSetting)
    {
        _contatoRepository = contatoRepository;
        _mapper = mapper;
    }

    public async Task<ResultService> AtualizarContato(AtualizaContatoDto dto, Guid id)
    {
        var contato = _contatoRepository.SelecionaPorId(id);

        if (contato is null)
            return ResultServiceFactory.NotFound("Não encontrado");

        contato.Atualizar(dto.Nome, dto.Telefone, dto.Ddd, dto.Email);
        if (!contato.EhValido())
            return ResultServiceFactory.BadRequest(contato.ResultadoValidacao.GetErrorsResult(), "Dados inválidos");

        await PublishMessageAsync(new AtualizarContatoCommand(contato));

        return ResultServiceFactory.NoContent("Contato será atualizado");
    }

    public async Task<ResultService> CadastrarContato(CadastroContatoDto dto)
    {
        var contato = _mapper.Map<Contato>(dto);
        if (!contato.EhValido())
            return ResultServiceFactory.BadRequest(contato.ResultadoValidacao.GetErrorsResult(), "Dados inválidos");
        
        await PublishMessageAsync(new CadastrarContatoCommand(contato));
        return ResultServiceFactory.NoContent("Contato será cadastrado em breve.");
    }

    public async Task<ResultService> ContatoPorId(Guid id)
    {
        var contato = _contatoRepository.SelecionaPorId(id);

        if (contato is null)
            return ResultServiceFactory.NotFound("Contato não encontrado");
        
        return ResultServiceFactory<ViewContatoDto>.Ok(_mapper.Map<ViewContatoDto>(contato));
    }

    public async Task<ResultService> ContatoPorRegiao(string ddd)
    {
        var contatos = _contatoRepository.SelecionaPorRegiao(ddd);

        if (contatos?.Count < 1)
            return ResultServiceFactory.NoContent();
        
        return ResultServiceFactory<IEnumerable<ViewContatoDto>>.Ok(_mapper.Map<IEnumerable<ViewContatoDto>>(contatos));
    }

    public async Task<ResultService> DeletarContato(Guid id)
    {
        var contato = _contatoRepository.SelecionaPorId(id);
        var result = _contatoRepository.Deletar(contato);

        if (!result)
            return ResultServiceFactory.InternalServerError("Falha ao deletar");

        return ResultServiceFactory.NoContent("Deletado com sucesso");
    }

    public async Task<ResultService> ListarContato()
    {
        var contatos = _contatoRepository.ListarTodos();

        if (contatos?.Count() < 1)
            return ResultServiceFactory.NoContent();

        return ResultServiceFactory<IEnumerable<ViewContatoDto>>.Ok(_mapper.Map<IEnumerable<ViewContatoDto>>(contatos));
    }
}