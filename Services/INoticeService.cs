using System.Collections.Generic;
using System.Threading.Tasks;
using EnFocoFRONT.Models;

namespace EnFocoFRONT.Services
{
    public interface INoticeService
    {
        Task<List<Notice>> GetAllAsync();
        Task<Notice?> GetByIdAsync(int id);
        Task<List<Notice>> GetBySectionAsync(string section);
        Task<List<Notice>> SearchAsync(string term);
        Task<bool> CreateAsync(Notice notice);
        Task<bool> UpdateAsync(int id, Notice notice);
        Task<bool> DeleteAsync(int id);
    }
}
