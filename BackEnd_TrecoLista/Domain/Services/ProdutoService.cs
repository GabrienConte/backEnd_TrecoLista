using AutoMapper;
using BackEnd_TrecoLista.Domain.DTOs.Produto;
using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;

namespace BackEnd_TrecoLista.Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoDto>> GetAllAsync()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        }

        public async Task<ProdutoDto> GetByIdAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            return _mapper.Map<ProdutoDto>(produto);
        }

        public async Task<ProdutoDto> AddAsync(ProdutoCreateDto produtoCreateDto)
        {
            var produto = _mapper.Map<Produto>(produtoCreateDto);
            var newProduto = await _produtoRepository.AddAsync(produto);
            return _mapper.Map<ProdutoDto>(newProduto);
        }

        public async Task<ProdutoDto> UpdateAsync(int id, ProdutoUpdateDto produtoUpdateDto)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null) return null;

            _mapper.Map(produtoUpdateDto, produto);
            var updatedProduto = await _produtoRepository.UpdateAsync(produto);
            return _mapper.Map<ProdutoDto>(updatedProduto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _produtoRepository.DeleteAsync(id);
        }
    }
}
