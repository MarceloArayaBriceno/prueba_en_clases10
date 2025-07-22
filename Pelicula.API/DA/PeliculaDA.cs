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
    public class PeliculaDA : IPeliculaDA
    {
        private readonly IRepositorioDapper _repo;
        private readonly SqlConnection _connection;

        public PeliculaDA(IRepositorioDapper repo)
        {
            _repo = repo;
            _connection = _repo.ObtenerRepositorio();
        }


        public async Task<int> Agregar(Pelicula pelicula, int rol)
        {
            try
            {
                await _connection.ExecuteAsync(
                    "sp_InsertarPelicula",
                    new
                    {
                        IdPelicula = pelicula.Id,
                        Titulo = pelicula.Titulo,
                        ImagenUrl = pelicula.Imagen,
                        Descripcion = pelicula.Descripcion,
                        FechaEstreno = pelicula.FechaLanzamiento,
                        Calificacion = pelicula.Calificacion,
                        IdGenero = rol
                    },
                    commandType: CommandType.StoredProcedure
                );
                return pelicula.Id;
            }
            catch (SqlException ex)
            {
                // Aquí verás el error específico del SP
                Console.WriteLine($"Error SQL: {ex.Message}");
                Console.WriteLine($"Número de error: {ex.Number}");
                throw;
            }
        }
    }



}