using Dapper;
using EHMSWebApp.Interface;
using EHMSModel;
using System.Data;

namespace EHMSWebApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDbConnection _dbConnection;

        public EmployeeService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> CreateEmployeeAsync(EmployeeDetails employee)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "CreateEmployee");
            parameters.Add("@EmployeeName", employee.EmployeeName);
            parameters.Add("@Email", employee.Email);
            parameters.Add("@AzureEntraId", employee.AzureEntraId);
            parameters.Add("@RoleId", employee.RoleId);
            return await _dbConnection.QuerySingleAsync<int>("MangeEmployeeData", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<EmployeeDetails>> GetAllEmployeesAsync()
        {
            try
            {
                return await _dbConnection.QueryAsync<EmployeeDetails>("MangeEmployeeData", commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                return new List<EmployeeDetails>();
            }
        }

        public async Task<int> UpdateEmployeeAsync(EmployeeDetails employee)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "UpdateEmployee");
            parameters.Add("@EmployeeCode", employee.EmployeeCode);
            parameters.Add("@EmployeeName", employee.EmployeeName);
            parameters.Add("@DepartmentId", employee.DepartmentId);
            parameters.Add("@JobTitle", employee.JobTitle);
            parameters.Add("@EmpId", employee.EmpId);
            return await _dbConnection.ExecuteAsync("MangeEmployeeData", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteEmployeeAsync(int empId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "DeleteEmployee");
            parameters.Add("@EmpId", empId);
            return await _dbConnection.ExecuteAsync("MangeEmployeeData", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> AddRoleAsync(EmployeeRole role)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "AddRole");
            parameters.Add("@RoleId", role.RoleId);
            parameters.Add("@EmpId", role.EmpId);
            return await _dbConnection.ExecuteAsync("MangeEmployeeData", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<EmployeeWithRoleDetails> GetRoleEmpWiseAsync(string empId)
        {
            EmployeeWithRoleDetails employeeWithRoleDetails = new EmployeeWithRoleDetails();
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "GetRoleEmpWise");
            parameters.Add("@AzureEntraId", empId);
            var multi = await _dbConnection.QueryMultipleAsync("MangeEmployeeData", parameters, commandType: CommandType.StoredProcedure);
            var employeeDetails = await multi.ReadAsync<EmployeeDetails>();
            var employeeRoles = await multi.ReadAsync<EmployeeRole>();
            employeeWithRoleDetails.EmployeeDetails = employeeDetails.FirstOrDefault();
            employeeWithRoleDetails.EmployeeRoles = employeeRoles.ToList();
            return employeeWithRoleDetails;
        }

        public async Task<int> DeletRoleEmpWiseAsync(EmployeeRole role)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "DeletRoleEmpWise");
            parameters.Add("@EmpId", role.EmpId);
            return await _dbConnection.ExecuteAsync("MangeEmployeeData", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<EmployeeRole>> GetEmpRole()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "GetEmpRole");
            return await _dbConnection.QueryAsync<EmployeeRole>("MangeEmployeeData", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<EmployeeRole>> GetAllRole()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Filter", "GetAllRole");
            return await _dbConnection.QueryAsync<EmployeeRole>("MangeEmployeeData", parameters, commandType: CommandType.StoredProcedure);
        }

    }
}
