using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);

        List<Usuario> Get();

        Usuario? GetAuth(string username, string senha);
    }
}