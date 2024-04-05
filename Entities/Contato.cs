using System.ComponentModel.DataAnnotations;

namespace TechChallengeContatos.Entities
{
    public class Contato
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string? Nome { get; set; }
        public string? DDD { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
    }
}
