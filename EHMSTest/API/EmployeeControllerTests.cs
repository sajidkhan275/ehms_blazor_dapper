using AutoFixture;
using EHMSWebApp.Controllers;
using EHMSWebApp.Interface;
using EHMSModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EHMSTest.API
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly Mock<ILogger<EmployeeController>> _logger;
        private readonly EmployeeController _targert;
        private readonly Fixture _fixture;

        public EmployeeControllerTests()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _logger = new Mock<ILogger<EmployeeController>>();
            _targert = new EmployeeController(_mockEmployeeService.Object, _logger.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturnOkResultWithData()
        {
            //Arragnge
            var expectedEmployee = _fixture.Build<EmployeeDetails>()
                .With(c => c.EmployeeName, "Test")
                .CreateMany(2);
            _mockEmployeeService.Setup(x => x.GetAllEmployeesAsync()).ReturnsAsync(expectedEmployee);

            //Act
            var actual = await _targert.GetAllEmployees();

            //Assert
            Assert.NotNull(actual);
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var returnEmployees = Assert.IsType<List<EmployeeDetails>>(okResult.Value);
            Assert.Equal(expectedEmployee.Count(), returnEmployees.Count);
            _mockEmployeeService.Verify(x => x.GetAllEmployeesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllEmployees_Returns_WithEmptyList()
        {
            // Arrange
            var employees = new List<EmployeeDetails>();
            _mockEmployeeService.Setup(service => service.GetAllEmployeesAsync())
                        .ReturnsAsync(employees);

            // Act
            var result = await _targert.GetAllEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnEmployees = Assert.IsType<List<EmployeeDetails>>(okResult.Value);
            _mockEmployeeService.Verify(x => x.GetAllEmployeesAsync(), Times.Once);
            Assert.Empty(returnEmployees);
        }

        [Fact]
        public async Task GetAllEmployees_ThrowsException_Returns()
        {
            // Arrange
            _mockEmployeeService.Setup(service => service.GetAllEmployeesAsync())
                        .ThrowsAsync(new System.Exception("Something went wrong"));

            // Act
            var result = await _targert.GetAllEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnEmployees = Assert.IsType<List<EmployeeDetails>>(okResult.Value);
            Assert.Empty(returnEmployees);
            _mockEmployeeService.Verify(x => x.GetAllEmployeesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateEmployee_ReturnsOkResult()
        {
            // Arrange
            var employee = _fixture.Create<EmployeeDetails>();
            int empId = 1;
            _mockEmployeeService.Setup(service => service.CreateEmployeeAsync(employee)).ReturnsAsync(empId);

            // Act
            var result = await _targert.CreateEmployee(employee);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            int returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(empId, returnValue);
            _mockEmployeeService.Verify(service => service.CreateEmployeeAsync(employee), Times.Once);
        }

        [Fact]
        public async Task CreateEmployee_WithNullEmployee()
        {
            // Arrange
            EmployeeDetails? employee = null;

            // Act
            var result = await _targert.CreateEmployee(employee!);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            int returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(0, returnValue);
            _mockEmployeeService.Verify(service => service.CreateEmployeeAsync(employee!), Times.Once);
        }

        [Fact]
        public async Task CreateEmployee_ServiceThrowsException()
        {
            // Arrange
            var employee = _fixture.Create<EmployeeDetails>();
            _mockEmployeeService.Setup(service => service.CreateEmployeeAsync(employee)).ThrowsAsync(new Exception());

            // Act
            var result = await _targert.CreateEmployee(employee);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            int returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(0, returnValue);
            _mockEmployeeService.Verify(service => service.CreateEmployeeAsync(employee), Times.Once);
        }

        [Fact]
        public async Task UpdateEmployee_ReturnsOkResult()
        {
            // Arrange
            var employee = _fixture.Create<EmployeeDetails>();
            int empId = 1;
            _mockEmployeeService.Setup(service => service.UpdateEmployeeAsync(employee)).ReturnsAsync(empId);

            // Act
            var result = await _targert.UpdateEmployee(employee);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            int returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(empId, returnValue);
            _mockEmployeeService.Verify(service => service.UpdateEmployeeAsync(employee), Times.Once);
        }

        [Fact]
        public async Task UpdateEmployee_WithNullEmployee()
        {
            // Arrange
            EmployeeDetails? employee = null;

            // Act
            var result = await _targert.UpdateEmployee(employee!);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            int returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(0, returnValue);
        }

        [Fact]
        public async Task UpdateEmployee_ServiceThrowsException()
        {
            // Arrange
            var employee = _fixture.Create<EmployeeDetails>();
            _mockEmployeeService.Setup(service => service.UpdateEmployeeAsync(employee)).ThrowsAsync(new Exception());

            // Act
            var result = await _targert.UpdateEmployee(employee);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            int returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(0, returnValue);
            _mockEmployeeService.Verify(service => service.UpdateEmployeeAsync(employee), Times.Once);
        }

        [Fact]
        public async Task DeleteEmployee_ReturnsOkResult()
        {
            // Arrange
            int empId = 1;
            _mockEmployeeService.Setup(service => service.DeleteEmployeeAsync(empId)).ReturnsAsync(empId);

            // Act
            var result = await _targert.DeleteEmployee(empId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            int returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(empId, returnValue);
            _mockEmployeeService.Verify(service => service.DeleteEmployeeAsync(empId), Times.Once);
        }

        [Fact]
        public async Task DeleteEmployee_ServiceThrowsException()
        {
            // Arrange
            _mockEmployeeService.Setup(service => service.DeleteEmployeeAsync(0)).ThrowsAsync(new Exception());

            // Act
            var result = await _targert.DeleteEmployee(0);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            int returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(0, returnValue);
            _mockEmployeeService.Verify(service => service.DeleteEmployeeAsync(0), Times.Once);
        }

        [Fact]
        public async Task AddRole_WithNullEmployee()
        {
            // Arrange
            EmployeeRole role = null;

            // Act
            var result = await _targert.AddRoleAsync(role);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            int returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(0, returnValue);
        }

        [Fact]
        public async Task AddRole_ServiceThrowsException()
        {
            // Arrange
            var role = _fixture.Create<EmployeeRole>();
            _mockEmployeeService.Setup(service => service.AddRoleAsync(role)).ThrowsAsync(new Exception());

            // Act
            var result = await _targert.AddRoleAsync(role);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(0, returnValue);
            _mockEmployeeService.Verify(service => service.AddRoleAsync(role), Times.Once);
        }


        [Fact]
        public async Task GetRoleEmpWise_ShouldReturnOkResultWithData()
        {
            //Arragnge
            string entraId = "test";
            var expectedEmployee = _fixture.Create<EmployeeWithRoleDetails>();
            _mockEmployeeService.Setup(x => x.GetRoleEmpWiseAsync(entraId)).ReturnsAsync(expectedEmployee);

            //Act
            var actual = await _targert.GetRoleEmpWiseAsync(entraId);

            //Assert
            Assert.NotNull(actual);
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var returnEmployees = Assert.IsType<EmployeeWithRoleDetails>(okResult.Value);
            Assert.NotNull(returnEmployees);
            _mockEmployeeService.Verify(x => x.GetRoleEmpWiseAsync(entraId), Times.Once);
        }

        [Fact]
        public async Task GetRoleEmpWise_ReturnsOkResult_WithEmptyList()
        {
            // Arrange
            string entraId = "test";
            var employees = new EmployeeWithRoleDetails();
            _mockEmployeeService.Setup(service => service.GetRoleEmpWiseAsync(entraId)).ReturnsAsync(employees);

            // Act
            var result = await _targert.GetRoleEmpWiseAsync(entraId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnEmployees = Assert.IsType<EmployeeWithRoleDetails>(okResult.Value);
            _mockEmployeeService.Verify(x => x.GetRoleEmpWiseAsync(entraId), Times.Once);
            Assert.NotNull(returnEmployees);
        }

        [Fact]
        public async Task GetRoleEmpWise_ThrowsException_Returns()
        {
            // Arrange
            string entraId = "test";
            _mockEmployeeService.Setup(service => service.GetRoleEmpWiseAsync(entraId)).ThrowsAsync(new System.Exception("Something went wrong"));

            // Act
            var result = await _targert.GetRoleEmpWiseAsync(entraId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnEmployees = Assert.IsType<EmployeeWithRoleDetails>(okResult.Value);
            Assert.NotNull(returnEmployees);
            _mockEmployeeService.Verify(x => x.GetRoleEmpWiseAsync(entraId), Times.Once);
        }

        [Fact]
        public async Task DeletRoleEmpWise_ReturnsOkResult()
        {
            // Arrange
            var role = _fixture.Create<EmployeeRole>();
            _mockEmployeeService.Setup(service => service.DeletRoleEmpWiseAsync(role)).ReturnsAsync(1);

            // Act
            var result = await _targert.DeletRoleEmpWiseAsync(role);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            int returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(1, returnValue);
            _mockEmployeeService.Verify(service => service.DeletRoleEmpWiseAsync(role), Times.Once);
        }

        [Fact]
        public async Task DeletRoleEmpWise_ServiceThrowsException()
        {
            // Arrange
            var role = _fixture.Create<EmployeeRole>();
            _mockEmployeeService.Setup(service => service.DeletRoleEmpWiseAsync(role)).ThrowsAsync(new Exception());

            // Act
            var result = await _targert.DeletRoleEmpWiseAsync(role);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            int returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(0, returnValue);
            _mockEmployeeService.Verify(service => service.DeletRoleEmpWiseAsync(role), Times.Once);
        }

        [Fact]
        public async Task GetEmpRole_ShouldReturnOkResultWithData()
        {
            //Arragnge
            var expectedEmployee = _fixture.Build<EmployeeRole>()
                .With(c => c.EmployeeName, "Test")
                .CreateMany(2);
            _mockEmployeeService.Setup(x => x.GetEmpRole()).ReturnsAsync(expectedEmployee);

            //Act
            var actual = await _targert.GetEmpRole();

            //Assert
            Assert.NotNull(actual);
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var returnEmployees = Assert.IsType<List<EmployeeRole>>(okResult.Value);
            Assert.Equal(expectedEmployee.Count(), returnEmployees.Count);
            _mockEmployeeService.Verify(x => x.GetEmpRole(), Times.Once);
        }

        [Fact]
        public async Task GetEmpRole_ReturnsOkResult_WithEmptyList()
        {
            // Arrange
            var employees = new List<EmployeeRole>();
            _mockEmployeeService.Setup(service => service.GetEmpRole())
                        .ReturnsAsync(employees);

            // Act
            var result = await _targert.GetEmpRole();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnEmployees = Assert.IsType<List<EmployeeRole>>(okResult.Value);
            _mockEmployeeService.Verify(x => x.GetEmpRole(), Times.Once);
            Assert.Empty(returnEmployees);
        }

        [Fact]
        public async Task GetEmpRole_ThrowsException_Returns()
        {
            // Arrange
            _mockEmployeeService.Setup(service => service.GetEmpRole())
                        .ThrowsAsync(new System.Exception("Something went wrong"));

            // Act
            var result = await _targert.GetEmpRole();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            _mockEmployeeService.Verify(x => x.GetEmpRole(), Times.Once);
        }


        [Fact]
        public async Task GetAllRole_ShouldReturnOkResultWithData()
        {
            //Arragnge
            var expectedEmployee = _fixture.Build<EmployeeRole>()
                .With(c => c.EmployeeName, "Test")
                .CreateMany(2);
            _mockEmployeeService.Setup(x => x.GetAllRole()).ReturnsAsync(expectedEmployee);

            //Act
            var actual = await _targert.GetAllRole();

            //Assert
            Assert.NotNull(actual);
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var returnEmployees = Assert.IsType<List<EmployeeRole>>(okResult.Value);
            Assert.Equal(expectedEmployee.Count(), returnEmployees.Count);
            _mockEmployeeService.Verify(x => x.GetAllRole(), Times.Once);
        }

        [Fact]
        public async Task GetAllRole_ReturnsOkResult_WithEmptyList()
        {
            // Arrange
            var employees = new List<EmployeeRole>();
            _mockEmployeeService.Setup(service => service.GetAllRole())
                        .ReturnsAsync(employees);

            // Act
            var result = await _targert.GetAllRole();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnEmployees = Assert.IsType<List<EmployeeRole>>(okResult.Value);
            _mockEmployeeService.Verify(x => x.GetAllRole(), Times.Once);
            Assert.Empty(returnEmployees);
        }

        [Fact]
        public async Task GetAllRole_ThrowsException_Returns()
        {
            // Arrange
            _mockEmployeeService.Setup(service => service.GetAllRole())
                        .ThrowsAsync(new System.Exception("Something went wrong"));

            // Act
            var result = await _targert.GetAllRole();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            _mockEmployeeService.Verify(x => x.GetAllRole(), Times.Once);
        }
    }
}
