using BackEnd_TrecoLista.Model;

namespace BackEnd_TrecoLista.Repository.Interfaces
{
    public interface IPlataformaRepository
    {
        void Add(Plataforma plataforma);

        List<Plataforma> Get();
    }
}
