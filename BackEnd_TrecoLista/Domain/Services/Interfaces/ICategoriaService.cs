using BackEnd_TrecoLista.Domain.DTOs.Categoria;
using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Domain.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDto>> GetAllAsync();
        Task<CategoriaDto> GetByIdAsync(int id);
        Task<CategoriaDto> AddAsync(CategoriaCreateDto categoriaCreateDto);
        Task UpdateAsync(int id, CategoriaUpdateDto categoriaUpdateDto);
        Task DeleteAsync(int id);
    }
}
