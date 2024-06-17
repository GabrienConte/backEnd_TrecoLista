using BackEnd_TrecoLista.Repository;
using BackEnd_TrecoLista.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IPlataformaRepository, PlataformaRepository>();
builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();
//builder.Services.AddTransient<IHistoricoEmailRepository, HistoricoEmailRepository>();
//builder.Services.AddTransient<IFavoritoRepository, FavoritoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
