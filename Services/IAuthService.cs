using EnFocoFRONT.Models;

namespace EnFocoFRONT.Services
{
    public interface IAuthService
    {
        Task<bool> Login(LoginRequest loginModel);
        Task Logout();
        Task<string?> GetTokenAsync();
        Task<bool> IsUserAuthenticatedAsync();
    }
}
