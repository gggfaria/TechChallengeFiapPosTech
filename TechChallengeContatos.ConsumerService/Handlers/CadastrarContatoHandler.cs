using MediatR;
using TechChallengeContatos.Domain.Commands;
using TechChallengeContatos.Domain.Repositories;

namespace TechChallengeContatos.ConsumerService.Handlers;

public class CadastrarContatoHandler : IRequestHandler<CadastrarContatoCommand>
{
    private readonly IContatoRepository _contatoRepository;

    public CadastrarContatoHandler(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }

    public async Task Handle(CadastrarContatoCommand request, CancellationToken cancellationToken)
    {
        _contatoRepository.Cadastrar(request.Contato);
    }
}