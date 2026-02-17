using BusinessLayer.Common;
using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IHelpdeskCategoryAdminService
    {
        Task<ApiResponse<IEnumerable<CreateUpdateHelpdeskCategoryDto>>> GetAll(int userId);

        Task<ApiResponse<CreateUpdateHelpdeskCategoryDto?>> GetByIdAsync(int id);

        Task<ApiResponse<string>> CreateAsync(CreateUpdateHelpdeskCategoryDto dto);

        Task<ApiResponse<string>> UpdateAsync(CreateUpdateHelpdeskCategoryDto dto);

        Task<ApiResponse<string>> DeleteAsync(int id);
    }
}
