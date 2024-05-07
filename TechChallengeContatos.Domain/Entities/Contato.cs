using FluentValidation.Results;
using TechChallengeContatos.Domain.Entities.ValueObjects;
using TechChallengeContatos.Domain.Extensions;
using TechChallengeContatos.Domain.Validators;

namespace TechChallengeContatos.Domain.Contatos;

public class Contato
{
    protected Contato()
    {
        
    }
    
    public ValidationResult ResultadoValidacao { get; private set; }
    public Ddd Ddd { get; private set; }
    public string Telefone { get; private set; }
    
    public bool EhValido()
    {
        var validador = new ContatoValidator();
        ResultadoValidacao = validador.Validate(this);
        ValidarDdd();

        return ResultadoValidacao.IsValid;
    }

    private void ValidarDdd()
    {
        if (Ddd.EhValido()) 
            return;
        
        ResultadoValidacao.AddErrors(Ddd.ResultadoValidacao);
    }
    
}