using Dapper;
using EHMSModel;
using EHMSWebApp.Interface;
using System.Data;

namespace EHMSWebApp.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDbConnection _dbConnection;
        public DepartmentService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<DepartmentDetails>> GetAllDepartments()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "GetAllDepartments");
            return await _dbConnection.QueryAsync<DepartmentDetails>("MangeDepartment", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> CreateDepartments(DepartmentDetails department)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "CreateDepartments");
            parameters.Add("@DepartmentName", department.DepartmentName);
            return await _dbConnection.QuerySingleAsync<int>("MangeDepartment", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteDepartments(int departmentId)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "DeleteDepartments");
            parameters.Add("@DepartmentId", departmentId);
            return await _dbConnection.ExecuteAsync("MangeDepartment", parameters, commandType: CommandType.StoredProcedure);
        }

  

        public async Task<int> UpdateDepartments(DepartmentDetails department)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "UpdateDepartments");
            parameters.Add("@DepartmentName", department.DepartmentName);
            parameters.Add("@DepartmentId", department.DepartmentId);
            return await _dbConnection.ExecuteAsync("MangeDepartment", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
