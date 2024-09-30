using TechChallengeContatos.Domain.Contatos;

namespace TechChallengeContatos.Domain.Commands;

public class AtualizarContatoCommand
{
    public Contato Contato { get; set; }

    public AtualizarContatoCommand(Contato contato)
    {
        Contato = contato;
    }
}