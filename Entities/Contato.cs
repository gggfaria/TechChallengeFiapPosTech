using System.ComponentModel.DataAnnotations;

namespace TechChallengeContatos.Entities
{
    public class Contato
    {
        public string Nome { get; private set; }

        public string DDD { get; private set; }

        public string Telefone { get; private set; }

        public string Email { get; private set; }
        
        public virtual Caderneta Caderneta { get;  set; }

        
        
        private Contato(){}
        public Contato(string nome, string ddd, string telefone, string email)
        {
            Nome = nome;
            DDD = ddd;
            Telefone = telefone;
            Email = email;
        }

        public void Atualizar(Contato contato)
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
