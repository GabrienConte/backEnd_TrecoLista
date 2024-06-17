using BackEnd_TrecoLista.Infraestrutura;
using BackEnd_TrecoLista.Model;
using BackEnd_TrecoLista.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_TrecoLista.Repository
{
    public class FavoritoRepository : IFavoritoRepository
    {
        private readonly ConnectionContext _context;

        public FavoritoRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Favorito favorito)
        {
            //_context.Favoritos.Add(favorito);
            //_context.SaveChanges();
        }

        public List<Favorito> Get()
        {
            //return _context.Favoritos
            //    .Include(f => f.Usuario)
            //    .Include(f => f.Produto)
            //    .ToList();

            return null;
        }
    }
}
