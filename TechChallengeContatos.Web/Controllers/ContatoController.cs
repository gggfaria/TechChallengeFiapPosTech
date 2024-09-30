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
        public async Task<IActionResult> Get()
        {
            var result = await _contatoService.ListarContato();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _contatoService.ContatoPorId(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("api/por-regiao/{ddd}")]
        public async Task<IActionResult> Get(string ddd)
        {
            var result = await _contatoService.ContatoPorRegiao(ddd);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CadastroContatoDto dto)
        {
            var result = await _contatoService.CadastrarContato(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(AtualizaContatoDto dto, Guid id)
        {
            var result = await _contatoService.AtualizarContato(dto, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _contatoService.DeletarContato(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}