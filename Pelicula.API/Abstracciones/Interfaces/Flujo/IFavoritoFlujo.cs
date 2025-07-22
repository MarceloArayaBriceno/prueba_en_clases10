using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IFavoritoFlujo
    {
        Task<IEnumerable<FavoritoResponse>> Obtener();
        Task<FavoritoDetalle> Obtener(Guid Id);
        Task<Guid> Agregar(FavoritoRequest favorito);
        Task<Guid> Editar(Guid Id, FavoritoRequest favorito);
        Task<IEnumerable<FavoritoResponse>> ObtenerPorUsuario(String idUsuario);

        Task<Guid> Eliminar(Guid Id);
    }
}
