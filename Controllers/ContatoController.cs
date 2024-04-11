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
        private readonly IContatoRepository _contatoRepository;

        public ContatoController(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_contatoRepository.ListarContato());
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var contato = _contatoRepository.ContatoPorId(id);
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
                var contato = _contatoRepository.ContatoPorRegiao(ddd);
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

             _contatoRepository.CadastrarContato(contatoNovo);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(ContatoModel contato, Guid Id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var contatoNovo = new Contato(contato.Nome, contato.DDD, contato.Telefone, contato.Email);
            var contatoAtualizado = _contatoRepository.AtualizarContato(contatoNovo, Id);

                return Ok(contatoAtualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _contatoRepository.DeletarContato(id);

                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest($"Não foi possível executar => {e.Message}");
            }
        }
    }
}
