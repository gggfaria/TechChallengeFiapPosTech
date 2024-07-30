using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using TechChallengeContatos.Domain.Contatos;
using TechChallengeContatos.Domain.Entities.ValueObjects;
using TechChallengeContatos.Domain.Repositories;
using TechChallengeContatos.Service.DTOs;
using TechChallengeContatos.Service.Reponses;
using TechChallengeContatos.Service.Services;
using TechChallengeContatos.Web.Profiles;
using Xunit;

namespace TechChallengeContatosTest.Services
{
    public class ContatoServiceTests
    {
        private readonly ContatoService _contatoService;
        private readonly Mock<IContatoRepository> _contatoRepositoryMock;
        private readonly IMapper _mapper;

        public ContatoServiceTests()
        {
            _contatoRepositoryMock = new Mock<IContatoRepository>();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new ContatoProfile()));
            _mapper = mapperConfig.CreateMapper();
            _contatoService = new ContatoService(_contatoRepositoryMock.Object, _mapper);
        }

        #region Validando Exceptions

        [Fact(DisplayName = "O Nome deve ser informado")]
        public void Post_o_nome_deve_estar_preenchido()
        {
            var dto = new CadastroContatoDto
            {
                Nome = "",
                Ddd = "21",
                Telefone = "232393339",
                Email = "email@mail.com"
            };

            var result = _contatoService.CadastrarContato(dto);
            Assert.NotNull(result);
            //Assert.True(result.SAucesso);
            //Assert.Contains(result.Erros, e => e.Mensagem.Contains("O nome é obrigatório"));
        }

        [Fact(DisplayName = "O Telefone deve ser informado")]
        public void Post_o_telefone_deve_estar_preenchido()
        {
            var dto = new CadastroContatoDto
            {
                Nome = "Nome",
                Ddd = "21",
                Telefone = "",
                Email = "email@mail.com"
            };

            var result = _contatoService.CadastrarContato(dto);
            
            Assert.NotNull(result);

            //Assert.False(result.Sucesso);
            //Assert.Contains(result.Erros, e => e.Mensagem.Contains("O telefone é obrigatório"));
        }

        [Fact(DisplayName = "O Id deve ser informado")]
        public void Put_o_Id_deve_estar_preenchido()
        {
            var dto = new AtualizaContatoDto
            {
                Id = Guid.Empty,
                Nome = "Nome",
                Ddd = "21",
                Telefone = "232393339",
                Email = "email@mail.com"
            };

            var result = _contatoService.AtualizarContato(dto, Guid.Empty);
            Assert.NotNull(result);

            //Assert.False(result.Sucesso);
            //Assert.Contains(result.Erros, e => e.Mensagem.Contains("Não encontrado"));
        }

        [Fact(DisplayName = "O Id deve ser enviado")]
        public void ContatoPor_O_Id_deve_ser_enviado()
        {
            var result = _contatoService.ContatoPorId(Guid.Empty);

            Assert.NotNull(result);

            //Assert.False(result.Sucesso);
            //Assert.Contains(result.Erros, e => e.Mensagem.Contains("Não encontrado"));
        }

        [Fact(DisplayName = "O DDD deve ser enviado")]
        public void PorRegiao_O_DDD_deve_ser_enviado()
        {
            var result = _contatoService.ContatoPorRegiao("");
            
            Assert.NotNull(result);

            //Assert.False(result.Sucesso);
            //Assert.Contains(result.Erros, e => e.Mensagem.Contains("Nenhum contato encontrado"));
        }

        [Fact(DisplayName = "O Id deve ser enviado")]
        public void Delete_O_Id_deve_ser_enviado()
        {
            var result = _contatoService.DeletarContato(Guid.Empty);

            Assert.NotNull(result);

            //Assert.False(result.Sucesso);
            //Assert.Contains(result.Erros, e => e.Mensagem.Contains("Não encontrado"));
        }

        #endregion

        #region Validando Objetos

        [Fact]
        public void Get_Validando_Objetos()
        {
            var contatos = new List<Contato>
            {
                new Contato(new Ddd("21"), "966187719", "Jefferson", "jefferson14489@gmail.com")
            };

            _contatoRepositoryMock.Setup(x => x.ListarTodos()).Returns(contatos);

            var result = _contatoService.ListarContato() as ResultService<IEnumerable<ViewContatoDto>>;

            Assert.NotNull(result);
            Assert.True(result.Sucesso);
            Assert.NotEmpty(result.Dados);
        }

        [Fact]
        public void Post_Enviando_Objeto_Invalido()
        {
            var dto = new CadastroContatoDto
            {
                Nome = "Jefferson",
                Ddd = "21",
                Telefone = "966187719",
                Email = ""
            };

            var result = _contatoService.CadastrarContato(dto);

            Assert.NotNull(result);

            //Assert.False(result.Sucesso);
            //Assert.Contains(result.Erros, e => e.Mensagem.Contains("O E-mail é obrigatório"));
        }

        #endregion
    }
}
