using System.Net.Http.Json;
using EnFocoFRONT.Models;

namespace EnFocoFRONT.Services
{
    public class NewsletterService : INewsletterService
    {
        private readonly HttpClient _http;

        public NewsletterService(HttpClient http)
        {
            _http = http;
        }

        // Obtiene una LISTA de suscriptores, espera directamente una List<Newsletter> en la respuesta
        public async Task<List<Newsletter>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<Newsletter>>("api/Newsletter") ?? new List<Newsletter>();
        }


        // Crea un nuevo suscriptor
        public async Task<bool> CreateAsync(Newsletter newsletter)
        {
            var response = await _http.PostAsJsonAsync("api/Newsletter", newsletter);
            return response.IsSuccessStatusCode;
        }

       

    }
}