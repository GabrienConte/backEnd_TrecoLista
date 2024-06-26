﻿using BackEnd_TrecoLista.Domain.DTOs.Produto;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BackEnd_TrecoLista.Domain.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDto>> GetAllAsync();
        Task<ProdutoDto> GetByIdAsync(int id);
        Task<ProdutoDto> AddAsync(ProdutoCreateDto produtoCreateDto);
        Task<ProdutoDto> UpdateAsync(int id, ProdutoUpdateDto produtoUpdateDto);
        Task<bool> DeleteAsync(int id);
        Task<ProdutoScrapDTO> GetProductInfoAsync(string url);
    }
}
