using System.ComponentModel.DataAnnotations;
using AutoMapper;
using TechChallengeContatos.Domain.Contatos;
using TechChallengeContatos.Domain.Extensions;
using TechChallengeContatos.Domain.Repositories;
using TechChallengeContatos.Service.DTOs;
using TechChallengeContatos.Service.Interfaces;
using TechChallengeContatos.Service.Reponses;

namespace TechChallengeContatos.Service.Services;

public class ContatoService : IContatoService
{
    private readonly IContatoRepository _contatoRepository;
    private readonly IMapper _mapper;

    public ContatoService(IContatoRepository contatoRepository, IMapper mapper)
    {
        _contatoRepository = contatoRepository;
        _mapper = mapper;
    }

    public ResultService AtualizarContato(AtualizaContatoDto dto, Guid id)
    {
        var contato = _contatoRepository.SelecionaPorId(id);

        if (contato is null)
            return ResultServiceFactory.NotFound("Não encontrado");
        
        contato.Atualizar(dto.Nome, dto.Telefone, dto.Ddd, dto.Email);
        if (!contato.EhValido())
            return ResultServiceFactory.BadRequest(contato.ResultadoValidacao.GetErrorsResult(), "Dados inválidos");

        var result = _contatoRepository.Atualizar(contato);

        if (!result)
            return ResultServiceFactory.InternalServerError("Falha ao atualizar");

        return ResultServiceFactory.NoContent("Atualizado com sucesso");
    }

    public ResultService CadastrarContato(CadastroContatoDto dto)
    {
        var contato = _mapper.Map<Contato>(dto);
        if (!contato.EhValido())
            return ResultServiceFactory.BadRequest(contato.ResultadoValidacao.GetErrorsResult(), "Dados inválidos");

        var result = _contatoRepository.Cadastrar(contato);

        if (!result)
            return ResultServiceFactory.InternalServerError("Falha ao cadastrar");

        var returnDto = _mapper.Map<ViewContatoDto>(contato);

        return ResultServiceFactory<ViewContatoDto>.Ok(returnDto, "Cadastrado com sucesso");
    }

    public ResultService ContatoPorId(Guid id)
    {
        var contato = _contatoRepository.SelecionaPorId(id);

        if (contato is null)
            return ResultServiceFactory.NotFound("Contato não encontrado");


        return ResultServiceFactory<ViewContatoDto>.Ok(_mapper.Map<ViewContatoDto>(contato));
    }

    public ResultService ContatoPorRegiao(string ddd)
    {
        var contatos = _contatoRepository.SelecionaPorRegiao(ddd);

        if (contatos?.Count() < 1)
            return ResultServiceFactory.NoContent();


        return ResultServiceFactory<IEnumerable<ViewContatoDto>>.Ok(_mapper.Map<IEnumerable<ViewContatoDto>>(contatos));
    }

    public ResultService DeletarContato(Guid id)
    {
        var contato = _contatoRepository.SelecionaPorId(id);
        var result = _contatoRepository.Deletar(contato);

        if (!result)
            return ResultServiceFactory.InternalServerError("Falha ao deletar");

        return ResultServiceFactory.NoContent("Deletado com sucesso");
    }

    public ResultService ListarContato()
    {
        var contatos = _contatoRepository.ListarTodos();

        if (contatos?.Count() < 1)
            return ResultServiceFactory.NoContent();


        return ResultServiceFactory<IEnumerable<ViewContatoDto>>.Ok(_mapper.Map<IEnumerable<ViewContatoDto>>(contatos));
    }
}