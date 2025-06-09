using EnFocoFRONT.Models;

namespace EnFocoFRONT.Services
{
    public interface INewsletterService
    {
        Task<List<Newsletter>> GetAllAsync();
        Task<bool> CreateAsync(Newsletter newsletter);
    }
}
