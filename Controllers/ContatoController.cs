using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechChallengeContatos.Entities;
using TechChallengeContatos.Interfaces;
using TechChallengeContatos.Repository;

namespace TechChallengeContatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
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
        public IActionResult Post(Contato contato)
        {
            var contatoCadastrado = _contatoRepository.CadastrarContato(contato);

            if (contatoCadastrado == null)
            {
                return BadRequest("Não foi possível cadastrar");
            }

            return Ok(contatoCadastrado);
        }

        [HttpPut]
        public IActionResult Put(Contato contato, Guid Id)
        {
            try
            {
                var contatoAtualizado = _contatoRepository.AtualizarContato(contato, Id);

                return Ok(contatoAtualizado);
            }
            catch (Exception e)
            {

                return BadRequest($"Não foi possível atualizar => {e.Message}");
            }
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
