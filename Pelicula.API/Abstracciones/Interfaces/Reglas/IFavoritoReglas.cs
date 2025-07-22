using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Reglas
{
    public interface IFavoritoReglas
    {
        Task<IEnumerable<FavoritoResponse>> Obtener();
        Task<FavoritoDetalle> Obtener(Guid id);
        Task<Guid> Agregar(FavoritoRequest favorito);
        Task<Guid> Editar(Guid id, FavoritoRequest favorito);
        Task<Guid> Eliminar(Guid id);
    }

}
