using FluentValidation;
using TechChallengeContatos.Domain.Contatos;

namespace TechChallengeContatos.Domain.Validators
{
    public class ContatoValidator : AbstractValidator<Contato>
    {
        public ContatoValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório");

            RuleFor(c => c.Ddd)
                .NotEmpty().WithMessage("O DDD é obrigatório");

            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("O Telefone é obrigatório");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O E-mail é obrigatório")
                .EmailAddress().WithMessage("E-mail em formato inválido.");
        }
    }
}
