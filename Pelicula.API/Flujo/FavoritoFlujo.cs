using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Servicios.Peliculas;

namespace Flujo
{
  
    public class FavoritoFlujo : IFavoritoFlujo
    {
        private readonly IFavoritoDA _favoritoDA;

        public FavoritoFlujo(IFavoritoDA favoritoDA)
        {
            _favoritoDA = favoritoDA;
        }

        public async Task<IEnumerable<FavoritoResponse>> Obtener()
        {
            return await _favoritoDA.Obtener();
        }

        public async Task<FavoritoDetalle> Obtener(Guid id)
        {
            return await _favoritoDA.Obtener(id);
        }

        public async Task<Guid> Agregar(FavoritoRequest favorito)
        {
            return await _favoritoDA.Agregar(favorito);
        }

        public async Task<Guid> Editar(Guid id, FavoritoRequest favorito)
        {
            return await _favoritoDA.Editar(id, favorito);
        }
        public Task<IEnumerable<FavoritoResponse>> ObtenerPorUsuario(String idUsuario)
    => _favoritoDA.ObtenerPorUsuario(idUsuario);


        public async Task<Guid> Eliminar(Guid id)
        {
            return await _favoritoDA.Eliminar(id);
        }
    }
}