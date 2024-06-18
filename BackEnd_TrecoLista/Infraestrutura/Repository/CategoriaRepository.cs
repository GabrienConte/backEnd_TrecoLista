using BackEnd_TrecoLista.Infraestrutura;
using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;

namespace BackEnd_TrecoLista.Infraestrutura.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        public List<Categoria> Get()
        {
            return _context.Categorias.ToList();
        }
    }
}
