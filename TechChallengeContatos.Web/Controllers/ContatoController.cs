using Microsoft.AspNetCore.Mvc;
using TechChallengeContatos.Service.DTOs;
using TechChallengeContatos.Service.Interfaces;

namespace TechChallengeContatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _contatoService;

        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _contatoService.ListarContato();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var result = _contatoService.ContatoPorId(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("api/por-regiao/{ddd}")]
        public IActionResult Get(string ddd)
        {
            var result = _contatoService.ContatoPorRegiao(ddd);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public IActionResult Post(CadastroContatoDto dto)
        {
            var result = _contatoService.CadastrarContato(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult Put(AtualizaContatoDto dto, Guid id)
        {
            var result = _contatoService.AtualizarContato(dto, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _contatoService.DeletarContato(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
