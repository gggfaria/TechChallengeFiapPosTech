using TechChallengeContatos.Service.DTOs;
using TechChallengeContatos.Service.Reponses;

namespace TechChallengeContatos.Service.Interfaces;

public interface IContatoService
{
    Task<ResultService> ListarContato();
    Task<ResultService> CadastrarContato(CadastroContatoDto contato);
    Task<ResultService> AtualizarContato(AtualizaContatoDto dto, Guid id);
    Task<ResultService> ContatoPorRegiao(string ddd);
    Task<ResultService> DeletarContato(Guid id);
    Task<ResultService> ContatoPorId(Guid id);
}