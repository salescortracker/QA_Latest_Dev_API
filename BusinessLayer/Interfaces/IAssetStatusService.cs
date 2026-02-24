using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAssetStatusService
    {
        

        Task<List<AssetStatusDto>> GetAllAssetStatusesAsync(int userId);
        Task<int> AddAssetStatusAsync(AssetStatusDto dto);
        Task<bool> UpdateAssetStatusAsync(AssetStatusDto dto);
        Task<bool> DeleteAssetStatusAsync(int assetStatusId);
    }
}
