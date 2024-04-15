using Moq;
using System.ComponentModel.DataAnnotations;
using TechChallengeContatos.Context;
using TechChallengeContatos.Entities;
using TechChallengeContatos.Interfaces;
using TechChallengeContatos.Services;

namespace TechChallengeContatosTest.Repository
{
    public class ContatoServiceTests
    {
        private ContatoService contatoService;


        public ContatoServiceTests()
        {
            contatoService = new ContatoService(new Mock<IContatoRepository>().Object);
        }


        #region Validando Exceptions

        [Fact(DisplayName = "O Nome deve ser informado")]
        public void Post_o_nome_deve_estar_preenchido()
        {

            var exception = Assert.Throws<Exception>(() => contatoService.CadastrarContato(new Contato("", "21", "232393339", "email")));
            Assert.Equal("O nome do contato não pode estar vazio.", exception.Message);
        }

        [Fact(DisplayName = "O Telefone deve ser informado")]
        public void Post_o_telefone_deve_estar_preenchido()
        {

            var exception = Assert.Throws<Exception>(() => contatoService.CadastrarContato(new Contato("Nome", "21", "", "email")));
            Assert.Equal("O telefone do contato não pode estar vazio.", exception.Message);
        }

        [Fact(DisplayName = "O Id deve ser informado")]
        public void Put_o_Id_deve_estar_preenchido()
        {

            var exception = Assert.Throws<Exception>(() => contatoService.AtualizarContato(new Contato("Nome", "21", "", "email"), Guid.Empty));
            Assert.Equal("Nenhum contato encontrado", exception.Message);
        }

        [Fact(DisplayName = "O Id deve ser enviado")]
        public void ContatoPor_O_Id_deve_ser_enviado()
        {

            var exception = Assert.Throws<Exception>(() => contatoService.ContatoPorId(Guid.Empty));
            Assert.Equal("Nenhum contato encontrado", exception.Message);
        }

        [Fact(DisplayName = "O DDD deve ser enviado")]
        public void PorRegiao_O_DDD_deve_ser_enviado()
        {

            var exception = Assert.Throws<Exception>(() => contatoService.ContatoPorRegiao(""));
            Assert.Equal("Nenhum contato encontrado", exception.Message);
        }

        [Fact(DisplayName = "O Id deve ser enviado")]
        public void Delete_O_Id_deve_ser_enviado()
        {

            var exception = Assert.Throws<Exception>(() => contatoService.DeletarContato(Guid.Empty));
            Assert.Equal("Nenhum contato encontrado", exception.Message);
        }

        #endregion



        #region Validando Objetos


        [Fact]
        public void Get_Validando_Objetos()
        {
            List<Contato> _contatos = new List<Contato>();

            _contatos.Add(new Contato("Jefferson", "21", "966187719", "jefferson14489@gmail.com"));

            var _contatoRepository = new Mock<IContatoRepository>();

            _contatoRepository.Setup(x => x.Listar()).Returns(_contatos);

            contatoService = new ContatoService(_contatoRepository.Object);

            var result = contatoService.ListarContato();

            Assert.True(result.Count > 0);
        }


        [Fact]
        public void Post_Enviando_Objeto_Invalido()
        {
            Contato contato = new Contato("Jefferson", "21", "966187719", "");

            var exception = Assert.Throws<ValidationException>(() => contatoService.CadastrarContato(contato));
            Assert.Equal("O E-mail é obrigatório", exception.Message);
        }

        #endregion
    }
}
