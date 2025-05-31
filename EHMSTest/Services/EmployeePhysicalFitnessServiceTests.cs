using AutoFixture;
using Dapper;
using EHMSWebApp.Interface;
using EHMSModel;
using EHMSWebApp.Services;
using Moq;
using Moq.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EHMSTest.Services
{
    public  class EmployeePhysicalFitnessServiceTests
    {
        private readonly Mock<IDbConnection> _mockDbConnection;
        private readonly EmployeePhysicalFitnessService _employeePhysicalFitnessService;
        private readonly Fixture _fixture;

        public EmployeePhysicalFitnessServiceTests()
        {
            _mockDbConnection = new Mock<IDbConnection>();
            _employeePhysicalFitnessService = new EmployeePhysicalFitnessService(_mockDbConnection.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAllEmployeePhysicalFitness_ShouldReturnList()
        {
            // Arrange
            var employeeHealthInfo = _fixture.Build<EmployeePhysicalFitness>()
                .With(c => c.EmployeeName, "Test")
                .CreateMany(1);
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<EmployeePhysicalFitness>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(employeeHealthInfo);

            // Act
            var result = await _employeePhysicalFitnessService.GetAllEmployeePhysicalFitness();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains(result, e => e.EmployeeName == "Test");
        }

        [Fact]
        public async Task GetAllEmployeePhysicalFitness_ShouldReturnEmptyList()
        {
            // Arrange
            var employeeHealthInfo = new List<EmployeePhysicalFitness>();
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<EmployeePhysicalFitness>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(employeeHealthInfo);
            // Act
            var result = await _employeePhysicalFitnessService.GetAllEmployeePhysicalFitness();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employeeHealthInfo.Count, result.Count());
        }

        [Fact]
        public async Task CreateEmployeePhysicalFitness_Success()
        {
            // Arrange
            var employeeHealthInfo = _fixture.Create<EmployeePhysicalFitness>();
            int rowsAffected = 1;
            _mockDbConnection.SetupDapperAsync(c => c.QuerySingleAsync<int>(It.IsAny<string>(), employeeHealthInfo, null, null, null)).ReturnsAsync(rowsAffected);

            // Act
            int result = await _employeePhysicalFitnessService.CreateEmployeePhysicalFitnessAsync(employeeHealthInfo);

            // Assert
            Assert.Equal(rowsAffected, result);
        }

        [Fact]
        public async Task UpdateEmployeePhysicalFitness_Success()
        {
            // Arrange
            var employeeHealthInfo = _fixture.Create<EmployeePhysicalFitness>();
            int rowsAffected = 1;
            _mockDbConnection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), employeeHealthInfo, null, null, null)).ReturnsAsync(rowsAffected);

            // Act
            int result = await _employeePhysicalFitnessService.UpdateEmployeePhysicalFitnessAsync(employeeHealthInfo);

            // Assert
            Assert.Equal(rowsAffected, result);
        }

        [Fact]
        public async Task DeleteEmployeePhysicalFitness_Success()
        {
            // Arrange
            int employeeHealthInfoId = 1;
            _mockDbConnection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), employeeHealthInfoId, null, null, null)).ReturnsAsync(employeeHealthInfoId);

            // Act
            int result = await _employeePhysicalFitnessService.DeleteEmployeePhysicalFitnessAsync(employeeHealthInfoId);

            // Assert
            Assert.Equal(employeeHealthInfoId, result);
        }

        [Fact]
        public async Task GetAllEmployeePhysicalFitnessByEmpId_ShouldReturnData()
        {
            // Arrange
            var employeeHealthInfo = _fixture.Build<EmployeePhysicalFitness>().CreateMany(1);
            int empId = 1;
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<EmployeePhysicalFitness>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(employeeHealthInfo);

            // Act
            var result = await _employeePhysicalFitnessService.GetAllEmployeePhysicalFitnessByEmpId(empId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(employeeHealthInfo.Count(), result.Count());
        }

    }
}
