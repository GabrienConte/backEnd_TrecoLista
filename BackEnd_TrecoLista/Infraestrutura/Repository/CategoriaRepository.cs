﻿using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_TrecoLista.Infraestrutura.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<IEnumerable<Categoria>> GetAtivasAsync()
        {
            return await _context.Categorias.Where(c => c.Ativo).ToListAsync();
        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        public async Task<Categoria> AddAsync(Categoria categoria)
        {
            var categoriaCriada = await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoriaCriada.Entity;
        }

        public async Task UpdateAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                categoria.Ativo = false;
                _context.Categorias.Update(categoria);
                await _context.SaveChangesAsync();
            }
        }
    }
}
