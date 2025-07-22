using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{

     public interface IFavoritoController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(FavoritoRequest favorito);
        Task<IActionResult> Editar(Guid Id, FavoritoRequest favorito);
        Task<IActionResult> Eliminar(Guid Id);
    }
}
