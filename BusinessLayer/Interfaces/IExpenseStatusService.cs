using DataAccessLayer.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
  public interface IExpenseStatusService
  {
    Task<List<ExpenseStatusDto>> GetExpenseStatus(int companyId, int regionId);
    Task<ExpenseStatusDto> CreateExpenseStatus(ExpenseStatusDto dto, int userId);
    Task<ExpenseStatusDto> UpdateExpenseStatus(ExpenseStatusDto dto, int userId);
    Task<bool> DeleteExpenseStatus(int id, int userId);


    Task<List<ExpenseStatusDto>> GetExpenseStatusByUser(int userId);

  }
}
