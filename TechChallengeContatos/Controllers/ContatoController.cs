using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechChallengeContatos.Entities;
using TechChallengeContatos.Interfaces;
using TechChallengeContatos.Models;
using TechChallengeContatos.Repository;

namespace TechChallengeContatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : MainController
    {
        private readonly IContatoService _contatoService;

        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_contatoService.ListarContato());
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var contato = _contatoService.ContatoPorId(id);
                return Ok(contato);
            }
            catch (Exception e)
            {
                return BadRequest($"Não foi possível executar => {e.Message}");
            }
        }

        [HttpGet("api/por-regiao/{ddd}")]
        public IActionResult Get(string ddd)
        {
            try
            {
                var contato = _contatoService.ContatoPorRegiao(ddd);
                return Ok(contato);
            }
            catch (Exception e)
            {
                return BadRequest($"Não foi possível executar => {e.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(ContatoModel contato)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var contatoNovo = new Contato(contato.Nome, contato.DDD, contato.Telefone, contato.Email);

             _contatoService.CadastrarContato(contatoNovo);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(ContatoModel contato, Guid Id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var contatoNovo = new Contato(contato.Nome, contato.DDD, contato.Telefone, contato.Email);
            var contatoAtualizado = _contatoService.AtualizarContato(contatoNovo, Id);

                return Ok(contatoAtualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _contatoService.DeletarContato(id);

                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest($"Não foi possível executar => {e.Message}");
            }
        }
    }
}
