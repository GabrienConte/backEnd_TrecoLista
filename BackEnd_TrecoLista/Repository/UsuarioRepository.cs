﻿using BackEnd_TrecoLista.Infraestrutura;
using BackEnd_TrecoLista.Model;
using BackEnd_TrecoLista.Repository.Interfaces;

namespace BackEnd_TrecoLista.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        public void Add(Usuario usuario)
        {
           _context.Usuarios.Add(usuario);
           _context.SaveChanges();
        }

        public List<Usuario> Get()
        {
            return _context.Usuarios.ToList();
        }
    }
}