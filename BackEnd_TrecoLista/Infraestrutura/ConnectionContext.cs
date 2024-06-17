using BackEnd_TrecoLista.Model;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_TrecoLista.Infraestrutura
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            "Server=localhost;" +
            "Port=5432;Database=db_trecolista;" +
            "User Id=postgres;" +
            "Password=1234;");
    }
}
