using BusinessLayer.Common;
using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IPerformanceService
    {
        Task<ApiResponse<IEnumerable<PerformanceReviewDto>>> GetByUserIdAsync(int userId);
        Task<ApiResponse<bool>> SaveAsync(PerformanceReviewDto dto);
        Task<ApiResponse<IEnumerable<PerformanceReviewDto>>> GetManagerReviewsAsync(int managerId);
        Task<ApiResponse<bool>> ApproveAsync(int reviewId, int managerId, string remarks);
        Task<ApiResponse<bool>> RejectAsync(int reviewId, int managerId, string remarks);
        // ✅ NEW METHOD — Reset status back to Submitted
        Task<ApiResponse<bool>>
            RequestAsync(int reviewId);
    }

}
