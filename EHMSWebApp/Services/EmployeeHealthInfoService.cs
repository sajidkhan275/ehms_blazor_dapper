using Dapper;
using EHMSModel;
using EHMSWebApp.Interface;
using System.Data;

namespace EHMSWebApp.Services
{
    public class EmployeeHealthInfoService : IEmployeeHealthInfoService
    {
        private readonly IDbConnection _dbConnection;

        public EmployeeHealthInfoService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> CreateEmployeeHealthInfoAsync(EmployeeHealthInfo healthInfo)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "CreateEmployeeHealthInfoAsync");
            parameters.Add("@EmpId", healthInfo.EmpId);
            parameters.Add("@BloodGroup", healthInfo.BloodGroup);
            parameters.Add("@Disability", healthInfo.Disability);
            parameters.Add("@MedicalReportFileName", healthInfo.MedicalReportFileName);
            parameters.Add("@RecentMedicalReportPath", healthInfo.RecentMedicalReportPath);
            return await _dbConnection.QuerySingleAsync<int>("MangeHealthInfo", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<EmployeeHealthInfo>> GetAllEmployeeHealthInfo()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "GetAllEmployeeHealthInfo");
            return await _dbConnection.QueryAsync<EmployeeHealthInfo>("MangeHealthInfo", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateEmployeeHealthInfoAsync(EmployeeHealthInfo healthInfo)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "UpdateEmployeeHealthInfoAsync");
            parameters.Add("@EmpId", healthInfo.EmpId);
            parameters.Add("@BloodGroup", healthInfo.BloodGroup);
            parameters.Add("@Disability", healthInfo.Disability);
            parameters.Add("@MedicalReportFileName", healthInfo.MedicalReportFileName);
            parameters.Add("@RecentMedicalReportPath", healthInfo.RecentMedicalReportPath);
            parameters.Add("@EmployeeHealthInfoId", healthInfo.EmployeeHealthInfoId);
            return await _dbConnection.ExecuteAsync("MangeHealthInfo", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteEmployeeHealthInfoAsync(int employeeHealthInfoId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "DeleteEmployeeHealthInfoAsync");
            parameters.Add("@EmployeeHealthInfoId", employeeHealthInfoId);
            return await _dbConnection.ExecuteAsync("MangeHealthInfo", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<EmployeeHealthInfo>> GetAllEmployeeHealthInfoByEmpId(int empId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmpId", empId);
            parameters.Add("@Filter", "GetAllEmployeeHealthInfoByEmpId");
            return await _dbConnection.QueryAsync<EmployeeHealthInfo>("MangeHealthInfo", parameters, commandType: CommandType.StoredProcedure);
        }


    }
}
