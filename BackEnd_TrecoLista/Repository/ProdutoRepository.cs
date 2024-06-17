using BackEnd_TrecoLista.Infraestrutura;
using BackEnd_TrecoLista.Model;
using BackEnd_TrecoLista.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_TrecoLista.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public List<Produto> Get()
        {
            return _context.Produtos
                .Include(p => p.Plataforma)
                .Include(p => p.Categoria)
                .ToList();
        }
    }
}
