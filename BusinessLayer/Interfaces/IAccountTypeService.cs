using BusinessLayer.Common;
using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
  public interface IAccountTypeService
  {
    Task<ApiResponse<IEnumerable<AccountTypeDto>>> GetAllAccounttypeAsync(int userId);
    Task<AccountTypeDto?> GetByIdAccounttypeAsync(int id);
    Task<AccountTypeDto> AddAccounttypeAsync(AccountTypeDto dto);
    Task<AccountTypeDto> UpdateAccounttypeAsync(AccountTypeDto dto);
    Task<bool> DeleteAccounttypeAsync(int id);
  }
}
