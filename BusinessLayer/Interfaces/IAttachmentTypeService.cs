  using BusinessLayer.DTOs;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  namespace BusinessLayer.Interfaces
  {
    public interface IAttachmentTypeService
    {
      Task<IEnumerable<AttachmentTypeDto>> GetAllByUserAttachmentTypeAsync(int userId);
      Task<bool> CreateAttachmentTypeAsync(AttachmentTypeDto dto);
      Task<bool> UpdateAttachmentTypeAsync(AttachmentTypeDto dto);
      Task<bool> DeleteAttachmentTypeAsync(int id);

    Task<IEnumerable<AttachmentTypeDto>> GetByCategoryAsync( string category);

    }
  }
