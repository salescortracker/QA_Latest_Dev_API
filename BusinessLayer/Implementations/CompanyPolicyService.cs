using BusinessLayer.DTOs;
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
    public class CompanyPolicyService : ICompanyPolicyService
    {
        private readonly HRMSContext _context;
        private readonly string _uploadPath =
            @"C:\Users\vamsh\Documents\GitHub\QA_Latest_Dev_API\HRMS_Backend\wwwroot\Uploads\companypolicyfiles";

        public CompanyPolicyService(HRMSContext context)
        {
            _context = context;

            if (!Directory.Exists(_uploadPath))
                Directory.CreateDirectory(_uploadPath);
        }

        // ----------------------
        // GET ALL POLICIES
        // ----------------------
        public async Task<List<CompanyPolicyDto>> GetAllPoliciesAsync()
        {
            return await _context.CompanyPolicies
                .Include(x => x.Category)
                .Select(x => new CompanyPolicyDto
                {
                    PolicyId = x.PolicyId,
                    CompanyId = x.CompanyId,
                    RegionId = x.RegionId,
                    Title = x.Title,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.PolicyCategoryName,
                    EffectiveDate = x.EffectiveDate.ToDateTime(TimeOnly.MinValue),
                    Description = x.Description,
                    FileName = x.FileName,
                    FilePath = x.FilePath
                })
                .ToListAsync();
        }

        // ----------------------
        // GET POLICY BY ID
        // ----------------------
        public async Task<CompanyPolicyDto> GetPolicyByIdAsync(int policyId)
        {
            var x = await _context.CompanyPolicies
                .Include(c => c.Category)
                .FirstOrDefaultAsync(p => p.PolicyId == policyId);

            if (x == null) return null!;

            return new CompanyPolicyDto
            {
                PolicyId = x.PolicyId,
                CompanyId = x.CompanyId,
                RegionId = x.RegionId,
                Title = x.Title,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.PolicyCategoryName,
                EffectiveDate = x.EffectiveDate.ToDateTime(TimeOnly.MinValue),
                Description = x.Description,
                FileName = x.FileName,
                FilePath = x.FilePath
            };
        }

        // ----------------------
        // CREATE POLICY
        // ----------------------
        public async Task<CompanyPolicyDto> CreatePolicyAsync(CompanyPolicyDto dto)
        {
            string? fileName = null;
            string? filePath = null;

            if (dto.File != null)
            {
                fileName = $"{Guid.NewGuid()}_{dto.File.FileName}";
                var physical = Path.Combine(_uploadPath, fileName);

                using var stream = new FileStream(physical, FileMode.Create);
                await dto.File.CopyToAsync(stream);

                filePath = $"/Uploads/companypolicyfiles/{fileName}";
            }

            var entity = new CompanyPolicy
            {
                CompanyId = dto.CompanyId,
                RegionId = dto.RegionId,
                Title = dto.Title,
                CategoryId = dto.CategoryId,
                EffectiveDate = DateOnly.FromDateTime(dto.EffectiveDate),
                Description = dto.Description,
                FileName = fileName ?? "",
                FilePath = filePath,
                CreatedAt = DateTime.Now
            };

            _context.CompanyPolicies.Add(entity);
            await _context.SaveChangesAsync();

            dto.PolicyId = entity.PolicyId;
            dto.FileName = entity.FileName;
            dto.FilePath = entity.FilePath;

            return dto;
        }

        // ----------------------
        // UPDATE POLICY
        // ----------------------
        public async Task<CompanyPolicyDto> UpdatePolicyAsync(CompanyPolicyDto dto)
        {
            var entity = await _context.CompanyPolicies.FindAsync(dto.PolicyId);
            if (entity == null) return null!;

            entity.CompanyId = dto.CompanyId;
            entity.RegionId = dto.RegionId;
            entity.Title = dto.Title;
            entity.CategoryId = dto.CategoryId;
            entity.EffectiveDate = DateOnly.FromDateTime(dto.EffectiveDate);
            entity.Description = dto.Description;
            entity.ModifiedAt = DateTime.Now;

            if (dto.File != null)
            {
                var fileName = $"{Guid.NewGuid()}_{dto.File.FileName}";
                var physical = Path.Combine(_uploadPath, fileName);

                using var stream = new FileStream(physical, FileMode.Create);
                await dto.File.CopyToAsync(stream);

                entity.FileName = fileName;
                entity.FilePath = $"/Uploads/companypolicyfiles/{fileName}";
            }

            await _context.SaveChangesAsync();
            return dto;
        }

        // ----------------------
        // DELETE POLICY
        // ----------------------
        public async Task<bool> DeletePolicyAsync(int policyId)
        {
            var entity = await _context.CompanyPolicies.FindAsync(policyId);
            if (entity == null) return false;

            _context.CompanyPolicies.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ----------------------
        // POLICY CATEGORY DROPDOWN
        // ----------------------
        public async Task<IEnumerable<PolicyCategoryDto>> GetAllPolicyCategoriesAsync(int companyId, int regionId)
        {
            return await _context.PolicyCategories
                .Where(x => x.CompanyId == companyId &&
                            x.RegionId == regionId &&
                            x.IsActive &&
                            !x.IsDeleted)
                .Select(x => new PolicyCategoryDto
                {
                    PolicyCategoryId = x.PolicyCategoryId,
                    CompanyId = x.CompanyId,
                    RegionId = x.RegionId,
                    PolicyCategoryName = x.PolicyCategoryName,
                    Description = x.Description
                })
                .ToListAsync();
        }
    }
}