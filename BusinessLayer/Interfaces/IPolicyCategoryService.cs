using BusinessLayer.Common;
using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IPolicyCategoryService
    {
        Task<ApiResponse<IEnumerable<PolicyCategoryDto>>> GetAll(int userId);

        Task<ApiResponse<PolicyCategoryDto?>> GetByIdAsync(int id);

        Task<ApiResponse<string>> CreateAsync(PolicyCategoryDto dto);

        Task<ApiResponse<string>> UpdateAsync(PolicyCategoryDto dto);

        Task<ApiResponse<string>> DeleteAsync(int id);
    }
}
