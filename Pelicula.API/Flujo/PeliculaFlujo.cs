using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Servicios.Peliculas;

namespace Flujo
{
    public class PeliculaFlujo : IPeliculaFlujo
    {
        private readonly IPeliculaServicio _PeliculaServicio;
        private readonly IConfiguracion _configuracion;

        public PeliculaFlujo(IPeliculaServicio peliculaServicio, IConfiguracion configuracion)
        {
            _PeliculaServicio = peliculaServicio;
            _configuracion = configuracion;
        }

        public async Task<IEnumerable<Pelicula>> Obtener(int Id)
        {
            var peliculas = await _PeliculaServicio.ObtenerPeliculasPorGenero(Id);
            return peliculas;
        }

        public async Task<Pelicula> ObtenerDetalle(int Id)
        {
            var pelicula = await _PeliculaServicio.ObtenerPeliculasDetalle(Id);
            return pelicula;
        }

        public async Task<IEnumerable<Genero>> ObtenerGeneros()
        {
            var generosPeliculas = await _PeliculaServicio.ObtenerGenerosPeliculas();
            return generosPeliculas;
        }
    }
}
