using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IMaritalStatusService
    {
        Task<List<MaritalStatusDto>> GetAllAsync();
        Task<bool> CreateAsync(MaritalStatusDto dto, int userId);
        Task<bool> UpdateAsync(int id, MaritalStatusDto dto, int userId);
        Task<bool> DeleteAsync(int id, int userId);
    }
}
