using AutoFixture;
using Dapper;
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
    public class RequestForHelpServiceTests
    {
        private readonly Mock<IDbConnection> _mockDbConnection;
        private readonly RequestForHelpService _mockRequestForHelpService;
        private readonly Fixture _fixture;

        public RequestForHelpServiceTests()
        {
            _mockDbConnection = new Mock<IDbConnection>();
            _mockRequestForHelpService = new RequestForHelpService(_mockDbConnection.Object);
            _fixture = new Fixture();
        }


        [Fact]
        public async Task GetRequestsByEmployeeAsync_ShouldReturnList()
        {
            // Arrange
            var employeeHealthInfo = _fixture.Build<RequestForHelp>().With(c => c.RequestDetails, "Test").CreateMany(1);
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<RequestForHelp>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(employeeHealthInfo);

            // Act
            var result = await _mockRequestForHelpService.GetRequestsByEmployeeAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains(result, e => e.RequestDetails == "Test");
        }

        [Fact]
        public async Task GetRequestsByEmployeeAsync_ShouldReturnEmptyList()
        {
            // Arrange
            var data = new List<RequestForHelp>();
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<RequestForHelp>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(data);

            // Act
            var result = await _mockRequestForHelpService.GetRequestsByEmployeeAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(data.Count, result.Count());
        }


        [Fact]
        public async Task GetRequestsByEmployeeIdAsync_ShouldReturnList()
        {
            // Arrange
            var employeeHealthInfo = _fixture.Build<RequestForHelp>().With(c => c.RequestDetails, "Test").CreateMany(1);
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<RequestForHelp>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(employeeHealthInfo);

            // Act
            var result = await _mockRequestForHelpService.GetRequestsByEmployeeIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains(result, e => e.RequestDetails == "Test");
        }

        [Fact]
        public async Task GetRequestsByEmployeeIdAsync_ShouldReturnEmptyList()
        {
            // Arrange
            var data = new List<RequestForHelp>();
            _mockDbConnection.SetupDapperAsync(c => c.QueryAsync<RequestForHelp>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(data);

            // Act
            var result = await _mockRequestForHelpService.GetRequestsByEmployeeIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(data.Count, result.Count());
        }

        [Fact]
        public async Task CreateRequestForHelpAsync_Success()
        {
            // Arrange
            var data = _fixture.Create<RequestForHelp>();
            int rowsAffected = 1;
            _mockDbConnection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), data, null, null, null)).ReturnsAsync(rowsAffected);

            // Act
            int result = await _mockRequestForHelpService.CreateRequestForHelpAsync(data);

            // Assert
            Assert.Equal(rowsAffected, result);
        }

        [Fact]
        public async Task UpdateHRRequestAsync_Success()
        {
            // Arrange
            var data = _fixture.Create<RequestForHelp>();
            int rowsAffected = 1;
            _mockDbConnection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), data, null, null, null)).ReturnsAsync(rowsAffected);

            // Act
            int result = await _mockRequestForHelpService.UpdateHRRequestAsync(data);

            // Assert
            Assert.Equal(rowsAffected, result);
        }

        [Fact]
        public async Task DeleteRequestForHelpAsync_Success()
        {
            // Arrange
            int id = 1;
            _mockDbConnection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), id, null, null, null)).ReturnsAsync(id);

            // Act
            int result = await _mockRequestForHelpService.DeleteRequestForHelpAsync(id);

            // Assert
            Assert.Equal(id, result);
        }


    }
}
