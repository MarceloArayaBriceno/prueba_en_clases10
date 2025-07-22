using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Servicios
{
    public interface IFavoritoServicio
    {
        Task<IEnumerable<FavoritoResponse>> ObtenerFavoritos();
        Task<FavoritoDetalle> ObtenerFavoritoDetalle(Guid id);
        Task<Guid> AgregarFavorito(FavoritoRequest favorito);
        Task<Guid> EditarFavorito(Guid id, FavoritoRequest favorito);
        Task<Guid> EliminarFavorito(Guid id);
    }
}
