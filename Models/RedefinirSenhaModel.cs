using System.ComponentModel.DataAnnotations;

namespace EmprestimoBanco.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Digite o E-mail")]

        public string Email { get; set; }


    }
}
