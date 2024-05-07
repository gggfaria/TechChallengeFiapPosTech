using FluentValidation.Results;
using TechChallengeContatos.Domain.Validators;

namespace TechChallengeContatos.Domain.Entities.ValueObjects;

public class Ddd
{
    public string Codigo { get; private set; }
    public ValidationResult ResultadoValidacao { get; private set; }
    
    protected Ddd()
    {
    }

    public Ddd(string codigo)
    {
        Codigo = codigo;
    }
    
    public bool EhValido()
    {
        var validator = new DddValidator();
        ResultadoValidacao = validator.Validate(this);

        return ResultadoValidacao.IsValid;
    }
 
    
    #region Overrides

    public override string ToString()
    {
        return Codigo;
    }

    public override bool Equals(object obj)
    {
        var compareTo = obj as Ddd;

        if (ReferenceEquals(this, compareTo)) return true;
        if (compareTo is null) return false;

        return this.Codigo.Equals(compareTo.Codigo);
    }

    public static bool operator ==(Ddd a, Ddd b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Ddd a, Ddd b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Codigo.GetHashCode();
    }

    #endregion

}
