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
        public Contato CadastrarContato(Contato contato)
        {
            _context.Contatos.Add(contato);
            _context.SaveChanges();

            return contato;
        }

        public IList<Contato> ListarContato()
        {
            return _context.Contatos.ToList();
        }

        public IList<Contato> ContatoPorRegiao(string DDD)
        {
            var contato = _context.Contatos.Where(x => x.DDD == DDD).ToList();

            if (contato == null)
            {
                throw new Exception("Nenhum valor encontrado");
            }

            return contato;
        }

        public Contato AtualizarContato(Contato contato, Guid Id)
        {
            var contatoEncontrado = _context.Contatos.Find(Id);

            if(contatoEncontrado == null)
            {
                throw new Exception("Nenhum valor encontrado");
            }
            contatoEncontrado.Telefone = contato.Telefone;
            contatoEncontrado.DDD = contato.DDD;
            contatoEncontrado.Nome = contato.Nome;
            contatoEncontrado.Email = contato.Email;

            _context.Contatos.Update(contatoEncontrado);
            _context.SaveChanges();

            return contato;
        }

        public void DeletarContato(Guid Id)
        {
            var contatoEncontrado = _context.Contatos.Find(Id);

            if (contatoEncontrado == null)
            {
                throw new Exception("Nenhum valor encontrado");
            }

            _context.Contatos.Remove(contatoEncontrado);
            _context.SaveChanges();
        }

        public Contato ContatoPorId(Guid Id)
        {
            var contatoEncontrado = _context.Contatos.Find(Id);

            if (contatoEncontrado == null)
            {
                throw new Exception("Nenhum valor encontrado");
            }

            return contatoEncontrado;
        }
    }
}
