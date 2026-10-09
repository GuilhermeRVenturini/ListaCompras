using API.ListaCompras.Middlewares;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Services;
using Domain.ListaCompras.Interfaces;
using Infrastructure.ListaCompras.Data;
using Infrastructure.ListaCompras.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "A connection string 'DefaultConnection' n�o foi configurada.");

builder.Services.AddDbContext<ListaCompras_DbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped<IPrecoMercadoRepository, PrecoMercadoRepository>();
builder.Services.AddScoped<IProdutoListaRepository, ProdutoListaRepository>();
builder.Services.AddScoped<IUsuarioListaRepository, UsuarioListaRepository>();

builder.Services.AddScoped<IHistoricoService, HistoricoService>();
builder.Services.AddScoped<IListaService, ListaService>();
builder.Services.AddScoped<IMercadoService, MercadoService>();
builder.Services.AddScoped<IPrecoMercadoService, PrecoMercadoService>();
builder.Services.AddScoped<IPrecoService, PrecoService>();
builder.Services.AddScoped<IProdutoListaService, ProdutoListaService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IUsuarioListaService, UsuarioListaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
