using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Servicios.Peliculas;

namespace Flujo
{
    public class SerieFlujo : ISerieFlujo
    {

        private readonly ISerieServicio _SerieServicio;
        private readonly IConfiguracion _configuracion;

        public SerieFlujo(ISerieServicio SerieServicio, IConfiguracion configuracion)
        {
            _SerieServicio = SerieServicio;
            _configuracion = configuracion;
        }

        public async Task<IEnumerable<Serie>> Obtener(int Id)
        {
            var Series = await _SerieServicio.ObtenerSeriesPorGenero(Id);
            return Series;
        }

        public async Task<Serie> ObtenerDetalle(int Id)
        {
            var Serie = await _SerieServicio.ObtenerSeriesDetalle(Id);
            return Serie;
        }

        public async Task<IEnumerable<Genero>> ObtenerGeneros()
        {
            var generosSeries = await _SerieServicio.ObtenerGenerosSeries();
            return generosSeries;
        }
    }
}
