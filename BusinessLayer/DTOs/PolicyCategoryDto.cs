using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class PolicyCategoryDto
    {
        public int PolicyCategoryId { get; set; }
        public int CompanyId { get; set; }
        public int RegionId { get; set; }
        public string PolicyCategoryName { get; set; }
        public bool IsActive { get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }
    }
}