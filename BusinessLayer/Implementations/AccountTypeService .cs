using BusinessLayer.Common;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.DBContext;
using DataAccessLayer.Repositories.GeneralRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
  public class AccountTypeService : IAccountTypeService
  {
    private readonly IUnitOfWork _unit;
    private readonly HRMSContext _context;

    public AccountTypeService(IUnitOfWork unit, HRMSContext context)
    {
      _unit = unit;
      _context = context;
    }

    public async Task<ApiResponse<IEnumerable<AccountTypeDto>>>GetAllAccounttypeAsync(int userId)
    {
      var list = await _unit.Repository<AccountType>()
          .FindAsync(x => !x.IsDeleted
                       && x.CreatedBy == userId);

      var dto = list.Select(x => new AccountTypeDto
      {
        AccountTypeId = x.AccountTypeId,
        AccountTypeName = x.AccountType1,
        Description = x.Description,
        IsActive = x.IsActive,
        CompanyId = x.CompanyId,
        RegionId = x.RegionId,
        CompanyName = _context.Companies
                .FirstOrDefault(c => c.CompanyId == x.CompanyId)?.CompanyName,
        RegionName = _context.Regions
                .FirstOrDefault(r => r.RegionId == x.RegionId)?.RegionName
      });

      return new ApiResponse<IEnumerable<AccountTypeDto>>(dto, "Retrieved Successfully");
    }

    public async Task<ApiResponse<IEnumerable<AccountTypeDto>>>GetAllAccounttypeNameAsync(int companyId, int regionId)
    {
      var list = await _unit.Repository<AccountType>()
          .FindAsync(x => !x.IsDeleted

                      && x.CompanyId == companyId &&
            x.RegionId == regionId
          );

      var dto = list.Select(x => new AccountTypeDto
      {
        AccountTypeId = x.AccountTypeId,
        AccountTypeName = x.AccountType1,
        Description = x.Description,
        IsActive = x.IsActive,
        CompanyId = x.CompanyId,
        RegionId = x.RegionId,
        CompanyName = _context.Companies
                .FirstOrDefault(c => c.CompanyId == x.CompanyId)?.CompanyName,
        RegionName = _context.Regions
                .FirstOrDefault(r => r.RegionId == x.RegionId)?.RegionName
      });

      return new ApiResponse<IEnumerable<AccountTypeDto>>(dto, "Retrieved Successfully");
    }

    public async Task<AccountTypeDto?> GetByIdAccounttypeAsync(int id)
    {
      var e = await _unit.Repository<AccountType>().GetByIdAsync(id);
      if (e == null) return null;

      return new AccountTypeDto
      {
        AccountTypeId = e.AccountTypeId,
        AccountTypeName = e.AccountType1,
        Description = e.Description,
        IsActive = e.IsActive,
        CompanyId = e.CompanyId,
        RegionId = e.RegionId
      };
    }

    public async Task<AccountTypeDto> AddAccounttypeAsync(AccountTypeDto dto)
    {
      var exists = _context.AccountTypes.Any(x =>
           !x.IsDeleted &&
           x.AccountType1.ToLower() == dto.AccountTypeName.ToLower() &&
           x.CreatedBy == dto.UserId
      );

      if (exists) return null;

      var entity = new AccountType
      {
        AccountType1 = dto.AccountTypeName,
        Description = dto.Description,
        CompanyId = dto.CompanyId,
        RegionId = dto.RegionId,
        IsActive = dto.IsActive,
        CreatedBy = dto.UserId,
        CreatedAt = DateTime.UtcNow
      };

      await _unit.Repository<AccountType>().AddAsync(entity);
      await _unit.CompleteAsync();

      dto.AccountTypeId = entity.AccountTypeId;
      return dto;
    }

    public async Task<AccountTypeDto> UpdateAccounttypeAsync(AccountTypeDto dto)
    {
      var entity = await _unit.Repository<AccountType>()
          .GetByIdAsync(dto.AccountTypeId);

      if (entity == null)
        throw new Exception("Not found");

      var exists = _context.AccountTypes.Any(x =>
          x.AccountType1.ToLower() == dto.AccountTypeName.ToLower()
          && x.AccountTypeId != dto.AccountTypeId);

      if (exists) return null;

      entity.AccountType1 = dto.AccountTypeName;
      entity.Description = dto.Description;
      entity.CompanyId = dto.CompanyId;
      entity.RegionId = dto.RegionId;
      entity.IsActive = dto.IsActive;
      entity.ModifiedAt = DateTime.UtcNow;
      entity.ModifiedBy = dto.UserId;

      _unit.Repository<AccountType>().Update(entity);
      await _unit.CompleteAsync();

      return dto;
    }

    public async Task<bool> DeleteAccounttypeAsync(int id)
    {
      var entity = await _unit.Repository<AccountType>().GetByIdAsync(id);
      if (entity == null) return false;

      entity.IsDeleted = true;

      _unit.Repository<AccountType>().Update(entity);
      await _unit.CompleteAsync();

      return true;
    }
  }

}
