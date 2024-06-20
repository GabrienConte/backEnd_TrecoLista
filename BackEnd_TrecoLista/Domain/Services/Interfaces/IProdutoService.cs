using BackEnd_TrecoLista.Domain.DTOs.Produto;

namespace BackEnd_TrecoLista.Domain.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDto>> GetAllAsync();
        Task<ProdutoDto> GetByIdAsync(int id);
        Task<ProdutoDto> AddAsync(ProdutoCreateDto produtoCreateDto);
        Task<ProdutoDto> UpdateAsync(int id, ProdutoUpdateDto produtoUpdateDto);
        Task<bool> DeleteAsync(int id);
    }
}
