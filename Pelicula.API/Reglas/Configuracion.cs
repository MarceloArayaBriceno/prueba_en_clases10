using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace Reglas
{
    public class Configuracion : IConfiguracion
    {
        private readonly IConfiguration _configuration;

        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ObtenerMetodo(string seccion, string nombre)
        {
            var apiEndPoint = _configuration.GetSection(seccion).Get<APIEndPoint>();

            if (apiEndPoint == null)
                throw new Exception($"No se encontró la sección '{seccion}' en appsettings.json");

            if (apiEndPoint.Metodos == null || !apiEndPoint.Metodos.Any())
                throw new Exception($"No hay métodos definidos en la sección '{seccion}'");

            var metodo = apiEndPoint.Metodos.FirstOrDefault(m => m.Nombre == nombre);
            if (metodo == null)
                throw new Exception($"No se encontró el método '{nombre}' en la sección '{seccion}'");

            return $"{apiEndPoint.UrlBase}/{metodo.Valor}";
        }

        private string? ObtenerUrlBase(string seccion)
        {
            var apiEndPoint = _configuration.GetSection(seccion).Get<APIEndPoint>();
            if (apiEndPoint == null)
                throw new Exception($"No se encontró la sección '{seccion}' en appsettings.json");
            return apiEndPoint.UrlBase;
        }

        public string ObtenerValor(string llave)
        {
            return _configuration.GetSection(llave).Value ?? throw new Exception($"No se encontró el valor para '{llave}'");
        }
    }
}