using FluentValidation.Results;
using TechChallengeContatos.Domain.Entities.ValueObjects;
using TechChallengeContatos.Domain.Extensions;
using TechChallengeContatos.Domain.Validators;

namespace TechChallengeContatos.Domain.Contatos;

public class Contato
{
    public Contato(Ddd ddd, string telefone, string nome, string email)
    {
        Ddd = ddd;
        Telefone = telefone;
        Nome = nome;
        Email = email;
        Id = Guid.NewGuid();
    }

    public Contato()
    {
    }

    public ValidationResult ResultadoValidacao { get; private set; }

    public Guid Id { get; set; }
    public Ddd Ddd { get; set; }
    public string Telefone { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }


    public void Atualizar(string nome, string telefone, string ddd, string email)
    {
        Ddd = new Ddd(ddd);
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }

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