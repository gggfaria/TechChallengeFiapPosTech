using AutoMapper;
using Moq;
using TechChallengeContatos.Domain.Contatos;
using TechChallengeContatos.Domain.Entities.ValueObjects;
using TechChallengeContatos.Domain.Repositories;
using TechChallengeContatos.Service.DTOs;
using TechChallengeContatos.Service.Services;
using Xunit;

namespace TechChallengeContatosTest.Repository
{
    public class ContatoServiceTests
    {
        private readonly ContatoService _contatoService;
        private readonly Mock<IContatoRepository> _contatoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public ContatoServiceTests()
        {
            _contatoRepositoryMock = new Mock<IContatoRepository>();
            _mapperMock = new Mock<IMapper>();
            _contatoService = new ContatoService(_contatoRepositoryMock.Object, _mapperMock.Object);
        }

        //[Fact(DisplayName = "O Nome deve ser informado")]
        //public void Post_o_nome_deve_estar_preenchido()
        //{
        //    var dto = new CadastroContatoDto
        //    {
        //        Nome = "",
        //        Ddd = "21",
        //        Telefone = "232393339",
        //        Email = "email@example.com"
        //    };

        //    var result = _contatoService.CadastrarContato(dto);
        //    Assert.False(result.Sucesso);
        //    //Assert.Contains("O nome é obrigatório", string.Join(", ", result.Erros?.Select(e => e.Mensagem ?? string.Empty) ?? new List<string>()));
        //}

        //[Fact(DisplayName = "O Telefone deve ser informado")]
        //public void Post_o_telefone_deve_estar_preenchido()
        //{
        //    var dto = new CadastroContatoDto
        //    {
        //        Nome = "Nome",
        //        Ddd = "21",
        //        Telefone = "",
        //        Email = "email@example.com"
        //    };

        //    var result = _contatoService.CadastrarContato(dto);
        //    Assert.False(result.Sucesso);
        //    //Assert.Contains("O Telefone é obrigatório", string.Join(", ", result.Erros?.Select(e => e.Mensagem ?? string.Empty) ?? new List<string>()));
        //}

        [Fact(DisplayName = "O Id deve ser informado")]
        public void Put_o_Id_deve_estar_preenchido()
        {
            var dto = new AtualizaContatoDto
            {
                Nome = "Nome",
                Ddd = "21",
                Telefone = "232393339",
                Email = "email@example.com"
            };

            var result = _contatoService.AtualizarContato(dto, Guid.Empty);
            Assert.False(result.Sucesso);
            Assert.Contains("Contato não encontrado", result.Mensagem ?? string.Empty);
        }

        [Fact(DisplayName = "O Id deve ser enviado")]
        public void ContatoPor_O_Id_deve_ser_enviado()
        {
            var result = _contatoService.ContatoPorId(Guid.Empty);
            Assert.False(result.Sucesso);
            Assert.Contains("Contato não encontrado", result.Mensagem ?? string.Empty);
        }

        [Fact(DisplayName = "O DDD deve ser enviado")]
        public void PorRegiao_O_DDD_deve_ser_enviado()
        {
            var result = _contatoService.ContatoPorRegiao("");
            Assert.False(result.Sucesso);
            //Assert.Contains("O DDD deve ser enviado", result.Mensagem ?? string.Empty);
        }

        [Fact(DisplayName = "O Id deve ser enviado")]
        public void Delete_O_Id_deve_ser_enviado()
        {
            var result = _contatoService.DeletarContato(Guid.Empty);
            Assert.False(result.Sucesso);
            //Assert.Contains("O Id deve ser enviado", result.Mensagem ?? string.Empty);
        }

        [Fact]
        public void Get_Validando_Objetos()
        {
            var contatos = new List<Contato>
            {
                new Contato(new Ddd("21"), "966187719", "Jefferson", "jefferson14489@gmail.com")
            };

            _contatoRepositoryMock.Setup(x => x.ListarTodos()).Returns(contatos);

            var result = _contatoService.ListarContato();

            Assert.True(result.Sucesso);
            //Assert.NotEmpty(result.Dados as IEnumerable<object>);
        }

        //[Fact]
        //public void Post_Enviando_Objeto_Invalido()
        //{
        //    var dto = new CadastroContatoDto
        //    {
        //        Nome = "Jefferson",
        //        Ddd = "21",
        //        Telefone = "966187719",
        //        Email = ""
        //    };

        //    var result = _contatoService.CadastrarContato(dto);
        //    Assert.False(result.Sucesso);
        //    Assert.Contains("O E-mail é obrigatório", string.Join(", ", result.Erros?.Select(e => e.Mensagem ?? string.Empty) ?? new List<string>()));
        //}
    }
}
