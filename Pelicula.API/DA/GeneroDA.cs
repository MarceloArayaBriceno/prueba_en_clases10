using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
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
    public class GeneroDA : IGeneroDA
    {
        private readonly IRepositorioDapper _repo;
        private readonly SqlConnection _connection;

        public GeneroDA(IRepositorioDapper repo)
        {
            _repo = repo;
            _connection = _repo.ObtenerRepositorio();
        }


        public async Task<int> Agregar(Genero genero)
        {
            //var nuevoId = Guid.NewGuid();

            await _connection.ExecuteAsync(
                "sp_ManteGeneros",
                new
                {

                    Id = genero.Id,
                    Nombre = genero.Nombre,
                    Tipo = "Pelicula"
                },
                commandType: CommandType.StoredProcedure
            );

            return genero.Id;
        }

        public async Task<int> AgregarSerie(Genero genero)
        {
            //var nuevoId = Guid.NewGuid();

            await _connection.ExecuteAsync(
                "sp_ManteGeneros",
                new
                {

                    Id = genero.Id,
                    Nombre = genero.Nombre,
                    Tipo = "Serie"
                },
                commandType: CommandType.StoredProcedure
            );

            return genero.Id;
        }

    }

}