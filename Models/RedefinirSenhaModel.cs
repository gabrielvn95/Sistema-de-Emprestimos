using System.ComponentModel.DataAnnotations;

namespace EmprestimoBanco.Models
{
    public class RedefinirSenhaModel
    {

        [Required(ErrorMessage = "Digite o e-mail ")]
        public string? Email { get; set; }


    }
}
