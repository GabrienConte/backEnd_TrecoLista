using AutoMapper;
using BackEnd_TrecoLista.Domain.DTOs.Plataforma;
using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;

namespace BackEnd_TrecoLista.Domain.Services
{
    public class PlataformaService : IPlataformaService
    {
        private readonly IPlataformaRepository _plataformaRepository;
        private readonly IMapper _mapper;

        public PlataformaService(IPlataformaRepository plataformaRepository, IMapper mapper)
        {
            _plataformaRepository = plataformaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlataformaDto>> GetAllAsync()
        {
            var plataformas = await _plataformaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PlataformaDto>>(plataformas);
        }

        public async Task<PlataformaDto> GetByIdAsync(int id)
        {
            var plataforma = await _plataformaRepository.GetByIdAsync(id);
            return _mapper.Map<PlataformaDto>(plataforma);
        }

        public async Task<PlataformaDto> AddAsync(PlataformaCreateDto plataformaCreateDto)
        {
            var plataforma = _mapper.Map<Plataforma>(plataformaCreateDto);
            var createdPlataforma = await _plataformaRepository.AddAsync(plataforma);
            return _mapper.Map<PlataformaDto>(createdPlataforma);
        }

        public async Task UpdateAsync(int id, PlataformaUpdateDto plataformaUpdateDto)
        {
            var plataforma = await _plataformaRepository.GetByIdAsync(id);
            if (plataforma != null)
            {
                _mapper.Map(plataformaUpdateDto, plataforma);
                await _plataformaRepository.UpdateAsync(plataforma);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _plataformaRepository.DeleteAsync(id);
        }
    }
}
