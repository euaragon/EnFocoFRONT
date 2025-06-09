using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

public class AppSettings
{
    public string EnFocoBACK { get; set; } = string.Empty;
    public string BaseFront { get; set; } = string.Empty;
}

public class AppSettingsService :  IAppSettingsService
{
    private readonly NavigationManager _navigationManager;
    private readonly HttpClient _httpClient;
    public AppSettings? Settings { get; private set; }

     public AppSettingsService(HttpClient httpClient, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
    }

    public async Task LoadAsync()
    {
       
         var uri = new Uri(new Uri(_navigationManager.BaseUri), "appsettings.json");
        Settings = await _httpClient.GetFromJsonAsync<AppSettings>(uri);
    }
}
