using EmprestimoBanco.Models;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace EmprestimoBanco.Services.EmprestimosService
{
    public interface IEmprestimosInterface
    {
        Task<DataTable> BuscarDadosEmprestimoExcel();
        Task<ResponseModel<List<EmprestimoModel>>> BuscarEmprestimos();
        Task<ResponseModel<EmprestimoModel>> BuscarEmprestimosPorId(int? id);
        Task<ResponseModel<EmprestimoModel>> CadastrarEmprestimo(EmprestimoModel emprestimoModel);
        Task<ResponseModel<EmprestimoModel>> EditarEmprestimo(EmprestimoModel emprestimoModel);
        Task<ResponseModel<EmprestimoModel>> RemoveEmprestimo(EmprestimoModel emprestimoModel);
        
        


    }
}
