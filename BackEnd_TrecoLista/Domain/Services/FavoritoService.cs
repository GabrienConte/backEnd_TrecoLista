using AutoMapper;
using BackEnd_TrecoLista.Domain.DTOs.Favorito;
using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;

namespace BackEnd_TrecoLista.Domain.Services
{
    public class FavoritoService : IFavoritoService
    {
        private readonly IFavoritoRepository _favoritoRepository;
        private readonly IMapper _mapper;

        public FavoritoService(IFavoritoRepository favoritoRepository, IMapper mapper)
        {
            _favoritoRepository = favoritoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FavoritoDto>> GetAllAsync()
        {
            var favoritos = await _favoritoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FavoritoDto>>(favoritos);
        }

        public async Task<FavoritoDto> GetByIdAsync(int id)
        {
            var favorito = await _favoritoRepository.GetByIdAsync(id);
            return _mapper.Map<FavoritoDto>(favorito);
        }

        public async Task<FavoritoDto> GetFavoritoByUserIdAndProdutoIdAsync(int userId, int produtoId)
        {
            var favorito = await _favoritoRepository.GetByUserIdAndProdutoIdAsync(userId, produtoId);
            return _mapper.Map<FavoritoDto>(favorito);
        }

        public async Task<FavoritoDto> AddAsync(FavoritoCreateDto favoritoCreateDto)
        {
            var favorito = _mapper.Map<Favorito>(favoritoCreateDto);
            var newFavorito = await _favoritoRepository.AddAsync(favorito);
            return _mapper.Map<FavoritoDto>(newFavorito);
        }

        public async Task<FavoritoDto> UpdateAsync(int id, FavoritoUpdateDto favoritoUpdateDto)
        {
            var favorito = await _favoritoRepository.GetByIdAsync(id);
            if (favorito == null) return null;

            _mapper.Map(favoritoUpdateDto, favorito);
            var updatedFavorito = await _favoritoRepository.UpdateAsync(favorito);
            return _mapper.Map<FavoritoDto>(updatedFavorito);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _favoritoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<FavoritoDto>> GetByUsuarioIdAsync(int usuarioId)
        {
            var favoritos = await _favoritoRepository.GetByUsuarioIdAsync(usuarioId);
            return _mapper.Map<IEnumerable<FavoritoDto>>(favoritos);
        }
    }
}
