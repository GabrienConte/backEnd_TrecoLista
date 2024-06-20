using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Email
{
    public class EmailRequisicao
    {
        public string Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Corpo { get; set; }
    }
}
