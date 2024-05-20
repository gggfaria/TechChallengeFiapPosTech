using TechChallengeContatos.Domain.Contatos;
using TechChallengeContatos.Domain.Entities.ValueObjects;

namespace TechChallengeContatosTest.Domain;

public class ContatoTest
{
    //AAA Arrange, Act, Assert

    //Nomenclatura 
    //MetodoTestado_EstadoEmTeste_ComportamentoEsperado
    //ObjetoEmTeste_MetodoComportamentoEmTeste_ComportamentoEsperado
    
    
    [Theory]
    [Trait("Categoria", "Contatos")]
    [InlineData("12")]
    [InlineData("14")]
    public void EhValido_DddValido_True(string ddd)
    {
        Contato contato = new Contato(new Ddd(ddd), "988838883","Teste", "teste@mail.com");
        
        Assert.True(contato.EhValido());
    }
    
        
    [Theory]
    [Trait("Categoria", "Contatos")]
    [InlineData("10")]
    [InlineData("1000")]
    public void EhValido_DddInvalido_False(string ddd)
    {
        Contato contato = new Contato(new Ddd(ddd), "988838883","Teste", "teste@mail.com");

        bool ehValido = contato.EhValido();
        Assert.False(ehValido);
        Assert.Contains(contato.ResultadoValidacao.Errors, p => p.PropertyName == "Codigo");
    }
}