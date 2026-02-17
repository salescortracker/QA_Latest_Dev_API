using BusinessLayer.Common;
using BusinessLayer.DTOs;
namespace BusinessLayer.Interfaces
{
    public interface IKpiCategoryService
    {
        Task<ApiResponse<IEnumerable<KpiCategoryDto>>> GetAll(int userId);

        Task<ApiResponse<KpiCategoryDto?>> GetByIdAsync(int id);

        Task<ApiResponse<string>> CreateAsync(CreateUpdateKpiCategoryDto dto);

        Task<ApiResponse<string>> UpdateAsync(CreateUpdateKpiCategoryDto dto);

        Task<ApiResponse<string>> DeleteAsync(int id);
    }
}
