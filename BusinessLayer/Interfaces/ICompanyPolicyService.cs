using BusinessLayer.DTOs;
using DataAccessLayer.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICompanyPolicyService
    {
        Task<List<CompanyPolicyDto>> GetAllPoliciesAsync();
        Task<CompanyPolicyDto> GetPolicyByIdAsync(int policyId);
        Task<CompanyPolicyDto> CreatePolicyAsync(CompanyPolicyDto dto);
        Task<CompanyPolicyDto> UpdatePolicyAsync(CompanyPolicyDto dto);
        Task<bool> DeletePolicyAsync(int policyId);

        // Dropdown - Policy Categories
        Task<IEnumerable<PolicyCategoryDto>> GetAllPolicyCategoriesAsync(int companyId, int regionId);
    }
}