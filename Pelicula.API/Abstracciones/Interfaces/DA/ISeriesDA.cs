using Abstracciones.Modelos.Servicios.Peliculas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ISeriesDA
    {
        public Task<int> Agregar(Serie serie, int rol);
    }
}