using System.ComponentModel.DataAnnotations;
using TechChallengeContatos.Entities;
using TechChallengeContatos.Interfaces;

namespace TechChallengeContatos.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public Contato AtualizarContato(Contato contato, Guid Id)
        {

            var contatoEncontrado = ContatoPorId(Id);

            if (contatoEncontrado == null)
            {
                throw new Exception("Nenhum contato encontrado");
            }

            contatoEncontrado.AtualizarCampos(contato);
            _contatoRepository.Atualizar(contatoEncontrado);

            return contatoEncontrado;
        }

        public void CadastrarContato(Contato contato)
        {
            if (contato == null) throw new Exception("Contato não pode ser vazio");
            if (string.IsNullOrEmpty(contato.Nome))
            {
                throw new Exception("O nome do contato não pode estar vazio.");
            }
            if (string.IsNullOrEmpty(contato.Telefone))
            {
                throw new Exception("O telefone do contato não pode estar vazio.");
            }

            Validator.ValidateObject(contato, new ValidationContext(contato), true);

            _contatoRepository.Cadastrar(contato);
        }

        public Contato ContatoPorId(Guid Id)
        {
            var contato = _contatoRepository.PorId(Id);

            if (contato == null)
            {
                throw new Exception("Nenhum contato encontrado");
            }

            return contato;
        }

        public List<Contato> ContatoPorRegiao(string DDD)
        {
            var contatos = _contatoRepository.PorRegiao(DDD);

            if (contatos == null)
            {
                throw new Exception("Nenhum contato encontrado");
            }

            return contatos;
        }

        public void DeletarContato(Guid Id)
        {
            var contato = ContatoPorId(Id);

            if (contato == null)
            {
                throw new Exception("Nenhum contato encontrado");
            }

            _contatoRepository.Deletar(contato);

        }

        public List<Contato> ListarContato()
        {
            return _contatoRepository.Listar();
        }
    }
}
