using BackEnd_TrecoLista.Model;

namespace BackEnd_TrecoLista.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);

        List<Usuario> Get();
    }
}