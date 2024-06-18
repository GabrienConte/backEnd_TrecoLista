using BackEnd_TrecoLista.Domain.DTOs.Usuario;
using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Domain.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto> GetByIdAsync(int id);
        Task<UsuarioDto> AddAsync(UsuarioCreateDto usuarioCreateDto);
        Task UpdateAsync(int id, UsuarioUpdateDto usuarioUpdateDto);
        Task DeleteAsync(int id);

        Task<Usuario> AuthenticateAsync(UsuarioLoginDto usuarioLoginDto);
    }
}
