using MediatR;
using TechChallengeContatos.Domain.Commands;
using TechChallengeContatos.Domain.Repositories;

namespace TechChallengeContatos.ConsumerService.Handlers;

public class AtualizarContatoHandler : IRequestHandler<AtualizarContatoCommand>
{
    private readonly IContatoRepository _contatoRepository;

    public AtualizarContatoHandler(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }

    public async Task Handle(AtualizarContatoCommand request, CancellationToken cancellationToken)
    {
        _contatoRepository.Atualizar(request.Contato);
    }
}