using BusinessLayer.Common;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.DBContext;
using DataAccessLayer.Repositories.GeneralRepository;

namespace BusinessLayer.Implementations
{
    public class PolicyCategoryService : IPolicyCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PolicyCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET ALL
        public async Task<ApiResponse<IEnumerable<PolicyCategoryDto>>> GetAll(int userId)
        {
            var list = (await _unitOfWork.Repository<PolicyCategory>()
                .FindAsync(x => !x.IsDeleted && x.UserId == userId))
                .OrderByDescending(x => x.PolicyCategoryId)
                .ToList();

            var dto = list.Select(x => new PolicyCategoryDto
            {
                PolicyCategoryId = x.PolicyCategoryId,
                CompanyId = x.CompanyId,
                RegionId = x.RegionId,
                PolicyCategoryName = x.PolicyCategoryName,
                Description = x.Description,
                IsActive = x.IsActive,
                UserId = x.UserId
            });

            return new ApiResponse<IEnumerable<PolicyCategoryDto>>(dto, "Policy categories retrieved.");
        }

        // GET BY ID
        public async Task<ApiResponse<PolicyCategoryDto?>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Repository<PolicyCategory>().GetByIdAsync(id);

            if (entity == null || entity.IsDeleted)
                return new ApiResponse<PolicyCategoryDto?>(null, "Policy Category not found.", false);

            var dto = new PolicyCategoryDto
            {
                PolicyCategoryId = entity.PolicyCategoryId,
                CompanyId = entity.CompanyId,
                RegionId = entity.RegionId,
                PolicyCategoryName = entity.PolicyCategoryName,
                Description = entity.Description,
                IsActive = entity.IsActive,
                UserId = entity.UserId
            };

            return new ApiResponse<PolicyCategoryDto?>(dto, "Policy Category retrieved.");
        }

        // CREATE
        public async Task<ApiResponse<string>> CreateAsync(PolicyCategoryDto dto)
        {
            var duplicate = (await _unitOfWork.Repository<PolicyCategory>().FindAsync(x =>
                !x.IsDeleted &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.PolicyCategoryName.ToLower() == dto.PolicyCategoryName.ToLower()))
                .Any();

            if (duplicate)
                return new ApiResponse<string>(null!, "Duplicate Policy Category exists.", false);

            var entity = new PolicyCategory
            {
                CompanyId = dto.CompanyId,
                RegionId = dto.RegionId,
                PolicyCategoryName = dto.PolicyCategoryName,
                Description = dto.Description,
                IsActive = dto.IsActive,
                UserId = dto.UserId,
                CreatedBy = dto.UserId ?? 0
            };

            await _unitOfWork.Repository<PolicyCategory>().AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<string>("Policy Category created.");
        }

        // UPDATE
        public async Task<ApiResponse<string>> UpdateAsync(PolicyCategoryDto dto)
        {
            var entity = await _unitOfWork.Repository<PolicyCategory>()
                .GetByIdAsync(dto.PolicyCategoryId);

            if (entity == null || entity.IsDeleted)
                return new ApiResponse<string>(null!, "Policy Category not found.", false);

            entity.CompanyId = dto.CompanyId;
            entity.RegionId = dto.RegionId;
            entity.PolicyCategoryName = dto.PolicyCategoryName;
            entity.Description = dto.Description;
            entity.IsActive = dto.IsActive;
            entity.ModifiedAt = DateTime.UtcNow;
            entity.ModifiedBy = dto.UserId;

            _unitOfWork.Repository<PolicyCategory>().Update(entity);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<string>("Policy Category updated.");
        }

        // DELETE
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Repository<PolicyCategory>().GetByIdAsync(id);

            if (entity == null || entity.IsDeleted)
                return new ApiResponse<string>(null!, "Policy Category not found.", false);

            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.UtcNow;

            _unitOfWork.Repository<PolicyCategory>().Update(entity);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<string>("Policy Category deleted.");
        }
    }
}
