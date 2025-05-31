using EHMSModel;

namespace EHMSWebApp.Interface
{
    public interface IEmployeeService
    {
        Task<int> CreateEmployeeAsync(EmployeeDetails employee);
        Task<IEnumerable<EmployeeDetails>> GetAllEmployeesAsync();
        Task<int> UpdateEmployeeAsync(EmployeeDetails employee);
        Task<int> DeleteEmployeeAsync(int empId);
        Task<int> AddRoleAsync(EmployeeRole role);
        Task<EmployeeWithRoleDetails> GetRoleEmpWiseAsync(string empId);
        Task<int> DeletRoleEmpWiseAsync(EmployeeRole role);
        Task<IEnumerable<EmployeeRole>> GetEmpRole();
        Task<IEnumerable<EmployeeRole>> GetAllRole();
    }
}
