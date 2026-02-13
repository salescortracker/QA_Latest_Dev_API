using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;

namespace BusinessLayer
{
  public class ExpenseStatusDto
  {
    public int ExpenseStatusId { get; set; }   // 0 for Create

    public int CompanyId { get; set; }
    public int RegionId { get; set; }

    public string ExpenseStatusName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
    public int UserId { get; set; }
  }
}
