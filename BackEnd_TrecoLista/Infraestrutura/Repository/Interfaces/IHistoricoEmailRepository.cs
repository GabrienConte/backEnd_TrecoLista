using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces
{
    public interface IHistoricoEmailRepository
    {
        Task AddAsync(HistoricoEmail historicoEmail);

        Task<IEnumerable<HistoricoEmail>> GetAllAsync();
    }
}
