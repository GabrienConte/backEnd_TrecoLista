using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces
{
    public interface IFavoritoRepository
    {
        Task<IEnumerable<Favorito>> GetAllAsync();
        Task<Favorito> GetByIdAsync(int id);
        Task<Favorito> GetByUserIdAndProdutoIdAsync(int userId, int produtoId);
        Task<Favorito> AddAsync(Favorito favorito);
        Task<Favorito> UpdateAsync(Favorito favorito);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Favorito>> GetByUsuarioIdAsync(int usuarioId);
    }
}
