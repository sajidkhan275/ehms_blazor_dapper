using EHMSWebApp.Interface;

namespace EHMSWebApp.DesignPattern
{
    public interface IDepartmentServiceFactory
    {
        IDepartmentService CreateDepartmentService();
    }
}
