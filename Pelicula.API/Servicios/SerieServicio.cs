using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.Peliculas
{
    public class SerieServicio : ISerieServicio
    {
        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClient;
        private const string BASE_IMAGE_URL = "https://image.tmdb.org/t/p/";
        private readonly IGeneroDA _generoDA;
        private readonly ISeriesDA _seriesDA;
        public SerieServicio(IConfiguracion configuracion, IHttpClientFactory httpClient, IGeneroDA generoDA, ISeriesDA seriesDA)
        {
            _generoDA = generoDA;
            _configuracion = configuracion;
            _httpClient = httpClient;
            _seriesDA = seriesDA;
        }

        #region helper
        //Basado en la información de la API https://developer.themoviedb.org/docs/image-basics
        private string ConstruirUrlImagen(string posterPath, string tamaño = "w500")
        {
            if (string.IsNullOrEmpty(posterPath))
                return "/images/no-image.jpg";

            return $"{BASE_IMAGE_URL}{tamaño}{posterPath}";
        }
        #endregion

        public async Task<IEnumerable<Genero>> ObtenerGenerosSeries()
        {
            var endpoint = _configuracion.ObtenerMetodo("ApiEndPointsPeliculas", "ObtenerGeneroSeries");
            var servicioRegistro = _httpClient.CreateClient("ServicioPeliculas");
            var token = _configuracion.ObtenerValor("TokenServicioPeliculas");
            servicioRegistro.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var respuesta = await servicioRegistro.GetAsync(endpoint);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resultadoDeserializado = JsonSerializer.Deserialize<GenresResponse>(resultado, opciones);
            var generos = new List<Genero>();
            foreach (var genre in resultadoDeserializado.Genres)
            {
                var genero = new Genero
                {
                    Id = genre.Id,
                    Nombre = genre.Name,
                };
                generos.Add(genero);
                var a = _generoDA.AgregarSerie(genero);
            }
            return generos;
        }

        public async Task<Serie> ObtenerSeriesDetalle(int Id)
        {
            var endpoint = _configuracion.ObtenerMetodo("ApiEndPointsPeliculas", "ObtenerDetalleSerie");
            var servicioRegistro = _httpClient.CreateClient("ServicioPeliculas");
            var token = _configuracion.ObtenerValor("TokenServicioPeliculas");
            servicioRegistro.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var respuesta = await servicioRegistro.GetAsync(string.Format(endpoint, Id));
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var tvshow = JsonSerializer.Deserialize<TvShow>(resultado, opciones);

            var serie = new Serie
            {
                Id = tvshow.Id,
                Titulo = tvshow.Name,
                Imagen = ConstruirUrlImagen(tvshow.PosterPath, "original"),
                Descripcion = tvshow.Overview,
                FechaLanzamiento = tvshow.FirstAirDate,
                Calificacion = tvshow.VoteAverage,
            };

            return serie;
        }

        public async Task<IEnumerable<Serie>> ObtenerSeriesPorGenero(int Id)
        {
            var endpoint = _configuracion.ObtenerMetodo("ApiEndPointsPeliculas", "ObtenerSeriesxGeneros");
            var servicioRegistro = _httpClient.CreateClient("ServicioPeliculas");
            var token = _configuracion.ObtenerValor("TokenServicioPeliculas");
            servicioRegistro.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var respuesta = await servicioRegistro.GetAsync(string.Format(endpoint, Id));
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resultadoDeserializado = JsonSerializer.Deserialize<TvShowResponse>(resultado, opciones);
            var generos = new List<Serie>();
            foreach (var tvshow in resultadoDeserializado.Results)
            {
                var genero = new Serie
                {
                    Id = tvshow.Id,
                    Titulo = tvshow.Name,
                    Imagen = ConstruirUrlImagen(tvshow.PosterPath, "original"),
                    Descripcion = tvshow.Overview,
                    FechaLanzamiento = tvshow.FirstAirDate,
                    Calificacion = tvshow.VoteAverage,
                };
                generos.Add(genero);
                var series = _seriesDA.Agregar(genero, Id);

            }
            return generos;
        }
    }
}