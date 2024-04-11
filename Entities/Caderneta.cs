namespace TechChallengeContatos.Entities;

public class Caderneta
{
    public string Nome { get; private set; }
    public int QuantidadeDeContatos { get; set; }
    private HashSet<Contato> _contatos;
    public IReadOnlyCollection<Contato> Contatos => _contatos;

    private Caderneta()
    {
        _contatos = new HashSet<Contato>();
    }

    public Caderneta(string nome)
    {
        Nome = nome;
    }
    public void AdicionarContato(Contato contato)
    {
        _contatos.Add(contato);
    }

}