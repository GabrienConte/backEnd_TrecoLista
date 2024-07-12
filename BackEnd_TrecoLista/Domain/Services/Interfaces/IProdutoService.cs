using BackEnd_TrecoLista.Domain.DTOs.Produto;
using BackEnd_TrecoLista.Domain.Model;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BackEnd_TrecoLista.Domain.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDto>> GetAllAsync();
        Task<ProdutoDto> GetProdutoByIdAsync(int id);
        Task<ProdutoFavoritadoDTO> GetProdutoFavoritadoByUserAndProdutoAsync(int userId, int produtoId);
        Task<ProdutoDto> AddAsync(ProdutoCreateDto produtoCreateDto, int userId);
        Task<ProdutoDto> UpdateAsync(int id, ProdutoUpdateDto produtoUpdateDto, int userId);
        Task<bool> DeleteAsync(int id);
        Task<ProdutoScrapDTO> GetProductInfoAsync(string url);
        Task VerificarAtualizarPrecosFavoritosAsync();
    }
}
