using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.Pages.Pendientes
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _config;

        [BindProperty(SupportsGet = true)]
        public Guid? IdUsuario { get; set; }

        public List<VisualizacionPendiente>? Pendientes { get; set; }

        public string DebugUrl { get; private set; } = "";

        public IndexModel(IConfiguracion config)
        {
            _config = config;
        }

        public async Task OnGetAsync()
        {
            if (!IdUsuario.HasValue) return;

            var template = _config.ObtenerMetodo(
                "ApiEndPointsVisualizacion",
                "ObtenerVisualizacionesPorUsuario");

            var url = string.Format(template, IdUsuario.Value);
            DebugUrl = url;

            using var client = new HttpClient();
            try
            {
                var resp = await client.GetAsync(url);
                resp.EnsureSuccessStatusCode();

                var json = await resp.Content.ReadAsStringAsync();
                Pendientes = JsonSerializer.Deserialize<List<VisualizacionPendiente>>(
                    json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            }
            catch
            {
                Pendientes = new List<VisualizacionPendiente>();
            }
        }

        public async Task<IActionResult> OnPostEliminarAsync(Guid idPendiente)
        {
            if (!IdUsuario.HasValue) return RedirectToPage();

            var template = _config.ObtenerMetodo(
                "ApiEndPointsVisualizacion",
                "EliminarVisualizacion");

            var url = string.Format(template, idPendiente);

            using var client = new HttpClient();
            var resp = await client.DeleteAsync(url);
            resp.EnsureSuccessStatusCode();

            return RedirectToPage(new { IdUsuario });
        }
    }
}
