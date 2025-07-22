using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
   public interface IFavoritoDA
    {
        Task<IEnumerable<FavoritoResponse>> Obtener();
        Task<FavoritoDetalle> Obtener(Guid Id);
        Task<Guid> Agregar(FavoritoRequest favorito);
        Task<IEnumerable<FavoritoResponse>> ObtenerPorUsuario(string idUsuario);

        Task<Guid> Editar(Guid Id, FavoritoRequest favorito);
        Task<Guid> Eliminar(Guid Id);
    }
}
