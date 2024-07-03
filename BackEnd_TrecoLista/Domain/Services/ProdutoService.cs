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

namespace BackEnd_TrecoLista.Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper, HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
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

            string fileName = Path.GetFileName(Regex.Replace(produtoInfo.Descricao, @"\s", "") + Path.GetExtension(produtoInfo.ImagemPath));
            var storagePath = Path.Combine("storage", fileName);
            if (!File.Exists(storagePath))
            {
                await DownloadImagemAsync(produtoInfo.ImagemPath, storagePath);
            }

            // Atualizando o caminho da imagem
            produtoInfo.ImagemPath = fileName;

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
