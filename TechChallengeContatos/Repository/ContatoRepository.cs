using TechChallengeContatos.Context;
using TechChallengeContatos.Entities;
using TechChallengeContatos.Interfaces;

namespace TechChallengeContatos.Repository
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly AppDbContext _context;
        public ContatoRepository(AppDbContext context)
        {
            _context = context;

        }
        public void Cadastrar(Contato contato)
        {
            _context.Contatos.Add(contato);
            _context.SaveChanges();
        }

        public List<Contato> Listar()
        {
            return _context.Contatos.ToList();
        }

        public List<Contato> PorRegiao(string DDD)
        {
            return _context.Contatos.Where(x => x.DDD == DDD).ToList();
        }

        public void Atualizar(Contato contato)
        {
            _context.Contatos.Update(contato);
            _context.SaveChanges();
        }

        public void Deletar(Contato contato)
        {
            _context.Contatos.Remove(contato);
            _context.SaveChanges();
        }

        public Contato PorId(Guid Id)
        {
            return _context.Contatos.Find(Id);
        }
    }
}
