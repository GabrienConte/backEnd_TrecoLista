using BackEnd_TrecoLista.Model;

namespace BackEnd_TrecoLista.Repository.Interfaces
{
    public interface IHistoricoEmailRepository
    {
        void Add(HistoricoEmail historicoEmail);

        List<HistoricoEmail> Get();
    }
}
