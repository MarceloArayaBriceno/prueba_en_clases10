using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;



namespace Web.Pages.Favoritos
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _config;

        [BindProperty(SupportsGet = true)]
        public string? IdUsuario { get; set; }

        public List<FavoritoDto>? Favoritos { get; set; }

        public string DebugUrl { get; private set; } = "";

        public IndexModel(IConfiguracion config)
        {
            _config = config;
        }

        public async Task OnGetAsync()
        {
            if (string.IsNullOrEmpty(IdUsuario)) return;

            var template = _config.ObtenerMetodo(
                "ApiEndPointsFavoritos",
                "ObtenerFavoritosPorUsuario");

            var url = string.Format(template, IdUsuario);
            DebugUrl = url;

            using var client = new HttpClient();
            try
            {
                var resp = await client.GetAsync(url);
                resp.EnsureSuccessStatusCode();

                var json = await resp.Content.ReadAsStringAsync();

                // Paso 1: deserializar a FavoritoRaw
                var rawList = JsonSerializer.Deserialize<List<FavoritoRaw>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // aso 2: transformar FavoritoRaw a FavoritoDto
                Favoritos = rawList?
                    .Where(f => f.IdFavorito != Guid.Empty)
                    .Select(f => new FavoritoDto
                    {
                        Id = f.IdFavorito,
                        Titulo = f.TituloPelicula ?? f.TituloSerie ?? "(Sin título)",
                        Tipo = f.TituloPelicula != null ? "Película" :
                               (f.TituloSerie != null ? "Serie" : "Desconocido"),
                        Descripcion = f.Comentario ?? "(Sin descripción)",
                        FechaRegistro = f.FechaRegistro
                    })
                    .ToList() ?? new List<FavoritoDto>();
            }
            catch
            {
                Favoritos = new List<FavoritoDto>();
            }
        }


        // IMPORTANTE: Cambiar el nombre del método para que coincida con el handler
        public async Task<IActionResult> OnPostEliminarAsync(Guid idFavorito)
        {
            if (string.IsNullOrWhiteSpace(IdUsuario))
            {
                return RedirectToPage();
            }

            try
            {
                // Obtener la URL base y template del endpoint
                var template = _config.ObtenerMetodo("ApiEndPointsFavoritos", "EliminarFavorito");
                var url = string.Format(template, idFavorito);

                using var client = new HttpClient();

                // Realizar petición DELETE
                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Éxito - redirigir con mensaje de éxito
                    TempData["Mensaje"] = "Favorito eliminado correctamente.";
                }
                else
                {
                    // Error - manejar respuesta de error
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = $"Error al eliminar favorito: {response.StatusCode} - {errorContent}";
                }
            }
            catch (HttpRequestException ex)
            {
                // Error de red
                TempData["Error"] = $"Error de conexión: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Error general
                TempData["Error"] = $"Error inesperado: {ex.Message}";
            }

            // Redirigir de vuelta a la página con el IdUsuario para recargar la lista
            return RedirectToPage(new { IdUsuario });
        }
    }

    public class FavoritoDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; } // "Película" o "Serie"
        public string Descripcion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
    public class FavoritoRaw
    {
        public Guid IdFavorito { get; set; }
        public string? TituloPelicula { get; set; }
        public string? TituloSerie { get; set; }
        public string? Comentario { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

}