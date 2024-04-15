using System.ComponentModel.DataAnnotations;

namespace TechChallengeContatos.Entities
{
    public class Contato
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; private set; }

        [Required(ErrorMessage = "O DDD é obrigatório")]
        public string DDD { get; private set; }

        [Required(ErrorMessage = "O Telefone é obrigatório")]
        public string Telefone { get; private set; }

        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; private set; }

        private Contato() { }
        public Contato(string nome, string ddd, string telefone, string email)
        {
            Nome = nome;
            DDD = ddd;
            Telefone = telefone;
            Email = email;
        }

        public void AtualizarCampos(Contato contato)
        {
            Nome = contato.Nome;
            DDD = contato.DDD;
            Telefone = contato.Telefone;
            Email = contato.Email;
        }

        public void SetNome(string nome)
        {
            this.Nome = nome;
        }

        public void SetDDD(string ddd)
        {
            this.DDD = ddd;
        }

        public void SetTelefone(string Telefone)
        {
            this.Telefone = Telefone;
        }

        public void SetEmail(string Email)
        {
            this.Email = Email;
        }

        protected bool Equals(Contato other)
        {
            return Nome == other.Nome && Telefone == other.Telefone && Email == other.Email;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Contato)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nome, Telefone, Email);
        }
    }
}
