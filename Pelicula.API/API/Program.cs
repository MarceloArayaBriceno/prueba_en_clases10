using Abstracciones.Interfaces.Flujo;
using Flujo;
using DA;
using Abstracciones.Interfaces.DA;
using DA.Repositorios;
using Abstracciones.Interfaces.Servicios;
using Reglas;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios.Peliculas;
using Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7258") 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();
builder.Services.AddScoped<IConfiguracion, Configuracion>();
builder.Services.AddScoped<IPeliculaFlujo, PeliculaFlujo>();
builder.Services.AddScoped<IPeliculaServicio, PeliculaServicio>();
builder.Services.AddScoped<ISerieFlujo, SerieFlujo>();
builder.Services.AddScoped<ISerieServicio, SerieServicio>();
builder.Services.AddScoped<IFavoritoFlujo, FavoritoFlujo>();
builder.Services.AddScoped<IFavoritoDA, FavoritoDA>();

builder.Services.AddScoped<IVisualizacionServicio, VisualizacionServicio>();
builder.Services.AddScoped<IVisualizacionReglas, VisualizacionReglas>();
builder.Services.AddScoped<IVisualizacionFlujo, VisualizacionFlujo>();
builder.Services.AddScoped<IVisualizacionDA, VisualizacionDA>();
builder.Services.AddScoped<IGeneroDA, GeneroDA>();
builder.Services.AddScoped<IPeliculaDA, PeliculaDA>();
builder.Services.AddScoped<ISeriesDA, SerieDA>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
