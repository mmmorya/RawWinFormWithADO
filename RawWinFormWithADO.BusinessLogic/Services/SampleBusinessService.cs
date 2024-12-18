
using Microsoft.Extensions.Logging;
using RawWinFormWithADO.BusinessLogic.Contracts;
using RawWinFormWithADO.DataAccess.Contracts;
using System.Data;

namespace RawWinFormWithADO.BusinessLogic.Services
{
    public class SampleBusinessService : ISampleBusinessService
    {
        private readonly IDAL _dal;
        private readonly ILogger<SampleBusinessService> _logger;

        public SampleBusinessService(IDAL dal, ILogger<SampleBusinessService> logger)
        {
            _dal = dal ?? throw new ArgumentNullException(nameof(dal));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DataTable> GetAllMasterDataAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all master data.");
                return await _dal.GetAsync("SELECT * FROM MasterData");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching master data.");
                throw;
            }
        }

        public async Task SaveMasterDataAsync(object data)
        {
            try
            {
                _logger.LogInformation("Saving master data.");
                // Use DAL to save the data (adjust parameters as per your schema)
                await _dal.ExecuteAsync("INSERT INTO MasterData (Column1) VALUES (@Value)", new[] {
                    new Microsoft.Data.SqlClient.SqlParameter("@Value", data)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while saving master data.");
                throw;
            }
        }

    }
}
