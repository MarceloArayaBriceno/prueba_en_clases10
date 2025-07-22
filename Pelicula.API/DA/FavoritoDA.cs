using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DA
{


    public class FavoritoDA : IFavoritoDA
    {
        private readonly IRepositorioDapper _repo;
        private readonly SqlConnection _connection;

        public FavoritoDA(IRepositorioDapper repo)
        {
            _repo = repo;
            _connection = _repo.ObtenerRepositorio();
        }

        public Task<IEnumerable<FavoritoResponse>> Obtener()
            => _connection.QueryAsync<FavoritoResponse>(
                "sp_ObtenerFavoritos",
                commandType: CommandType.StoredProcedure
            );

        public Task<FavoritoDetalle?> Obtener(Guid idFavorito)
            => _connection.QueryFirstOrDefaultAsync<FavoritoDetalle>(
                "sp_ObtenerFavorito",
                new { IdFavorito = idFavorito },
                commandType: CommandType.StoredProcedure
            );

        public async Task<Guid> Agregar(FavoritoRequest favorito)
        {
            var nuevoId = Guid.NewGuid();

            await _connection.ExecuteAsync(
                "sp_GestionarFavoritos",
                new
                {
                    Accion = "Insertar",
                    IdFavorito = nuevoId,
                    favorito.Email,
                    favorito.IdPelicula,
                    favorito.IdSerie,
                    favorito.Comentario,
                    favorito.Puntuacion
                },
                commandType: CommandType.StoredProcedure
            );

            return nuevoId;
        }

        public async Task<Guid> Editar(Guid idFavorito, FavoritoRequest favorito)
        {
            await VerificarExiste(idFavorito);

            await _connection.ExecuteAsync(
                "sp_GestionarFavoritos",
                new
                {
                    Accion = "Actualizar",
                    IdFavorito = idFavorito,
                    favorito.Comentario,
                    favorito.Puntuacion
                },
                commandType: CommandType.StoredProcedure
            );

            return idFavorito;
        }
        public Task<IEnumerable<FavoritoResponse>> ObtenerPorUsuario(string idUsuario)
 => _connection.QueryAsync<FavoritoResponse>(
     "sp_ObtenerFavoritosPorUsuario",
     new { IdUsuario = idUsuario },
     commandType: CommandType.StoredProcedure
 );




        public async Task<Guid> Eliminar(Guid idFavorito)
        {
            await _connection.ExecuteAsync(
                "sp_GestionarFavoritos",
                new
                {
                    Accion = "Eliminar",
                    IdFavorito = idFavorito
                },
                commandType: CommandType.StoredProcedure
            );

            return idFavorito;
        }

        private async Task VerificarExiste(Guid id)
        {
            var favorito = await Obtener(id);
            if (favorito == null)
                throw new KeyNotFoundException($"No se encontró el favorito con Id {id}");
        }
    }

}
