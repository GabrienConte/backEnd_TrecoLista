using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_TrecoLista.Infraestrutura.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            var entityEntry = await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Usuario> FindByEmailOuLoginAsync(string emailOuLogin)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == emailOuLogin || u.Login == emailOuLogin);
        }
    }
}
