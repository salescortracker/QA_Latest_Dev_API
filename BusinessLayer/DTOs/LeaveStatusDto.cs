using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
  public class LeaveStatusDto
  {
    public int LeaveStatusId { get; set; }
    public int CompanyId { get; set; }
    public int RegionId { get; set; }
    public string LeaveStatusName { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
  }
}
