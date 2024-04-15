using TechChallengeContatos.Entities;

namespace TechChallengeContatos.Interfaces
{
    public interface IContatoRepository
    {
        public List<Contato> Listar();
        public void Cadastrar(Contato contato);
        public void Atualizar(Contato contato);
        public List<Contato> PorRegiao(string DDD);
        public void Deletar(Contato contato);
        public Contato PorId(Guid Id);
    }
}
