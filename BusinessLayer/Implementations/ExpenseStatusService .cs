using BusinessLayer.Interfaces;
using DataAccessLayer.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
  public class ExpenseStatusService : IExpenseStatusService
  {
    private readonly HRMSContext _context;


    public ExpenseStatusService(HRMSContext context)
    {
      _context = context;
    }

    public async Task<List<ExpenseStatusDto>> GetExpenseStatus(int companyId, int regionId)
    {
      var data = await _context.ExpenseStatuses
          .Where(x => x.CompanyId == companyId
                   && x.RegionId == regionId
                    
                   && x.IsDeleted == false)
          .Select(x => new ExpenseStatusDto
          {
            ExpenseStatusId = x.ExpenseStatusId,
            CompanyId = x.CompanyId,
            RegionId = x.RegionId,
            ExpenseStatusName = x.ExpenseStatusName,
            Description = x.Description,
            IsActive = x.IsActive
          })
          .ToListAsync();

      return data;
    }

    public async Task<ExpenseStatusDto> CreateExpenseStatus(ExpenseStatusDto dto, int userId)
    {
      var exists = await _context.ExpenseStatuses
          .AnyAsync(x =>
              x.ExpenseStatusName.Trim().ToLower() == dto.ExpenseStatusName.Trim().ToLower()
              && x.CompanyId == dto.CompanyId
              && x.RegionId == dto.RegionId
               && x.CreatedBy == userId
              && x.IsDeleted == false);

      if (exists)
        throw new Exception("Expense Status already exists.");

      var model = new ExpenseStatus
      {
        CompanyId = dto.CompanyId,
        RegionId = dto.RegionId,
        ExpenseStatusName = dto.ExpenseStatusName,
        Description = dto.Description,
        IsActive = dto.IsActive,
        CreatedBy = userId,
        CreatedAt = DateTime.Now,
        IsDeleted = false
      };

      _context.ExpenseStatuses.Add(model);
      await _context.SaveChangesAsync();

      dto.ExpenseStatusId = model.ExpenseStatusId;

      return dto;
    }

    public async Task<ExpenseStatusDto> UpdateExpenseStatus(ExpenseStatusDto dto, int userId)
    {
      var data = await _context.ExpenseStatuses
          .FirstOrDefaultAsync(x => x.ExpenseStatusId == dto.ExpenseStatusId);

      if (data == null)
        throw new Exception("Expense Status not found.");

      data.ExpenseStatusName = dto.ExpenseStatusName;
      data.Description = dto.Description;
      data.IsActive = dto.IsActive;
      data.ModifiedBy = userId;
      data.ModifiedAt = DateTime.Now;

      await _context.SaveChangesAsync();

      return dto;
    }

    public async Task<bool> DeleteExpenseStatus(int id, int userId)
    {
      var data = await _context.ExpenseStatuses
          .FirstOrDefaultAsync(x => x.ExpenseStatusId == id);

      if (data == null)
        throw new Exception("Expense Status not found.");

      data.IsDeleted = true;
      data.ModifiedBy = userId;
      data.ModifiedAt = DateTime.Now;

      await _context.SaveChangesAsync();

      return true;
    }



    public async Task<List<ExpenseStatusDto>> GetExpenseStatusByUser(int userId)
    {
      // Get user's company & region
      var user = await _context.Users
          .Where(u => u.UserId == userId)
          .Select(u => new { u.CompanyId, u.RegionId })
          .FirstOrDefaultAsync();

      if (user == null)
        throw new Exception("User not found.");

      // Fetch expense statuses for that user
      return await _context.ExpenseStatuses
          .Where(x => x.CompanyId == user.CompanyId
                   && x.RegionId == user.RegionId
                   && x.CreatedBy == userId

                   && x.IsDeleted == false)
          .Select(x => new ExpenseStatusDto
          {
            ExpenseStatusId = x.ExpenseStatusId,
            CompanyId = x.CompanyId,
            RegionId = x.RegionId,
            ExpenseStatusName = x.ExpenseStatusName,
            Description = x.Description,
            IsActive = x.IsActive,

            
          })
          .ToListAsync();
    }

  }
}
