using FluentValidation;
using TechChallengeContatos.Domain.Contatos;

namespace TechChallengeContatos.Domain.Validators
{
    public class ContatoValidator : AbstractValidator<Contato>
    {
        public ContatoValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome � obrigat�rio");

            RuleFor(c => c.Ddd)
                .NotEmpty().WithMessage("O DDD � obrigat�rio");

            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("O Telefone � obrigat�rio");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O E-mail � obrigat�rio")
                .EmailAddress().WithMessage("E-mail em formato inv�lido.");
        }
    }
}
