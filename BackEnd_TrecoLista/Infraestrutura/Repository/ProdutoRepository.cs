using BackEnd_TrecoLista.Infraestrutura;
using BackEnd_TrecoLista.Domain.Model;
using Microsoft.EntityFrameworkCore;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;

namespace BackEnd_TrecoLista.Infraestrutura.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public List<Produto> Get(int pageNumber, int pageQuantity)
        {
            return _context.Produtos
                .Include(p => p.Plataforma)
                .Include(p => p.Categoria)
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToList();
        }

        public Produto? Get(int id)
        {
            return _context.Produtos.Find(id);
        }
    }
}
