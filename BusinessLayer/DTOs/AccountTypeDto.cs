using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class AccountTypeDto
    {
    //public int AccountTypeId { get; set; }
    //public string AccountTypeName { get; set; } = null!;
    //public bool? IsActive { get; set; }

        public int AccountTypeId { get; set; }
        public string AccountTypeName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        public int CompanyId { get; set; }
        public int RegionId { get; set; }
        public int? UserId { get; set; }

        public string? CompanyName { get; set; }
        public string? RegionName { get; set; }
  }
}
