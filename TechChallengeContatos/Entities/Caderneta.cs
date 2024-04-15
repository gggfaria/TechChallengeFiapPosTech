namespace TechChallengeContatos.Entities;

public class Caderneta
{
    public Guid Id { get; set;}
    public string Nome { get; private set; }
    public int QuantidadeDeContatos { get; set; }
}