using Dapper;
using EHMSWebApp.Interface;
using EHMSModel;
using System.Data;

namespace EHMSWebApp.Services
{
    public class RequestForHelpService : IRequestForHelpService
    {
        private readonly IDbConnection _dbConnection;

        public RequestForHelpService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> CreateRequestForHelpAsync(RequestForHelp request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "CreateRequestForHelpAsync");
            parameters.Add("@EmpId", request.EmpId);
            parameters.Add("@RequestDetails", request.RequestDetails);
            parameters.Add("@Status", request.Status);
            parameters.Add("@CreatedAt", request.CreatedAt);
            return await _dbConnection.ExecuteAsync("RequestForHelpManage", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<RequestForHelp>> GetRequestsByEmployeeIdAsync(int empId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmpId", empId);
            parameters.Add("@Filter", "GetRequestsByEmployeeIdAsync");
            return await _dbConnection.QueryAsync<RequestForHelp>("RequestForHelpManage", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<RequestForHelp>> GetRequestsByEmployeeAsync()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "GetRequestsByEmployeeAsync");
            return await _dbConnection.QueryAsync<RequestForHelp>("RequestForHelpManage", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateHRRequestAsync(RequestForHelp request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "UpdateHRRequestAsync");
            parameters.Add("@RequestForHelpId", request.RequestForHelpId);
            parameters.Add("@RespondedStatus", request.RespondedStatus);
            parameters.Add("@Status", request.Status);
            parameters.Add("@RespondedAt", request.RespondedAt);
            return await _dbConnection.ExecuteAsync("RequestForHelpManage", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteRequestForHelpAsync(int RequestForHelpId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "DeleteRequestForHelpAsync");
            parameters.Add("@RequestForHelpId", RequestForHelpId);
            return await _dbConnection.ExecuteAsync("RequestForHelpManage", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}

