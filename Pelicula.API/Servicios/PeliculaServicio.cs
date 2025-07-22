using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.Peliculas
{
    public class PeliculaServicio : IPeliculaServicio
    {
        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClient;
        private readonly IGeneroDA _generoDA;
        private readonly IPeliculaDA _peliculaDA;
        private const string BASE_IMAGE_URL = "https://image.tmdb.org/t/p/";

        public PeliculaServicio(IConfiguracion configuracion, IHttpClientFactory httpClient, IGeneroDA generoDA, IPeliculaDA peliculaDA)
        {
            _configuracion = configuracion;
            _httpClient = httpClient;
            _generoDA = generoDA;
            _peliculaDA = peliculaDA;
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
        public async Task<IEnumerable<Genero>> ObtenerGenerosPeliculas()
        {
            var endpoint = _configuracion.ObtenerMetodo("ApiEndPointsPeliculas", "ObtenerGeneroPelicula");
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
                var a = _generoDA.Agregar(genero);
            }
            return generos;
        }

        public async Task<Pelicula> ObtenerPeliculasDetalle(int Id)
        {
            var endpoint = _configuracion.ObtenerMetodo("ApiEndPointsPeliculas", "ObtenerDetallePelicula");
            var servicioRegistro = _httpClient.CreateClient("ServicioPeliculas");
            var token = _configuracion.ObtenerValor("TokenServicioPeliculas");
            servicioRegistro.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var respuesta = await servicioRegistro.GetAsync(string.Format(endpoint, Id));
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var movie = JsonSerializer.Deserialize<Movie>(resultado, opciones);

            var pelicula = new Pelicula
            {
                Id = movie.Id,
                Titulo = movie.Title,
                Imagen = ConstruirUrlImagen(movie.PosterPath, "original"),
                ImagenFondo = ConstruirUrlImagen(movie.BackdropPath, "original"),
                Descripcion = movie.Overview,
                FechaLanzamiento = movie.ReleaseDate,
                Calificacion = movie.VoteAverage,
            };


            return pelicula;
        }

        public async Task<IEnumerable<Pelicula>> ObtenerPeliculasPorGenero(int Id)
        {
            var endpoint = _configuracion.ObtenerMetodo("ApiEndPointsPeliculas", "ObtenerPeliculasxGeneros");
            var servicioRegistro = _httpClient.CreateClient("ServicioPeliculas");
            var token = _configuracion.ObtenerValor("TokenServicioPeliculas");
            servicioRegistro.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var respuesta = await servicioRegistro.GetAsync(string.Format(endpoint, Id));
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resultadoDeserializado = JsonSerializer.Deserialize<MovieResponse>(resultado, opciones);
            var peliculas = new List<Pelicula>();
            foreach (var movie in resultadoDeserializado.Movies)
            {
                var pelicula = new Pelicula
                {
                    Id = movie.Id,
                    Titulo = movie.Title,
                    Imagen = ConstruirUrlImagen(movie.PosterPath, "original"),
                    ImagenFondo = ConstruirUrlImagen(movie.BackdropPath, "original"),
                    Descripcion = movie.Overview,
                    FechaLanzamiento = movie.ReleaseDate,
                    Calificacion = movie.VoteAverage,
                };
                _peliculaDA.Agregar(pelicula, Id);
                peliculas.Add(pelicula);
            }
            return peliculas;
        }
    }
}