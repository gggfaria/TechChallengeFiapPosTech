using System.Linq.Expressions;
using TechChallengeContatos.Domain.Contatos;

namespace TechChallengeContatos.Domain.Repositories;

public interface IContatoRepository
{
    List<Contato> ListarTodos();
    bool Cadastrar(Contato contato);
    bool Atualizar(Contato contato);
    List<Contato>? SelecionaPorRegiao(string ddd);
    bool Deletar(Contato contato);
    Contato? SelecionaPorId(Guid id, bool asNoTracking = false);
}