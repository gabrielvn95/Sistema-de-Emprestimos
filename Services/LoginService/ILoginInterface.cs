using EmprestimoBanco.Dto;
using EmprestimoBanco.Models;

namespace EmprestimoBanco.Services.LoginService
{
    public interface ILoginInterface
    {
        Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioRegisterDto usuarioRegisterDto);
        Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuariologinDto);

    }
}
