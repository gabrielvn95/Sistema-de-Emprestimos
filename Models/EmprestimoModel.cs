using System.ComponentModel.DataAnnotations;

namespace EmprestimoBanco.Models
{
    public class EmprestimoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do Recebedor!")]
        public string Recebedor { get; set; }
        [Required(ErrorMessage = "Digite o nome do Banco Fornecedor!")]
        public string BancoFornecedor { get; set; }
        [Required(ErrorMessage = "Digite a quantia Emprestada!")]
        public string QuantiaEmprestimo { get; set; }

        public DateTime DataUltimaAtualizacao { get; set; } = DateTime.Now;
    }
}
