using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces
{
    public interface IPlataformaRepository
    {
        Task<IEnumerable<Plataforma>> GetAllAsync();
        Task<Plataforma> GetByIdAsync(int id);
        Task<Plataforma> AddAsync(Plataforma plataforma);
        Task UpdateAsync(Plataforma plataforma);
        Task DeleteAsync(int id);
    }
}
