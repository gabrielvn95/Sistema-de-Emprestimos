using EmprestimoBanco.Data;
using EmprestimoBanco.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmprestimoBanco.Services.EmprestimosService
{
    public class EmprestimosService : IEmprestimosInterface
    {
        private readonly ApplicationDbContext _context;
        public EmprestimosService(ApplicationDbContext context)
        {
            _context = context;  
        }

        public async Task<DataTable> BuscarDadosEmprestimoExcel()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados empréstimos";

            dataTable.Columns.Add("Recebedor", typeof(string));
            dataTable.Columns.Add("Banco Fornecedor", typeof(string));
            dataTable.Columns.Add("Quantia Emprestada", typeof(string));
            dataTable.Columns.Add("Data empréstimo", typeof(DateTime));

            var emprestimos = await BuscarEmprestimos();

            if (emprestimos.Dados.Count > 0)
            {
                emprestimos.Dados.ForEach(emprestimo =>
                {
                    dataTable.Rows.Add(emprestimo.Recebedor, emprestimo.BancoFornecedor, emprestimo.QuantiaEmprestimo, emprestimo.DataUltimaAtualizacao);
                });
            }


            return dataTable;
        }

        public async Task<ResponseModel<List<EmprestimoModel>>> BuscarEmprestimos()
        {

            ResponseModel<List<EmprestimoModel>> response = new ResponseModel<List<EmprestimoModel>>();

            
            try
            {
                var emprestimos = await _context.Emprestimos.ToListAsync();

                response.Dados = emprestimos;
                response.Mensagem = "Dados coletados com sucesso!";

                return response;
            }
            catch (Exception ex)
            {

                response.Mensagem = ex.Message;
                response.Status = false;
                return response;


            }
        }

        public async Task<ResponseModel<EmprestimoModel>> BuscarEmprestimosPorId(int? id)
        {
            ResponseModel<EmprestimoModel> response = new ResponseModel<EmprestimoModel>();

            try
            {
               if(id == null)
                {
                    response.Mensagem = "Emprestimo não localizado!";
                    response.Status = false;
                    return response;
                }

               var emprestimo = await _context.Emprestimos.FirstOrDefaultAsync(x => x.Id == id);

                if(emprestimo == null)
                {
                    response.Mensagem = "Emprestimo não localizado!";
                    response.Status = false;
                    return response;
                }

                response.Dados = emprestimo;
                response.Mensagem = "Dados coletados com sucesso!";

                return response;


            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<EmprestimoModel>> CadastrarEmprestimo(EmprestimoModel emprestimoModel)
        {
            ResponseModel<EmprestimoModel> response = new ResponseModel<EmprestimoModel>();

            try
            {
                _context.Add(emprestimoModel);
                await _context.SaveChangesAsync();
                response.Mensagem = "Cadastro realizado com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<EmprestimoModel>> EditarEmprestimo(EmprestimoModel emprestimoModel)
        {
            ResponseModel<EmprestimoModel> response = new ResponseModel<EmprestimoModel>();

            try
            {
                var emprestimo = await BuscarEmprestimosPorId(emprestimoModel.Id);

                if(emprestimo.Status == false)
                {
                    return emprestimo;
                }

                emprestimo.Dados.QuantiaEmprestimo = emprestimoModel.QuantiaEmprestimo;
                emprestimo.Dados.BancoFornecedor = emprestimoModel.BancoFornecedor;
                emprestimo.Dados.Recebedor = emprestimoModel.Recebedor;

                _context.Update(emprestimo.Dados);
                await _context.SaveChangesAsync();

                response.Mensagem = "Edição realizada com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<EmprestimoModel>> RemoveEmprestimo(EmprestimoModel emprestimoModel)
        {
            ResponseModel<EmprestimoModel> response = new ResponseModel<EmprestimoModel>();

            try
            {
                _context.Remove(emprestimoModel);
                await _context.SaveChangesAsync();

                response.Mensagem = "Remoção realizada com sucesso!";
                return response;



            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
