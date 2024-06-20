using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Domain.Services;
using BackEnd_TrecoLista.Infraestrutura.Identity;
using BackEnd_TrecoLista.Infraestrutura.Repository;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BackEnd_TrecoLista.Infraestrutura.Configurations;
using Microsoft.Extensions.Configuration;
using BackEnd_TrecoLista.Infraestrutura.Email;
using MailKit;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            { 
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        }); 
});

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();

builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<ICategoriaService, CategoriaService>();


builder.Services.AddTransient<IPlataformaRepository, PlataformaRepository>();
builder.Services.AddTransient<IPlataformaService, PlataformaService>();

builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();
builder.Services.AddTransient<IProdutoService, ProdutoService>();

builder.Services.AddScoped<IFavoritoRepository, FavoritoRepository>();
builder.Services.AddScoped<IFavoritoService, FavoritoService>();

builder.Services.AddTransient<IEmailService, EmailService>();
//builder.Services.AddTransient<IHistoricoEmailRepository, HistoricoEmailRepository>();

var AllowAllOrigins = "_AllowAllOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAllOrigins,
            policy =>
            {
                policy.WithOrigins("http://localhost:4200/")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin();
            });

});

var key = Encoding.ASCII.GetBytes(Key.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(AllowAllOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
