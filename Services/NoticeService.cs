using System.Net.Http.Json;
using Blazored.LocalStorage;
using EnFocoFRONT.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Forms;




namespace EnFocoFRONT.Services
{
    public class NoticeService : INoticeService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public NoticeService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

         private async Task<HttpRequestMessage> CreateRequestAsync(HttpMethod method, string url, object? content = null)
        {
            var request = new HttpRequestMessage(method, url);

            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            if (content is not null)
            {
                request.Content = JsonContent.Create(content);
            }

            return request;
        }


        // Obtiene una LISTA de noticias, espera directamente una List<Notice> en la respuesta
        public async Task<List<Notice>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<Notice>>("EnFocoBACK/api/Notice") ?? new List<Notice>();
        }

        // Obtiene una UNICA noticia por ID, espera directamente un objeto Notice en la respuesta
        public async Task<Notice?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Notice>($"EnFocoBACK/api/Notice/{id}");
        }

        // Obtiene una LISTA de noticias por sección, espera directamente una List<Notice> en la respuesta
        public async Task<List<Notice>> GetBySectionAsync(string section)
        {
            return await _http.GetFromJsonAsync<List<Notice>>($"EnFocoBACK/api/Notice/section/{section}") ?? new List<Notice>();
        }

        // Busca una LISTA de noticias por término, espera directamente una List<Notice> en la respuesta
        public async Task<List<Notice>> SearchAsync(string term)
        {
            return await _http.GetFromJsonAsync<List<Notice>>(
                $"EnFocoBACK/api/Notice/search?searchTerm={Uri.EscapeDataString(term)}&page=1&pageSize=6"
            ) ?? new List<Notice>();
        }

        public async Task<bool> CreateAsync(Notice notice)
        {
           var request = await CreateRequestAsync(HttpMethod.Post, "EnFocoBACK/api/Notice", notice);
            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }





        public async Task<bool> UpdateAsync(int id, Notice notice)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(notice.Id.ToString()), "Id");
            content.Add(new StringContent(notice.Title ?? ""), "Title");
            content.Add(new StringContent(notice.Subtitle ?? ""), "Subtitle");
            content.Add(new StringContent(notice.Text ?? ""), "Text");
            content.Add(new StringContent(notice.Issue ?? ""), "Issue");
            content.Add(new StringContent(notice.Section.ToString()), "Section");
            content.Add(new StringContent(notice.Category.ToString()), "Category");

            if (!string.IsNullOrWhiteSpace(notice.Img))
                content.Add(new StringContent(notice.Img), "Img");

           if (notice.ImageFile != null)
            {
                var imageFile = (IBrowserFile)notice.ImageFile;

                var fileContent = new StreamContent(imageFile.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(imageFile.ContentType);
                content.Add(fileContent, "ImageFile", imageFile.Name);
            }

            var token = await _localStorage.GetItemAsync<string>("authToken");
            var request = new HttpRequestMessage(HttpMethod.Put, $"EnFocoBACK/api/Notice/{id}")
            {
                Content = content
            };

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }






        public async Task<bool> DeleteAsync(int id)
        {
            var request = await CreateRequestAsync(HttpMethod.Delete, $"EnFocoBACK/api/Notice/{id}");
            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
