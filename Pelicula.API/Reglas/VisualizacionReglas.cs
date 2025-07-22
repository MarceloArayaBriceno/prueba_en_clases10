using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reglas
{
    public class VisualizacionReglas : IVisualizacionReglas
    {
        private readonly IVisualizacionServicio _servicio;
        private readonly IPeliculaServicio _peliculaReglas;
        private readonly ISerieServicio _serieReglas;

        public VisualizacionReglas(
            IVisualizacionServicio servicio,
            IPeliculaServicio peliculaReglas,
            ISerieServicio serieReglas)
        {
            _servicio = servicio;
            _peliculaReglas = peliculaReglas;
            _serieReglas = serieReglas;
        }

        public async Task InsertarVisualizacion(VisualizacionPendiente visualizacion)
        {
            await _servicio.InsertarVisualizacion(visualizacion);
        }

        public async Task<IEnumerable<object>> ObtenerVisualizacionesPorUsuario(Guid idUsuario)
        {
            var pendientes = await _servicio.ObtenerVisualizacionesPorUsuario(idUsuario);
            var resultado = new List<object>();

            foreach (var p in pendientes)
            {
                if (p.IdPelicula.HasValue)
                {
                    
                    var det = await _peliculaReglas.ObtenerPeliculasDetalle(p.IdPelicula.Value);
                    resultado.Add(new
                    {
                        p.IdPendiente,
                        p.IdUsuario,
                        idPelicula = p.IdPelicula.Value,
                        titulo = det.Titulo,      
                        descripcion = det.Descripcion, 
                        p.Prioridad,
                        p.FechaRegistro
                    });
                }
                else if (p.IdSerie.HasValue)
                {
                    var det = await _serieReglas.ObtenerSeriesDetalle(p.IdSerie.Value);
                    resultado.Add(new
                    {
                        p.IdPendiente,
                        p.IdUsuario,
                        idSerie = p.IdSerie.Value,
                        titulo = det.Titulo, 
                        descripcion = det.Descripcion, 
                        p.Prioridad,
                        p.FechaRegistro
                    });
                }
            }

            return resultado;
        }

        public async Task EliminarVisualizacion(Guid idPendiente)
        {
            await _servicio.EliminarVisualizacion(idPendiente);
        }

        public async Task ActualizarPrioridad(Guid idPendiente, int nuevaPrioridad)
        {
            await _servicio.ActualizarPrioridad(idPendiente, nuevaPrioridad);
        }
    }
}
