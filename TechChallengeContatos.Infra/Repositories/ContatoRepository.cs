using Microsoft.EntityFrameworkCore;
using TechChallengeContatos.Domain.Contatos;
using TechChallengeContatos.Domain.Repositories;
using TechChallengeContatos.Infra.Context;

namespace TechChallengeContatos.Infra.Repositories;

public class ContatoRepository : IContatoRepository
{
    protected readonly ContatosDbContext _context;

    public ContatoRepository(ContatosDbContext context)
    {
        _context = context;
    }


    public List<Contato> ListarTodos()
    {
        return _context.Set<Contato>()
            .AsNoTracking()
            .ToList();
    }

    public bool Cadastrar(Contato contato)
    {
        _context.Set<Contato>().AddAsync(contato);
        return Commit();
    }

    public bool Atualizar(Contato contato)
    {
        var entry = _context.Entry(contato);
        if (entry.State == EntityState.Detached)
        {
            _context.Set<Contato>().Attach(contato);
        }

        entry.State = EntityState.Modified;
        return Commit();
    }

    public List<Contato>? SelecionaPorRegiao(string ddd)
    {
        return _context.Set<Contato>()
            .AsNoTracking()
            .Where(p => p.Ddd.Codigo == ddd)
            .ToList();
    }

    public bool Deletar(Contato contato)
    {
        if (_context.Entry(contato).State == EntityState.Detached)
            _context.Set<Contato>().Attach(contato);

        _context.Set<Contato>().Remove(contato);
        return Commit();
    }

    public Contato? SelecionaPorId(Guid id, bool asNoTracking)
    {
        IQueryable<Contato?> query = _context.Set<Contato>().AsQueryable();
        if (asNoTracking) query = query.AsNoTracking();

        return query.SingleOrDefault(e => e.Id.Equals(id));
    }


    private bool Commit()
    {
        var numberEntriesSaved = _context.SaveChanges();
        return numberEntriesSaved > 0;
    }
}