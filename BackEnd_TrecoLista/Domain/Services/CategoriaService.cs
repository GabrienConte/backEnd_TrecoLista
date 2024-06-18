using AutoMapper;
using BackEnd_TrecoLista.Domain.DTOs.Categoria;
using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;

namespace BackEnd_TrecoLista.Domain.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaDto>> GetAllAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
        }

        public async Task<CategoriaDto> GetByIdAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            return _mapper.Map<CategoriaDto>(categoria);
        }

        public async Task<CategoriaDto> AddAsync(CategoriaCreateDto categoriaCreateDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaCreateDto);
            var categoriaCriada = await _categoriaRepository.AddAsync(categoria);
            return _mapper.Map<CategoriaDto>(categoriaCriada);
        }

        public async Task UpdateAsync(int id, CategoriaUpdateDto categoriaUpdateDto)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria != null)
            {
                _mapper.Map(categoriaUpdateDto, categoria);
                await _categoriaRepository.UpdateAsync(categoria);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _categoriaRepository.DeleteAsync(id);
        }
    }
}
