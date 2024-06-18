using BackEnd_TrecoLista.Infraestrutura;
using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;

namespace BackEnd_TrecoLista.Infraestrutura.Repository
{
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(Plataforma plataforma)
        {
            _context.Plataformas.Add(plataforma);
            _context.SaveChanges();
        }

        public List<Plataforma> Get()
        {
            return _context.Plataformas.ToList();
        }
    }
}
