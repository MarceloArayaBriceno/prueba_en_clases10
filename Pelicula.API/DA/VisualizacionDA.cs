using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DA
{
    public class VisualizacionDA : IVisualizacionDA
    {
        private readonly IRepositorioDapper _repo;
        private readonly SqlConnection _connection;

        public VisualizacionDA(IRepositorioDapper repo)
        {
            _repo = repo;
            _connection = _repo.ObtenerRepositorio();
        }

        public Task InsertarVisualizacion(VisualizacionPendiente v)
            => _connection.ExecuteAsync(
                "sp_GestionarPendientesVisualizacion",
                new
                {
                    Accion = "Insertar",
                    IdPendiente = v.IdPendiente,
                    v.IdUsuario,
                    v.IdPelicula,
                    v.IdSerie,
                    v.Prioridad
                },
                commandType: CommandType.StoredProcedure
            );

        public Task<IEnumerable<VisualizacionPendiente>> ObtenerVisualizacionesPorUsuario(Guid idUsuario)
            => _connection.QueryAsync<VisualizacionPendiente>(
                "sp_ObtenerVisualizacionesPorUsuario",
                new { IdUsuario = idUsuario },
                commandType: CommandType.StoredProcedure
            );


        public Task EliminarVisualizacion(Guid idPendiente)
            => _connection.ExecuteAsync(
                "sp_GestionarPendientesVisualizacion",
                new { Accion = "Eliminar", IdPendiente = idPendiente },
                commandType: CommandType.StoredProcedure
            );

        public Task ActualizarPrioridad(Guid idPendiente, int nuevaPrioridad)
            => _connection.ExecuteAsync(
                "sp_GestionarPendientesVisualizacion",
                new { Accion = "Actualizar", IdPendiente = idPendiente, Prioridad = nuevaPrioridad },
                commandType: CommandType.StoredProcedure
            );
    }
}
