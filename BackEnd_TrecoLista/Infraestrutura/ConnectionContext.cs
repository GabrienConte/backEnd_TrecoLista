using BackEnd_TrecoLista.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_TrecoLista.Infraestrutura
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Plataforma> Plataformas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<HistoricoEmail> HistoricoEmails { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            "Server=localhost;" +
            "Port=5432;Database=db_trecolista;" +
            "User Id=postgres;" +
            "Password=1234;");
    }
}
