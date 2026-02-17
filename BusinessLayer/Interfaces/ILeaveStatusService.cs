using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
  public interface ILeaveStatusService
  {
    Task<List<LeaveStatusDto>> GetLeaveStatusAsync( int userId);
    Task<bool> CreateLeaveStatusAsync(LeaveStatusDto dto, int userId);
    Task<bool> UpdateLeaveStatusAsync(LeaveStatusDto dto, int userId);
    Task<bool> DeleteLeaveStatusAsync(int id, int userId);
   // Task<bool> BulkInsertAsync(List<LeaveStatusDto> list, int userId);
  }
}
