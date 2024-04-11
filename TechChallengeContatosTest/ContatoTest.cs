using Moq;
using Moq.AutoMock;
using TechChallengeContatos.Entities;
using TechChallengeContatos.Interfaces;

namespace TechChallengeContatosTest
{
    public class ContatoTest
    {
        private readonly AutoMocker _mocker;

        public ContatoTest()
        {
            _mocker = new AutoMocker();
        }

        [Fact]
        public void CadastrarContato()
        {
            Caderneta caderneta = new Caderneta("Jefferson");

            caderneta.AdicionarContato(new Contato("Nome", "21", "232393339", "email"));


            Assert.Single(caderneta.Contatos);
        }

    }
}