using TechChallengeContatos.Entities;

namespace TechChallengeContatos.Interfaces
{
    public interface IContatoRepository
    {
        public IList<Contato> ListarContato();
        public Contato CadastrarContato(Contato contato);
        public Contato AtualizarContato(Contato contato, Guid Id);
        public IList<Contato> ContatoPorRegiao(string DDD);
        public void DeletarContato(Guid Id);
        public Contato ContatoPorId(Guid Id);
    }
}
