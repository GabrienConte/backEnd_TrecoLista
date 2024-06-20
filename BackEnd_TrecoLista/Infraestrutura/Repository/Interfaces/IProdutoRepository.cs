using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> GetByIdAsync(int id);
        Task<Produto> AddAsync(Produto produto);
        Task<Produto> UpdateAsync(Produto produto);
        Task<bool> DeleteAsync(int id);
    }
}
