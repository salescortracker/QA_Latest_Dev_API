using BusinessLayer.Common;
using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IHolidayListService
    {
        Task<ApiResponse<IEnumerable<HolidayListDto>>> GetAll(int userId);

        Task<ApiResponse<HolidayListDto?>> GetByIdAsync(int id);

        Task<ApiResponse<string>> CreateAsync(CreateUpdateHolidayListDto dto);

        Task<ApiResponse<string>> UpdateAsync(CreateUpdateHolidayListDto dto);

        Task<ApiResponse<string>> DeleteAsync(int id);
    }
}
