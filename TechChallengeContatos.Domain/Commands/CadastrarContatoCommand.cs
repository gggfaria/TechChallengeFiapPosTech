using MediatR;
using TechChallengeContatos.Domain.Contatos;

namespace TechChallengeContatos.Domain.Commands;

public class CadastrarContatoCommand : IRequest
{
    public Contato Contato { get; set; }

    public CadastrarContatoCommand(Contato contato)
    {
        Contato = contato;
    }
}