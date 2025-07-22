using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos.Servicios.Peliculas;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class SerieDA : ISeriesDA
    {
        private readonly IRepositorioDapper _repo;
        private readonly SqlConnection _connection;

        public SerieDA(IRepositorioDapper repo)
        {
            _repo = repo;
            _connection = _repo.ObtenerRepositorio();
        }


        public async Task<int> Agregar(Serie serie, int rol)
        {
            try
            {
                await _connection.ExecuteAsync(
                    "sp_InsertarSerie",
                    new
                    {
                        IdSerie = serie.Id,
                        Titulo = serie.Titulo,
                        ImagenUrl = serie.Imagen,
                        Descripcion = serie.Descripcion,
                        FechaEstreno = serie.FechaLanzamiento,
                        Calificacion = serie.Calificacion,
                        IdGenero = rol
                    },
                    commandType: CommandType.StoredProcedure
                );

                return serie.Id;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error SQL: {ex.Message}");
                Console.WriteLine($"Número de error: {ex.Number}");
                throw;
            }
        }

    }

}