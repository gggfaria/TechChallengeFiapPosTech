namespace TechChallengeContatos.Domain.Entities;

public class DadosInvalidosResult
{
    public DadosInvalidosResult(string propriedade, string mensagem)
    {
        Propriedade = propriedade;
        Mensagem = mensagem;
    }

    public string Propriedade { get; private set; }
    public string Mensagem { get; private set; }
}