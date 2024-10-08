using System.ComponentModel.DataAnnotations;

namespace EmprestimoBanco.Models
{
    public class AlterarSenhaModel
    {
        [Required(ErrorMessage = "Digite a senha atual do usuário")]
        public int Id { get; set; }
        public string? SenhaAtual { get; set; }

        [Required(ErrorMessage = "Digite a nova senha do usuário")]
        public string? NovaSenha { get; set; }
        [Required(ErrorMessage = "Confirme a nova senha do usuário")]
        [Compare("NovaSenha", ErrorMessage = "Senha não confere com a nova senha")]
        public string? ConfirmarNovaSenha { get; set; }
    }
}
