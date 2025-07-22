using Abstracciones.Modelos;
using Abstracciones.Modelos.Servicios.Peliculas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IPeliculaController
    {
        Task<IActionResult> Obtener(int Id);
        Task<IActionResult> ObtenerDetalle(int Id);
        Task<IActionResult> ObtenerGeneros();
    }
}
