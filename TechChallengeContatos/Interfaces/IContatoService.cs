using TechChallengeContatos.Entities;

namespace TechChallengeContatos.Interfaces
{
    public interface IContatoService
    {
        public List<Contato> ListarContato();
        public void CadastrarContato(Contato contato);
        public Contato AtualizarContato(Contato contato, Guid Id);
        public List<Contato> ContatoPorRegiao(string DDD);
        public void DeletarContato(Guid Id);
        public Contato ContatoPorId(Guid Id);
    }
}
