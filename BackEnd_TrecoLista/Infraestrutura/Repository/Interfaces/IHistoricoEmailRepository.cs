using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces
{
    public interface IHistoricoEmailRepository
    {
        void Add(HistoricoEmail historicoEmail);

        List<HistoricoEmail> Get();
    }
}
