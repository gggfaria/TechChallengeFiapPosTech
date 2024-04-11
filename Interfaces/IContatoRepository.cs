using TechChallengeContatos.Entities;
using TechChallengeContatos.Models;

namespace TechChallengeContatos.Interfaces
{
    public interface IContatoRepository
    {
        public List<Contato> ListarContato();
        public void CadastrarContato(Contato contato);
        public Contato AtualizarContato(Contato contato, Guid Id);
        public List<Contato> ContatoPorRegiao(string DDD);
        public void DeletarContato(Guid Id);
        public Contato ContatoPorId(Guid Id);
    }
}
