using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task<Usuario> AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);

        Task<Usuario> FindByEmailOuLoginAsync(string emailOuLogin);
    }
}