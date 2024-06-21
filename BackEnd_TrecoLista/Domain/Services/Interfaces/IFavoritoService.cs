using BackEnd_TrecoLista.Domain.DTOs.Favorito;

namespace BackEnd_TrecoLista.Domain.Services.Interfaces
{
    public interface IFavoritoService
    {
        Task<IEnumerable<FavoritoDto>> GetAllAsync();
        Task<FavoritoDto> GetByIdAsync(int id);
        Task<FavoritoDto> AddAsync(FavoritoCreateDto favoritoCreateDto);
        Task<FavoritoDto> UpdateAsync(int id, FavoritoUpdateDto favoritoUpdateDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<FavoritoDto>> GetByUsuarioIdAsync(int usuarioId);
    }
}
