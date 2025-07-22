
using Abstracciones.Modelos;
using Abstracciones.Modelos.Servicios.Peliculas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IGeneroDA
    {
        public Task<int> Agregar(Genero favorito);

        public Task<int> AgregarSerie(Genero favorito);
    }
}