using Dapper;
using EHMSWebApp.Interface;
using EHMSModel;
using System.Data;

namespace EHMSWebApp.Services
{
    public class EmployeePhysicalFitnessService : IEmployeePhysicalFitnessService
    {
        private readonly IDbConnection _dbConnection;

        public EmployeePhysicalFitnessService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> CreateEmployeePhysicalFitnessAsync(EmployeePhysicalFitness fitness)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "CreateEmployeePhysicalFitnessAsync");
            parameters.Add("@EmpId", fitness.EmpId);
            parameters.Add("@Weight", fitness.Weight);
            parameters.Add("@Height", fitness.Height);
            return await _dbConnection.QuerySingleAsync<int>("MangePhysicalFitness", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<EmployeePhysicalFitness>> GetAllEmployeePhysicalFitness()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "GetAllEmployeePhysicalFitness");
            return await _dbConnection.QueryAsync<EmployeePhysicalFitness>("MangePhysicalFitness", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateEmployeePhysicalFitnessAsync(EmployeePhysicalFitness fitness)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "UpdateEmployeePhysicalFitnessAsync");
            parameters.Add("@EmpId", fitness.EmpId);
            parameters.Add("@Weight", fitness.Weight);
            parameters.Add("@Height", fitness.Height);
            parameters.Add("@EmployeePhysicalFitnessId", fitness.EmployeePhysicalFitnessId);
            return await _dbConnection.ExecuteAsync("MangePhysicalFitness", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteEmployeePhysicalFitnessAsync(int employeePhysicalFitnessId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "DeleteEmployeePhysicalFitnessAsync");
            parameters.Add("@EmployeePhysicalFitnessId", employeePhysicalFitnessId);
            return await _dbConnection.ExecuteAsync("MangePhysicalFitness", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<EmployeePhysicalFitness>> GetAllEmployeePhysicalFitnessByEmpId(int empId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmpId", empId);
            parameters.Add("@Filter", "GetAllEmployeePhysicalFitnessByEmpId");
            return await _dbConnection.QueryAsync<EmployeePhysicalFitness>("MangePhysicalFitness", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
