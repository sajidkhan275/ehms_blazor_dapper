using EHMSModel;

namespace EHMSWebApp.Interface
{
    public interface  IDepartmentService
    {
        Task<IEnumerable<DepartmentDetails>> GetAllDepartments();
        Task<int> CreateDepartments(DepartmentDetails department);
        Task<int> UpdateDepartments(DepartmentDetails department);
        Task<int> DeleteDepartments(int departmentId);
    }
}
