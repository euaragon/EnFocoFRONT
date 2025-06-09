using System.Net.Http.Json;
using EnFocoFRONT.Models;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;


namespace EnFocoFRONT.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthStateService _authStateService;

        public AuthService(HttpClient httpClient, NavigationManager navigationManager, Blazored.LocalStorage.ILocalStorageService localStorage, AuthStateService authStateService)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorage = localStorage;
            _authStateService = authStateService;
        }

        public async Task<bool> Login(LoginRequest loginModel)
        {
            var result = await _httpClient.PostAsJsonAsync("EnFocoBACK/api/Auth/login", loginModel);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
                if (response?.Token != null)
                {
                    await _localStorage.SetItemAsync("authToken", response.Token);
                    _authStateService.IsAuthenticated = true; // Actualiza el estado
                    _navigationManager.NavigateTo("/");
                    return true;
                }
            }
            return false;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _authStateService.IsAuthenticated = false; // Actualiza el estado
            _navigationManager.NavigateTo("/");
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }

        public async Task<bool> IsUserAuthenticatedAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            return !string.IsNullOrEmpty(token);
        }
    }
}