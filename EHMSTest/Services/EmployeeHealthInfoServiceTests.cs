using AutoFixture;
using Dapper;
using EHMSModel;
using EHMSWebApp.Services;
using Moq;
using Moq.Dapper;
using System.Data;
using Xunit;

namespace EHMSTest.Services
{
    public class EmployeeHealthInfoServiceTests
    {
        private readonly Mock<IDbConnection> _mockDbConnection;
        private readonly EmployeeHealthInfoService _employeeHealthInfoService;
        private readonly Fixture _fixture;

        public EmployeeHealthInfoServiceTests()
        {
            _mockDbConnection = new Mock<IDbConnection>();
            _employeeHealthInfoService = new EmployeeHealthInfoService(_mockDbConnection.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAllEmployeeHealthInfo_ShouldReturnList()
        {
            // Arrange
            var employeeHealthInfo = _fixture.Build<EmployeeHealthInfo>()
                .With(c => c.EmployeeName, "Test")
                .CreateMany(1);
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<EmployeeHealthInfo>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(employeeHealthInfo);

            // Act
            var result = await _employeeHealthInfoService.GetAllEmployeeHealthInfo();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains(result, e => e.EmployeeName == "Test");
        }

        [Fact]
        public async Task GetAllEmployeeHealthInfo_ShouldReturnEmptyList()
        {
            // Arrange
            var employeeHealthInfo = new List<EmployeeHealthInfo>();
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<EmployeeHealthInfo>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(employeeHealthInfo);
            // Act
            var result = await _employeeHealthInfoService.GetAllEmployeeHealthInfo();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employeeHealthInfo.Count, result.Count());
        }

        [Fact]
        public async Task CreateEmployeeHealthInfo_Success()
        {
            // Arrange
            var employeeHealthInfo = _fixture.Create<EmployeeHealthInfo>();
            int rowsAffected = 1;
            _mockDbConnection.SetupDapperAsync(c => c.QuerySingleAsync<int>(It.IsAny<string>(), employeeHealthInfo, null, null, null)).ReturnsAsync(rowsAffected);

            // Act
            int result = await _employeeHealthInfoService.CreateEmployeeHealthInfoAsync(employeeHealthInfo);

            // Assert
            Assert.Equal(rowsAffected, result);
        }

        [Fact]
        public async Task UpdateEmployeeHealthInfo_Success()
        {
            // Arrange
            var employeeHealthInfo = _fixture.Create<EmployeeHealthInfo>();
            int rowsAffected = 1;
            _mockDbConnection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), employeeHealthInfo, null, null, null)).ReturnsAsync(rowsAffected);

            // Act
            int result = await _employeeHealthInfoService.UpdateEmployeeHealthInfoAsync(employeeHealthInfo);

            // Assert
            Assert.Equal(rowsAffected, result);
        }

        [Fact]
        public async Task DeleteEmployeeHealthInfo_Success()
        {
            // Arrange
            int employeeHealthInfoId = 1;
            _mockDbConnection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), employeeHealthInfoId, null, null, null)).ReturnsAsync(employeeHealthInfoId);

            // Act
            int result = await _employeeHealthInfoService.DeleteEmployeeHealthInfoAsync(employeeHealthInfoId);

            // Assert
            Assert.Equal(employeeHealthInfoId, result);
        }

        [Fact]
        public async Task GetAllEmployeeHealthInfoByEmpId_ShouldReturnData()
        {
            // Arrange
            var employeeHealthInfo = _fixture.Build<EmployeeHealthInfo>().CreateMany(1);
            int empId = 1;
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<EmployeeHealthInfo>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(employeeHealthInfo);

            // Act
            var result = await _employeeHealthInfoService.GetAllEmployeeHealthInfoByEmpId(empId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(employeeHealthInfo.Count(), result.Count());
        }

    }
}
