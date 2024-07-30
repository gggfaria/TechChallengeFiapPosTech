using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechChallengeContatos.Domain.Contatos;
using TechChallengeContatos.Domain.Entities.ValueObjects;
using TechChallengeContatos.Infra.Context;
using TechChallengeContatos.Infra.Repositories;
using TechChallengeContatos.Service.DTOs;
using TechChallengeContatos.Service.Interfaces;
using TechChallengeContatos.Service.Reponses;
using TechChallengeContatos.Service.Services;
using TechChallengeContatos.Web.Profiles;
using TechChallengeContatosTest.Factories;
using Xunit;

namespace TechChallengeContatosTest.Factories
{
    public class ContatoRepositoryTests : IDisposable
    {
        private readonly ContatosDbContext _context;

        public ContatoRepositoryTests()
        {
            _context = TestDbContextFactory.Create();
        }

        [Fact]
        public void CanAddContato()
        {
            var contato = new Contato(new Ddd("12"), "988838883", "Teste", "teste@mail.com");

            _context.Set<Contato>().Add(contato);
            _context.SaveChanges();

            var savedContato = _context.Set<Contato>().FirstOrDefault(c => c.Email == "teste@mail.com");

            Assert.NotNull(savedContato);
            Assert.Equal("Teste", savedContato.Nome);
        }

        [Fact]
        public void CanUpdateContato()
        {
            var contato = new Contato(new Ddd("12"), "988838883", "Teste", "teste@mail.com");
            _context.Set<Contato>().Add(contato);
            _context.SaveChanges();

            contato.Atualizar("Teste Atualizado", "977777777", "11", "atualizado@mail.com");
            _context.Set<Contato>().Update(contato);
            _context.SaveChanges();

            var updatedContato = _context.Set<Contato>().FirstOrDefault(c => c.Email == "atualizado@mail.com");

            Assert.NotNull(updatedContato);
            Assert.Equal("Teste Atualizado", updatedContato.Nome);
            Assert.Equal("977777777", updatedContato.Telefone);
            Assert.Equal("11", updatedContato.Ddd.Codigo);
        }

        [Fact]
        public void CanCadastrarContato()
        {
            var contatoService = new ContatoService(new ContatoRepository(_context), new MapperConfiguration(cfg => cfg.AddProfile(new ContatoProfile())).CreateMapper());
            var dto = new CadastroContatoDto
            {
                Nome = "Novo Contato",
                Ddd = "12",
                Telefone = "988888888",
                Email = "novo@mail.com"
            };

            var result = contatoService.CadastrarContato(dto);

            Assert.True(result.Sucesso);
            Assert.NotNull(_context.Set<Contato>().FirstOrDefault(c => c.Email == "novo@mail.com"));
        }

        [Fact]
        public void CanAtualizarContato()
        {
            var contatoService = new ContatoService(new ContatoRepository(_context), new MapperConfiguration(cfg => cfg.AddProfile(new ContatoProfile())).CreateMapper());
            var contato = new Contato(new Ddd("12"), "988838883", "Teste", "teste@mail.com");
            _context.Set<Contato>().Add(contato);
            _context.SaveChanges();

            var dto = new AtualizaContatoDto
            {
                Id = contato.Id,
                Nome = "Contato Atualizado",
                Ddd = "11",
                Telefone = "977777777",
                Email = "atualizado@mail.com"
            };

            var result = contatoService.AtualizarContato(dto, contato.Id);

            Assert.True(result.Sucesso);
            var updatedContato = _context.Set<Contato>().FirstOrDefault(c => c.Email == "atualizado@mail.com");
            Assert.NotNull(updatedContato);
            Assert.Equal("Contato Atualizado", updatedContato.Nome);
            Assert.Equal("11", updatedContato.Ddd.Codigo);
            Assert.Equal("977777777", updatedContato.Telefone);
        }

        [Fact]
        public void CanListarContatos()
        {
            var contatoService = new ContatoService(new ContatoRepository(_context), new MapperConfiguration(cfg => cfg.AddProfile(new ContatoProfile())).CreateMapper());
            _context.Set<Contato>().Add(new Contato(new Ddd("12"), "988838883", "Teste", "teste@mail.com"));
            _context.SaveChanges();

            var result = contatoService.ListarContato() as ResultService<IEnumerable<ViewContatoDto>>;

            Assert.True(result.Sucesso);
            Assert.NotEmpty(result.Dados);
        }

        [Fact]
        public void CanBuscarContatoPorRegiao()
        {
            var contatoService = new ContatoService(new ContatoRepository(_context), new MapperConfiguration(cfg => cfg.AddProfile(new ContatoProfile())).CreateMapper());
            _context.Set<Contato>().Add(new Contato(new Ddd("12"), "988838883", "Teste", "teste@mail.com"));
            _context.SaveChanges();

            var result = contatoService.ContatoPorRegiao("12") as ResultService<IEnumerable<ViewContatoDto>>;

            Assert.True(result.Sucesso);
            Assert.NotEmpty(result.Dados);
        }

        [Fact]
        public void CanDeletarContato()
        {
            var contatoService = new ContatoService(new ContatoRepository(_context), new MapperConfiguration(cfg => cfg.AddProfile(new ContatoProfile())).CreateMapper());
            var contato = new Contato(new Ddd("12"), "988838883", "Teste", "teste@mail.com");
            _context.Set<Contato>().Add(contato);
            _context.SaveChanges();

            var result = contatoService.DeletarContato(contato.Id);

            Assert.True(result.Sucesso);
            Assert.Null(_context.Set<Contato>().FirstOrDefault(c => c.Id == contato.Id));
        }

        [Fact]
        public void CanBuscarContatoPorId()
        {
            var contatoService = new ContatoService(new ContatoRepository(_context), new MapperConfiguration(cfg => cfg.AddProfile(new ContatoProfile())).CreateMapper());
            var contato = new Contato(new Ddd("12"), "988838883", "Teste", "teste@mail.com");
            _context.Set<Contato>().Add(contato);
            _context.SaveChanges();

            var result = contatoService.ContatoPorId(contato.Id) as ResultService<ViewContatoDto>;

            Assert.True(result.Sucesso);
            Assert.NotNull(result.Dados);
        }

        public void Dispose()
        {
            TestDbContextFactory.Destroy(_context);
        }
    }
}
