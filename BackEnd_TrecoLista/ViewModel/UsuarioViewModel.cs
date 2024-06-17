namespace BackEnd_TrecoLista.ViewModel
{
    public class UsuarioViewModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Login { get; set; }
        public string TipoUsuario { get; set; } = "cliente";
    }
}
