﻿using EmprestimoBanco.Models;

namespace EmprestimoBanco.Services.SessaoService
{
    public interface ISessaoInterface
    {
        UsuarioModel BuscarSessao();
        void CriarSessao(UsuarioModel usuarioModel);

        void RemoveSessao();
    }
}
