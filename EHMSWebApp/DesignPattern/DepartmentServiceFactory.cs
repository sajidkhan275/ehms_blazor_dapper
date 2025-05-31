using EHMSWebApp.Interface;
using EHMSWebApp.Services;
using System.Data;
using System.Data.Common;

namespace EHMSWebApp.DesignPattern
{
    public class DepartmentServiceFactory : IDepartmentServiceFactory
    {
        private readonly IDbConnection _dbConnection;

        public DepartmentServiceFactory(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IDepartmentService CreateDepartmentService()
        {
            return new DepartmentService(_dbConnection);
        }
    }
}
