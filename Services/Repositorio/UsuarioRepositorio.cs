using EmprestimoBanco.Data;
using EmprestimoBanco.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmprestimoBanco.Services.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }


        public UsuarioModel BuscarPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() );
        }

        public UsuarioModel BuscarPorID(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _context.Usuarios.ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = BuscarPorID(usuario.Id);

            if (usuarioDB == null) throw new Exception("Erro na atualização do usuário.");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;

            _context.Usuarios.Update(usuarioDB);
            _context.SaveChanges();

            return usuarioDB;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = BuscarPorID(id);

            if (usuarioDB == null) throw new Exception("Erro ao deletar o usuário.");

            _context.Usuarios.Remove(usuarioDB);
            _context.SaveChanges();

            return true;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = BuscarPorID(alterarSenhaModel.Id);

            if (usuarioDB == null)
                throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");

            if (!VerificarSenhaHash(alterarSenhaModel.SenhaAtual, usuarioDB.SenhaHash, usuarioDB.SenhaSalt))
                throw new Exception("Senha atual não confere!");

            if (VerificarSenhaHash(alterarSenhaModel.NovaSenha, usuarioDB.SenhaHash, usuarioDB.SenhaSalt))
                throw new Exception("Nova senha deve ser diferente da senha atual!");

            CriarSenhaHash(alterarSenhaModel.NovaSenha, out byte[] novaSenhaHash, out byte[] novaSenhaSalt);
            usuarioDB.SenhaHash = novaSenhaHash;
            usuarioDB.SenhaSalt = novaSenhaSalt;
         

            _context.Usuarios.Update(usuarioDB);
            _context.SaveChanges();

            return usuarioDB;
        }

        private bool VerificarSenhaHash(string senha, byte[] senhaHashArmazenada, byte[] senhaSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(senhaSalt))
            {
                var senhaHashCalculada = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return senhaHashCalculada.SequenceEqual(senhaHashArmazenada);
            }
        }

        private void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        
    }
}
