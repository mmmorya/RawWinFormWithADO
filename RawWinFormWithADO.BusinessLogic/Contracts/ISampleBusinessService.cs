
using System.Data;

namespace RawWinFormWithADO.BusinessLogic.Contracts
{
    public interface ISampleBusinessService
    {
        Task<DataTable> GetAllMasterDataAsync();
        Task SaveMasterDataAsync(object data);
    }
}
