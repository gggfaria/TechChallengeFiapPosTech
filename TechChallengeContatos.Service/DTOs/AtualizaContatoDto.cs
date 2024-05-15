using System.ComponentModel.DataAnnotations;

namespace TechChallengeContatos.Service.DTOs;

public class AtualizaContatoDto
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O DDD é obrigatório")]
    public string Ddd { get; set; }

    [Required(ErrorMessage = "O Telefone é obrigatório")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "O E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
    public string Email { get; set; }
}