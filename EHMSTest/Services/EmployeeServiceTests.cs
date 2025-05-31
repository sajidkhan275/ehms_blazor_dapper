using Moq;
using Moq.Dapper;
using Xunit;
using System.Data;
using Dapper;
using EHMSWebApp.Services;
using EHMSModel;
using AutoFixture;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EHMSTest.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IDbConnection> _mockDbConnection;
        private readonly EmployeeService _employeeService;
        private readonly Fixture _fixture;

        public EmployeeServiceTests()
        {
            _mockDbConnection = new Mock<IDbConnection>();
            _employeeService = new EmployeeService(_mockDbConnection.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetEmployeesAsync_ShouldReturnEmployeeList()
        {
            // Arrange
            var expectedEmployee = _fixture.Build<EmployeeDetails>()
                .With(c => c.EmployeeName, "Test")
                .CreateMany(1);
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<EmployeeDetails>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expectedEmployee);

            // Act
            var result = await _employeeService.GetAllEmployeesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains(result, e => e.EmployeeName == "Test");
        }

        [Fact]
        public async Task GetEmployeesAsync_ShouldReturnEmployeeEmptyList()
        {
            // Arrange
            var employeeDetails = new List<EmployeeDetails>();
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<EmployeeDetails>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(employeeDetails);

            // Act
            var result = await _employeeService.GetAllEmployeesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employeeDetails.Count, result.Count());
        }

        [Fact]
        public async Task CreateEmployeeAsync_Employee()
        {
            // Arrange
            var employee = _fixture.Create<EmployeeDetails>();
            int rowsAffected = 1;

            _mockDbConnection.SetupDapperAsync(c => c.QuerySingleAsync<int>(It.IsAny<string>(), employee, null, null, null)).ReturnsAsync(1);

            // Act
            int result = await _employeeService.CreateEmployeeAsync(employee);

            // Assert
            Assert.Equal(rowsAffected, result);
        }


        [Fact]
        public async Task UpdateEmployeeAsync_Employee()
        {
            // Arrange
            var employee = _fixture.Create<EmployeeDetails>();
            int rowsAffected = 1;

            _mockDbConnection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), employee, null, null, null)).ReturnsAsync(rowsAffected);

            // Act
            int result = await _employeeService.UpdateEmployeeAsync(employee);

            // Assert
            Assert.Equal(rowsAffected, result);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_Employee()
        {
            // Arrange
            int empId = 1;

            _mockDbConnection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), empId, null, null, null)).ReturnsAsync(empId);

            // Act
            int result = await _employeeService.DeleteEmployeeAsync(empId);

            // Assert
            Assert.Equal(empId, result);
        }


        [Fact]
        public async Task AddRoleAsync_Success()
        {
            // Arrange
            var role = _fixture.Create<EmployeeRole>();
            int rowsAffected = 1;

            _mockDbConnection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), role, null, null, null)).ReturnsAsync(1);

            // Act
            int result = await _employeeService.AddRoleAsync(role);

            // Assert
            Assert.Equal(rowsAffected, result);
        }

        [Fact]
        public async Task DeletRoleEmpWiseAsync_Success()
        {
            // Arrange
            var role = _fixture.Create<EmployeeRole>();
            int rowsAffected = 1;

            _mockDbConnection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), role, null, null, null)).ReturnsAsync(rowsAffected);

            // Act
            int result = await _employeeService.DeletRoleEmpWiseAsync(role);

            // Assert
            Assert.Equal(rowsAffected, result);
        }

        [Fact]
        public async Task GetEmpRole_ShouldReturnEmployeeList()
        {
            // Arrange
            var expectedEmployee = _fixture.Build<EmployeeRole>()
                .With(c => c.EmployeeName, "Test")
                .CreateMany(1);
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<EmployeeRole>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expectedEmployee);

            // Act
            var result = await _employeeService.GetEmpRole();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains(result, e => e.EmployeeName == "Test");
        }

        [Fact]
        public async Task GetAllRole_ShouldReturnEmployeeList()
        {
            // Arrange
            var expectedEmployee = _fixture.Build<EmployeeRole>()
                .With(c => c.EmployeeName, "Test")
                .CreateMany(1);
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<EmployeeRole>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expectedEmployee);

            // Act
            var result = await _employeeService.GetAllRole();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains(result, e => e.EmployeeName == "Test");
        }

    }
}
