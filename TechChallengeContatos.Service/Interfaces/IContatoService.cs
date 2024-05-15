using TechChallengeContatos.Service.DTOs;
using TechChallengeContatos.Service.Reponses;

namespace TechChallengeContatos.Service.Interfaces;

public interface IContatoService
{
    ResultService ListarContato();
    ResultService CadastrarContato(CadastroContatoDto contato);
    ResultService AtualizarContato(AtualizaContatoDto dto, Guid id);
    ResultService ContatoPorRegiao(string ddd);
    ResultService DeletarContato(Guid id);
    ResultService ContatoPorId(Guid id);
}