using BackEnd_TrecoLista.Domain.DTOs.Plataforma;

namespace BackEnd_TrecoLista.Domain.Services.Interfaces
{
    public interface IPlataformaService
    {
        Task<IEnumerable<PlataformaDto>> GetAllAsync();
        Task<PlataformaDto> GetByIdAsync(int id);
        Task<PlataformaDto> AddAsync(PlataformaCreateDto plataformaCreateDto);
        Task UpdateAsync(int id, PlataformaUpdateDto plataformaUpdateDto);
        Task DeleteAsync(int id);
    }
}
