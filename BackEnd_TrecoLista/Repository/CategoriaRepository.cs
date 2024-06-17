using BackEnd_TrecoLista.Infraestrutura;
using BackEnd_TrecoLista.Model;
using BackEnd_TrecoLista.Repository.Interfaces;

namespace BackEnd_TrecoLista.Repository
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
