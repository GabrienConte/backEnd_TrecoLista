using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces
{
    public interface IPlataformaRepository
    {
        void Add(Plataforma plataforma);

        List<Plataforma> Get();
    }
}
