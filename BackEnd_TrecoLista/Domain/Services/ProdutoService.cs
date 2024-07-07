using AutoMapper;
using BackEnd_TrecoLista.Domain.DTOs.Produto;
using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using BackEnd_TrecoLista.Infraestrutura.Configurations;
using Microsoft.Extensions.Options;
using BackEnd_TrecoLista.Infraestrutura.Util;
using System.Text.RegularExpressions;
using BackEnd_TrecoLista.Domain.DTOs.Favorito;
using BackEnd_TrecoLista.Infraestrutura.Repository;

namespace BackEnd_TrecoLista.Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFavoritoService _favoritoService;
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, IFavoritoService favoritoService, IMapper mapper, HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _produtoRepository = produtoRepository;
            _favoritoService = favoritoService;
            _mapper = mapper;
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IEnumerable<ProdutoDto>> GetAllAsync()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        }

        public async Task<ProdutoDto> GetProdutoByIdAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            return _mapper.Map<ProdutoDto>(produto);
        }

        public async Task<ProdutoFavoritadoDTO> GetProdutoFavoritadoByUserAndProdutoAsync(int userId, int produtoId)
        {
            var produto = await _produtoRepository.GetByIdAsync(produtoId);
            var produtoDetalhadoDto = _mapper.Map<ProdutoFavoritadoDTO>(produto);

            var favorito = await _favoritoService.GetFavoritoByUserIdAndProdutoIdAsync(userId, produtoId);
            if (favorito != null)
            {
                produtoDetalhadoDto.Prioridade = favorito.Prioridade;
                produtoDetalhadoDto.Aviso = favorito.Aviso;
            }

            return produtoDetalhadoDto;
        }

        public async Task<ProdutoDto> AddAsync(ProdutoCreateDto produtoCreateDto, int userId)
        {
            var produto = _mapper.Map<Produto>(produtoCreateDto);

            if (produtoCreateDto.Imagem != null)
            {
                string fileName = Path.GetFileName(Regex.Replace(produtoCreateDto.Descricao.Substring(0, 20), @"\s", "") + Path.GetExtension(produtoCreateDto.ImagemPath));
                var storagePath = Path.Combine("storage", fileName);

                if (!File.Exists(storagePath))
                {
                    await DownloadImagemAsync(produtoCreateDto.ImagemPath, storagePath);
                }

                produtoCreateDto.ImagemPath = fileName;
            }

            var newProduto = await _produtoRepository.AddAsync(produto);

            var createFavorito = new FavoritoCreateDto
            {
                ProdutoId = newProduto.Id,
                UsuarioId = userId,
                Prioridade = produtoCreateDto.Prioridade,
                Aviso = produtoCreateDto.IsAvisado
            };

            var favorito = await _favoritoService.AddAsync(createFavorito);

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

        public async Task<ProdutoScrapDTO> GetProductInfoAsync(string url)
        {
            var flaskApiUrl = _apiSettings.FlaskApiUrl;
            var requestData = new { url };
            var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(flaskApiUrl, jsonContent);
            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var produtoInfo = JsonSerializer.Deserialize<ProdutoScrapDTO>(jsonResponse, options);

            produtoInfo.ValorConvertido = ValorConverter.ConvertPrice(produtoInfo.Valor);

            return produtoInfo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _produtoRepository.DeleteAsync(id);
        }

        private async Task DownloadImagemAsync(string imageUrl, string storagePath)
        {
            using (var httpClient = new HttpClient())
            {
                var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                Directory.CreateDirectory(Path.GetDirectoryName(storagePath));
                await File.WriteAllBytesAsync(storagePath, imageBytes);
            }
        }
    }
}
