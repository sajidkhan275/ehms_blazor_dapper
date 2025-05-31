using AutoFixture;
using Dapper;
using EHMSModel;
using EHMSWebApp.Interface;
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
    public class DepartmentServiceTests
    {
        private readonly Mock<IDbConnection> _mockDbConnection;
        private readonly DepartmentService _mockDepartmentService;
        private readonly Fixture _fixture;

        public DepartmentServiceTests()
        {
            _mockDbConnection = new Mock<IDbConnection>();
            _mockDepartmentService = new DepartmentService(_mockDbConnection.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAllDepartments_ShouldReturnList()
        {
            // Arrange
            var employeeHealthInfo = _fixture.Build<DepartmentDetails>().With(c => c.DepartmentName, "Test").CreateMany(1);
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<DepartmentDetails>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(employeeHealthInfo);

            // Act
            var result = await _mockDepartmentService.GetAllDepartments();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains(result, e => e.DepartmentName == "Test");
        }


        [Fact]
        public async Task GetAllDepartmentss_ShouldReturnEmptyList()
        {
            // Arrange
            var departmentDetails = new List<DepartmentDetails>();
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<DepartmentDetails>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(departmentDetails);
            // Act
            var result = await _mockDepartmentService.GetAllDepartments();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(departmentDetails.Count, result.Count());
        }

        [Fact]
        public async Task CreateEmployeePhysicalFitness_Success()
        {
            // Arrange
            var departmentDetails = _fixture.Create<DepartmentDetails>();
            int rowsAffected = 1;
            _mockDbConnection.SetupDapperAsync(c => c.QuerySingleAsync<int>(It.IsAny<string>(), departmentDetails, null, null, null)).ReturnsAsync(rowsAffected);

            // Act
            int result = await _mockDepartmentService.CreateDepartments(departmentDetails);

            // Assert
            Assert.Equal(rowsAffected, result);
        }

        [Fact]
        public async Task UpdateDepartments_Success()
        {
            // Arrange
            var departmentDetails = _fixture.Create<DepartmentDetails>();
            int rowsAffected = 1;
            _mockDbConnection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), departmentDetails, null, null, null)).ReturnsAsync(rowsAffected);
            
            // Act
            int result = await _mockDepartmentService.UpdateDepartments(departmentDetails);

            // Assert
            Assert.Equal(rowsAffected, result);
        }

        [Fact]
        public async Task DeleteDepartments_Success()
        {
            // Arrange
            int id = 1;
            _mockDbConnection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), id , null, null, null)).ReturnsAsync(id);

            // Act
            int result = await _mockDepartmentService.DeleteDepartments(id);

            // Assert
            Assert.Equal(id, result);
        }
    }
}
