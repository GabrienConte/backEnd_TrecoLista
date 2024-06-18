using AutoMapper;
using BackEnd_TrecoLista.Domain.DTOs.Usuario;
using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;

namespace BackEnd_TrecoLista.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
        }

        public async Task<UsuarioDto> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> AddAsync(UsuarioCreateDto usuarioCreateDto)
        {
            if (string.IsNullOrEmpty(usuarioCreateDto.TipoUsuario))
            {
                usuarioCreateDto.TipoUsuario = "Cliente";
            }

            var usuario = _mapper.Map<Usuario>(usuarioCreateDto);
            var addedUsuario = await _usuarioRepository.AddAsync(usuario);
            return _mapper.Map<UsuarioDto>(addedUsuario);
        }

        public async Task UpdateAsync(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario != null)
            {
                _mapper.Map(usuarioUpdateDto, usuario);
                await _usuarioRepository.UpdateAsync(usuario);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<Usuario> AuthenticateAsync(UsuarioLoginDto usuarioLoginDto)
        {
            var usuario = await _usuarioRepository.FindByEmailOuLoginAsync(usuarioLoginDto.EmailOuLogin);

            if (usuario == null || usuario.Senha != usuarioLoginDto.Senha)
                return null;

            return usuario;
        }
    }
}
