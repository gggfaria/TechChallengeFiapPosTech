using FluentValidation.Results;
using TechChallengeContatos.Domain.Contatos;
using TechChallengeContatos.Domain.Entities;

namespace TechChallengeContatos.Domain.Extensions;

public static class ValidatorExtension
{
    public static void AddErrors(this ValidationResult validationResult, ValidationResult otherValidationResult)
    {
        foreach (var error in otherValidationResult.Errors)
        {
            validationResult.Errors.Add(error);
        }
    }

    public static void AddError(this ValidationResult validationResult, ValidationFailure error)
    {
        validationResult.Errors.Add(error);
    }

    public static ICollection<DadosInvalidosResult> GetErrorsResult(this ValidationResult validationResult)
    {
        var errorsResult = new List<DadosInvalidosResult>();

        foreach (var error in validationResult.Errors)
        {
            errorsResult.Add(new DadosInvalidosResult(error.PropertyName, error.ErrorMessage));
        }

        return errorsResult;
    }
}