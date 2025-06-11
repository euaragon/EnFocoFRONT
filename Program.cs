using EnFocoFRONT;
using EnFocoFRONT.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

var baseAddress = builder.Configuration["EnFocoBACK"];

if (string.IsNullOrEmpty(baseAddress))
{
    throw new InvalidOperationException("EnFocoBACK no está configurado.");
}

Console.WriteLine($"BaseAddress: {baseAddress}");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(baseAddress)
});




Console.WriteLine("Registrando servicios...");
builder.Services.AddScoped<AuthStateService>();
builder.Services.AddSingleton<IAuthStateService, AuthStateService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<INoticeService, NoticeService>();
builder.Services.AddScoped<INewsletterService, NewsletterService>();
builder.Services.AddScoped<IAppSettingsService, AppSettingsService>();
builder.Services.AddBlazoredLocalStorage();

var host = builder.Build();

var authService = host.Services.GetRequiredService<IAuthService>();
var token = await authService.GetTokenAsync();

if (!string.IsNullOrEmpty(token))
{
    var httpClient = host.Services.GetRequiredService<HttpClient>();
    httpClient.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

    var authStateService = host.Services.GetRequiredService<AuthStateService>();
    authStateService.IsAuthenticated = true;
}

await host.RunAsync();

