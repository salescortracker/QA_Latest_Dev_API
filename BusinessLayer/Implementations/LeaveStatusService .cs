using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.DBContext;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
  public class LeaveStatusService :ILeaveStatusService
  {
    private readonly HRMSContext _context;

    public LeaveStatusService(HRMSContext context)
    {
      _context = context;
    }


    public async Task<List<LeaveStatusDto>> GetLeaveStatusAsync( int userId)
    {
      return await _context.LeaveStatuses
          .Where(x => 
                       x.CreatedBy == userId &&
                      !x.IsDeleted)
          .Select(x => new LeaveStatusDto
          {
            LeaveStatusId = x.LeaveStatusId,
            LeaveStatusName = x.LeaveStatusName,
            Description = x.Description,
            CompanyId = x.CompanyId,
            RegionId = x.RegionId,
            IsActive = x.IsActive
          })
          .OrderByDescending(x => x.LeaveStatusId)
          .ToListAsync();
    }
    public async Task<bool> CreateLeaveStatusAsync(LeaveStatusDto dto, int userId)
    {
      var name = dto.LeaveStatusName.Trim().ToLower();

      var exists = await _context.LeaveStatuses.AnyAsync(x =>
          !x.IsDeleted &&
          x.CompanyId == dto.CompanyId &&
          x.RegionId == dto.RegionId &&
          x.CreatedBy == userId &&
          x.LeaveStatusName.ToLower().Trim() == name
      );

      if (exists)
        throw new Exception("Leave Status already exists.");

      var entity = new LeaveStatus
      {
        LeaveStatusName = dto.LeaveStatusName.Trim(),
        Description = dto.Description,
        CompanyId = dto.CompanyId,
        RegionId = dto.RegionId,
        IsActive = dto.IsActive,
        CreatedBy = userId,
        CreatedAt = DateTime.Now,
        IsDeleted = false
      };

      await _context.LeaveStatuses.AddAsync(entity);
      await _context.SaveChangesAsync();
      return true;
    }




    public async Task<bool> UpdateLeaveStatusAsync(LeaveStatusDto dto, int userId)
    {
      var name = dto.LeaveStatusName.Trim().ToLower();

      var duplicate = await _context.LeaveStatuses.AnyAsync(x =>
          x.LeaveStatusId != dto.LeaveStatusId &&
          !x.IsDeleted &&
          x.CompanyId == dto.CompanyId &&
          x.RegionId == dto.RegionId &&
          x.CreatedBy == userId &&
          x.LeaveStatusName.ToLower().Trim() == name
      );

      if (duplicate)
        throw new Exception("Leave Status already exists.");

      var existing = await _context.LeaveStatuses
          .FirstOrDefaultAsync(x => x.LeaveStatusId == dto.LeaveStatusId && !x.IsDeleted);

      if (existing == null)
        return false;

      existing.LeaveStatusName = dto.LeaveStatusName.Trim();
      existing.Description = dto.Description;
      existing.IsActive = dto.IsActive;
      existing.ModifiedBy = userId;
      existing.ModifiedAt = DateTime.Now;

      await _context.SaveChangesAsync();
      return true;
    }


    public async Task<bool> DeleteLeaveStatusAsync(int id, int userId)
    {
      var data = await _context.LeaveStatuses.FindAsync(id);
      if (data == null) return false;

      // soft delete
      data.IsDeleted = true;
      data.ModifiedBy = userId;
      data.ModifiedAt = DateTime.Now;

      await _context.SaveChangesAsync();
      return true;
    }
  }
}
