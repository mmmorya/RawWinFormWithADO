using Microsoft.Data.SqlClient;
using System.Data;

namespace RawWinFormWithADO.DataAccess.Contracts
{
    public interface IDAL
    {
        void Execute(string CommandName);
        void Execute(string CommandName, SqlParameter[] parameters);
        Task ExecuteAsync(string CommandName);
        Task ExecuteAsync(string CommandName, SqlParameter[] parameters);
        DataTable Get(string CommandName);
        DataTable Get(string CommandName, SqlParameter[] parameters);
        Task<DataTable> GetAsync(string CommandName);
        Task<DataTable> GetAsync(string CommandName, SqlParameter[] parameters);
        DataSet GetByProcedureAdapterDS(string CommandName, SqlParameter[] parameters);
    }
}
