using BackEnd_TrecoLista.Infraestrutura;
using BackEnd_TrecoLista.Model;
using BackEnd_TrecoLista.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_TrecoLista.Repository
{
    public class HistoricoEmailRepository : IHistoricoEmailRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(HistoricoEmail historicoEmail)
        {
            //_context.HistoricoEmails.Add(historicoEmail);
            //_context.SaveChanges();
        }

        public List<HistoricoEmail> Get()
        {
            //return _context.HistoricoEmails
            //    .Include(he => he.Destino)
            //    .Include(he => he.Produto)
            //    .ToList();
            return null;
        }
    }
}
